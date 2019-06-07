using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cdo.Models
{
    public class ListLK : PageModel
    {
        public List<LK> Listlk { get; set; }

    }
    public class LK
    {
        public int id { get; set; }
        public string MO { get; set; }
        public string fam { get; set; }
        public string ima { get; set; }
        public string otch { get; set; }
        public DateTime data_roj { get; set; }
        public string address_proj { get; set; }
        public string Fio_rod_zp { get; set; }
        public string inventr { get; set; }
        public string prikaz { get; set; }
        public string status { get; set; }

    }
}
