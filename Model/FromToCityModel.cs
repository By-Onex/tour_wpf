using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourApp.ViewModel;

namespace TourApp.Model
{
    public class FromToCityModel
    {
        /*public async Task<ObservableCollection<CityItem>> GetCityFrom()
        {
            var url = "https://search.bankturov.ru/directions/?country=1";
            var httpClient = new HttpClient();
            string html = await httpClient.GetStringAsync(url);

            dynamic citys = JsonConvert.DeserializeObject(html);

            var result = new ObservableCollection<CityItem>();
            foreach (var el in citys.data)
            {
                var toCity = new ObservableCollection<CityItem>();
                foreach(var elTo in el.destinations)
                {
                    toCity.Add(new CityItem() { Id = elTo.id, Value = elTo.value });
                }
                result.Add( new CityItem() {Id = el.id, Value = el.value, ToCity=toCity });
            }
            return result;
        }*/
       
        public async Task<ObservableCollection<CityItem>> GetCityFrom()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36");

            var urlCitys = "https://webapi.anextour.com/B2C/Cities";
            var urlCountries = "https://webapi.anextour.com/B2C/CountriesSimple";

            string htmlCisty = await httpClient.GetStringAsync(urlCitys);
            dynamic citys = JsonConvert.DeserializeObject(htmlCisty);

            string htmlCountries = await httpClient.GetStringAsync(urlCountries);
            dynamic countries = JsonConvert.DeserializeObject(htmlCountries);

            var result = new ObservableCollection<CityItem>();
            foreach (var el in citys)
            {
                 var toCity = new ObservableCollection<CityItem>();
                
                 result.Add(new CityItem() { Id = el.properties.main.samoId.value, Value = el.title, ToCity = toCity });
            }

            foreach(var el in countries)
            {
                var item = new CityItem() { Id = el.samoId, Value = el.title };
                
                foreach (int city in el.townFrom)
                {
                    result.FirstOrDefault(c => c.Id == city)?.ToCity.Add(item);
                }
            }
            return result;
        }

    }
}
