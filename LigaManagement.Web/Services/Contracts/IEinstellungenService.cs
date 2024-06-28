using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IEinstellungenService
    {
        Task<EinstellungenLM> GetEinstellungen();
        Task<EinstellungenLM> UpdateEinstellungen(EinstellungenLM updatedEinstellungen);
    }
}


