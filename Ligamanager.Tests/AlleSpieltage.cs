using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigaManagerManagement.Web.Services;
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
                    Assert.That(iCount, Is.EqualTo(28 * 9));
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
                    Assert.That(iCount, Is.EqualTo(27 * 9));
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
                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 &&  x.StadionID == 0);
                int? iCount = spieltage?.Count();

                TestContext.Out.WriteLine("Saison:" + item.Saisonname + ", LigaID: " + item.LigaID + ", Anzahl Spiele mit StadionID:" + iCount);

                if (iCount > 0)
                {
                    // Assert
                    Assert.That(iCount, Is.InRange(1,380));
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
                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 && x.Schiedrichter != "SR");
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

                if (iCount > 0)
                {
                    // Assert
                    Assert.That(iCount, Is.InRange(1, 100000));
                }
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
                var spieltage = spieltageAll?.Where(x => x.SaisonID == item.SaisonID && x.LigaID == 1 && x.Abgeschlossen == true);
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

                if (iCount == 0)
                {
                    // Assert
                    Assert.That(iCount, Is.EqualTo(0));

                }
            }

        }
    }

}