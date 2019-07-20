using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cdo.Models
{
    public class ListLK : PageModel
    {
        public Filters Filt { get; set; }
        public List<LKPP> Listlk { get; set; }



    }

    public class ListLK1 : PageModel
    {
        public Filters Filt { get; set; }
        public List<LKUVR> Listlk { get; set; }


    }
    public class ListLK2 : PageModel
    {
        public FilterLKTO Filt { get; set; }
        public List<LKTO> Listlk { get; set; }


    }
    public class ListLK3 : PageModel
    {
        public FilterSklad Filt { get; set; }
        public List<LKSKlad> Listlk { get; set; }


    }
    public class ListLK4 : PageModel
    {
        public FilterInter Filt { get; set; }
        public List<LKInter> Listlk { get; set; }


    }
    public class ListLK5 : PageModel
    {
        public FilterRem Filt { get; set; }
        public List<LKRem> Listlk { get; set; }


    }
    public class ListLK6 : PageModel
    {
        public FilterIst Filt { get; set; }
        public List<LKPP> Listlk { get; set; }


    }

    public class LKInter
    {
        public inter internet { get; set; }
        public int id_main { get; set; }
    }
    public class LKRem
    {
        public rem rem { get; set; }
        public int id_main { get; set; }
    }
    public class LKSKlad
    {
        public sklad_to sklad { get; set; }
        public int id_main { get; set; }
    }

    public class LKPP
    {
        public int id { get; set; }
        public int inventr { get; set; }
        public string MO { get; set; }
        public string fam { get; set; }
        public string ima { get; set; }
        public string otch { get; set; }
        public DateTime data_roj { get; set; }
        public string address_proj { get; set; }
        public string tel { get; set; }
        public string Fio_rod_zp { get; set; }
        public string diagn { get; set; }
        public DateTime prikazd { get; set; }
        public string prikaz { get; set; }
        public string srok_mse { get; set; }
        public int klass { get; set; }
        public string tip_kompl { get; set; }
        public string status { get; set; }

    }
    public class LKUVR
    {
        public int id { get; set; }
        public string MO { get; set; }
        public string fam { get; set; }
        public string ima { get; set; }
        public string otch { get; set; }
        public string tel { get; set; }
        public DateTime data_roj { get; set; }
        public string address_proj { get; set; }
        public string Fio_rod_zp { get; set; }
        public int inventr { get; set; }
        public int klass { get; set; }
        public string sch_baz { get; set; }
        public string sch_jit { get; set; }
        public List<kurs> kurs { get; set; }
        public string FIO_ped { get; set; }
        public string status { get; set; }

    }
    public class LKUO
    {
        public int id { get; set; }
        public string MO { get; set; }
        public string fam { get; set; }
        public string ima { get; set; }
        public string otch { get; set; }
        public string nomer_dog { get; set; }
        public string srok_dog { get; set; }
        public string akt { get; set; }
        public string dop_sogl { get; set; }
        public string status { get; set; }

    }
    public class LKTO
    {
        public int id { get; set; }
        public string MO { get; set; }
        public string fam { get; set; }
        public string ima { get; set; }
        public string otch { get; set; }
        public int inventr { get; set; }
        public string tel { get; set; }
        public string nazv_kompl { get; set; }
        public string address_proj { get; set; }
        public string Fio_rod_zp { get; set; }
        public string tip_kompl { get; set; }


    }
}
