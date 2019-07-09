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
        public string Fio_rod_zp { get; set; }
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
    public class FilterSklad
    {
        public int Nom { get; set; }
        public string Nazv_kompl { get; set; }
        public int nov_inv { get; set; }
        public int star_inv { get; set; }

        public DateTime DatNachU { get; set; }
        public DateTime DatKoncU { get; set; }

        public DateTime DatNachV { get; set; }
        public DateTime DatKoncV { get; set; }

        public DateTime DatNachVV { get; set; }
        public DateTime DatKoncVV { get; set; }
        public bool prim { get; set; }
        public bool pret { get; set; }
        public string Status { get; set; }

    }
    public class FilterInter
    {
        public int Nom { get; set; }
        public string FIO_prin { get; set; }


        public DateTime DatNachZ { get; set; }
        public DateTime DatKoncZ { get; set; }

        public DateTime DatNachV { get; set; }
        public DateTime DatKoncV { get; set; }


        public bool prim { get; set; }
        public bool neisp { get; set; }

    }
    public class FilterRem
    {
        public int Nom { get; set; }
        public string FIO_prin { get; set; }
        public string FIO_vipol { get; set; }
        public string Status { get; set; }
        public DateTime DatNachZ { get; set; }
        public DateTime DatKoncZ { get; set; }

        public DateTime DatNachV { get; set; }
        public DateTime DatKoncV { get; set; }


        public bool prim { get; set; }
        public bool neisp { get; set; }
        public bool zamena { get; set; }
        public bool viezd { get; set; }

    }

}