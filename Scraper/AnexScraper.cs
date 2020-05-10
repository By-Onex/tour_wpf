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
    public class AnexScraper : BaseScraper
    {
        public override Task<List<FoundTour>> GetAutoSearchTours(List<SearchTourItem> searchParams)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<TourItem>> GetTours(SearchTourItem searchParams)
        {
            try
            {
                var t = searchParams.DateTo.Split('.');
                string dateTo = t[2] + t[1] + t[0];

                t = searchParams.DateFrom.Split('.');
                string dateFrom = t[2] + t[1] + t[0];

                string urlParams = string.Format("ADULT={0}&CHILD={1}&CHECKIN_BEG={2}&CHECKIN_END={3}&NIGHTMAX={4}&NIGHTMIN={5}&STATE={6}&CURRENCY=1&PARTITION_PRICE=40&COSTMAX={9}&COSTMIN={8}&FILTER=6&PRICE_PAGE=1&RECONPAGE=400&UFILTER=0&REGULAR=True&CHARTER=True&SORT_TYPE=0&REGIONTO=&TOWNFROM={7}",
                   searchParams.AdultCount, searchParams.ChildCount, dateFrom, dateTo, searchParams.NightEnd, searchParams.NightStart, searchParams.ToCityId, searchParams.FromCityId, searchParams.MinCost, searchParams.MaxCost);

                string url = "https://webapisearch.anextour.com/b2c/Search?" + urlParams;

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36");

                string html = await httpClient.GetStringAsync(url);

                var result = new List<TourItem>();

                dynamic items = JsonConvert.DeserializeObject(html);

                foreach (var it in items[0].prices)
                {
                    Console.WriteLine(it.HotelInc);
                    var item = new TourItem() { Cost = it.converted_price,
                        PageUrl = "https://www.anextour.com" + it.Slug + "?" + urlParams,
                        HotelId = (int)it.HotelInc,
                        MealDescription = it.MealNote,
                        Date = it.hotelCheckIn,
                        TownName = it.TownName
                    };
                    result.Add(item);
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<TourItem>();
            }
        }

        public async Task<string> ImgUrl(int hotelId)
        {
            try
            {
                var url = string.Format("https://files.anextour.com/hotel/hotelimagelist?code={0}&take=1&width=400", hotelId);
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36");

                string html = await httpClient.GetStringAsync(url);
                dynamic items = JsonConvert.DeserializeObject(html);

                return (string)items[0].Img;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }
    }
}
