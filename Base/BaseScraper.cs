using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourApp.DB;

namespace TourApp.Base
{
    public abstract class BaseScraper
    {
        public abstract Task<List<TourItem>> GetTours(SearchTourItem argumentModel);
        public abstract Task<List<FoundTour>> GetAutoSearchTours(List<SearchTourItem> argumentModel);
    }
}
