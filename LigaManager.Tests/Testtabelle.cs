using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Web.Services;

namespace LigaManager.Tests
{
    public class Testtabelle
    {
        public ITabelleService TabelleService { get; set; }
        public ISpieltagService SpieltagService { get; set; }

        public IVereineService VereineService { get; set; }

        Vereine = VereineService.GetVereine();
        //Arrange

        //Act
        Tabellen = TabelleService.BerechneTabelle(SpieltagService, True, Vereine, 34, Ligamanager.Components.Globals.currentSaison, 1);
        //Assert
    }
}
