using System.Collections.Generic;
using Emerson_ShoppingCart.Models;

namespace Emerson_ShoppingCart.Database.Data
{
    /// <summary>
    /// Utility class used to seed the DB with initial data in the event of initial startup or a missing DB File.
    /// This is strictly for testing purposes
    /// </summary>
    public static class Seed
    {
        /// <summary>
        /// A List of Item Model Objects used to seed the DB with test data
        /// </summary>
        public static readonly List<Item> ItemSeed = new List<Item>
        {
            new Item
            {
                Description = @"A well-crafted sword named after the ruined land. 

                Astora, before its fall, was a land replete with royal blood, and this weapon is both a reminder and heirloom of that era.",
                Id = 1,
                Name = "Astora Straight Sword",
                Price = 2000
            },
            new Item
            {
                Description = @"This ultra greatsword, with its thick blade, is one of the heaviest of its kind.

                Highly destructive if intolerably heavy.

                There would appear to be some credence to the rumors that this sword tested the true limits of human strength.",
                Id = 2,
                Name = "Greatsword",
                Price = 7000,
                BuyOneGetOne_ItemId = 5
            },
            new Item
            {
                Description = @"Ultra greatsword of the venerable knights of Lothric. Imbued with the strength of lightning, the trademark of dragon hunting.

                Very few have demonstrated the extreme strength and dexterity required for this weapon, even in the long history of the Lothric Knights.",
                Id = 3,
                Name = "Lothric Knight Greatsword",
                Price = 12000
            },
            new Item
            {
                Description = @"Thrusting sword wielded by Corvian Knights, and a special paired weapon. When twin-handed, brandish four thin-edge blades in the left hand.

                In their infatuation with Sister Friede, the Corvian Knights swore to protect the painting from fire, and to this end, took to the execution of their own brethren.",
                Id = 4,
                Name = "Crow Quills",
                Price = 0
            },
            new Item
            {
                Description = @"Armor of the Drang Knights, proclaimed descendants from the land known for the legend of the Linking of the Fire.

                Fine protection that is both light and strong, having been reinforced with rare geisteel.

                The Drang Knights were once feared sellswords, until treason meant descending into the abyss, and they were seperated forever.",
                Id = 5,
                Name = "Drang Armor",
                Price = 7550
            }
        };

        /// <summary>
        /// A list of User Model Objects used to seed the DB with test data
        /// </summary>
        public static readonly List<User> UserSeed = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Anri of Astora",
                RegionCode = "NH"
            },
            new User
            {
                Id = 2,
                Name = "Ashen One",
                RegionCode = "MA"
            },
            new User
            {
                Id = 3,
                Name = "Guts",
                RegionCode = "NY"
            }
        };

        /// <summary>
        /// A list of Shopping Cart Link Table Objects used to seed the DB with test data
        /// </summary>
        public static readonly List<ShoppingCart_LinkTable> LinkTableSeed = new List<ShoppingCart_LinkTable>
        {
            new ShoppingCart_LinkTable
            {
                ItemId = 1,
                UserId = 1
            },
            new ShoppingCart_LinkTable
            {
                ItemId = 1,
                UserId = 2
            },
            new ShoppingCart_LinkTable
            {
                ItemId = 2,
                UserId = 2
            },
            new ShoppingCart_LinkTable
            {
                ItemId = 3,
                UserId = 3
            },
            new ShoppingCart_LinkTable
            {
                ItemId = 5,
                UserId = 3
            }
        };

        public static readonly List<Discount> DiscountTableSeed = new List<Discount>
        {
            new Discount
            {
                DiscountCode = "LABOR45",
                Id = 1,
                ItemId = 1,
                OverridePrice = 1000
            }
        };
    }
}