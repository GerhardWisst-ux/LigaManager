﻿
using LigaManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LigaManagement.Models;
using System.Diagnostics;

namespace ToreManagerManagement.Web.Services
{
    public class PokalergebnisseService : IPokalergebnisseService
    {
        private readonly HttpClient httpClient;

        public PokalergebnisseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PokalergebnisSpieltag> CreatePokalergebnisSpieltag(PokalergebnisSpieltag pokalergebnisSpieltag)
        {
            try
            {
                return await httpClient.PostJsonAsync<PokalergebnisSpieltag>("api/pokalergebnisse", pokalergebnisSpieltag);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public Task DeletePokalergebnis(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteTor(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PokalergebnisSpieltag>> GetPokalergebnisseSpieltag()
        {
            try
            {
                return await httpClient.GetJsonAsync<PokalergebnisSpieltag[]>("api/pokalergebnisse");
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return null;
            }
        }

        public async Task<PokalergebnisSpieltag> GetPokalergebnisSpieltag(int id)
        {
            return await httpClient.GetJsonAsync<PokalergebnisSpieltag>($"api/pokalergebnisse/{id}");
        }

        public async Task<IEnumerable<PokalergebnisSpieltag>> GetTore()
        {
            return await httpClient.GetJsonAsync<PokalergebnisSpieltag[]>("api/pokalergebnisse");
        }

        public async Task<PokalergebnisSpieltag> UpdatePokalergebnisSpieltag(PokalergebnisSpieltag pokalergebnisSpieltag)
        {
            return await httpClient.PutJsonAsync<PokalergebnisSpieltag>("api/pokalergebnisse", pokalergebnisSpieltag);
        }
       
    }
}