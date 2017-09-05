using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Emerson_ShoppingCart.Database;
using Emerson_ShoppingCart.Models;
using Emerson_ShoppingCart.Models.RequestModels;
using Emerson_ShoppingCart.ViewReaders.Helpers;
using Emerson_ShoppingCart.ViewReaders.Interfaces;

namespace Emerson_ShoppingCart.ViewReaders
{
    /// <summary>
    /// ViewReader Class for the ShoppingCart Controller.
    /// Handles mediation and communication with SQLite data storage
    /// </summary>
    /// <seealso cref="Emerson_ShoppingCart.ViewReaders.Interfaces.IShoppingCartViewReader" />
    public class ShoppingCartViewReader : IShoppingCartViewReader
    {
        #region Private Members
        private static SQLiteConnection _dbconn;
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartViewReader"/> class.
        /// </summary>
        public ShoppingCartViewReader()
        {
            //@TODO: Add logging
            _dbconn = DatabaseDriver.DbConn;
        }
        #endregion

        #region Public Interface Members        
        /// <inheritdoc />
        public bool AddItems(GenericRequestModel grm) =>
            BulkExecute(grm.Items.Select(item =>
                    $"INSERT INTO ShoppingCart_LinkTable (UserId, ItemId) VALUES ({grm.CurrentUser.Id}, {item.Id})")
                .ToList());

        /// <inheritdoc />
        public bool RemoveItems(GenericRequestModel grm) =>
            BulkExecute(grm.Items.Select(item =>
                $"DELETE FROM ShoppingCart_LinkTable WHERE UserId = {grm.CurrentUser.Id} AND ItemId = {item.Id}")
                .ToList());

        /// <inheritdoc />
        public IEnumerable<Item> ListItems(GenericRequestModel grm)
        {
            var selectStatement =
                $@"SELECT Item.Id, Item.Name, Item.Description, Item.Price 
                    FROM ShoppingCart_LinkTable JOIN Item
                    on ShoppingCart_LinkTable.ItemId = Item.Id 
                    WHERE ShoppingCart_LinkTable.UserId = {grm.CurrentUser.Id}";

            //Open DB
            _dbconn.Open();

            //Start query
            try
            {
                using (var cmd = _dbconn.CreateCommand())
                {
                    cmd.CommandText = selectStatement;
                    var rdr = cmd.ExecuteReader();

                    //Instantiate return object and iterate results
                    var returnList = new List<Item>();
                    while (rdr.Read())
                    {
                        returnList.Add(new Item
                        {
                            Description = Convert.ToString(rdr["Description"]),
                            Id = Convert.ToInt32(rdr["Id"]),
                            Name = Convert.ToString(rdr["Name"]),
                            Price = Convert.ToDouble(rdr["Price"])
                        });
                    }

                    //Return results
                    return returnList;
                }
            }
            catch (Exception)
            {
                //@TODO: Add logging
                return new List<Item>();
            }
            finally
            {
                if (_dbconn.State == ConnectionState.Open) _dbconn.Close();
            }
        }

        /// <inhericdoc />
        public bool ClearCart(GenericRequestModel grm) =>
            Execute($"DELETE FROM ShoppingCart_LinkTable WHERE UserId = {grm.CurrentUser.Id}");

        /// <inheritdoc />
        public double GetTotalCost(GenericRequestModel grm)
        {
            var selectStatement = $@"SELECT SUM(Price) AS total
                                    FROM Item
                                    JOIN ShoppingCart_LinkTable
                                    ON Item.Id = ShoppingCart_LinkTable.ItemId
                                    WHERE ShoppingCart_LinkTable.UserId = {grm.CurrentUser.Id}";

            //Open DB
            _dbconn.Open();

            //Start query
            try
            {
                using (var cmd = _dbconn.CreateCommand())
                {
                    cmd.CommandText = selectStatement;
                    var rdr = cmd.ExecuteReader();

                    return rdr.Read() ? Convert.ToDouble(rdr["total"]) : 0d;
                }
            }
            catch (Exception)
            {
                //@TODO: Add logging
                return 0d;
            }
            finally
            {
                if (_dbconn.State == ConnectionState.Open) _dbconn.Close();
            }
        }

