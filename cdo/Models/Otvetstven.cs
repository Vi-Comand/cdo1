using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attest.Models
{

    public class ListOtv:PageModel
    {
        public List<Otvetstven> ListOtvetstven { get; set; }

    }

    public class Otvetstven
    {
        public int Id { get; set; }
        public string name { get; set; }
        public DateTime data_podachi { get; set; }
        public string oo { get; set; }
        public string dolgnost { get; set; }
        public string kategor { get; set; }
        public string file { get; set; }
        public string status { get; set; }
        public string ball { get; set; }
    }
}
