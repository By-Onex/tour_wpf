﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourApp.DB;
using TourApp.Scraper;
using TourApp.ViewModel;

namespace TourApp.Model
{
    public class SearchModel
    {
        public async Task<ObservableCollection<ResortItem>> GetResorts(int from, int to)
        {
            var url = string.Format("https://search.bankturov.ru/api/mobile/v1/formData?departure={0}&destination={1}", from, to);
            var httpClient = new HttpClient();
            string html = await httpClient.GetStringAsync(url);

            dynamic resorts = JsonConvert.DeserializeObject(html);
            var result = new ObservableCollection<ResortItem>();
            foreach (var res in resorts.data.resorts)
            {
                result.Add(new ResortItem() { Id = res.id, Value = res.value });
            }
            return result;
        }

        public async void GetTours(SearchTourItem searchParams)
        {
            var tt = new BankScraper();
            var result = await tt.GetTours(searchParams);

            ResultViewModel.Instance.Items = result;

            ResultViewModel.Instance.ShowAnimation = Visibility.Hidden;
            ResultViewModel.Instance.ShowResult = Visibility.Visible;

            if (result.Count == 0)
            {
                ResultViewModel.Instance.ShowStatus = Visibility.Visible;
                ResultViewModel.Instance.ShowResult = Visibility.Hidden;
            }
        }
    }
}