        /// <inheritdoc />
        public double GetSalesTax(GenericRequestModel grm)
        {
            //Retrieve price
            var price = GetTotalCost(grm);

            //Retrieve sales tax
            var taxRate = ExternCall.GetTaxRate(grm.CurrentUser.RegionCode);

            //Return value
            return price * taxRate;
        }

        /// <inheritdoc />
        public double ApplyDiscountCode(DiscountRequestModel drm)
        {
            //Create select statement
            var selectStatement = $@"SELECT SUM(IFNULL(Discount.OverridePrice, Item.Price)) as total
                                    FROM Item
                                    JOIN ShoppingCart_LinkTable
                                    ON Item.Id = ShoppingCart_LinkTable.ItemId
                                    LEFT OUTER JOIN Discount
                                    ON Item.Id = Discount.ItemId AND Discount.DiscountCode = '{drm.DiscountCode}'
                                    WHERE ShoppingCart_LinkTable.UserId = {drm.CurrentUser.Id}";

            //Open DB
            _dbconn.Open();

            //Start query
            try
            {
                using (var cmd = _dbconn.CreateCommand())
                {
                    cmd.CommandText = selectStatement;
                    var rdr = cmd.ExecuteReader();

                    return rdr.Read() ? Convert.ToDouble(rdr["total"]) : 0d;
                }
            }
            catch (Exception)
            {
                //@TODO: Add logging
                return 0d;
            }
            finally
            {
                if (_dbconn.State == ConnectionState.Open) _dbconn.Close();
            }
        }

        /// <inheritdoc />
        public double ApplyBuyOneGetOne(GenericRequestModel grm)
        {
            //Create select statement
            var selectStatement = $@"SELECT SUM(Price) AS total
                                    FROM Item
                                    JOIN ShoppingCart_LinkTable
                                    ON ShoppingCart_LinkTable.ItemId = Item.Id
                                    WHERE UserId = {grm.CurrentUser.Id}
                                    AND Item.Id NOT IN (
                                        SELECT bItem.Id FROM Item as fItem
                                        JOIN Item as bItem
                                        ON fItem.BuyOneGetOne_ItemId = bItem.Id)";

            //Open DB
            _dbconn.Open();

            //Start query
            try
            {
                using (var cmd = _dbconn.CreateCommand())
                {
                    cmd.CommandText = selectStatement;
                    var rdr = cmd.ExecuteReader();

                    return rdr.Read() ? Convert.ToDouble(rdr["total"]) : 0d;
                }
            }
            catch (Exception)
            {
                //@TODO: Add logging
                return 0d;
            }
            finally
            {
                if (_dbconn.State == ConnectionState.Open) _dbconn.Close();
            }
        }
        #endregion

        #region Private Interface Members
        private bool Execute(string statement)
        {
            //Open DB
            _dbconn.Open();

            //Instantiate Transaction
            using (var trans = _dbconn.BeginTransaction())
            {
                try
                {
                    //Instantiate SQLite Command
                    using (var cmd = _dbconn.CreateCommand())
                    {
                        cmd.CommandText = statement;
                        cmd.ExecuteNonQuery();

                        //Commit and close
                        trans.Commit();
                        _dbconn.Close();
                    }

                    //If no errors, return true
                    return true;
                }
                catch (Exception)
                {
                    //Return a failed execution
                    trans.Rollback();
                    return false;
                }
                finally
                {
                    //Dispose and close connections
                    trans?.Dispose();
                    if (_dbconn.State == ConnectionState.Open) _dbconn.Close();
                }
            }
        }

        private bool BulkExecute(IEnumerable<string> statements)
        {
            //Open DB
            _dbconn.Open();

            //Instantiate Transaction
            using (var trans = _dbconn.BeginTransaction())
            {
                try
                {
                    //Instantiate SQLite Command
                    using (var cmd = _dbconn.CreateCommand())
                    {
                        foreach (var statement in statements)
                        {
                            cmd.CommandText = statement;
                            cmd.ExecuteNonQuery();
                        }

                        //Commit and close
                        trans.Commit();
                        _dbconn.Close();
                    }

                    //If no errors, return true
                    return true;
                }
                catch (Exception)
                {
                    //Return a failed execution
                    trans.Rollback();
                    return false;
                }
                finally
                {
                    //Dispose and close connections
                    trans?.Dispose();
                    if (_dbconn.State == ConnectionState.Open) _dbconn.Close();
                }
            }
        }
        #endregion
    }
}