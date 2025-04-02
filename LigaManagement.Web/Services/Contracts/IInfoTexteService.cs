
using LigaManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IInfoTexteService
    {
        Task<IEnumerable<InfoText>> GetTexte();
        Task<InfoText> GetText(int id);        
        Task<InfoText> UpdateText(InfoText updateText);
        Task<InfoText> CreateText(InfoText newText);
        Task DeleteText(int id);
        
    }
}
