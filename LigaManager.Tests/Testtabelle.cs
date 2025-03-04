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
        public required LigaManagement.Web.Services.Contracts.ITabelleService TabelleService { get; set; }

        [Inject]
        public required ISpieltagService SpieltagService { get; set; }

        [Inject]
        public required IVereineSaisonService VereineSaisonService { get; set; }

        [Inject]
        public required IVereineService VereineService { get; set; }

        [Test]
        public void Test_AddMethod()
        {
            double res = BasicMaths.Add(10, 10);
            Assert.Equals(res, 20);
        }
        [Test]
        public void Test_MultiplyMethod()
        {            
            double res = BasicMaths.Multiply(10, 10);
            Assert.Equals(res, 100);
        }
        [Test]
        public void Test_SubstractMethod()
        {
            double res = BasicMaths.Substract(10, 10);
            Assert.Equals(res, 0);
        }
        [Test]
        public void Test_DivideMethod()
        {            
            double res = BasicMaths.Divide(10, 5);
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

                if (verList.Count == 0)
                    continue;

                var Tabellen = await TabelleService.BerechneTabelleDE(SpieltagService, true, verList, Vereine, 34, 1);

                Assert.Equals(Tabellen.Count(), 34);
            }
           
        }

        public class BasicMaths
        {
            public static double Add(double num1, double num2)
            {
                return num1 + num2;
            }
            public static double Multiply(double num1, double num2)
            {
                return num1 * num2;
            }
            public static double Substract(double num1, double num2)
            {
                return num1 - num2;
            }
            public static double Divide(double num1, double num2)
            {
                return num1 / num2;
            }
        }
    }    
}