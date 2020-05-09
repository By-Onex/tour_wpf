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
    }
}
