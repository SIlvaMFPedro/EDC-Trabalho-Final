using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDC_Trabalho_Final.Controllers
{   
    public class PresHref
    {
        public string href { get; set; }
    }

    public class LinksPresident
    {
        public Self _self { get; set;  }
        public PresHref preslist { get; set; }
    }

    public class President
    {
        public string name { get; set; }
        public string political_party { get; set; }
        public int age { get;  set;}
        public string dateOfBirth { get; set; }
        public string nationality { get; set; }
        public string startOfPresidency { get; set; }
        public string endOfPresidency { get; set; }
        public int president_number { get; set; }

    }
    public class PresidentList
    {
        public LinksPresident _links { get; set; }
        public int count { get; set; }
        public List<President> presidents { get; set;}
    }
}