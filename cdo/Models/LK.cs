using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
    public class LKPP
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
        public DateTime prikazd { get; set; }
        public int klass { get; set; }
        public string diagn { get; set; }
        public string srok_mse { get; set; }
        public string tip_kompl { get; set; }
        public string tel { get; set; }
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
        public string inventr { get; set; }
        public int klass { get; set; }
        public string sch_baz { get; set; }
        public string sch_jit { get; set; }
        public List<kurs> kurs { get; set; }
        public string FIO_ped { get; set; }


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
}
