using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace RugbyController.Test
{
    [TestClass]
    public class RugbyControllerTest
    {
        [TestMethod]
        public void NewRugbyUnionWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");

            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.AreEqual("Six Nations Championship", view._newRugbyUnionName);

            Assert.IsTrue(controller.IsOpen);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual("Six Nations Championship", controller.RugbyUnionName);
            Assert.IsFalse(controller.HasBeenSaved);
            Assert.IsTrue(string.IsNullOrEmpty(controller.PathName));
            Assert.IsNull(controller.Teams);
            Assert.IsNull(controller.Players);
            Assert.IsNull(controller.SignedPlayers);
        }

        [TestMethod]
        public void NewRugbyUnionDetectsNullOrEmptyName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.NewRugbyUnion(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.NewRugbyUnion("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.NewRugbyUnion(string.Empty);
            });
        }

        [TestMethod]
        public void RenameRugbyUnionWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            controller.RenameRugbyUnion("Five Nations Championship");
            Assert.IsTrue(view._onRugbyUnionRenamedCalled);

            Assert.AreEqual("Six Nations Championship", view._oldRugbyUnionName);
            Assert.AreEqual("Five Nations Championship", view._newRugbyUnionName);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual("Five Nations Championship", controller.RugbyUnionName);
        }

        [TestMethod]
        public void RenameRugbyUnionDetectsNullOrEmptyName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.RenameRugbyUnion(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.RenameRugbyUnion("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.RenameRugbyUnion(string.Empty);
            });
        }

        [TestMethod]
        public void OpenRugbyUnionWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");

            Assert.IsTrue(view._onRugbyUnionOpenedCalled);
            Assert.AreEqual("Boss Baby", view._newRugbyUnionName);
            Assert.AreEqual("filename.txt", view._rugbyUnionPathName);
            Assert.IsNotNull(view._teams);
            Assert.AreEqual(1, view._teams.Count);
            Assert.IsNotNull(view._players);
            Assert.AreEqual(3, view._players.Count);
            Assert.IsNotNull(view._signedPlayers);
            Assert.AreEqual(2, view._signedPlayers.Count);

            Assert.IsTrue(controller.IsOpen);
            Assert.IsFalse(controller.IsModified);
            Assert.AreEqual("Boss Baby", controller.RugbyUnionName);
            Assert.IsTrue(controller.HasBeenSaved);
            Assert.IsNotNull(controller.PathName);
            Assert.AreEqual("filename.txt", controller.PathName);
            Assert.IsNotNull(controller.Teams);
            Assert.AreEqual(1, controller.Teams.Count);
            Assert.IsNotNull(controller.Players);
            Assert.AreEqual(3, controller.Players.Count);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);
        }

        [TestMethod]
        public void OpenRugbyUnionDetectsNullOrEmptyFileName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.OpenRugbyUnion(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.OpenRugbyUnion("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.OpenRugbyUnion(string.Empty);
            });
        }

        [TestMethod]
        public void OpenRugbyUnionCanFileContainingZeroItems()
        {

            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "0",
                    "Players",
                    "0",
                    "Signed Players",
                    "0"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");

            Assert.IsTrue(view._onRugbyUnionOpenedCalled);
            Assert.AreEqual("Boss Baby", view._newRugbyUnionName);
            Assert.AreEqual("filename.txt", view._rugbyUnionPathName);
            Assert.IsNotNull(view._teams);
            Assert.AreEqual(0, view._teams.Count);
            Assert.IsNotNull(view._players);
            Assert.AreEqual(0, view._players.Count);
            Assert.IsNotNull(view._signedPlayers);
            Assert.AreEqual(0, view._signedPlayers.Count);

            Assert.IsTrue(controller.IsOpen);
            Assert.IsFalse(controller.IsModified);
            Assert.AreEqual("Boss Baby", controller.RugbyUnionName);
            Assert.IsTrue(controller.HasBeenSaved);
            Assert.IsNotNull(controller.PathName);
            Assert.AreEqual("filename.txt", controller.PathName);
            Assert.IsNotNull(controller.Teams);
            Assert.AreEqual(0, controller.Teams.Count);
            Assert.IsNotNull(controller.Players);
            Assert.AreEqual(0, controller.Players.Count);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(0, controller.SignedPlayers.Count);
        }

        [TestMethod]
        public void OpenRugbyUnionDetectsNonExistanceZeroLengthFile()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.ThrowsException<Exception>(() =>
            {
                controller.OpenRugbyUnion("filename.txt");
            });
        }

        [TestMethod]
        public void CloseRugbyUnionWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.IsTrue(view._onRugbyUnionOpenedCalled);

            controller.CloseRugbyUnion();
            Assert.IsTrue(view._onRugbyUnionClosedCalled);
            Assert.IsFalse(controller.IsOpen);
            Assert.IsFalse(controller.IsModified);
            Assert.IsNull(controller.RugbyUnionName);
            Assert.IsFalse(controller.HasBeenSaved);
            Assert.IsNull(controller.PathName);
            Assert.IsNull(controller.Teams);
            Assert.IsNull(controller.Players);
            Assert.IsNull(controller.SignedPlayers);
        }

        [TestMethod]
        public void SaveAsRugbyUnionWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var player = new RugbyModel.Player()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Height = 180,
                Weight = 80,
                DateOfBirth = new DateTime(1970, 10, 20),
                PlaceOfBirth = "Nelson"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            player = view._newPlayerAdded;

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            team = view._newTeamAdded;

            controller.SignPlayerToTeam(player.Id, team.Name);
            Assert.IsTrue(view._onPlayerSignedToTeamCalled);

            controller.SaveAsRugbyUnion("pathname.txt");
            Assert.IsTrue(view._onRugbyUnionSavedCalled);

            Assert.IsFalse(controller.IsModified);
            Assert.IsTrue(controller.HasBeenSaved);
            Assert.AreEqual("pathname.txt", controller.PathName);
            Assert.AreEqual(12, fileIo._writtenFileLines.Count);
            Assert.AreEqual("Rugby Union File Format", fileIo._writtenFileLines[0]);
            Assert.AreEqual("1.0", fileIo._writtenFileLines[1]);
            Assert.AreEqual("Six Nations Championship", fileIo._writtenFileLines[2]);
            Assert.AreEqual("Teams", fileIo._writtenFileLines[3]);
            Assert.AreEqual("1", fileIo._writtenFileLines[4]);
            Assert.AreEqual("Tigers; Eden Park; Melanie Jones; Founded 1950, Taranaki", fileIo._writtenFileLines[5]);
            Assert.AreEqual("Players", fileIo._writtenFileLines[6]);
            Assert.AreEqual("1", fileIo._writtenFileLines[7]);
            Assert.AreEqual($"{player.Id}; Bob; Smith; 20/10/1970; 180; 80; Nelson", fileIo._writtenFileLines[8]);
            Assert.AreEqual("Signed Players", fileIo._writtenFileLines[9]);
            Assert.AreEqual("1", fileIo._writtenFileLines[10]);
            Assert.AreEqual($"{player.Id}; Tigers", fileIo._writtenFileLines[11]);
        }

        [TestMethod]
        public void SaveAsRugbyUnionDetectsNullOrEmptyName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.SaveAsRugbyUnion(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.SaveAsRugbyUnion("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.SaveAsRugbyUnion(string.Empty);
            });
        }

        [TestMethod]
        public void SaveRugbyUnionDoesntSaveIfSaveAsHasNotBeenUsedYet()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.SaveRugbyUnion();
            });
        }

        [TestMethod]
        public void SaveRugbyUnionWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            controller.SaveAsRugbyUnion("pathname.txt");
            Assert.IsTrue(view._onRugbyUnionSavedCalled);
            Assert.IsFalse(controller.IsModified);
            Assert.IsTrue(controller.HasBeenSaved);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            controller.SaveRugbyUnion();
            Assert.IsTrue(view._onRugbyUnionSavedCalled);
            Assert.IsFalse(controller.IsModified);
            Assert.IsTrue(controller.HasBeenSaved);
            Assert.AreEqual("pathname.txt", controller.PathName);
            Assert.AreEqual(10, fileIo._writtenFileLines.Count);
            Assert.AreEqual("Rugby Union File Format", fileIo._writtenFileLines[0]);
            Assert.AreEqual("1.0", fileIo._writtenFileLines[1]);
            Assert.AreEqual("Six Nations Championship", fileIo._writtenFileLines[2]);
            Assert.AreEqual("Teams", fileIo._writtenFileLines[3]);
            Assert.AreEqual("1", fileIo._writtenFileLines[4]);
            Assert.AreEqual("Tigers; Eden Park; Melanie Jones; Founded 1950, Taranaki", fileIo._writtenFileLines[5]);
            Assert.AreEqual("Players", fileIo._writtenFileLines[6]);
            Assert.AreEqual("0", fileIo._writtenFileLines[7]);
            Assert.AreEqual("Signed Players", fileIo._writtenFileLines[8]);
            Assert.AreEqual("0", fileIo._writtenFileLines[9]);
        }

        [TestMethod]
        public void AddTeamDetectsNull()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.AddTeam(null);
            });
        }

        [TestMethod]
        public void AddTeamDetectsNullOrEmptyName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.AddTeam(new RugbyModel.Team());
            });
        }

        [TestMethod]
        public void AddTeamWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
        }

        [TestMethod]
        public void AddTeamDetectsDuplicates()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.ThrowsException<ArgumentException>(() =>
            {
                controller.AddTeam(team);
            });
        }

        [TestMethod]
        public void EditTeamDetectsNull()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.EditTeam(null);
            });
        }

        [TestMethod]
        public void EditTeamDetectsNullOrEmptyName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.EditTeam(new RugbyModel.Team());
            });
        }

        [TestMethod]
        public void EditTeamWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            var foundTeam = controller.FindTeam("Tigers");
            Assert.IsNotNull(foundTeam);
            Assert.AreEqual(foundTeam.Region, "Taranaki");
            Assert.AreEqual(foundTeam.YearFounded, 1950);
            Assert.AreEqual(foundTeam.Coach, "Melanie Jones");
            Assert.AreEqual(foundTeam.HomeGround, "Eden Park");

            team.Region = "Canterbury";
            team.YearFounded = 1961;
            team.Coach = "Jessica Pilgrim";
            team.HomeGround = "Hagley Park";
            controller.EditTeam(team);
            Assert.IsTrue(view._onTeamEditedCalled);
            foundTeam = controller.FindTeam("Tigers");
            Assert.IsNotNull(foundTeam);
            Assert.AreEqual(foundTeam.Region, "Canterbury");
            Assert.AreEqual(foundTeam.YearFounded, 1961);
            Assert.AreEqual(foundTeam.Coach, "Jessica Pilgrim");
            Assert.AreEqual(foundTeam.HomeGround, "Hagley Park");
        }

        [TestMethod]
        public void EditTeamDetectsUnknownName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            var foundTeam = controller.FindTeam("Tigers");
            Assert.IsNotNull(foundTeam);
            Assert.AreEqual(foundTeam.Region, "Taranaki");
            Assert.AreEqual(foundTeam.YearFounded, 1950);
            Assert.AreEqual(foundTeam.Coach, "Melanie Jones");
            Assert.AreEqual(foundTeam.HomeGround, "Eden Park");

            var otherTeam = new RugbyModel.Team()
            {
                Name = "Bobcats",
                Region = "Canterbury",
                YearFounded = 1961,
                Coach = "Jessica Pilgrim",
                HomeGround = "Hagley Park"
            };
            Assert.ThrowsException<ArgumentException>(() =>
            {
                controller.EditTeam(otherTeam);
            });
        }

        [TestMethod]
        public void RenameTeamDetectsNullOrEmpty1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.RenameTeam(null, "Bob");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.RenameTeam("", "Bob");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.RenameTeam(string.Empty, "Bob");
            });
        }

        [TestMethod]
        public void RenameTeamDetectsNullOrEmpty2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.RenameTeam("Tigers", null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.RenameTeam("Tigers", "");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.RenameTeam("Tigers", string.Empty);
            });
        }

        [TestMethod]
        public void RenameTeamDetectsUnknownName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.ThrowsException<ArgumentException>(() =>
            {
                controller.RenameTeam("Pumas", "Bobcats");
            });
        }

        [TestMethod]
        public void RenameTeamDetectsDuplicateName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            team = new RugbyModel.Team()
            {
                Name = "Jaguars",
                Region = "Invercargill",
                YearFounded = 1955,
                Coach = "Carlton Jones",
                HomeGround = "Hook Peninsula"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                controller.RenameTeam("Jaguars", "Tigers");
            });
        }

        [TestMethod]
        public void RenameTeamWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            team = new RugbyModel.Team()
            {
                Name = "Jaguars",
                Region = "Invercargill",
                YearFounded = 1955,
                Coach = "Carlton Jones",
                HomeGround = "Hook Peninsula"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            controller.RenameTeam("Tigers", "Panthers");
            Assert.IsTrue(view._onTeamRenamedCalled);

            var foundTeam = controller.FindTeam("Panthers");
            Assert.IsNotNull(foundTeam);
            Assert.AreEqual("Taranaki", foundTeam.Region);
            Assert.AreEqual(1950, foundTeam.YearFounded);
            Assert.AreEqual("Melanie Jones", foundTeam.Coach);
            Assert.AreEqual("Eden Park", foundTeam.HomeGround);
            Assert.IsNull(controller.FindTeam("Tigers"));
        }

        [TestMethod]
        public void DeleteTeamDetectsNullOrEmpty()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.DeleteTeam(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.DeleteTeam("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.DeleteTeam(string.Empty);
            });
        }

        [TestMethod]
        public void DeleteTeamDetectsUnknownName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.ThrowsException<ArgumentException>(() =>
            {
                controller.DeleteTeam("Pumas");
            });
        }

        [TestMethod]
        public void DeleteTeamWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            controller.DeleteTeam("Tigers");
            Assert.IsNull(controller.FindTeam("Tigers"));
        }

        [TestMethod]
        public void DeleteTeamRemovesSignedPlayersToo()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Josh",
                LastName = "Smith",
                PlaceOfBirth = "Levin",
                DateOfBirth = new DateTime(1990, 1, 3),
                Height = 190,
                Weight = 101
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Sarah",
                LastName = "Parkinson",
                PlaceOfBirth = "Levin",
                DateOfBirth = new DateTime(1990, 1, 3),
                Height = 190,
                Weight = 101
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Francis",
                LastName = "Yu",
                PlaceOfBirth = "Levin",
                DateOfBirth = new DateTime(1990, 1, 3),
                Height = 190,
                Weight = 101
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            controller.SignPlayerToTeam(player1.Id, team.Name);
            Assert.IsTrue(view._onPlayerAddedCalled);
            controller.SignPlayerToTeam(player2.Id, team.Name);
            Assert.IsTrue(view._onPlayerAddedCalled);
            controller.SignPlayerToTeam(player3.Id, team.Name);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.DeleteTeam("Tigers");
            Assert.IsNull(controller.FindTeam("Tigers"));
            Assert.IsNull(controller.SignedPlayers);
        }

        [TestMethod]
        public void DeleteTeamsDetectsNull1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Teams.Count);

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.DeleteTeams(null);
            });
        }

        [TestMethod]
        public void DeleteTeamsDetectsNull2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);
            controller.DeleteTeams(new List<string>() { "Sharks" });
        }

        [TestMethod]
        public void DeleteTeamsWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Teams.Count);

            var team2 = new RugbyModel.Team()
            {
                Name = "Bulls",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Teams.Count);

            controller.DeleteTeams(new List<string>());
            Assert.IsTrue(view._onTeamsDeletedCalled);
            Assert.AreEqual(0, view._teamNamesDeleted.Count);
        }

        [TestMethod]
        public void DeleteTeamsWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Teams.Count);

            var team2 = new RugbyModel.Team()
            {
                Name = "Bulls",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Teams.Count);

            var team3 = new RugbyModel.Team()
            {
                Name = "Sharks",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team3);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Teams.Count);

            var team4 = new RugbyModel.Team()
            {
                Name = "Ducks",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team4);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Teams.Count);

            controller.DeleteTeams(new List<string>() { "Sharks", "Bulls" });
            Assert.IsTrue(view._onTeamsDeletedCalled);
            Assert.AreEqual(2, view._teamNamesDeleted.Count);
            Assert.AreEqual("Sharks", view._teamNamesDeleted[0]);
            Assert.AreEqual("Bulls", view._teamNamesDeleted[1]);
            Assert.AreEqual(2, controller.Teams.Count);
            Assert.AreEqual("Tigers", controller.Teams[0].Name);
            Assert.AreEqual("Ducks", controller.Teams[1].Name);
        }

        [TestMethod]
        public void DeleteAllTeamsWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Teams.Count);

            var team2 = new RugbyModel.Team()
            {
                Name = "Bulls",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Teams.Count);

            var team3 = new RugbyModel.Team()
            {
                Name = "Sharks",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team3);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Teams.Count);

            var team4 = new RugbyModel.Team()
            {
                Name = "Ducks",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team4);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Teams.Count);

            controller.DeleteAllTeams();
            Assert.IsTrue(view._onTeamsDeletedCalled);
            Assert.AreEqual(4, view._teamNamesDeleted.Count);
            Assert.IsNull(controller.Teams);
        }

        [TestMethod]
        public void DeleteAllTeamsWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Teams);
            controller.DeleteAllTeams();
            Assert.IsNull(controller.Teams);
        }

        [TestMethod]
        public void IsOpenWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsFalse(controller.IsOpen);
        }

        [TestMethod]
        public void IsOpenWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Pumas");
            Assert.IsTrue(controller.IsOpen);
        }

        [TestMethod]
        public void IsOpenWorksCorrectly3()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsFalse(controller.IsOpen);
            controller.NewRugbyUnion("Pumas");
            Assert.IsTrue(controller.IsOpen);
            controller.CloseRugbyUnion();
            Assert.IsFalse(controller.IsOpen);
        }

        [TestMethod]
        public void IsOpenWorksCorrectly4()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.IsTrue(controller.IsOpen);
            controller.CloseRugbyUnion();
            Assert.IsFalse(controller.IsOpen);
        }

        [TestMethod]
        public void IsModifiedWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsFalse(controller.IsModified);
        }

        [TestMethod]
        public void IsModifiedWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Pumas");
            Assert.IsTrue(controller.IsModified);
        }

        [TestMethod]
        public void IsModifiedWorksCorrectly3()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsFalse(controller.IsModified);
            controller.NewRugbyUnion("Pumas");
            Assert.IsTrue(controller.IsModified);
            controller.CloseRugbyUnion();
            Assert.IsFalse(controller.IsModified);
        }

        [TestMethod]
        public void IsModifiedWorksCorrectly4()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.IsFalse(controller.IsModified);
            controller.CloseRugbyUnion();
            Assert.IsFalse(controller.IsModified);
        }

        [TestMethod]
        public void RugbyUnionNameIsEmptyAfterCreation()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsNull(controller.RugbyUnionName);
        }

        [TestMethod]
        public void RugbyUnionNameIsCorrectAfterFileOpen()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.AreEqual("Boss Baby", controller.RugbyUnionName);
        }

        [TestMethod]
        public void RugbyUnionNameIsCorrectAfterFileNew()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Boss Baby");
            Assert.AreEqual("Boss Baby", controller.RugbyUnionName);
        }

        [TestMethod]
        public void RugbyUnionNameIsEmptyAfterFileClose()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Boss Baby");
            Assert.AreEqual("Boss Baby", controller.RugbyUnionName);
            controller.CloseRugbyUnion();
            Assert.IsNull(controller.RugbyUnionName);
        }

        [TestMethod]
        public void HasBeenSavedWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsFalse(controller.HasBeenSaved);
        }

        [TestMethod]
        public void HasBeenSavedWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Pumas");
            Assert.IsFalse(controller.HasBeenSaved);
        }

        [TestMethod]
        public void HasBeenSavedWorksCorrectly3()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.IsTrue(controller.HasBeenSaved);
            controller.CloseRugbyUnion();
            Assert.IsFalse(controller.HasBeenSaved);
        }

        [TestMethod]
        public void HasBeenSavedWorksCorrectly4()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations");
            Assert.IsFalse(controller.HasBeenSaved);
            controller.SaveAsRugbyUnion("pathname.txt");
            Assert.IsTrue(view._onRugbyUnionSavedCalled);
            Assert.IsFalse(controller.IsModified);
            Assert.IsTrue(controller.HasBeenSaved);
        }

        [TestMethod]
        public void PathNameIsEmptyAfterCreation()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsNull(controller.PathName);
        }

        [TestMethod]
        public void PathNameIsCorrectAfterFileOpen()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.AreEqual("filename.txt", controller.PathName);
        }

        [TestMethod]
        public void PathNameIsCorrectAfterFileNew()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsNull(controller.PathName);
            controller.NewRugbyUnion("Boss Baby");
            Assert.IsNull(controller.PathName);
        }

        [TestMethod]
        public void PathNameIsEmptyAfterFileClose()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.AreEqual("filename.txt", controller.PathName);
            controller.CloseRugbyUnion();
            Assert.IsNull(controller.PathName);
        }

        [TestMethod]
        public void TeamsIsEmptyAfterCreation()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsNull(controller.Teams);
        }

        [TestMethod]
        public void TeamsIsEmptyAfterFileClose()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.IsNotNull(controller.Teams);
            Assert.AreEqual(1, controller.Teams.Count);
            controller.CloseRugbyUnion();
            Assert.IsNull(controller.Teams);
        }

        [TestMethod]
        public void TeamsIsEmptyAfterFileNew()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations");
            Assert.IsNull(controller.Teams);
        }

        [TestMethod]
        public void TeamsAreCorrectAfterFileOpen()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "8",
                    "Reds; Suncorp Stadium; Brad Thorn; Founded 1882, Brisbane, Queensland, Australia",
                    "Waratahs; Sydney Football Stadium; Rob Penney; Founded 1882, Sydney, New South Wales, Australia",
                    "Bulls; Loftus Versfeld Stadium; Jake White; Founded 1997, Pretoria, South Africa",
                    "Cheetahs; Free State Stadium; Hawies Fourie; Founded 2005, Bloemfontein, Free State, South Africa",
                    "Lions; Emirates Airline Park; Ivan van Rooyen; Founded 1996, Johannesburg, South Africa",
                    "Sharks; Jonsson Kings Park Stadium; Sean Everitt; Founded 1995, Durban, South Africa",
                    "Southern; Nelson Mandela Bay Stadium; Robbi Kempson; Founded 2009, Port Elizabeth, South Africa",
                    "Stormers; Cape Town Stadium; John Dobson; Founded 1997, Cape Town, South Africa",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Waratahs",
                    "2; Sharks"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.IsNotNull(controller.Teams);
            Assert.AreEqual(8, controller.Teams.Count);
        }

        [TestMethod]
        public void PlayersIsEmptyAfterCreation()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsNull(controller.Players);
        }

        [TestMethod]
        public void PlayersIsEmptyAfterFileClose()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.IsNotNull(controller.Teams);
            Assert.AreEqual(3, controller.Players.Count);
            controller.CloseRugbyUnion();
            Assert.IsNull(controller.Players);
        }

        [TestMethod]
        public void PlayersIsEmptyAfterFileNew()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations");
            Assert.IsNull(controller.Players);
        }

        [TestMethod]
        public void PlayersAreCorrectAfterFileOpen()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.AreEqual(3, controller.Players.Count);
        }

        [TestMethod]
        public void SignedPlayersIsEmptyAfterCreation()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            Assert.IsNull(controller.SignedPlayers);
        }

        [TestMethod]
        public void SignedPlayersIsEmptyAfterFileClose()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.IsNotNull(controller.Teams);
            Assert.AreEqual(2, controller.SignedPlayers.Count);
            controller.CloseRugbyUnion();
            Assert.IsNull(controller.SignedPlayers);
        }

        [TestMethod]
        public void SignedPlayersIsEmptyAfterFileNew()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations");
            Assert.IsNull(controller.SignedPlayers);
        }

        [TestMethod]
        public void SignedPlayersAreCorrectAfterFileOpen()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "3",
                    "1; Chiefs",
                    "2; Chiefs",
                    "3; Chiefs",
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.AreEqual(3, controller.SignedPlayers.Count);
        }

        [TestMethod]
        public void TeamsAvailableWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations");
            Assert.IsFalse(controller.TeamsAvailable);
        }

        [TestMethod]
        public void TeamsAvailableWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "3",
                    "1; Chiefs",
                    "2; Chiefs",
                    "3; Chiefs",
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.IsTrue(controller.TeamsAvailable);
        }

        [TestMethod]
        public void PlayersAvailableWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations");
            Assert.IsFalse(controller.PlayersAvailable);
        }

        [TestMethod]
        public void PlayersAvailableWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "3",
                    "1; Chiefs",
                    "2; Chiefs",
                    "3; Chiefs",
                }
            };
            RugbyController controller = new RugbyController(view, fileIo);
            controller.OpenRugbyUnion("filename.txt");
            Assert.IsTrue(controller.PlayersAvailable);
        }

        [TestMethod]
        public void AddPlayerWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var player = new RugbyModel.Player()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Height = 180,
                Weight = 80,
                DateOfBirth = new DateTime(1970, 10, 20),
                PlaceOfBirth = "Nelson"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsNotNull(controller.Players);
            Assert.AreEqual(1, controller.Players.Count);
            player = view._newPlayerAdded;

            Assert.AreNotEqual(0, player.Id);
            Assert.AreEqual("Bob", controller.Players[0].FirstName);
            Assert.AreEqual("Smith", controller.Players[0].LastName);
            Assert.AreEqual(180, controller.Players[0].Height);
            Assert.AreEqual(80, controller.Players[0].Weight);
            Assert.AreEqual(new DateTime(1970, 10, 20).Date, controller.Players[0].DateOfBirth.Date);
            Assert.AreEqual("Nelson", controller.Players[0].PlaceOfBirth);

            player = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsNotNull(controller.Players);
            Assert.AreEqual(2, controller.Players.Count);
            player = view._newPlayerAdded;

            Assert.AreNotEqual(0, player.Id);
            Assert.AreEqual("Lizo", controller.Players[1].FirstName);
            Assert.AreEqual("Gqoboka", controller.Players[1].LastName);
            Assert.AreEqual(183, controller.Players[1].Height);
            Assert.AreEqual(115, controller.Players[1].Weight);
            Assert.AreEqual(new DateTime(1990, 3, 24).Date, controller.Players[1].DateOfBirth.Date);
            Assert.AreEqual("Tabankulu, South Africa", controller.Players[1].PlaceOfBirth);
        }

        [TestMethod]
        public void AddPlayerDetectsNull()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.AddPlayer(null);
            });
        }

        [TestMethod]
        public void AddPlayerDetectsNullOrEmpty1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            var player = new RugbyModel.Player()
            {
                FirstName = null,
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            Assert.ThrowsException<ArgumentNullException>(() => controller.AddPlayer(player));
            player.FirstName = "";
            Assert.ThrowsException<ArgumentNullException>(() => controller.AddPlayer(player));
            player.FirstName = string.Empty;
            Assert.ThrowsException<ArgumentNullException>(() => controller.AddPlayer(player));
        }

        [TestMethod]
        public void AddPlayerDetectsNullOrEmpty2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            var player = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = null,
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            Assert.ThrowsException<ArgumentNullException>(() => controller.AddPlayer(player));
            player.LastName = "";
            Assert.ThrowsException<ArgumentNullException>(() => controller.AddPlayer(player));
            player.LastName = string.Empty;
            Assert.ThrowsException<ArgumentNullException>(() => controller.AddPlayer(player));
        }

        [TestMethod]
        public void AddPlayerDetectsDuplicatePlayerName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var player = new RugbyModel.Player()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Height = 180,
                Weight = 80,
                DateOfBirth = new DateTime(1970, 10, 20),
                PlaceOfBirth = "Nelson"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsNotNull(controller.Players);
            Assert.AreEqual(1, controller.Players.Count);

            player = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsNotNull(controller.Players);
            Assert.AreEqual(2, controller.Players.Count);

            player = new RugbyModel.Player()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Height = 190,
                Weight = 118,
                DateOfBirth = new DateTime(1998, 6, 8),
                PlaceOfBirth = "Welkom, South Africa"
            };
            Assert.ThrowsException<ArgumentException>(() => controller.AddPlayer(player));
        }

        [TestMethod]
        public void EditPlayerDetectsNull()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.EditPlayer(null);
            });
        }

        [TestMethod]
        public void EditPlayerDetectsInvalidPlayerId()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                controller.EditPlayer(new RugbyModel.Player());
            });
        }

        [TestMethod]
        public void EditPlayerWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            var foundPlayer = controller.FindPlayer("Lizo", "Gqoboka");
            Assert.IsNotNull(foundPlayer);
            Assert.AreEqual(foundPlayer.Height, 183);
            Assert.AreEqual(foundPlayer.Weight, 115);
            Assert.AreEqual(foundPlayer.DateOfBirth, new DateTime(1990, 3, 24).Date);
            Assert.AreEqual(foundPlayer.PlaceOfBirth, "Tabankulu, South Africa");

            player.Id = foundPlayer.Id;
            player.FirstName = "Warrick";
            player.LastName = "Gelant";
            player.Height = 178;
            player.Weight = 89;
            player.DateOfBirth = new DateTime(1995, 5, 20);
            player.PlaceOfBirth = "Knysna, South Africa";
            controller.EditPlayer(player);
            Assert.IsTrue(view._onPlayerEditedCalled);
            foundPlayer = controller.FindPlayer("Warrick", "Gelant");
            Assert.IsNotNull(foundPlayer);
            Assert.AreEqual(foundPlayer.Height, 178);
            Assert.AreEqual(foundPlayer.Weight, 89);
            Assert.AreEqual(foundPlayer.DateOfBirth, new DateTime(1995, 5, 20).Date);
            Assert.AreEqual(foundPlayer.PlaceOfBirth, "Knysna, South Africa");
        }

        [TestMethod]
        public void EditPlayerDetectsUnknownName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            var foundPlayer = controller.FindPlayer("Lizo", "Gqoboka");
            Assert.IsNotNull(foundPlayer);
            Assert.AreEqual(foundPlayer.Height, 183);
            Assert.AreEqual(foundPlayer.Weight, 115);
            Assert.AreEqual(foundPlayer.DateOfBirth, new DateTime(1990, 3, 24).Date);
            Assert.AreEqual(foundPlayer.PlaceOfBirth, "Tabankulu, South Africa");

            var otherPlayer = new RugbyModel.Player()
            {
                Id = 1,
                FirstName = "Warrick",
                LastName = "Gelant",
                Height = 178,
                Weight = 89,
                DateOfBirth = new DateTime(1995, 5, 20),
                PlaceOfBirth = "Knysna, South Africa"
            };
            Assert.ThrowsException<ArgumentException>(() =>
            {
                controller.EditPlayer(otherPlayer);
            });
        }

        [TestMethod]
        public void DeletePlayerDetectsInvalidId()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                controller.DeletePlayer(0);
            });
        }

        [TestMethod]
        public void DeletePlayerDetectsUnknownName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.ThrowsException<ArgumentException>(() =>
            {
                controller.DeletePlayer(14);
            });
        }

        [TestMethod]
        public void DeletePlayerWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            controller.DeletePlayer(view._newPlayerAdded.Id);
            Assert.IsNull(controller.FindPlayer(view._newPlayerAdded.Id));
        }

        [TestMethod]
        public void DeletePlayerRemovesSignedPlayerToo()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Rivez",
                LastName = "Reihana",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Whetukamokamo",
                LastName = "Douglas",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            controller.SignPlayerToTeam(player1.Id, team.Name);
            Assert.IsTrue(view._onPlayerAddedCalled);
            controller.SignPlayerToTeam(player2.Id, team.Name);
            Assert.IsTrue(view._onPlayerAddedCalled);
            controller.SignPlayerToTeam(player3.Id, team.Name);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.DeletePlayer(player3.Id);
            Assert.IsNotNull(controller.FindPlayer(player1.Id));
            Assert.IsNotNull(controller.FindPlayer(player2.Id));
            Assert.IsNull(controller.FindPlayer(player3.Id));
            Assert.IsNotNull(controller.FindSignedPlayer(player1.Id));
            Assert.IsNotNull(controller.FindSignedPlayer(player2.Id));
            Assert.IsNull(controller.FindSignedPlayer(player3.Id));

            controller.DeletePlayer(player1.Id);
            Assert.IsNull(controller.FindPlayer(player1.Id));
            Assert.IsNotNull(controller.FindPlayer(player2.Id));
            Assert.IsNull(controller.FindPlayer(player3.Id));
            Assert.IsNull(controller.FindSignedPlayer(player1.Id));
            Assert.IsNotNull(controller.FindSignedPlayer(player2.Id));
            Assert.IsNull(controller.FindSignedPlayer(player3.Id));
        }

        [TestMethod]
        public void DeletePlayersDetectsNull1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.DeletePlayers(null);
            });
        }

        [TestMethod]
        public void DeletePlayersDetectsNull2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);
            controller.DeletePlayers(new List<int>() { 3, 2, 1 });
        }

        [TestMethod]
        public void DeletePlayersWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);

            controller.DeletePlayers(new List<int>());
            Assert.IsTrue(view._onPlayersDeletedCalled);
            Assert.AreEqual(0, view._playerIdsDeleted.Count);
        }

        [TestMethod]
        public void DeletePlayersWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "William",
                LastName = "Harmon",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Mayco",
                LastName = "Vivas",
                Height = 185,
                Weight = 122,
                DateOfBirth = new DateTime(1998, 6, 2),
                PlaceOfBirth = "Argentina"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Nicholas",
                LastName = "Mayhew",
                Height = 180,
                Weight = 116,
                DateOfBirth = new DateTime(1989, 11, 28),
                PlaceOfBirth = "Auckland, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);

            controller.DeletePlayers(new List<int>() { player3.Id, player1.Id });
            Assert.IsTrue(view._onPlayersDeletedCalled);
            Assert.AreEqual(2, view._playerIdsDeleted.Count);
            Assert.AreEqual(player3.Id, view._playerIdsDeleted[0]);
            Assert.AreEqual(player1.Id, view._playerIdsDeleted[1]);
            Assert.AreEqual(2, controller.Players.Count);
            Assert.AreEqual("William", controller.Players[0].FirstName);
            Assert.AreEqual("Nicholas", controller.Players[1].FirstName);
        }

        [TestMethod]
        public void DeleteAllPlayersWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Kwenzokuhle",
                LastName = "Blose",
                Height = 187,
                Weight = 109,
                DateOfBirth = new DateTime(1997, 5, 15),
                PlaceOfBirth = "Paulpietersburg, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Mayco",
                LastName = "Vivas",
                Height = 185,
                Weight = 122,
                DateOfBirth = new DateTime(1998, 6, 2),
                PlaceOfBirth = "Argentina"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Nicholas",
                LastName = "Mayhew",
                Height = 180,
                Weight = 116,
                DateOfBirth = new DateTime(1989, 11, 28),
                PlaceOfBirth = "Auckland, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);

            controller.DeleteAllPlayers();
            Assert.IsTrue(view._onPlayersDeletedCalled);
            Assert.AreEqual(4, view._playerIdsDeleted.Count);
            Assert.IsNull(controller.Players);
        }

        [TestMethod]
        public void DeleteAllPlayersWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);
            controller.DeleteAllPlayers();
            Assert.IsNull(controller.Players);
        }

        [TestMethod]
        public void SignPlayerToTeamWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);
        }

        [TestMethod]
        public void UnsignPlayerFromTeamWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.UnsignPlayerFromTeam(player1.Id, team.Name);
            Assert.AreEqual(1, controller.SignedPlayers.Count);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);
            Assert.AreEqual(player2.Id, controller.SignedPlayers[0].PlayerId);
            Assert.AreEqual("Tigers", controller.SignedPlayers[0].TeamName);
        }

        [TestMethod]
        public void UnsignPlayersFromTeamsWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            var signedPlayer1 = new RugbyModel.SignedPlayer() { PlayerId = player1.Id, TeamName = team1.Name };
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            var signedPlayer2 = new RugbyModel.SignedPlayer() { PlayerId = player2.Id, TeamName = team2.Name };
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            var signedPlayer3 = new RugbyModel.SignedPlayer() { PlayerId = player3.Id, TeamName = team1.Name };
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            var signedPlayer4 = new RugbyModel.SignedPlayer() { PlayerId = player4.Id, TeamName = team2.Name };
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.UnsignPlayersFromTeams(new List<RugbyModel.SignedPlayer>() { signedPlayer2, signedPlayer3 });
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);
            Assert.IsNotNull(controller.FindSignedPlayer(signedPlayer1.PlayerId));
            Assert.IsNull(controller.FindSignedPlayer(signedPlayer2.PlayerId));
            Assert.IsNull(controller.FindSignedPlayer(signedPlayer3.PlayerId));
            Assert.IsNotNull(controller.FindSignedPlayer(signedPlayer4.PlayerId));
        }

        [TestMethod]
        public void UnsignAllPlayersFromTeamWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.UnsignAllPlayersFromTeam(team1.Name);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);
            Assert.AreEqual(player2.Id, controller.SignedPlayers[0].PlayerId);
            Assert.AreEqual(player2.DisplayName, controller.SignedPlayers[0].PlayerName);
            Assert.AreEqual(player4.Id, controller.SignedPlayers[1].PlayerId);
            Assert.AreEqual(player4.DisplayName, controller.SignedPlayers[1].PlayerName);
        }

        [TestMethod]
        public void UnsignAllPlayersFromAllTeamsWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.UnsignAllPlayersFromAllTeams();
            Assert.IsTrue(view._onPlayersUnsignedFromTeamsCalled);
            Assert.IsNull(controller.SignedPlayers);
        }

        [TestMethod]
        public void GetPlayersSignedToTeamDetectsNullOrEmpty()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.GetPlayersSignedToTeam(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.GetPlayersSignedToTeam("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.GetPlayersSignedToTeam(string.Empty);
            });
        }

        [TestMethod]
        public void GetPlayersSignedToTeamDetectsNoSignedPlayers()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);
            Assert.IsNull(controller.GetPlayersSignedToTeam("blah"));
        }

        [TestMethod]
        public void GetPlayersSignedToTeamDetectsUnknownTeamName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            Assert.IsNull(controller.GetPlayersSignedToTeam("blah"));
        }

        [TestMethod]
        public void GetPlayersSignedToTeamWorksCorrectly()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            var tigersPlayers = controller.GetPlayersSignedToTeam(team1.Name);
            Assert.IsNotNull(tigersPlayers);
            Assert.AreEqual(2, tigersPlayers.Count);
            Assert.AreEqual(player1.Id, tigersPlayers[0].PlayerId);
            Assert.AreEqual("Lizo Gqoboka", tigersPlayers[0].PlayerName);
            Assert.AreEqual("Tigers", tigersPlayers[0].TeamName);
            Assert.AreEqual(player3.Id, tigersPlayers[1].PlayerId);
            Assert.AreEqual("Kane Le'aupepe", tigersPlayers[1].PlayerName);
            Assert.AreEqual("Tigers", tigersPlayers[1].TeamName);

            var jaguaresPlayers = controller.GetPlayersSignedToTeam(team2.Name);
            Assert.IsNotNull(jaguaresPlayers);
            Assert.AreEqual(2, jaguaresPlayers.Count);
            Assert.AreEqual(player2.Id, jaguaresPlayers[0].PlayerId);
            Assert.AreEqual("Jeff Thwaites", jaguaresPlayers[0].PlayerName);
            Assert.AreEqual("Jaguares", jaguaresPlayers[0].TeamName);
            Assert.AreEqual(player4.Id, jaguaresPlayers[1].PlayerId);
            Assert.AreEqual("Irae Simone", jaguaresPlayers[1].PlayerName);
            Assert.AreEqual("Jaguares", jaguaresPlayers[1].TeamName);
        }

        [TestMethod]
        public void SignedPlayersAvailableWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);
            Assert.IsFalse(controller.SignedPlayersAvailable);
        }

        [TestMethod]
        public void SignedPlayersAvailableWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            Assert.IsTrue(controller.SignedPlayersAvailable);
        }

        [TestMethod]
        public void FindTeamDetectsNullOrEmpty()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.FindTeam(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.FindTeam("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.FindTeam(string.Empty);
            });
        }

        [TestMethod]
        public void FindTeamWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.FindTeam("Blah"));
        }

        [TestMethod]
        public void FindTeamWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var foundTeam = controller.FindTeam("Jaguares");
            Assert.IsNotNull(foundTeam);
            Assert.AreEqual("Jaguares", foundTeam.Name);
            Assert.AreEqual("Buenos Aires, Argentina", foundTeam.Region);
            Assert.AreEqual(2015, foundTeam.YearFounded);
            Assert.AreEqual("Gonzalo Quesada", foundTeam.Coach);
            Assert.AreEqual("José Amalfitani Stadium", foundTeam.HomeGround);
        }

        [TestMethod]
        public void FindTeamDetectsUnknownTeam()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            Assert.IsNull(controller.FindTeam("Pumas"));
        }

        [TestMethod]
        public void FindPlayerDetectsInvalidPlayerId()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                controller.FindPlayer(0);
            });
        }

        [TestMethod]
        public void FindPlayerWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.FindPlayer(10));
        }

        [TestMethod]
        public void FindPlayerWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Josh",
                LastName = "Smith",
                PlaceOfBirth = "Levin",
                DateOfBirth = new DateTime(1990, 1, 3),
                Height = 190,
                Weight = 101
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Sarah",
                LastName = "Parkinson",
                PlaceOfBirth = "Levin",
                DateOfBirth = new DateTime(1990, 1, 3),
                Height = 190,
                Weight = 101
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Francis",
                LastName = "Yu",
                PlaceOfBirth = "Levin",
                DateOfBirth = new DateTime(1990, 1, 3),
                Height = 190,
                Weight = 101
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);

            var foundPlayer = controller.FindPlayer(player2.Id);
            Assert.IsNotNull(foundPlayer);
            Assert.AreEqual("Sarah", foundPlayer.FirstName);
            Assert.AreEqual("Parkinson", foundPlayer.LastName);
            Assert.AreEqual(190, foundPlayer.Height);
            Assert.AreEqual(101, foundPlayer.Weight);
            Assert.AreEqual("Levin", foundPlayer.PlaceOfBirth);
            Assert.AreEqual(new DateTime(1990, 1, 3).Date, foundPlayer.DateOfBirth.Date);
        }

        [TestMethod]
        public void FindPlayerWorksCorrectly3()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Josh",
                LastName = "Smith",
                PlaceOfBirth = "Levin",
                DateOfBirth = new DateTime(1990, 1, 3),
                Height = 190,
                Weight = 101
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Sarah",
                LastName = "Parkinson",
                PlaceOfBirth = "Levin",
                DateOfBirth = new DateTime(1990, 1, 3),
                Height = 190,
                Weight = 101
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Francis",
                LastName = "Yu",
                PlaceOfBirth = "Arkansas",
                DateOfBirth = new DateTime(2001, 10, 30),
                Height = 190,
                Weight = 121
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);

            var foundPlayer = controller.FindPlayer("Francis", "Yu");
            Assert.IsNotNull(foundPlayer);
            Assert.AreEqual("Francis", foundPlayer.FirstName);
            Assert.AreEqual("Yu", foundPlayer.LastName);
            Assert.AreEqual(190, foundPlayer.Height);
            Assert.AreEqual(121, foundPlayer.Weight);
            Assert.AreEqual("Arkansas", foundPlayer.PlaceOfBirth);
            Assert.AreEqual(new DateTime(2001, 10, 30).Date, foundPlayer.DateOfBirth.Date);
        }

        [TestMethod]
        public void FindPlayerDetectsUnknownPlayer()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Josh",
                LastName = "Smith",
                PlaceOfBirth = "Levin",
                DateOfBirth = new DateTime(1990, 1, 3),
                Height = 190,
                Weight = 101
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Sarah",
                LastName = "Parkinson",
                PlaceOfBirth = "Levin",
                DateOfBirth = new DateTime(1990, 1, 3),
                Height = 190,
                Weight = 101
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Francis",
                LastName = "Yu",
                PlaceOfBirth = "Arkansas",
                DateOfBirth = new DateTime(2001, 10, 30),
                Height = 190,
                Weight = 121
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);

            Assert.IsNull(controller.FindPlayer("Brendan", "Chao"));
        }

        [TestMethod]
        public void FindSignedPlayerDetectsInvalidId()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                controller.FindSignedPlayer(0);
            });
        }

        [TestMethod]
        public void FindSignedPlayerWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.FindSignedPlayer(12));
        }

        [TestMethod]
        public void FindSignedPlayerWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            var foundSignedPlayer = controller.FindSignedPlayer(player3.Id);
            Assert.IsNotNull(foundSignedPlayer);
            Assert.AreEqual(player3.Id, foundSignedPlayer.PlayerId);
            Assert.AreEqual("Kane Le'aupepe", foundSignedPlayer.PlayerName);
            Assert.AreEqual("Tigers", foundSignedPlayer.TeamName);
        }

        [TestMethod]
        public void FindSignedPlayerDetectsUnknownSignedPlayer()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            Assert.IsNull(controller.FindSignedPlayer(5));
        }

        [TestMethod]
        public void FindDetectsNull()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.Find(null);
            });
        }

        [TestMethod]
        public void FindDetectsNullOrEmpty()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.Find(new RugbyView.FindReplaceOptions() { FindWhat = null });
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.Find(new RugbyView.FindReplaceOptions() { FindWhat = "" });
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.Find(new RugbyView.FindReplaceOptions() { FindWhat = string.Empty });
            });
        }

        [TestMethod]
        public void FindCorrectlyFindsTeamsCaseInsensitiveNoWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "GONZA",
                MatchCase = false,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(1, view._findResults.Count);
            Assert.AreEqual("Coach", view._findResults[0].FieldName);
            Assert.AreEqual("Gonzalo Quesada", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Team);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "asdf",
                MatchCase = false,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsTeamsCaseInsensitiveWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "GoNzAlO QuEsAdA",
                MatchCase = false,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(1, view._findResults.Count);
            Assert.AreEqual("Coach", view._findResults[0].FieldName);
            Assert.AreEqual("Gonzalo Quesada", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Team);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "GoNzAlO",
                MatchCase = false,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsTeamsCaseSensitiveNoWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Gonza",
                MatchCase = true,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(1, view._findResults.Count);
            Assert.AreEqual("Coach", view._findResults[0].FieldName);
            Assert.AreEqual("Gonzalo Quesada", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Team);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "GoNzAlO",
                MatchCase = true,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsTeamsCaseSensitiveWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Gonzalo Quesada",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(1, view._findResults.Count);
            Assert.AreEqual("Coach", view._findResults[0].FieldName);
            Assert.AreEqual("Gonzalo Quesada", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Team);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "GONZALO Quesada",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Gonzalo",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsPlayersCaseInsensitiveNoWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "QUEENSLAND",
                MatchCase = false,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(1, view._findResults.Count);
            Assert.AreEqual("PlaceOfBirth", view._findResults[0].FieldName);
            Assert.AreEqual("Brisbane, Queensland, Australia", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Player);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "asdf",
                MatchCase = false,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsPlayersCaseInsensitiveWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "BrISbane, QuEENSland, AUSTRALIA",
                MatchCase = false,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(1, view._findResults.Count);
            Assert.AreEqual("PlaceOfBirth", view._findResults[0].FieldName);
            Assert.AreEqual("Brisbane, Queensland, Australia", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Player);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "BrISbane, QuEENSland",
                MatchCase = false,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsPlayersCaseSensitiveNoWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Queensland",
                MatchCase = true,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(1, view._findResults.Count);
            Assert.AreEqual("PlaceOfBirth", view._findResults[0].FieldName);
            Assert.AreEqual("Brisbane, Queensland, Australia", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Player);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "QUEENSLAND",
                MatchCase = true,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsPlayersCaseSensitiveWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Brisbane, Queensland, Australia",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(1, view._findResults.Count);
            Assert.AreEqual("PlaceOfBirth", view._findResults[0].FieldName);
            Assert.AreEqual("Brisbane, Queensland, Australia", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Player);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Brisbane, QUEENSLAND, Australia",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Brisbane, Queensland",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsSignedPlayersCaseInsensitiveNoWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "JEF",
                MatchCase = false,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(2, view._findResults.Count);
            Assert.AreEqual("FirstName", view._findResults[0].FieldName);
            Assert.AreEqual("Jeff", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Player);
            Assert.AreEqual("PlayerName", view._findResults[1].FieldName);
            Assert.AreEqual("Jeff Thwaites", view._findResults[1].FieldValue);
            Assert.IsTrue(view._findResults[1].Item is RugbyModel.SignedPlayer);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "asdf",
                MatchCase = false,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsSignedPlayersCaseInsensitiveWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "JEFF",
                MatchCase = false,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(1, view._findResults.Count);
            Assert.AreEqual("FirstName", view._findResults[0].FieldName);
            Assert.AreEqual("Jeff", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Player);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Jeffrey",
                MatchCase = false,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsSignedPlayersCaseSensitiveNoWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "eff",
                MatchCase = true,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(2, view._findResults.Count);
            Assert.AreEqual("FirstName", view._findResults[0].FieldName);
            Assert.AreEqual("Jeff", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Player);
            Assert.AreEqual("PlayerName", view._findResults[1].FieldName);
            Assert.AreEqual("Jeff Thwaites", view._findResults[1].FieldValue);
            Assert.IsTrue(view._findResults[1].Item is RugbyModel.SignedPlayer);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "EFF",
                MatchCase = true,
                MatchWholeWord = false,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyFindsSignedPlayersCaseSensitiveWholeWord()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Jeff",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(1, view._findResults.Count);
            Assert.AreEqual("FirstName", view._findResults[0].FieldName);
            Assert.AreEqual("Jeff", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Player);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "JEFF",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Jef",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(0, view._findResults.Count);
        }

        [TestMethod]
        public void FindCorrectlyMatchesUsingARegex()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Find(new RugbyView.FindReplaceOptions()
            {
                FindWhat = "Park|Samoa",
                MatchCase = false,
                MatchWholeWord = false,
                UseRegularExpression = true,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true
            });
            Assert.IsTrue(view._onFindResultsCalled);
            Assert.AreEqual(2, view._findResults.Count);
            Assert.AreEqual("HomeGround", view._findResults[0].FieldName);
            Assert.AreEqual("Eden Park", view._findResults[0].FieldValue);
            Assert.IsTrue(view._findResults[0].Item is RugbyModel.Team);
            Assert.AreEqual("PlaceOfBirth", view._findResults[1].FieldName);
            Assert.AreEqual("Apia, Samoa", view._findResults[1].FieldValue);
            Assert.IsTrue(view._findResults[1].Item is RugbyModel.Player);
        }

        [TestMethod]
        public void ReplaceWorksCorrectly1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Replace(new RugbyView.ReplaceOptions()
            {
                FindWhat = "Taranaki",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true,
                ReplaceWith = "Bunnythorpe"
            });
            Assert.IsTrue(view._onReplaceResultsCalled);
            Assert.AreEqual(1, view._replaceResults.Count);
            Assert.AreEqual("Region", view._replaceResults[0].FieldName);
            Assert.AreEqual("Taranaki", view._replaceResults[0].FieldValue);
            Assert.AreEqual("Bunnythorpe", view._replaceResults[0].ReplacedValue);
            Assert.IsTrue(view._replaceResults[0].Item is RugbyModel.Team);
            var team = controller.FindTeam("Tigers");
            Assert.IsNotNull(team);
            Assert.AreEqual("Bunnythorpe", team.Region);
        }

        [TestMethod]
        public void ReplaceWorksCorrectly2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Replace(new RugbyView.ReplaceOptions()
            {
                FindWhat = "Wellington, New Zealand",
                MatchCase = true,
                MatchWholeWord = true,
                UseRegularExpression = false,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true,
                ReplaceWith = "Dunedin, New Zealand"
            });
            Assert.IsTrue(view._onReplaceResultsCalled);
            Assert.AreEqual(1, view._replaceResults.Count);
            Assert.AreEqual("PlaceOfBirth", view._replaceResults[0].FieldName);
            Assert.AreEqual("Wellington, New Zealand", view._replaceResults[0].FieldValue);
            Assert.AreEqual("Dunedin, New Zealand", view._replaceResults[0].ReplacedValue);
            Assert.IsTrue(view._replaceResults[0].Item is RugbyModel.Player);
            var player = controller.FindPlayer("Irae", "Simone");
            Assert.IsNotNull(player);
            Assert.AreEqual("Dunedin, New Zealand", player.PlaceOfBirth);
        }

        [TestMethod]
        public void ReplaceCorrectlyMatchesWithARegex()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.IsNull(controller.Players);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.Replace(new RugbyView.ReplaceOptions()
            {
                FindWhat = "Park|Samoa",
                MatchCase = false,
                MatchWholeWord = false,
                UseRegularExpression = true,
                FindTeams = true,
                FindPlayers = true,
                FindSignedPlayers = true,
                ReplaceWith = "Coffee"
            });
            Assert.IsTrue(view._onReplaceResultsCalled);
            Assert.AreEqual(2, view._replaceResults.Count);
            Assert.AreEqual("HomeGround", view._replaceResults[0].FieldName);
            Assert.AreEqual("Eden Park", view._replaceResults[0].FieldValue);
            Assert.AreEqual("Eden Coffee", view._replaceResults[0].ReplacedValue);
            Assert.IsTrue(view._replaceResults[0].Item is RugbyModel.Team);
            Assert.AreEqual("PlaceOfBirth", view._replaceResults[1].FieldName);
            Assert.AreEqual("Apia, Samoa", view._replaceResults[1].FieldValue);
            Assert.AreEqual("Apia, Coffee", view._replaceResults[1].ReplacedValue);
            Assert.IsTrue(view._replaceResults[1].Item is RugbyModel.Player);
            var team = controller.FindTeam("Tigers");
            Assert.IsNotNull(team);
            Assert.AreEqual("Eden Coffee", team.HomeGround);
            var player = controller.FindPlayer("Lizo", "Gqoboka");
            Assert.IsNotNull(player);
            Assert.AreEqual("Apia, Coffee", player.PlaceOfBirth);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceDetectsNull1()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.AdvancedFindAndReplace(null);
            });
        }

        [TestMethod]
        public void AdvancedFindAndReplaceDetectsNull2()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions());
            });
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsTeamBasedOnName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Teams,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "Name",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.Contains,
                        BeginValue = "Jaguares",
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("Name", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("Jaguares", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsTeamBasedOnHomeGround()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Teams,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "HomeGround",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.StartsWith,
                        BeginValue = "Eden",
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("HomeGround", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("Eden Park", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsTeamBasedOnCoach()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Teams,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "Coach",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.EndsWith,
                        BeginValue = "Quesada",
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("Coach", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("Gonzalo Quesada", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsTeamBasedOnRegion()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Teams,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "Region",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.IsEqualTo,
                        BeginValue = "Buenos Aires, ARGENTINA",
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("Region", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("Buenos Aires, Argentina", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsTeamBasedOnYearFounded()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Teams,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "YearFounded",
                        Type = typeof(int),
                        Operation = RugbyView.FindOperation.Between,
                        BeginValue = 1900,
                        EndValue = 1960,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("YearFounded", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("1950", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsPlayerBasedOnId()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Players,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "Id",
                        Type = typeof(int),
                        Operation = RugbyView.FindOperation.GreaterThan,
                        BeginValue = player4.Id - 1,
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("Id", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual(player4.Id.ToString(), view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsPlayerBasedOnFirstName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Players,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "FirstName",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.IsEqualTo,
                        BeginValue = "Kane",
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("FirstName", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("Kane", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsPlayerBasedOnLastName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Players,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "LastName",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.Contains,
                        BeginValue = "Gqoboka",
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("LastName", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("Gqoboka", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsPlayerBasedOnHeight()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 190,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 175,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 201,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Players,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "Height",
                        Type = typeof(int),
                        Operation = RugbyView.FindOperation.FewerThan,
                        BeginValue = 180,
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("Height", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("175", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsPlayerBasedOnWeight()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 85,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 90,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 97,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Players,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "Weight",
                        Type = typeof(int),
                        Operation = RugbyView.FindOperation.Between,
                        BeginValue = 90,
                        EndValue = 100,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("Weight", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("97", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsPlayerBasedOnDateOfBirth()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1993, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1997, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1989, 4, 9),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Players,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "DateOfBirth",
                        Type = typeof(DateTime),
                        Operation = RugbyView.FindOperation.Before,
                        BeginValue = new DateTime(1990, 1, 1).Date,
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("DateOfBirth", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual(new DateTime(1989, 4, 9).Date.ToShortDateString(), view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsPlayerBasedOnPlaceOfBirth()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Players,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "PlaceOfBirth",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.Contains,
                        BeginValue = "Tabankulu",
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("PlaceOfBirth", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("Tabankulu, South Africa", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsSignedPlayerBasedOnPlayerId()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.SignedPlayers,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "PlayerId",
                        Type = typeof(int),
                        Operation = RugbyView.FindOperation.EqualTo,
                        BeginValue = player2.Id,
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("PlayerId", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual(player2.Id.ToString(), view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsSignedPlayerBasedOnPlayerName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.SignedPlayers,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "PlayerName",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.Contains,
                        BeginValue = "Le'aupepe",
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
            Assert.AreEqual("PlayerName", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("Kane Le'aupepe", view._advancedFindResults[0].Fields[0].Item2);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceFindsSignedPlayerBasedOnTeamName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.SignedPlayers,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "TeamName",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.StartsWith,
                        BeginValue = "Jag",
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(2, view._advancedFindResults.Count);
            Assert.AreEqual("TeamName", view._advancedFindResults[0].Fields[0].Item1);
            Assert.AreEqual("Jaguares", view._advancedFindResults[0].Fields[0].Item2);
            Assert.IsTrue(view._advancedFindResults[0].Item is RugbyModel.SignedPlayer);
            Assert.AreEqual("TeamName", view._advancedFindResults[1].Fields[0].Item1);
            Assert.AreEqual("Jaguares", view._advancedFindResults[1].Fields[0].Item2);
            Assert.IsTrue(view._advancedFindResults[1].Item is RugbyModel.SignedPlayer);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceCannotReplaceTeamName()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Teams,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "Region",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.Contains,
                        BeginValue = "Argentina",
                        EndValue = null,
                    }
                },
                ReplaceField = new RugbyView.ReplaceField()
                {
                    Name = "Name",
                    Type = typeof(string),
                    Value = "Desktop"
                }
            });
            Assert.IsTrue(view._onAdvancedReplaceResultsCalled);
            Assert.AreEqual(1, view._advancedReplaceResults.Count);
            Assert.AreEqual("Region", view._advancedReplaceResults[0].Fields[0].Item1);
            Assert.AreEqual("Buenos Aires, Argentina", view._advancedReplaceResults[0].Fields[0].Item2);
            Assert.AreEqual("Name", view._advancedReplaceResults[0].ReplacedField);
            Assert.IsNull(view._advancedReplaceResults[0].ReplacedValue); // Because the replacment should have failed
            Assert.IsTrue(view._advancedReplaceResults[0].ReplaceMessage.Contains("Will not replace"));
        }

        [TestMethod]
        public void AdvancedFindAndReplaceCanReplaceNonTeamNameField()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Teams,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "Coach",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.EndsWith,
                        BeginValue = "Jones",
                        EndValue = null,
                    }
                },
                ReplaceField = new RugbyView.ReplaceField()
                {
                    Name = "Region",
                    Type = typeof(string),
                    Value = "New South Wales"
                }
            });
            Assert.IsTrue(view._onAdvancedReplaceResultsCalled);
            Assert.AreEqual(1, view._advancedReplaceResults.Count);
            Assert.AreEqual("Coach", view._advancedReplaceResults[0].Fields[0].Item1);
            Assert.AreEqual("Melanie Jones", view._advancedReplaceResults[0].Fields[0].Item2);
            Assert.AreEqual("Region", view._advancedReplaceResults[0].ReplacedField);
            Assert.AreEqual("New South Wales", view._advancedReplaceResults[0].ReplacedValue);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceCannotReplacePlayerId()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Players,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "FirstName",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.Contains,
                        BeginValue = "Jeff",
                        EndValue = null,
                    }
                },
                ReplaceField = new RugbyView.ReplaceField()
                {
                    Name = "Id",
                    Type = typeof(int),
                    Value = 23
                }
            });
            Assert.IsTrue(view._onAdvancedReplaceResultsCalled);
            Assert.AreEqual(1, view._advancedReplaceResults.Count);
            Assert.AreEqual("FirstName", view._advancedReplaceResults[0].Fields[0].Item1);
            Assert.AreEqual("Jeff", view._advancedReplaceResults[0].Fields[0].Item2);
            Assert.AreEqual("Id", view._advancedReplaceResults[0].ReplacedField);
            Assert.IsNull(view._advancedReplaceResults[0].ReplacedValue); // Because the replacment should have failed
            Assert.IsTrue(view._advancedReplaceResults[0].ReplaceMessage.Contains("Will not replace"));
        }

        [TestMethod]
        public void AdvancedFindAndReplaceCanReplaceNonPlayerIdField()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Players,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "FirstName",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.StartsWith,
                        BeginValue = "Jeff",
                        EndValue = null,
                    }
                },
                ReplaceField = new RugbyView.ReplaceField()
                {
                    Name = "FirstName",
                    Type = typeof(string),
                    Value = "Melissa"
                }
            });
            Assert.IsTrue(view._onAdvancedReplaceResultsCalled);
            Assert.AreEqual(1, view._advancedReplaceResults.Count);
            Assert.AreEqual("FirstName", view._advancedReplaceResults[0].Fields[0].Item1);
            Assert.AreEqual("Jeff", view._advancedReplaceResults[0].Fields[0].Item2);
            Assert.AreEqual("FirstName", view._advancedReplaceResults[0].ReplacedField);
            Assert.AreEqual("Melissa", view._advancedReplaceResults[0].ReplacedValue);
        }

        [TestMethod]
        public void AdvancedFindAndReplaceCannotReplaceSignedPlayer()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Tigers",
                Region = "Taranaki",
                YearFounded = 1950,
                Coach = "Melanie Jones",
                HomeGround = "Eden Park"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Jaguares",
                Region = "Buenos Aires, Argentina",
                YearFounded = 2015,
                Coach = "Gonzalo Quesada",
                HomeGround = "José Amalfitani Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var player1 = new RugbyModel.Player()
            {
                FirstName = "Lizo",
                LastName = "Gqoboka",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Apia, Samoa"
            };
            controller.AddPlayer(player1);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(1, controller.Players.Count);
            player1 = view._newPlayerAdded;

            var player2 = new RugbyModel.Player()
            {
                FirstName = "Jeff",
                LastName = "Thwaites",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Tabankulu, South Africa"
            };
            controller.AddPlayer(player2);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(2, controller.Players.Count);
            player2 = view._newPlayerAdded;

            var player3 = new RugbyModel.Player()
            {
                FirstName = "Kane",
                LastName = "Le'aupepe",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Brisbane, Queensland, Australia"
            };
            controller.AddPlayer(player3);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(3, controller.Players.Count);
            player3 = view._newPlayerAdded;

            var player4 = new RugbyModel.Player()
            {
                FirstName = "Irae",
                LastName = "Simone",
                Height = 183,
                Weight = 115,
                DateOfBirth = new DateTime(1990, 3, 24),
                PlaceOfBirth = "Wellington, New Zealand"
            };
            controller.AddPlayer(player4);
            Assert.IsTrue(view._onPlayerAddedCalled);
            Assert.IsTrue(controller.IsModified);
            Assert.AreEqual(4, controller.Players.Count);
            player4 = view._newPlayerAdded;

            Assert.IsNull(controller.SignedPlayers);

            controller.SignPlayerToTeam(player1.Id, team1.Name);
            Assert.AreEqual(player1.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(1, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player2.Id, team2.Name);
            Assert.AreEqual(player2.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(2, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player3.Id, team1.Name);
            Assert.AreEqual(player3.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Tigers", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(3, controller.SignedPlayers.Count);

            controller.SignPlayerToTeam(player4.Id, team2.Name);
            Assert.AreEqual(player4.Id, view._playerIdSignedToTeam);
            Assert.AreEqual("Jaguares", view._teamNameSignedToTeam);
            Assert.IsNotNull(controller.SignedPlayers);
            Assert.AreEqual(4, controller.SignedPlayers.Count);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.SignedPlayers,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "PlayerName",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.Contains,
                        BeginValue = "Thwaites",
                        EndValue = null,
                    }
                },
                ReplaceField = new RugbyView.ReplaceField()
                {
                    Name = "TeamName",
                    Type = typeof(int),
                    Value = "Wildcats"
                }
            });
            Assert.IsTrue(view._onAdvancedReplaceResultsCalled);
            Assert.AreEqual(1, view._advancedReplaceResults.Count);
            Assert.AreEqual("PlayerName", view._advancedReplaceResults[0].Fields[0].Item1);
            Assert.AreEqual("Jeff Thwaites", view._advancedReplaceResults[0].Fields[0].Item2);
            Assert.AreEqual("TeamName", view._advancedReplaceResults[0].ReplacedField);
            Assert.IsNull(view._advancedReplaceResults[0].ReplacedValue); // Because the replacment should have failed
            Assert.IsTrue(view._advancedReplaceResults[0].ReplaceMessage.Contains("Will not replace"));
        }

        [TestMethod]
        public void AdvancedFindAndReplaceUsesLogicalAND()
        {
            MockRugbyView view = new MockRugbyView();
            MockFileIo fileIo = new MockFileIo();
            RugbyController controller = new RugbyController(view, fileIo);
            controller.NewRugbyUnion("Six Nations Championship");
            Assert.IsTrue(view._onRugbyUnionCreatedCalled);

            var team1 = new RugbyModel.Team()
            {
                Name = "Brumbies",
                Region = "Canberra, ACT, Australia",
                YearFounded = 1996,
                Coach = "Dan McKellar",
                HomeGround = "GIO Stadium"
            };
            controller.AddTeam(team1);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team2 = new RugbyModel.Team()
            {
                Name = "Bulls",
                Region = "Pretoria, South Africa",
                YearFounded = 1997,
                Coach = "Jake White",
                HomeGround = "Loftus Versfeld Stadium"
            };
            controller.AddTeam(team2);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team3 = new RugbyModel.Team()
            {
                Name = "Lions",
                Region = "Johannesburg, South Africa",
                YearFounded = 1996,
                Coach = "Ivan van Rooyen",
                HomeGround = "Emirates Airline Park"
            };
            controller.AddTeam(team3);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            var team4 = new RugbyModel.Team()
            {
                Name = "Sunwolves",
                Region = "Tokyo, Japan",
                YearFounded = 2015,
                Coach = "Eddie Jones",
                HomeGround = "Chichibunomiya Stadium"
            };
            controller.AddTeam(team4);
            Assert.IsTrue(view._onTeamAddedCalled);
            Assert.IsTrue(controller.IsModified);

            controller.AdvancedFindAndReplace(new RugbyView.AdvancedFindReplaceOptions()
            {
                FindWhat = RugbyView.AdvancedFindReplaceOptions.What.Teams,
                FindFields = new List<RugbyView.FindField>()
                {
                    new RugbyView.FindField()
                    {
                        Name = "Region",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.EndsWith,
                        BeginValue = "South Africa",
                        EndValue = null,
                    },
                    new RugbyView.FindField()
                    {
                        Name = "HomeGround",
                        Type = typeof(string),
                        Operation = RugbyView.FindOperation.Contains,
                        BeginValue = "Park",
                        EndValue = null,
                    }
                }
            });
            Assert.IsTrue(view._onAdvancedFindResultsCalled);
            Assert.AreEqual(1, view._advancedFindResults.Count);
        }
    }
}
