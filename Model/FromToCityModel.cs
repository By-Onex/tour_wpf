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
        public async Task<ObservableCollection<CityItem>> GetCityFrom()
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
        }
        
    }
}
