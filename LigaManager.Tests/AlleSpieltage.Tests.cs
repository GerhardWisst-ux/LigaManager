using Castle.Core.Resource;
using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Web.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Data;

namespace Ligamanager.Tests2025
{

    [TestFixture]
    public class Spieltage
    {
        private Mock<IDbConnectionFactory> _mockConnectionFactory;
        private Mock<IDbConnection> _mockConnection;
        private SpieltageRepository _SpieltageRepository;
        private VereineRepository _VereineRepository;

        [SetUp]
        public void SetUp()
        {
            _mockConnectionFactory = new Mock<IDbConnectionFactory>();
            _mockConnection = new Mock<IDbConnection>();
            _mockConnectionFactory.Setup(cf => cf.CreateConnection()).Returns(_mockConnection.Object);
            _SpieltageRepository = new SpieltageRepository(_mockConnectionFactory.Object);
        }

        [Test]
        public async Task GetHeimspieleBL()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();
            if (spieltageAll == null)
            {
                Assert.Fail("Spieltage ist null");
            }

            var vereineSaison = await _SpieltageRepository.GetVereineSaison();

            if (saisonen == null)
            {
                Assert.Fail("Saisonen ist null");
            }

            foreach (var item in saisonen)
            {
                List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == item.SaisonID).ToList();

                if (verList == null)
                {
                    Assert.Fail("VereineSaison ist null");
                }

                foreach (var vereinSaison in verList)
                {
                    var spieltage = spieltageAll.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 && x.Verein1_Nr == vereinSaison.VereinNr.ToString());
                    int? iCount = spieltage?.Count();

