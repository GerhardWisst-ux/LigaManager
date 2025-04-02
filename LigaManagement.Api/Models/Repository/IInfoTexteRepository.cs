using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigamanagerManagement.Api.Models.Repository
{
    public interface IInfoTexteRepository
    {
        Task<IEnumerable<InfoText>> GetInfoTexte();
        Task<InfoText> GetInfoText(int InfoTextIdId);
        Task<InfoText> AddInfoText(InfoText InfoTextId);
        Task<InfoText> UpdateInfoText(InfoText InfoTextId);
        Task<InfoText> DeleteInfoText(int InfoTextIdId);
    }
}
