using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace RugbyModel.Test
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void ConstructorDoesntThrow()
        {
            _ = new Player();
        }

        [TestMethod]
        public void SettingMemberValuesDoesntThrow()
        {
            _ = new Player
            {
                Id = 1234,
                FirstName = "Bob",
                LastName = "Smith",
                Height = 1234,
                Weight = 1234,
                DateOfBirth = new DateTime(),
                PlaceOfBirth = "Taupo"
            };
        }

        [TestMethod]
        public void CloneWorksCorrectly()
        {
            var player1 = new Player
            {
                Id = 9988,
                FirstName = "Sarah",
                LastName = "Jackson",
                Height = 520,
                Weight = 951,
                DateOfBirth = new DateTime(1980, 3, 3),
                PlaceOfBirth = "Levin"
            };

            var player2 = (Player)player1.Clone();
            Assert.AreEqual(player1.Id, player2.Id);
            Assert.AreEqual(player1.FirstName, player2.FirstName);
            Assert.AreEqual(player1.LastName, player2.LastName);
            Assert.AreEqual(player1.DisplayName, player2.DisplayName);
            Assert.AreEqual(player1.Height, player2.Height);
            Assert.AreEqual(player1.Weight, player2.Weight);
            Assert.AreEqual(player1.DateOfBirth, player2.DateOfBirth);
            Assert.AreEqual(player1.PlaceOfBirth, player2.PlaceOfBirth);
        }

        [TestMethod]
        public void IsValidFileStringDetectsNullAndEmptyString()
        {
            Assert.IsFalse(Player.IsValidFileString(null));
            Assert.IsFalse(Player.IsValidFileString(""));
            Assert.IsFalse(Player.IsValidFileString(string.Empty));
        }

        [TestMethod]
        public void IsValidFileStringDetectsNonsense()
        {
            Assert.IsFalse(Player.IsValidFileString("aafajhsdfladjshfladjshfllasdf"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsInvalidNumberOfFields()
        {
            Assert.IsFalse(Player.IsValidFileString("abc;def;ghi;jkl;mno;pqr;stu;vwx;yz"));
            Assert.IsFalse(Player.IsValidFileString("1; a; b; 1/1/1900; 2; 3; b; c"));
            Assert.IsFalse(Player.IsValidFileString("51; Kieran; Longbottom; 20/12/1985; 185; 118; Perth, Western Australia; Australia"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsValidNumberOfFieldsButIncorrectTypes()
        {
            Assert.IsFalse(Player.IsValidFileString("abc;def;ghi;jkl;mno;pqr"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsListOfSemiColons()
        {
            Assert.IsFalse(Player.IsValidFileString(";;;;;;;;;;;;;;;;;;;;;;;;;"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsValidData()
        {
            Assert.IsTrue(Player.IsValidFileString("1; a; b; 1/1/1900; 2; 3; b"));
            Assert.IsTrue(Player.IsValidFileString("51; Kieran; Longbottom; 20/12/1985; 185; 118; Perth, Western Australia, Australia"));
        }

        [TestMethod]
        public void FromFileStringDetectsNullAndEmptyString()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var player = Player.FromFileString(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var player = Player.FromFileString("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var player = Player.FromFileString(string.Empty);
            });
        }

        [TestMethod]
        public void FromFileStringDetectsInvalidFieldCount()
        {
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var player = Player.FromFileString("abc;def;ghi;jkl;mno;pqr;stu;vwx;yz");
            });
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var player = Player.FromFileString("1; a; b; 1/1/1900; 2; 3; b; c");
            });
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var player = Player.FromFileString("51; Kieran; Longbottom; 20/12/1985; 185; 118; Perth, Western Australia; Australia");
            });
        }

        [TestMethod]
        public void FromFileStringDetectsNonIntegerId()
        {
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var player = Player.FromFileString("abc; Kieran; Longbottom; 20/12/1985; 185; 118; Perth, Western Australia, Australia");
            });
        }

        [TestMethod]
        public void FromFileStringDetectsNonDateDob()
        {
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var player = Player.FromFileString("51; Kieran; Longbottom; abc; 185; 118; Perth, Western Australia, Australia");
            });
        }

        [TestMethod]
        public void FromFileStringDetectsNonIntegerHeight()
        {
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var player = Player.FromFileString("51; Kieran; Longbottom; 20/12/1985; abc; 118; Perth, Western Australia, Australia");
            });
        }

        [TestMethod]
        public void FromFileStringDetectsNonIntegerWeight()
        {
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var player = Player.FromFileString("51; Kieran; Longbottom; 20/12/1985; 185; abc; Perth, Western Australia, Australia");
            });
        }

        [TestMethod]
        public void FromFileStringClampsTooYoungAge()
        {
            var now = DateTime.Now;
            var player = Player.FromFileString($"51; Kieran; Longbottom; 20/12/{now.Year - 10}; 185; 118; Perth, Western Australia, Australia");
            var expected = now.AddYears(-Player.MinAge);
            Assert.AreEqual(expected.Year, actual: player.DateOfBirth.Year);
        }

        [TestMethod]
        public void FromFileStringClampsTooOldAge()
        {
            var now = DateTime.Now;
            var player = Player.FromFileString($"51; Kieran; Longbottom; 20/12/{now.Year - 100}; 185; 118; Perth, Western Australia, Australia");
            var expected = now.AddYears(-Player.MaxAge);
            Assert.AreEqual(expected.Year, actual: player.DateOfBirth.Year);
        }

        [TestMethod]
        public void FromFileStringClampsTooTallHeight()
        {
            var player = Player.FromFileString("51; Kieran; Longbottom; 20/12/1985; 1850; 118; Perth, Western Australia, Australia");
            Assert.AreEqual(Player.MaxHeight, player.Height);
        }

        [TestMethod]
        public void FromFileStringClampsTooShortHeight()
        {
            var player = Player.FromFileString("51; Kieran; Longbottom; 20/12/1985; 18; 118; Perth, Western Australia, Australia");
            Assert.AreEqual(Player.MinHeight, player.Height);
        }

        [TestMethod]
        public void FromFileStringClampsTooTallWeight()
        {
            var player = Player.FromFileString("51; Kieran; Longbottom; 20/12/1985; 185; 1180; Perth, Western Australia, Australia");
            Assert.AreEqual(Player.MaxWeight, player.Weight);
        }

        [TestMethod]
        public void FromFileStringClampsTooShortWeight()
        {
            var player = Player.FromFileString("51; Kieran; Longbottom; 20/12/1985; 185; 11; Perth, Western Australia, Australia");
            Assert.AreEqual(Player.MinWeight, player.Weight);
        }

        [TestMethod]
        public void ToFileStringProducesCorrectOutput()
        {
            var player = new Player
            {
                Id = 9988,
                FirstName = "Sarah",
                LastName = "Jackson",
                Height = 520,
                Weight = 951,
                DateOfBirth = new DateTime(1980, 3, 3),
                PlaceOfBirth = "Levin, New Zealand"
            };
            const string expected = "9988; Sarah; Jackson; 3/03/1980; 520; 951; Levin, New Zealand";
            Assert.AreEqual(expected, player.ToFileString());
        }
    }
}
