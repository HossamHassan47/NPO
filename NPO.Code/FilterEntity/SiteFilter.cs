using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPO.Code.FilterEntity
{
    public class SiteFilter
    {
        public string SiteCode { get; set; }

        public string SiteName { get; set; }

        public int CityId { get; set; }

        public bool _2g { get; set; }

        public bool _3g { get; set; }

        public bool _4g { get; set; }

    }
}
