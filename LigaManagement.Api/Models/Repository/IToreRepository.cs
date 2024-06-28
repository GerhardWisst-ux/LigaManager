using System.Collections.Generic;
using System.Threading.Tasks;
using LigaManagement.Models;

namespace ToremanagerManagement.Api.Models.Repository
{
    public interface IEinstellungenRepository
    {
        Task<EinstellungenLM> GetEinstellungen();
        
        Task<EinstellungenLM> UpdateEinstellungen(EinstellungenLM einstellungen);
        
    }
}
