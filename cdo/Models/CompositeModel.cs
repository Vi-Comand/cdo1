using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cdo.Models

{
    public class CompositeModel : PageModel
    {
        private readonly DataContext _context;

        public int id { get; set; }
        public int MO { get; set; }
        public string fam { get; set; }
        public string ima { get; set; }
        public string otch { get; set; }
        public DateTime data_roj { get; set; }
        public string address_proj { get; set; }
        public string address_reg { get; set; }
        public string tel { get; set; }
        public string Fio_rod { get; set; }
        public string Fio_rod_zp { get; set; }
        public string diag { get; set; }
        public string status { get; set; }
        public string inter { get; set; }
        public DateTime data_ust_oborud { get; set; }
        public string data_sprav { get; set; }
        public string prikaz { get; set; }
        public DateTime prikaz_d { get; set; }
        public DateTime prikaz_iskl_d { get; set; }
        public int klass { get; set; }
        public string soh_jit { get; set; }
        public string soh_baz { get; set; }
        public string tip_kompl { get; set; }
        public string prik_o_obrud { get; set; }
        public string nom_dogov_bvp { get; set; }
        public string dvig_dogov_bvp { get; set; }
        public to tehot { get; set; }
        public uo urot { get; set; }
        public kurs kursadd { get; set; }
        public int role { get; set; }
        public rem remadd { get; set; }
        public inter intadd { get; set; }
        public bvp bvpadd { get; set; }
        public dop_dog dopadd { get; set; }
        public List<kurs> kursi { get; set; }
        public List<kurs> kurs_per { get; set; }
        public List<inter> internet { get; set; }
        public List<rem> remonti { get; set; }
        public sklad_to sklad { get; set; }
        //public string status { get; set; }
        public List<bvp> bvps { get; set; }
        public List<dop_dog> dop_Dogs { get; set; }
        public string FIO_ped { get; set; }




        public CompositeModel()
        {

        }
        public CompositeModel(DataContext db)
        {
            _context = db;
        }



        //public CompositeModel(DataContext db)
        //{
        //    _context = db;
        //}
        //public void OnGet()
        //{
        //   var FileModels = _context.File.AsNoTracking().ToList();
        //}


    }
    public class sklad : PageModel
    {
        public sklad_to sk { get; set; }
        public int id_main { get; set; }
    }
    public class main
    {
        public int id { get; set; }
        public int id_mo { get; set; }
        public int id_adr_progiv { get; set; }
        public int id_to { get; set; }
        public int id_f { get; set; }
        public int id_i { get; set; }
        public int id_o { get; set; }
        public DateTime data_rojd { get; set; }
        public int id_adr_reg { get; set; }
        public int id_fio_rod { get; set; }
        public int id_fio_rod_predst { get; set; }
        public string diagn { get; set; }
        public string prik_o_zach_n { get; set; }
        public DateTime prik_o_zach_d { get; set; }
        public DateTime prik_o_iskl_d { get; set; }
        public string FIO_ped { get; set; }
        public int id_tel { get; set; }
        public string tip_kompl { get; set; }
        public string status { get; set; }
        public int klass { get; set; }
        public int id_srok_mse { get; set; }
        public int id_soh_jit { get; set; }
        public int id_soh_baz { get; set; }
        public int id_uo { get; set; }
        public DateTime data_sozd { get; set; }
        public DateTime data_izm { get; set; }
        public int id_sklad { get; set; }


    }

    public class auth_date
    {
        public int id { get; set; }
        public string login { get; set; }
        public DateTime date { get; set; }
        public int autorizing { get; set; }
        public string ip { get; set; }

    }

    public class user
    {
        public int id { get; set; }
        public string fio { get; set; }
        public int role { get; set; }
        public string login { get; set; }
        public string pass { get; set; }
        //  public string last_date_autor { get; set; }
    }
    public class uo
    {
        public int id { get; set; }
        public int nom_p { get; set; }
        public int ser_p { get; set; }
        public string kod_p { get; set; }
        public DateTime data_p { get; set; }
        public string vidan_p { get; set; }
        public string propis_p { get; set; }
        public string rogd_p { get; set; }


        public string dop_sogl_bvp_n { get; set; }
        public DateTime dop_sogl_bvp_d { get; set; }
        public string id_dvij_dog_bvp { get; set; }
        public DateTime data_roj { get; set; }
    }
    public class bvp
    {
        public int id { get; set; }
        public string prik_o_oborud { get; set; }
        public DateTime data_ust_oborud { get; set; }
        public string nom_dog_bvp { get; set; }
        public DateTime srok_dog_bvp { get; set; }
        public DateTime akt_vozvr_oborud { get; set; }
        public DateTime prik_o_oborud_d { get; set; }
        public DateTime nom_dog_bvp_d { get; set; }
        public int id_uo { get; set; }
        public string dop_obor { get; set; }
        public DateTime data_u_dop_o { get; set; }
        public DateTime data_v_dop_o { get; set; }
    }
    public class dop_dog
    {
        public int id { get; set; }
        public int id_uo { get; set; }
        public string dop_sogl_bvp_n { get; set; }
        public DateTime dop_sogl_bvp_d { get; set; }
        public string soderg { get; set; }
    }
    public class ist
    {
        public int id { get; set; }
        public string kluch { get; set; }
        public string znach { get; set; }
        public DateTime data_izm { get; set; }
        public int id_user { get; set; }
        public int role { get; set; }
        public int id_main { get; set; }
        public int id_uo { get; set; }

    }
    public class to
    {
        public int id { get; set; }

        public string kompl { get; set; }
        public string inter { get; set; }
        public DateTime date_inter { get; set; }
        public string skype_l { get; set; }
        public string skype_p { get; set; }


    }

    public class rem
    {
        public int Id { get; set; }
        public int id_to { get; set; }
        public DateTime data_z_r { get; set; }
        public string prich_r { get; set; }
        public DateTime data_v_r { get; set; }
        public string fio_prin_r { get; set; }
        public string fio_vipol_r { get; set; }
        public string status_r { get; set; }
        public string prim_r { get; set; }
        public bool zamena { get; set; }
        public bool viezd { get; set; }
    }

    public class sklad_to
    {
        public int Id { get; set; }
        public string nazv_komp { get; set; }
        public DateTime data_ust_o { get; set; }
        public long star_inv { get; set; }
        public long nov_inv { get; set; }
        public string stoim { get; set; }
        public DateTime data_vozvr_kompl { get; set; }
        public DateTime data_vvoda_kompl { get; set; }
        public string status { get; set; }
        public string pritenz { get; set; }
        public string prim { get; set; }

    }

    public class inter
    {
        public int Id { get; set; }
        public int id_to { get; set; }
        public DateTime data_z_i { get; set; }
        public string fio_prin_i { get; set; }
        public DateTime data_v_i { get; set; }
        public string zayav_neisp_i { get; set; }
        public string prim_i { get; set; }
    }

    public class kurs
    {
        public int id { get; set; }
        public int id_main { get; set; }
        public string kurs_do { get; set; }
        public string period { get; set; }
    }



    public class Mo
    {
        public int Id { get; set; }
        public string name { get; set; }
    }

    public class ist_kompl
    {
        public int id { get; set; }
        public int id_kompl { get; set; }
        public int id_main { get; set; }
        public DateTime data_p { get; set; }
    }
}
