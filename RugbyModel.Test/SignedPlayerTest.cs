using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace RugbyModel.Test
{
    [TestClass]
    public class SignedPlayerTest
    {
        [TestMethod]
        public void ConstructorDoesntThrow()
        {
            _ = new SignedPlayer();
        }

        [TestMethod]
        public void SettingMemberValuesDoesntThrow()
        {
            _ = new SignedPlayer
            {
                PlayerId = 1234,
                PlayerName = "Bob Smith",
                TeamName = "Chiefs",
            };
        }

        [TestMethod]
        public void CloneWorksCorrectly()
        {
            var signedPlayer1 = new SignedPlayer
            {
                PlayerId = 1234,
                PlayerName = "Bob Smith",
                TeamName = "Chiefs",
            };

            var signedPlayer2 = (SignedPlayer)signedPlayer1.Clone();
            Assert.AreEqual(signedPlayer1.PlayerId, signedPlayer2.PlayerId);
            Assert.AreEqual(signedPlayer1.PlayerName, signedPlayer2.PlayerName);
            Assert.AreEqual(signedPlayer1.TeamName, signedPlayer2.TeamName);
        }

        [TestMethod]
        public void IsValidFileStringDetectsNullAndEmptyString()
        {
            Assert.IsFalse(SignedPlayer.IsValidFileString(null));
            Assert.IsFalse(SignedPlayer.IsValidFileString(""));
            Assert.IsFalse(SignedPlayer.IsValidFileString(string.Empty));
        }

        [TestMethod]
        public void IsValidFileStringDetectsNonsense()
        {
            Assert.IsFalse(SignedPlayer.IsValidFileString("aafajhsdfladjshfladjshfllasdf"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsInvalidNumberOfFields()
        {
            Assert.IsFalse(SignedPlayer.IsValidFileString("abc;def;ghi"));
            Assert.IsFalse(SignedPlayer.IsValidFileString("1; a; b"));
            Assert.IsFalse(SignedPlayer.IsValidFileString("144; Stormers; Perth"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsValidNumberOfFieldsButIncorrectTypes()
        {
            Assert.IsFalse(SignedPlayer.IsValidFileString("Chiefs; Stormers"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsListOfSemiColons()
        {
            Assert.IsFalse(SignedPlayer.IsValidFileString(";;;;;;;;;;;;;;;;;;;;;;;;;"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsValidData()
        {
            Assert.IsTrue(SignedPlayer.IsValidFileString("102; Cheetahs"));
            Assert.IsTrue(SignedPlayer.IsValidFileString("144; Stormers"));
        }

        [TestMethod]
        public void FromFileStringDetectsNullAndEmptyString()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var signedPlayer = SignedPlayer.FromFileString(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var signedPlayer = SignedPlayer.FromFileString("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var signedPlayer = SignedPlayer.FromFileString(string.Empty);
            });
        }

        [TestMethod]
        public void FromFileStringDetectsInvalidFieldCount()
        {
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var signedPlayer = SignedPlayer.FromFileString("abc;def;ghi");
            });
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var signedPlayer = SignedPlayer.FromFileString("1; a; b");
            });
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var signedPlayer = SignedPlayer.FromFileString("144; Stormers; Perth");
            });
        }

        [TestMethod]
        public void FromFileStringDetectsNonIntegerId()
        {
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var signedPlayer = SignedPlayer.FromFileString("abc; Kieran");
            });
        }

        [TestMethod]
        public void ToFileStringProducesCorrectOutput()
        {
            var SignedPlayer = new SignedPlayer
            {
                PlayerId = 1234,
                PlayerName = "Bob Smith",
                TeamName = "Chiefs",
            };
            const string expected = "1234; Chiefs";
            Assert.AreEqual(expected, SignedPlayer.ToFileString());
        }
    }
}
