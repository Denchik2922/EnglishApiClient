﻿using EnglishApiClient.Infrastructure;
using EnglishApiClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class Dictionary : IDisposable
    {
        private ICollection<EnglishDictionary> englishDictionaries;

        [Inject]
        public IDictionaryHttpService DictionaryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            englishDictionaries = await DictionaryService.GetPublicDictionaries();
        }
        public void NavigateToDictionary(int id)
        {
            NavigationManager.NavigateTo($"dictionary/{id}");
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
