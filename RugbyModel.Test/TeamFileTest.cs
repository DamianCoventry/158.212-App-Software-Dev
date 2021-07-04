using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace RugbyModel.Test
{
    [TestClass]
    public class TeamFileTest
    {
        [TestMethod]
        public void ConstructorDoesntThrow()
        {
            MockFileIo mockFileIo = new MockFileIo();
            _ = new TeamFile(mockFileIo);
        }

        [TestMethod]
        public void ConstructorDetectsNullAndEmptyString()
        {
            MockFileIo mockFileIo = new MockFileIo();
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new TeamFile(null, mockFileIo);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new TeamFile("", mockFileIo);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new TeamFile(string.Empty, mockFileIo);
            });
        }

        [TestMethod]
        public void LoadDetectsNullAndEmptyString()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var TeamFile = new TeamFile(mockFileIo);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                TeamFile.Load(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                TeamFile.Load("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                TeamFile.Load(string.Empty);
            });
        }

        [TestMethod]
        public void SaveDetectsNullAndEmptyString()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var TeamFile = new TeamFile(mockFileIo);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                TeamFile.Save(null);
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                TeamFile.Save("");
            });
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                TeamFile.Save(string.Empty);
            });
        }

        [TestMethod]
        public void LoadDetectsAbsentFile()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var TeamFile = new TeamFile(mockFileIo);
            Assert.ThrowsException<Exception>(() =>
            {
                TeamFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsEmptyFile()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>()
            };
            var TeamFile = new TeamFile(mockFileIo);
            Assert.ThrowsException<Exception>(() =>
            {
                TeamFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile1()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() { "asdfadsfadsf", "rftujrftjhgfj", "zxcvzxcvzxcv", "rtyu567u56rtuh" }
            };
            var TeamFile = new TeamFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                TeamFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsUnsupportedVersion()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Team File Format",
                    "1.1",
                    "Teams",
                    "3",
                    "Cats; AAMI Park; David Wessels; Founded 2010, Melbourne, Australia",
                    "Pinks; Suncorp Stadium; Brad Thorn; Founded 1882, Brisbane, Queensland, Australia",
                    "Baratahs; Sydney Football Stadium; Rob Penney; Founded 1882, Sydney, New South Wales, Australia"
                }
            };
            var TeamFile = new TeamFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                TeamFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile2()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Team File Format",
                    "1.0",
                    "Helicopter",
                    "3",
                    "Cats; AAMI Park; David Wessels; Founded 2010, Melbourne, Australia",
                    "Pinks; Suncorp Stadium; Brad Thorn; Founded 1882, Brisbane, Queensland, Australia",
                    "Baratahs; Sydney Football Stadium; Rob Penney; Founded 1882, Sydney, New South Wales, Australia"
                }
            };
            var TeamFile = new TeamFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                TeamFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile3()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Team File Format",
                    "1.0",
                    "Teams",
                    "3456",
                    "Cats; AAMI Park; David Wessels; Founded 2010, Melbourne, Australia",
                    "Pinks; Suncorp Stadium; Brad Thorn; Founded 1882, Brisbane, Queensland, Australia",
                    "Baratahs; Sydney Football Stadium; Rob Penney; Founded 1882, Sydney, New South Wales, Australia"
                }
            };
            var TeamFile = new TeamFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                TeamFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsInvalidFile4()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Team File Format",
                    "1.0",
                    "Teams",
                    "65",
                    "k;asdkfj;adksfj;aksdfj;aksjf;asd;fa;sf;jaksfj;aksdfjljasdhflasdk;fkajs;",
                    "dfk;jaskdfhjlajushflasdf;aksdjf;kadjs;fkaj;lsfkj;akdjf;aksdfk;asd;faajsd;lfkhlajhdfaf;kjadsflhj"
                }
            };
            var TeamFile = new TeamFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                TeamFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsMismatchingNumberOfTeams1()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Team File Format",
                    "1.0",
                    "Teams",
                    "1000000",
                    "Cats; AAMI Park; David Wessels; Founded 2010, Melbourne, Australia",
                    "Pinks; Suncorp Stadium; Brad Thorn; Founded 1882, Brisbane, Queensland, Australia",
                    "Baratahs; Sydney Football Stadium; Rob Penney; Founded 1882, Sydney, New South Wales, Australia"
                }
            };
            var TeamFile = new TeamFile(mockFileIo);
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                TeamFile.Load("filename.txt");
            });
        }

        [TestMethod]
        public void LoadDetectsMismatchingNumberOfTeams2()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Team File Format",
                    "1.0",
                    "Teams",
                    "2",
                    "Cats; AAMI Park; David Wessels; Founded 2010, Melbourne, Australia",
                    "Pinks; Suncorp Stadium; Brad Thorn; Founded 1882, Brisbane, Queensland, Australia",
                    "Baratahs; Sydney Football Stadium; Rob Penney; Founded 1882, Sydney, New South Wales, Australia"
                }
            };
            var TeamFile = new TeamFile(mockFileIo);
            TeamFile.Load("filename.txt");
            Assert.AreEqual(2, TeamFile.Teams.Count);
        }

        [TestMethod]
        public void LoadHandlesZeroTeams()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Team File Format",
                    "1.0",
                    "Teams",
                    "0"
                }
            };
            var TeamFile = new TeamFile(mockFileIo);
            TeamFile.Load("filename.txt");
            Assert.AreEqual(0, TeamFile.Teams.Count);
        }

        [TestMethod]
        public void LoadWorksCorrectly()
        {
            MockFileIo mockFileIo = new MockFileIo
            {
                _readFileLines = new List<string>() {
                    "Team File Format",
                    "1.0",
                    "Teams",
                    "3",
                    "Cats; AAMI Park; David Wessels; Founded 2010, Melbourne, Australia",
                    "Pinks; Suncorp Stadium; Brad Thorn; Founded 1982, Brisbane, Queensland, Australia",
                    "Baratahs; Sydney Football Stadium; Rob Penney; Founded 1982, Sydney, New South Wales, Australia"
                }
            };
            var TeamFile = new TeamFile(mockFileIo);
            TeamFile.Load("filename.txt");
            Assert.AreEqual(TeamFile.SupportedFileVersion, TeamFile.Version);
            Assert.AreEqual(3, TeamFile.Teams.Count);
            Assert.AreEqual("Cats", TeamFile.Teams[0].Name);
            Assert.AreEqual("AAMI Park", TeamFile.Teams[0].HomeGround);
            Assert.AreEqual("David Wessels", TeamFile.Teams[0].Coach);
            Assert.AreEqual(2010, TeamFile.Teams[0].YearFounded);
            Assert.AreEqual("Melbourne, Australia", TeamFile.Teams[0].Region);
            Assert.AreEqual("Pinks", TeamFile.Teams[1].Name);
            Assert.AreEqual("Suncorp Stadium", TeamFile.Teams[1].HomeGround);
            Assert.AreEqual("Brad Thorn", TeamFile.Teams[1].Coach);
            Assert.AreEqual(1982, TeamFile.Teams[1].YearFounded);
            Assert.AreEqual("Brisbane, Queensland, Australia", TeamFile.Teams[1].Region);
            Assert.AreEqual("Baratahs", TeamFile.Teams[2].Name);
            Assert.AreEqual("Sydney Football Stadium", TeamFile.Teams[2].HomeGround);
            Assert.AreEqual("Rob Penney", TeamFile.Teams[2].Coach);
            Assert.AreEqual(1982, TeamFile.Teams[2].YearFounded);
            Assert.AreEqual("Sydney, New South Wales, Australia", TeamFile.Teams[2].Region);
        }

        [TestMethod]
        public void SaveWorksCorrectly()
        {
            MockFileIo mockFileIo = new MockFileIo();
            var TeamFile = new TeamFile(mockFileIo)
            {
                Teams = new List<Team>()
                {
                    new Team()
                    {
                        Name = "Cats",
                        HomeGround = "AAMI Park",
                        Coach = "David Wessels",
                        YearFounded = 2010,
                        Region = "Melbourne, Australia",
                    },
                    new Team()
                    {
                        Name = "Pinks",
                        HomeGround = "Suncorp Stadium",
                        Coach = "Brad Thorn",
                        YearFounded = 1882,
                        Region = "Brisbane, Queensland, Australia",
                    },
                    new Team()
                    {
                        Name = "Baratahs",
                        HomeGround = "Sydney Football Stadium",
                        Coach = "Rob Penney",
                        YearFounded = 1882,
                        Region = "Sydney, New South Wales, Australia",
                    }
                }
            };

            var expected = new List<string>() {
                    "Team File Format",
                    "1.0",
                    "Teams",
                    "3",
                    "Cats; AAMI Park; David Wessels; Founded 2010, Melbourne, Australia",
                    "Pinks; Suncorp Stadium; Brad Thorn; Founded 1882, Brisbane, Queensland, Australia",
                    "Baratahs; Sydney Football Stadium; Rob Penney; Founded 1882, Sydney, New South Wales, Australia"
                };

            TeamFile.Save("filename.txt");
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
                    "Team File Format",
                    "1.0",
                    "Teams",
                    "3",
                    "Cats; AAMI Park; David Wessels; Founded 2010, Melbourne, Australia",
                    "Pinks; Suncorp Stadium; Brad Thorn; Founded 1882, Brisbane, Queensland, Australia",
                    "Baratahs; Sydney Football Stadium; Rob Penney; Founded 1882, Sydney, New South Wales, Australia"
                }
            };
            var TeamFile = new TeamFile(mockFileIo);
            TeamFile.Load("filename.txt");
            Assert.AreEqual(TeamFile.SupportedFileVersion, TeamFile.Version);
            Assert.AreEqual(3, TeamFile.Teams.Count);
            TeamFile.Clear();
            Assert.IsNull(TeamFile.Version);
            Assert.IsNull(TeamFile.Teams);
        }
    }
}
