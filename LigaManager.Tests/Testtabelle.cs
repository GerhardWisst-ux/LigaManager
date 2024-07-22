using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using NUnit.Framework;
using Ligamanager.Components;

namespace Ligamanager.Tests
{
    [TestFixture]
    public class LigamanagerUnitTests
    {
        [Inject]
        public LigaManagement.Web.Services.Contracts.ITabelleService TabelleService { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IVereineSaisonService VereineSaisonService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Test]
        public void Test_AddMethod()
        {
            BasicMaths bm = new BasicMaths();
            double res = bm.Add(10, 10);
            Assert.Equals(res, 20);
        }
        [Test]
        public void Test_MultiplyMethod()
        {
            BasicMaths bm = new BasicMaths();
            double res = bm.Multiply(10, 10);
            Assert.Equals(res, 100);
        }
        [Test]
        public void Test_SubstractMethod()
        {
            BasicMaths bm = new BasicMaths();
            double res = bm.Substract(10, 10);
            Assert.Equals(res, 0);
        }
        [Test]
        public void Test_DivideMethod()
        {
            BasicMaths bm = new BasicMaths();
            double res = bm.Divide(10, 5);
            Assert.Equals(res, 2);
        }

        [Test]
        public async void CheckNumberofGamesPerSaison()
        {
            var vereineSaison = await VereineSaisonService.GetVereineSaison();
            var Vereine = await VereineService.GetVereine();

            for (int i = 0; i < 385; i++)
            {
                List<LigaManagement.Models.VereineSaison> verList = vereineSaison.Where(x => x.SaisonID == 1 && x.LigaID == 1).ToList();

                if (verList.Count() == 0)
                    continue;

                var Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, true, verList, Vereine, 34, 1);

                Assert.Equals(Tabellen.Count(), 34);
            }
           
        }

        public class BasicMaths
        {
            public double Add(double num1, double num2)
            {
                return num1 + num2;
            }
            public double Multiply(double num1, double num2)
            {
                return num1 * num2;
            }
            public double Substract(double num1, double num2)
            {
                return num1 - num2;
            }
            public double Divide(double num1, double num2)
            {
                return num1 / num2;
            }
        }
    }    
}