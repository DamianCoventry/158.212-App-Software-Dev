using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace RugbyModel.Test
{
    [TestClass]
    public class RugbyUnionFileTest
    {
        [TestMethod]
        public void ConstructorDoesntThrow()
        {
            MockFileIo mockFileIo = new MockFileIo();
            _ = new RugbyUnionFile(mockFileIo);
        }

        [TestMethod]
        public void ConstructorDetectsNullAndEmptyString()
        {
            MockFileIo mockFileIo = new MockFileIo();
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new RugbyUnionFile(null, mockFileIo);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new RugbyUnionFile("", mockFileIo);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new RugbyUnionFile(string.Empty, mockFileIo);
            });
        }

        [TestMethod]
        public void LoadDetectsNullAndEmptyString()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                rugbyUnionFile.Load(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                rugbyUnionFile.Load("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                rugbyUnionFile.Load(string.Empty);
            });
        }

        [TestMethod]
        public void SaveDetectsNullAndEmptyString()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                rugbyUnionFile.Save(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                rugbyUnionFile.Save("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                rugbyUnionFile.Save(string.Empty);
            });
        }

        [TestMethod]
        public void LoadDetectsAbsentFile()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            Assert.ThrowsException<Exception>(() =>
            {
                rugbyUnionFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsEmptyFile()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>()
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            Assert.ThrowsException<Exception>(() =>
            {
                rugbyUnionFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile1()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() { "asdfadsfadsf", "rftujrftjhgfj", "zxcvzxcvzxcv", "rtyu567u56rtuh" }
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                rugbyUnionFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsUnsupportedVersion()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.1",
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
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                rugbyUnionFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile2()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Cats",
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
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                rugbyUnionFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile3()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "3456",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                rugbyUnionFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile4()
        {
            MockFileIo mockFileIo = new MockFileIo
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
                    "k;asdkfj;adksfj;aksdfj;aksjf;asd;fa;sf;jaksfj;aksdfjljasdhflasdk;fkajs;",
                    "dfk;jaskdfhjlajushflasdf;aksdjf;kadjs;fkaj;lsfkj;akdjf;aksdfk;asd;faajsd;lfkhlajhdfaf;kjadsflhj",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                rugbyUnionFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsMismatchingNumberOfPlayers()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "1000000",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "2",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                rugbyUnionFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsMismatchingNumberOfSignedPlayers()
        {
            MockFileIo mockFileIo = new MockFileIo
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
                    "1",
                    "1; Chiefs",
                    "2; Chiefs"
                }
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            rugbyUnionFile.Load("filename.txt");
            Assert.AreEqual(1, rugbyUnionFile.Teams.Count);
            Assert.AreEqual(3, rugbyUnionFile.Players.Count);
            Assert.AreEqual(1, rugbyUnionFile.SignedPlayers.Count);
        }

        [TestMethod]
        public void LoadHandlesZeroTeams()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "0",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga",
                    "Signed Players",
                    "0"
                }
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            rugbyUnionFile.Load("filename.txt");
            Assert.AreEqual(0, rugbyUnionFile.Teams.Count);
            Assert.AreEqual(3, rugbyUnionFile.Players.Count);
            Assert.AreEqual(0, rugbyUnionFile.SignedPlayers.Count);
        }

        [TestMethod]
        public void LoadHandlesZeroPlayers()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "0",
                    "Signed Players",
                    "0"
                }
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            rugbyUnionFile.Load("filename.txt");
            Assert.AreEqual(1, rugbyUnionFile.Teams.Count);
            Assert.AreEqual(0, rugbyUnionFile.Players.Count);
            Assert.AreEqual(0, rugbyUnionFile.SignedPlayers.Count);
        }

        [TestMethod]
        public void LoadHandlesZeroSignedPlayers()
        {
            MockFileIo mockFileIo = new MockFileIo
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
                    "0"
                }
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            rugbyUnionFile.Load("filename.txt");
            Assert.AreEqual(1, rugbyUnionFile.Teams.Count);
            Assert.AreEqual(3, rugbyUnionFile.Players.Count);
            Assert.AreEqual(0, rugbyUnionFile.SignedPlayers.Count);
        }

        [TestMethod]
        public void LoadWorksCorrectly()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "1",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "Signed Players",
                    "1",
                    "1; Chiefs",
                    }
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            rugbyUnionFile.Load("filename.txt");
            Assert.AreEqual(RugbyUnionFile.SupportedFileVersion, rugbyUnionFile.Version);
            Assert.AreEqual(1, rugbyUnionFile.Teams.Count);
            Assert.AreEqual(1, rugbyUnionFile.Players.Count);
            Assert.AreEqual(1, rugbyUnionFile.SignedPlayers.Count);
            Assert.AreEqual(rugbyUnionFile.Teams[0].Name, "Chiefs");
            Assert.AreEqual(rugbyUnionFile.Teams[0].HomeGround, "Waikato Stadium");
            Assert.AreEqual(rugbyUnionFile.Teams[0].Coach, "Clayton McMillan");
            Assert.AreEqual(rugbyUnionFile.Teams[0].YearFounded, 1996);
            Assert.AreEqual(rugbyUnionFile.Teams[0].Region, "Hamilton, New Zealand");
            Assert.AreEqual(rugbyUnionFile.Players[0].Id, 1);
            Assert.AreEqual(rugbyUnionFile.Players[0].FirstName, "Sione");
            Assert.AreEqual(rugbyUnionFile.Players[0].LastName, "Mafileo");
            Assert.AreEqual(rugbyUnionFile.Players[0].DateOfBirth.Date, new DateTime(1993, 4, 14).Date);
            Assert.AreEqual(rugbyUnionFile.Players[0].Height, 178);
            Assert.AreEqual(rugbyUnionFile.Players[0].Weight, 128);
            Assert.AreEqual(rugbyUnionFile.Players[0].PlaceOfBirth, "Auckland, New Zealand");
            Assert.AreEqual(rugbyUnionFile.SignedPlayers[0].PlayerId, 1);
            Assert.AreEqual(rugbyUnionFile.SignedPlayers[0].TeamName, "Chiefs");
        }

        [TestMethod]
        public void SaveWorksCorrectly()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo)
            {
                Name = "Boss Baby",
                Teams = new List<Team>()
                    {
                        new Team()
                        {
                            Name = "Chiefs",
                            HomeGround = "Waikato Stadium",
                            Coach = "Clayton McMillan",
                            YearFounded = 1996,
                            Region = "Hamilton, New Zealand",
                        }
                    },
                Players = new List<Player>()
                    {
                        new Player()
                        {
                            Id = 1,
                            FirstName = "Sione",
                            LastName = "Mafileo",
                            DateOfBirth = new DateTime(1993, 4, 14),
                            Height = 178,
                            Weight = 128,
                            PlaceOfBirth = "Auckland, New Zealand",
                        }
                    },
                SignedPlayers = new List<SignedPlayer>()
                    {
                        new SignedPlayer()
                        {
                            PlayerId = 1,
                            PlayerName = "Sione Mafileo",
                            TeamName = "Chiefs"
                        }
                    }
            };

            var expected = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "1",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "Signed Players",
                    "1",
                    "1; Chiefs",
                };

            rugbyUnionFile.Save("filename.txt");
            Assert.AreEqual(expected.Count, mockFileIo._writtenFileLines.Count);
            for (int i = 0; i < expected.Count; ++i)
                Assert.AreEqual(expected[i], mockFileIo._writtenFileLines[i]);
        }

        [TestMethod]
        public void ClearWorksCorrectly()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Rugby Union File Format",
                    "1.0",
                    "Boss Baby",
                    "Teams",
                    "1",
                    "Chiefs; Waikato Stadium; Clayton McMillan; Founded 1996, Hamilton, New Zealand",
                    "Players",
                    "1",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "Signed Players",
                    "1",
                    "1; Chiefs",
                    }
            };
            var rugbyUnionFile = new RugbyUnionFile(mockFileIo);
            rugbyUnionFile.Load("filename.txt");
            Assert.AreEqual(RugbyUnionFile.SupportedFileVersion, rugbyUnionFile.Version);
            Assert.AreEqual(1, rugbyUnionFile.Teams.Count);
            Assert.AreEqual(1, rugbyUnionFile.Players.Count);
            Assert.AreEqual(1, rugbyUnionFile.SignedPlayers.Count);
            rugbyUnionFile.Clear();
            Assert.IsNull(rugbyUnionFile.Version);
            Assert.IsNull(rugbyUnionFile.Teams);
            Assert.IsNull(rugbyUnionFile.Players);
            Assert.IsNull(rugbyUnionFile.SignedPlayers);
        }
    }
}
