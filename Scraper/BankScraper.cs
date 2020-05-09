using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourApp.Base;
using TourApp.DB;

namespace TourApp.Scraper
{
    public class BankScraper : BaseScraper
    {
        public override Task<List<FoundTour>> GetAutoSearchTours(List<SearchTourItem> argumentModel)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<TourItem>> GetTours(SearchTourItem searchParams)
        {

            int time = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            string url = string.Format("https://search.bankturov.ru/api/mobile/v1/search?departure_id={0}&destination_id={1}&adult_num={2}&child_num={3}&arrivalDateFrom={4}&arrivalDateTo={5}&nonstop=true&ticket=true&nightsStart={6}&nightsEnd={7}&hotelStars[]=1&hotelStars[]=2&hotelStars[]=3&hotelStars[]=4&hotelStars[]=5&mealType[]=1&mealType[]=2&mealType[]=3&mealType[]=4&mealType[]=5&mealType[]=6&resort_id[]={11}&minCost={8}&maxCost={9}&valute=RUB&offer_currency=RUB&source=search_online_page&_={10}",
                searchParams.FromCityId, searchParams.ToCityId, searchParams.AdultCount, searchParams.ChildCount, searchParams.DateFrom, searchParams.DateTo, searchParams.NightStart, searchParams.NightEnd, searchParams.MinCost, searchParams.MaxCost, time, searchParams.ResortId);

            var httpClient = new HttpClient();
            string html = await httpClient.GetStringAsync(url);

            dynamic result = JsonConvert.DeserializeObject(html);
            Console.WriteLine(result);
            var tours = new List<TourItem>();
            if (result.success == false) return tours;

            foreach(var el in result.data.rows)
            {
                var tour = new TourItem() {
                    Cost = el.costValues.RUB.source,
                    MealDescription = el.meal_description,
                    Resort = el.curort,
                    Date = el.date,
                    PageUrl = String.Format("https://sletat.ru/tour/{0}-{1}-{2}", el.to, el.offer_id, el.search_id),
                };
                tours.Add(tour);
            }
            return tours;
        }
    }
}
