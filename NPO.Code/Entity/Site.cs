using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPO.Code.Entity
{
    public class Site
    {
        public int SiteId { get; set; }

        public string SiteCode { get; set; }

        public string SiteName { get; set; }

        public int RegionId { get; set; }

        public int CityId { get; set; }

        public int ZoneId { get; set; }

        public int SiteType { get; set; }

        public bool _2g { get; set; }

        public bool _3g { get; set; }

        public bool _4g { get; set; }

        public int ControlerId2g { get; set; }

        public int ControlerId3g { get; set; }

        public int ControlerId4g { get; set; }

        public bool IsDeleted { get; set; }

    }
}