                    if (iCount > 0)
                    {
                        TestContext.Out.WriteLine("Heimspiele VereinNr:" + vereinSaison.VereinNr + " Saison:" + item.Saisonname + ", SaisonID:" + item.SaisonID + ", LigaID:" + item.LigaID + ", Anzahl Spiele:" + iCount);
                        if (item.SaisonID == 35 || item.SaisonID == 36)
                        {
                            // Assert
                            Assert.That(iCount, Is.EqualTo(15));
                        }
                        else if (item.SaisonID == 67)
                        {
                            // Assert
                            Assert.That(iCount, Is.EqualTo(19));
                        }
                        else if (item.SaisonID == 385)
                        {
                            // Assert
                            Assert.That(iCount, Is.InRange(13, 15));
                        }
                        else
                        {
                            // Assert                    
                            Assert.That(iCount, Is.EqualTo(17));
                        }
                    }
                }
            }
        }

        [Test]
        public async Task GetAuswaertsspieleBL()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();
            if (spieltageAll == null)
            {
                Assert.Fail("Spieltage ist null");
            }

            var vereineSaison = await _SpieltageRepository.GetVereineSaison();

            foreach (var item in saisonen)
            {
                List<VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == item.SaisonID).ToList();

                if (verList == null)
                {
                    Assert.Fail("VereineSaison ist null");
                }

                foreach (var vereinSaison in verList)
                {
                    var spieltage = spieltageAll.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 && x.Verein2_Nr == vereinSaison.VereinNr.ToString());
                    int? iCount = spieltage?.Count();

                    if (iCount > 0)
                    {
                        TestContext.Out.WriteLine("Auswärtspiele VereinNr:" + vereinSaison.VereinNr + " Saison:" + item.Saisonname + ", SaisonID:" + item.SaisonID + ", LigaID:" + item.LigaID + ", Anzahl Spiele:" + iCount);
                        if (item.SaisonID == 35 || item.SaisonID == 36)
                        {
                            // Assert
                            Assert.That(iCount, Is.EqualTo(15));
                        }
                        else if (item.SaisonID == 67)
                        {
                            // Assert
                            Assert.That(iCount, Is.EqualTo(19));
                        }
                        else if (item.SaisonID == 385)
                        {
                            // Assert
                            Assert.That(iCount, Is.InRange(13, 15));
                        }
                        else
                        {
                            // Assert                    
                            Assert.That(iCount, Is.EqualTo(17));
                        }
                    }
                }
            }
        }

        [Test]
        public async Task GetSaisonSpieltageBL()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var item in saisonen)
            {

                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1);
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", SaisonID:" + item.SaisonID + ", LigaID:" + item.LigaID + ", Anzahl Spiele:" + iCount);
                if (item.SaisonID == 35 || item.SaisonID == 36)
                {
                    // Assert
                    Assert.That(iCount, Is.EqualTo(240));
                }
                else if (item.SaisonID == 67)
                {
                    // Assert
                    Assert.That(iCount, Is.EqualTo(380));
                }
                else if (item.SaisonID == 385)
                {
                    // Assert
                    Assert.That(iCount, Is.EqualTo(34 * 9));
                }
                else if (item.SaisonID == 429)
                {
                    // Assert
                    Assert.That(iCount, Is.EqualTo(0));
                }                
                else
                {
                    // Assert                    
                    Assert.That(iCount, Is.EqualTo(306));
                }
            }

        }

        [Test]
        public async Task GetSaisonSpieltage2BL()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 2);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 2);
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", SaisonID:" + item.SaisonID + ", LigaID: " + item.LigaID + ", Anzahl Spiele:" + iCount);

                if (item.SaisonID == 386)
                {
                    // Assert
                    Assert.That(iCount, Is.EqualTo(34 * 9));
                }
                else if (item.SaisonID == 106)
                {
                    // Assert
                    Assert.That(iCount, Is.EqualTo(380));
                }
                else
                {
                    // Assert                    
                    Assert.That(iCount, Is.EqualTo(306));
                }
            }
        }


        [Test]
        public async Task GetStadienIDGesetztBL()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 && x.StadionID == 0);
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Anzahl Spiele mit StadionID:" + iCount);

                if (iCount > 0)
                {
                    // Assert
                    Assert.That(iCount, Is.InRange(1, 380));
                }
            }

        }

        [Test]
        public async Task GetOrtGesetztBL()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 && x.Ort != "");
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Anzahl Spiele mit Ort:" + iCount);

                if (iCount > 0)
                {
                    // Assert
                    Assert.That(iCount, Is.InRange(1, 380));
                }
            }

        }

        [Test]
        public async Task GetSchiedsrichterGesetztBL()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 && x.Schiedsrichter != "SR");
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Anzahl Spiele mit SR <> 'SR':" + iCount);

                if (iCount > 0)
                {
                    // Assert
                    Assert.That(iCount, Is.InRange(1, 380));
                }
            }

        }

        [Test]
        public async Task GetSaisonIDGesetztBL()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll?.Where(x => x.SaisonID == 0 || x.SaisonID == null);
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Anzahl Spiele mit SaisonID 0 or null:" + iCount);

                if (iCount > 0)
                {
                    // Assert
                    Assert.That(iCount, Is.InRange(0, 0));
                }
            }

        }
        [Test]
        public async Task GetZuschauerGesetztBL()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 && x.Zuschauer > 0);
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Anzahl Spiele mit Zuschauer > 0:" + iCount);

                // Assert
                Assert.That(iCount, Is.InRange(0, 100000));
            }
        }


        [Test]
        public async Task GetSpieleAbgeschlossenBL()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 && x.SpieltagAbgeschlossen == true);
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Anzahl Spiele mit KZ Abgeschlossen= true:" + iCount);

                if (iCount > 0)
                {
                    if (item.SaisonID == 35 || item.SaisonID == 36)
                    {
                        // Assert
                        Assert.That(iCount, Is.EqualTo(240));
                    }
                    else if (item.SaisonID == 67)
                    {
                        // Assert
                        Assert.That(iCount, Is.EqualTo(380));
                    }
                    else if (item.SaisonID == 385)
                    {
                        // Assert
                        Assert.That(iCount, Is.EqualTo(243));
                    }
                    else
                    {
                        // Assert                    
                        Assert.That(iCount, Is.EqualTo(306));
                    }
                }
            }

        }

        [Test]
        public async Task LigaIDNicht1oder2()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && (x.LigaID != 1 && x.LigaID != 2));
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Anzahl nicht LigaID 1 oder 2:" + iCount);

                // Assert
                Assert.That(iCount, Is.EqualTo(0));


            }

        }

        [Test]
        public async Task IsHeimNotAuswaertsLiga1()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("GetAllSpieltage liefert null zurück");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll;
                int? iCount = spieltage?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 && x.Verein1 == x.Verein2).Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", HeimVerein = Auswärtsverein:" + iCount);

                // Assert
                Assert.That(iCount, Is.EqualTo(0));
            }
        }

        [Test]
        public async Task BL_Is_AnzahlHeim_Gleich_AnzahlAuswäaerts()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("GetAllSpieltage liefert null zurück");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll;
                int? iCountH = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein1 == "Bayer Leverkusen").Count();
                int? iCountA = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein2 == "Bayer Leverkusen").Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Bayer Leverkusen , Heim Anzahl " + iCountH + ", AuswärtsAnzahl:" + iCountA);

                // Assert
                Assert.That(iCountH, Is.EqualTo(iCountA));

                spieltage = spieltageAll;
                iCountH = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein1 == "Bayern München").Count();
                iCountA = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein2 == "Bayern München").Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Bayern München , Heim Anzahl " + iCountH + ", AuswärtsAnzahl:" + iCountA);

                // Assert
                Assert.That(iCountH, Is.EqualTo(iCountA));

                spieltage = spieltageAll;
                iCountH = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein1 == "VfB Stuttgart").Count();
                iCountA = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein2 == "VfB Stuttgart").Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", VfB Stuttgart , Heim Anzahl " + iCountH + ", AuswärtsAnzahl:" + iCountA);

                // Assert
                Assert.That(iCountH, Is.EqualTo(iCountA));

                spieltage = spieltageAll;
                iCountH = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein1 == "Eintracht Frankfurt").Count();
                iCountA = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein2 == "Eintracht Frankfurt").Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Eintracht Frankfurt , Heim Anzahl " + iCountH + ", AuswärtsAnzahl:" + iCountA);

                // Assert
                Assert.That(iCountH, Is.EqualTo(iCountA));

                spieltage = spieltageAll;
                iCountH = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein1 == "TSG Hoffenheim").Count();
                iCountA = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein2 == "TSG Hoffenheim").Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", TSG Hoffenheim , Heim Anzahl " + iCountH + ", AuswärtsAnzahl:" + iCountA);

                spieltage = spieltageAll;
                iCountH = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein1 == "FC Köln").Count();
                iCountA = spieltage?.Where(x => x.SaisonID == item.SaisonID && item.Abgeschlossen && x.LigaID == 1 && x.Verein2 == "FC Köln").Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", FC Köln , Heim Anzahl " + iCountH + ", AuswärtsAnzahl:" + iCountA);

                // Assert
                Assert.That(iCountH, Is.EqualTo(iCountA));
            }
        }

        [Test]
        public async Task IsHeimNotAuswaertsLiga2()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 2);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("GetAllSpieltage liefert null zurück");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll;
                int? iCount = spieltage?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 2 && x.Verein1 == x.Verein2).Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", HeimVerein = Auswärtsverein:" + iCount);

                // Assert
                Assert.That(iCount, Is.EqualTo(0));
            }
        }


        [Test]
        public async Task IsHeimNotAuswaertsLiga3()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 3);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltageL3();

            if (spieltageAll == null)
            {
                Assert.Fail("GetAllSpieltageL3 liefert null zurück");
            }

            foreach (var item in saisonen)
            {
                var spieltage = spieltageAll;
                int? iCount = spieltage?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 3 && x.Verein1 == x.Verein2).Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", HeimVerein = Auswärtsverein:" + iCount);

                // Assert
                Assert.That(iCount, Is.EqualTo(0));
            }
        }

        [Test]
        public async Task GetSaisonSpieltageLiga3()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 3);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltageL3();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var item in saisonen)
            {

                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 3);
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", SaisonID:" + item.SaisonID + ", LigaID:" + item.LigaID + ", Anzahl Spiele:" + iCount);

                if (item.SaisonID == 396)
                {
                    // Assert
                    Assert.That(iCount, Is.EqualTo(38 * 10));
                }               
                else
                {
                    // Assert                    
                    Assert.That(iCount, Is.EqualTo(380));
                }
            }

        }

        [Test]
        public async Task GetSpieltageBundesliga_Anzahl_9()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 1);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var saison in saisonen)
            {
                var spieltage = spieltageAll?
                    .Where(x => x.SaisonID == saison.SaisonID && x.LigaID == 1)
                    .GroupBy(x => x.SpieltagNr);

                if (spieltage == null)
                    continue;

                foreach (var spieltagGroup in spieltage)
                {
                    int? spielCount = spieltagGroup.Count();
                    TestContext.Out.WriteLine(
                        $"Saison: {saison.Saisonname}, SaisonID: {saison.SaisonID}, LigaID: {saison.LigaID}, SpieltagNr: {spieltagGroup.Key}, Anzahl Spiele: {spielCount}");



                    if (saison.SaisonID == 35 || saison.SaisonID == 36)
                    {
                        // Assert
                        Assert.That(spielCount, Is.EqualTo(8));
                    }
                    else if (saison.SaisonID == 67)
                    {
                        // Assert
                        Assert.That(spielCount, Is.EqualTo(10));
                    }
                    else
                    {
                        // Assert                    
                        Assert.That(spielCount, Is.EqualTo(9));
                    }
                }
            }

        }

        [Test]
        public async Task GetSpieltage2Bundesliga_Anzahl_9()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 2);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltage();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var saison in saisonen)
            {
                var spieltage = spieltageAll?
                    .Where(x => x.SaisonID == saison.SaisonID && x.LigaID == 2)
                    .GroupBy(x => x.SpieltagNr);

                if (spieltage == null)
                    continue;

                foreach (var spieltagGroup in spieltage)
                {
                    int? spielCount = spieltagGroup.Count();
                    TestContext.Out.WriteLine($"Saison: {saison.Saisonname}, SaisonID: {saison.SaisonID}, LigaID: {saison.LigaID}, SpieltagNr: {spieltagGroup.Key}, Anzahl Spiele: {spielCount}");

                    if (saison.SaisonID == 106)
                    {
                        // Assert
                        Assert.That(spielCount, Is.EqualTo(10));
                    }
                    else
                    {
                        // Assert                    
                        Assert.That(spielCount, Is.EqualTo(9));
                    }
                }
            }

        }


        [Test]
        public async Task GetSpieltageLiga3_Anzahl_10()
        {
            // Arrange
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();

            _mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var saisonen = await _SpieltageRepository.GetSaisonen();
            saisonen = saisonen?.Where(x => x.LigaID == 3);

            var spieltageAll = await _SpieltageRepository.GetAllSpieltageL3();

            if (spieltageAll == null)
            {
                Assert.Fail("SpieltageAll ist null");
            }

            foreach (var saison in saisonen)
            {
                var spieltage = spieltageAll?
                    .Where(x => x.SaisonID == saison.SaisonID && x.LigaID == 3)
                    .GroupBy(x => x.SpieltagNr);

                if (spieltage == null)
                    continue;

                foreach (var spieltagGroup in spieltage)
                {
                    int? spielCount = spieltagGroup.Count();
                    TestContext.Out.WriteLine(
                        $"Saison: {saison.Saisonname}, SaisonID: {saison.SaisonID}, LigaID: {saison.LigaID}, SpieltagNr: {spieltagGroup.Key}, Anzahl Spiele: {spielCount}");

                    Assert.That(spielCount, Is.EqualTo(10));
                }
            }
        }


        [Test]
        public async Task BL_2BL_Should_TabelleSpeiltage_13_Felder_Gefüllt()
        {
            // Tabelle Spieltage abrufen
            var spieltageAll = (await _SpieltageRepository.GetAllSpieltage())?.
            Where((s => string.IsNullOrWhiteSpace(s.Datum.ToString())
            || (string.IsNullOrWhiteSpace(s.SaisonID.ToString()))
            || (string.IsNullOrWhiteSpace(s.LigaID.ToString()))
            || (string.IsNullOrWhiteSpace(s.Saison.ToString()))
            || (string.IsNullOrWhiteSpace(s.Verein1_Nr.ToString()))
            || (string.IsNullOrWhiteSpace(s.Verein2_Nr.ToString()))
            || (string.IsNullOrWhiteSpace(s.Verein1)) 
            || (string.IsNullOrWhiteSpace(s.Verein2))
            || (string.IsNullOrWhiteSpace(s.Tore1_Nr.ToString()))
            || (string.IsNullOrWhiteSpace(s.Tore2_Nr.ToString()))
            || (string.IsNullOrWhiteSpace(s.Ort.ToString()))
            || (string.IsNullOrWhiteSpace(s.Ort.ToString()))
            || (string.IsNullOrWhiteSpace(s.SpieltagNr.ToString()))
            )).ToList();
                        
            // Überprüfen, ob keine Einträge fehlen
            Assert.That(spieltageAll?.Count(), Is.EqualTo(0), "Es gibt Spieltage mit unvollständigen Feldern!");
        }




        [Test]
        public async Task BL3_Should_TabelleSpeiltage_13_Felder_Gefüllt()
        {
            // Tabelle Spieltage abrufen
            var spieltageAll = (await _SpieltageRepository.GetAllSpieltage())?.
            Where((s => string.IsNullOrWhiteSpace(s.Datum.ToString())
            || (string.IsNullOrWhiteSpace(s.LigaID.ToString()))
            || (string.IsNullOrWhiteSpace(s.SaisonID.ToString()))
            || (string.IsNullOrWhiteSpace(s.Saison.ToString()))
            || (string.IsNullOrWhiteSpace(s.Verein1))
            || (string.IsNullOrWhiteSpace(s.Verein1_Nr.ToString()))
            || (string.IsNullOrWhiteSpace(s.Verein2_Nr.ToString()))
            || (string.IsNullOrWhiteSpace(s.Verein2))
            || (string.IsNullOrWhiteSpace(s.Tore1_Nr.ToString()))
            || (string.IsNullOrWhiteSpace(s.Tore2_Nr.ToString()))
            || (string.IsNullOrWhiteSpace(s.Ort.ToString()))
            || (string.IsNullOrWhiteSpace(s.Ort.ToString()))
            || (string.IsNullOrWhiteSpace(s.SpieltagNr.ToString()))
            )).ToList();

            // Überprüfen, ob keine Einträge fehlen
            Assert.That(spieltageAll?.Count(), Is.EqualTo(0), "Es gibt Spieltage mit unvollständigen Feldern!");
        }

        [Test]
        public async Task BL_2BL_3L_Should_LIGAID_SAISONID_GROESSER_0()
        {
            // Tabelle Spieltage abrufen
            var spieltageAll = (await _SpieltageRepository.GetVereineSaison())?.
            Where(s => s.VereinNr.ToString() == "0"  || s.LigaID.ToString() == "0" || s.SaisonID.ToString() == "0") .ToList();
            
            // Überprüfen, ob Einträge ungütltig sind
            Assert.That(spieltageAll?.Count(), Is.EqualTo(0), "Es gibt in der Tabelle VereineSaison Datensätze mit ungültigen Werten.");
        }

    }
}