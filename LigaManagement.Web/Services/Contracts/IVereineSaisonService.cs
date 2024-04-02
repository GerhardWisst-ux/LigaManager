using LigaManagement.Models;
using LigamanagerManagement.Web.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IVereineSaisonService
    {
        //Task<IEnumerable<VereineSaison>> GetVereine();
        //Task<Verein> GetVerein(int id);        
        //Task<Verein> UpdateVerein(Verein updatedVerein);
        //Task<Verein> CreateVerein(Verein newVerein);
        //Task DeleteVerein(int id);
        Task<IEnumerable<VereineSaison>> GetVereineSaison();

        Task<List<VereineSaison>> CreateVereineSaison(List<VereineSaison> vereine);
    }
}
