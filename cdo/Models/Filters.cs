using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cdo.Models

{
    public class Filters
    {
        public int Nom { get; set; }
        public string F { get; set; }
        public string I { get; set; }
        public string O { get; set; }
        public int MO { get; set; }
        public DateTime DatNachDR { get; set; }
        public DateTime DatKoncDR { get; set; }
        public string Add_proj { get; set; }
        public string Tel { get; set; }
        public string Fio_rod { get; set; }
        public string Diagn { get; set; }
        public DateTime DatNachSD { get; set; }
        public DateTime DatKoncSD { get; set; }
        public string prikaz { get; set; }
        public DateTime DatNachP { get; set; }
        public DateTime DatKoncP { get; set; }
        public string tip_kompl { get; set; }
        public int Klass { get; set; }
        public string BS { get; set; }
        public string MS { get; set; }
        public string Fio_ped { get; set; }
        public string Status { get; set; }
    }

}