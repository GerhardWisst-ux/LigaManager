using LigaManagement.Models;
using LigamanagerManagement.Web.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IVereineSaisonAusService
    {
        //Task<IEnumerable<VereineSaison>> GetVereine();
        //Task<Verein> GetVerein(int id);        
        //Task<Verein> UpdateVerein(Verein updatedVerein);
        //Task<Verein> CreateVerein(Verein newVerein);
        //Task DeleteVerein(int id);
        Task<IEnumerable<VereineSaisonAus>> GetVereineSaisonAus();

        Task<List<VereineSaisonAus>> CreateVereineSaisonAus(List<VereineSaisonAus> vereine);
    }
}
