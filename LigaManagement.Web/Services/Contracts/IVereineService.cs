﻿using LigaManagement.Models;
using LigamanagerManagement.Web.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Web.Services.Contracts
{
    public interface IVereineService
    {
        Task<IEnumerable<Verein>> GetVereine();

        Task<IEnumerable<VereinAktSaison>> GetVereineCL();
        Task<IEnumerable<VereinAktSaison>> GetVereineL3();
        Task<IEnumerable<VereinAktSaison>> GetVereineEMWM();
        Task<Verein> GetVerein(int id);
        Task<VereinAktSaison> GetVereinCL(int id);
        Task<VereinAktSaison> GetVereinEMWM(int id);
        Task<VereinAktSaison> GetVereinL3(int id);
        Task<Verein> UpdateVerein(Verein updatedVerein);
        Task<Verein> CreateVerein(Verein newVerein);
        Task DeleteVerein(int id);
        Task<IEnumerable<VereinAktSaison>> GetVereineSaison();
        Task<List<VereineSaison>> CreateVereineSaison(List<VereineSaison> vereine);
        
    }
}
