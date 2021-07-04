using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace RugbyModel.Test
{
    [TestClass]
    public class TeamTest
    {
        [TestMethod]
        public void ConstructorDoesntThrow()
        {
            _ = new Team();
        }

        [TestMethod]
        public void SettingMemberValuesDoesntThrow()
        {
            _ = new Team
            {
                Name = "Avengers",
                HomeGround = "Marvel",
                Coach = "Tony Stark",
                YearFounded = 1960,
                Region = "New York"
            };
        }

        [TestMethod]
        public void CloneWorksCorrectly()
        {
            var team1 = new Team
            {
                Name = "Avengers",
                HomeGround = "Marvel",
                Coach = "Tony Stark",
                YearFounded = 1960,
                Region = "New York"
            };

            var team2 = (Team)team1.Clone();
            Assert.AreEqual(team1.Name, team2.Name);
            Assert.AreEqual(team1.HomeGround, team2.HomeGround);
            Assert.AreEqual(team1.Coach, team2.Coach);
            Assert.AreEqual(team1.YearFounded, team2.YearFounded);
            Assert.AreEqual(team1.Region, team2.Region);
        }

        [TestMethod]
        public void IsValidFileStringDetectsNullAndEmptyString()
        {
            Assert.IsFalse(Team.IsValidFileString(null));
            Assert.IsFalse(Team.IsValidFileString(""));
            Assert.IsFalse(Team.IsValidFileString(string.Empty));
        }

        [TestMethod]
        public void IsValidFileStringDetectsNonsense()
        {
            Assert.IsFalse(Team.IsValidFileString("aafajhsdfladjshfladjshfllasdf"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsInvalidNumberOfFields()
        {
            Assert.IsFalse(Team.IsValidFileString("asdf; adsf; asdf; asdf; asdfadsf; adsf"));
            Assert.IsFalse(Team.IsValidFileString("a; b; c; d; Founded 1; e"));
            Assert.IsFalse(Team.IsValidFileString("Cats; AAMI Park; David Wessels; Founded 2010, Melbourne; Australia"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsValidNumberOfFieldsButIncorrectTypes()
        {
            Assert.IsFalse(Team.IsValidFileString("asdf; adsf; asdf; Founded abc, sfgh"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsListOfSemiColons()
        {
            Assert.IsFalse(Team.IsValidFileString(";;;;;;;;;;;;;;;;;;;;;;;;;"));
        }

        [TestMethod]
        public void IsValidFileStringDetectsValidData()
        {
            Assert.IsTrue(Team.IsValidFileString("a; b; c; Founded 1900, d"));
            Assert.IsTrue(Team.IsValidFileString("Cats; AAMI Park; David Wessels; Founded 2010, Melbourne, Australia"));
        }

        [TestMethod]
        public void FromFileStringDetectsNullAndEmptyString()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var team = Team.FromFileString(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var team = Team.FromFileString("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var team = Team.FromFileString(string.Empty);
            });
        }

        [TestMethod]
        public void FromFileStringDetectsInvalidFieldCount()
        {
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var team = Team.FromFileString("asdf; adsf; asdf; asdf; asdfadsf; adsf");
            });
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var team = Team.FromFileString("a; b; c; d; Founded 1; e");
            });
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var team = Team.FromFileString("Cats; AAMI Park; David Wessels; Founded 2010, Melbourne; Australia");
            });
        }

        [TestMethod]
        public void FromFileStringDetectsNonIntegerYearFounded()
        {
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                var team = Team.FromFileString("asdf; adsf; asdf; Founded abc, sfgh");
            });
        }

        [TestMethod]
        public void FromFileStringClampsMinYearFounded()
        {
            var team = Team.FromFileString("Cats; AAMI Park; David Wessels; Founded 100, Melbourne, Australia");
            Assert.AreEqual(Team.MinYearFounded, actual: team.YearFounded);
        }

        [TestMethod]
        public void FromFileStringClampsMaxYearFounded()
        {
            var team = Team.FromFileString("Cats; AAMI Park; David Wessels; Founded 10000, Melbourne, Australia");
            Assert.AreEqual(Team.MaxYearFounded, actual: team.YearFounded);
        }

        [TestMethod]
        public void ToFileStringProducesCorrectOutput()
        {
            var team = new Team
            {
                Name = "Avengers",
                HomeGround = "Marvel",
                Coach = "Tony Stark",
                YearFounded = 1960,
                Region = "New York"
            };
            const string expected = "Avengers; Marvel; Tony Stark; Founded 1960, New York";
            Assert.AreEqual(expected, team.ToFileString());
        }
    }
}
