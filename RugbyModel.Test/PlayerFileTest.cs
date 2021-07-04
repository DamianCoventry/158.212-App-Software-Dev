using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace RugbyModel.Test
{
    [TestClass]
    public class PlayerFileTest
    {
        [TestMethod]
        public void ConstructorDoesntThrow()
        {
            MockFileIo mockFileIo = new MockFileIo();
            _ = new PlayerFile(mockFileIo);
        }

        [TestMethod]
        public void ConstructorDetectsNullAndEmptyString()
        {
            MockFileIo mockFileIo = new MockFileIo();
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new PlayerFile(null, mockFileIo);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new PlayerFile("", mockFileIo);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new PlayerFile(string.Empty, mockFileIo);
            });
        }

        [TestMethod]
        public void LoadDetectsNullAndEmptyString()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var playerFile = new PlayerFile(mockFileIo);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                playerFile.Load(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                playerFile.Load("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                playerFile.Load(string.Empty);
            });
        }

        [TestMethod]
        public void SaveDetectsNullAndEmptyString()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var playerFile = new PlayerFile(mockFileIo);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                playerFile.Save(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                playerFile.Save("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                playerFile.Save(string.Empty);
            });
        }

        [TestMethod]
        public void LoadDetectsAbsentFile()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var playerFile = new PlayerFile(mockFileIo);
            Assert.ThrowsException<Exception>(() =>
            {
                playerFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsEmptyFile()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>()
            };
            var playerFile = new PlayerFile(mockFileIo);
            Assert.ThrowsException<Exception>(() =>
            {
                playerFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile1()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() { "asdfadsfadsf", "rftujrftjhgfj", "zxcvzxcvzxcv", "rtyu567u56rtuh" }
            };
            var playerFile = new PlayerFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                playerFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsUnsupportedVersion()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Player File Format",
                    "1.1",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga"
                }
            };
            var playerFile = new PlayerFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                playerFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile2()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Player File Format",
                    "1.0",
                    "Cats",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga"
                }
            };
            var playerFile = new PlayerFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                playerFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile3()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Player File Format",
                    "1.0",
                    "Players",
                    "3456",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga"
                }
            };
            var playerFile = new PlayerFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                playerFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile4()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Player File Format",
                    "1.0",
                    "Players",
                    "65",
                    "k;asdkfj;adksfj;aksdfj;aksjf;asd;fa;sf;jaksfj;aksdfjljasdhflasdk;fkajs;",
                    "dfk;jaskdfhjlajushflasdf;aksdjf;kadjs;fkaj;lsfkj;akdjf;aksdfk;asd;faajsd;lfkhlajhdfaf;kjadsflhj"
                }
            };
            var playerFile = new PlayerFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                playerFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsMismatchingNumberOfPlayers1()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Player File Format",
                    "1.0",
                    "Players",
                    "1000000",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga"
                }
            };
            var playerFile = new PlayerFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                playerFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsMismatchingNumberOfPlayers2()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Player File Format",
                    "1.0",
                    "Players",
                    "2",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga"
                }
            };
            var playerFile = new PlayerFile(mockFileIo);
            playerFile.Load("filename.txt");
            Assert.AreEqual(2, playerFile.Players.Count);
        }

        [TestMethod]
        public void LoadHandlesZeroPlayers()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Player File Format",
                    "1.0",
                    "Players",
                    "0"
                }
            };
            var playerFile = new PlayerFile(mockFileIo);
            playerFile.Load("filename.txt");
            Assert.AreEqual(0, playerFile.Players.Count);
        }

        [TestMethod]
        public void LoadWorksCorrectly()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Player File Format",
                    "1.0",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga"
                }
            };
            var playerFile = new PlayerFile(mockFileIo);
            playerFile.Load("filename.txt");
            Assert.AreEqual(PlayerFile.SupportedFileVersion, playerFile.Version);
            Assert.AreEqual(3, playerFile.Players.Count);
            Assert.AreEqual(playerFile.Players[0].Id, 1);
            Assert.AreEqual(playerFile.Players[0].FirstName, "Sione");
            Assert.AreEqual(playerFile.Players[0].LastName, "Mafileo");
            Assert.AreEqual(playerFile.Players[0].DateOfBirth.Date, new DateTime(1993, 4, 14).Date);
            Assert.AreEqual(playerFile.Players[0].Height, 178);
            Assert.AreEqual(playerFile.Players[0].Weight, 128);
            Assert.AreEqual(playerFile.Players[0].PlaceOfBirth, "Auckland, New Zealand");
            Assert.AreEqual(playerFile.Players[1].Id, 2);
            Assert.AreEqual(playerFile.Players[1].FirstName, "Aidan");
            Assert.AreEqual(playerFile.Players[1].LastName, "Ross");
            Assert.AreEqual(playerFile.Players[1].DateOfBirth.Date, new DateTime(1995, 10, 25).Date);
            Assert.AreEqual(playerFile.Players[1].Height, 189);
            Assert.AreEqual(playerFile.Players[1].Weight, 111);
            Assert.AreEqual(playerFile.Players[1].PlaceOfBirth, "Sydney, New South Wales, Australia");
            Assert.AreEqual(playerFile.Players[2].Id, 3);
            Assert.AreEqual(playerFile.Players[2].FirstName, "Samisoni");
            Assert.AreEqual(playerFile.Players[2].LastName, "Taukei'aho");
            Assert.AreEqual(playerFile.Players[2].DateOfBirth.Date, new DateTime(1997, 8, 8).Date);
            Assert.AreEqual(playerFile.Players[2].Height, 183);
            Assert.AreEqual(playerFile.Players[2].Weight, 115);
            Assert.AreEqual(playerFile.Players[2].PlaceOfBirth, "Tongatapu, Tonga");
        }

        [TestMethod]
        public void SaveWorksCorrectly()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var playerFile = new PlayerFile(mockFileIo)
            {
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
                    },
                    new Player()
                    {
                        Id = 2,
                        FirstName = "Aidan",
                        LastName = "Ross",
                        DateOfBirth = new DateTime(1995, 10, 25),
                        Height = 189,
                        Weight = 111,
                        PlaceOfBirth = "Sydney, New South Wales, Australia",
                    },
                    new Player()
                    {
                        Id = 3,
                        FirstName = "Samisoni",
                        LastName = "Taukei'aho",
                        DateOfBirth = new DateTime(1997, 8, 8),
                        Height = 183,
                        Weight = 115,
                        PlaceOfBirth = "Tongatapu, Tonga",
                    }
                }
            };

            var expected = new List<string>() {
                    "Player File Format",
                    "1.0",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga"
                };

            playerFile.Save("filename.txt");
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
                    "Player File Format",
                    "1.0",
                    "Players",
                    "3",
                    "1; Sione; Mafileo; 14/04/1993; 178; 128; Auckland, New Zealand",
                    "2; Aidan; Ross; 25/10/1995; 189; 111; Sydney, New South Wales, Australia",
                    "3; Samisoni; Taukei'aho; 8/08/1997; 183; 115; Tongatapu, Tonga"
                }
            };
            var playerFile = new PlayerFile(mockFileIo);
            playerFile.Load("filename.txt");
            Assert.AreEqual(PlayerFile.SupportedFileVersion, playerFile.Version);
            Assert.AreEqual(3, playerFile.Players.Count);
            playerFile.Clear();
            Assert.IsNull(playerFile.Version);
            Assert.IsNull(playerFile.Players);
        }
    }
}
