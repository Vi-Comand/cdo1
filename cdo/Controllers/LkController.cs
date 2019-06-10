using cdo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cdo.Controllers
{
    public class LkController : Controller
    {
        private DataContext db = new DataContext();
        [Authorize]
        public IActionResult Lk()
        {



            ListLK list = new ListLK();
            list.Listlk = (from main in db.Main

                           join mo in db.Mo on main.id_mo equals mo.Id into mo
                           from m in mo.DefaultIfEmpty()
                           join ist in db.Ist on main.id_f equals ist.id into ist
                           from f in ist.DefaultIfEmpty()
                           join im in db.Ist on main.id_i equals im.id into im
                           from i in im.DefaultIfEmpty()
                           join ot in db.Ist on main.id_o equals ot.id into ot
                           from o in ot.DefaultIfEmpty()
                           join rod in db.Ist on main.id_fio_rod_predst equals rod.id into rod
                           from r in rod.DefaultIfEmpty()
                           join add in db.Ist on main.id_adr_progiv equals add.id into add
                           from a in add.DefaultIfEmpty()
                           join to in db.To on main.id_to equals to.id into to
                           from t in to.DefaultIfEmpty()
                           select new LK
                           {
                               id = main.id,
                               MO = (m == null ? String.Empty : m.name),
                               fam = f.znach,
                               ima = i.znach,
                               otch = o.znach,
                               data_roj = main.data_rojd,
                               address_proj = a.znach,
                               Fio_rod_zp = r.znach,
                               inventr = t.nov_inv,
                               prikaz = main.prik_o_zach_n,
                               status = main.status

                           }).ToList();

            //CompositeModel compositeModel=new CompositeModel(db);
            return View("obch", list);
        }
        public IActionResult kartochka(int id)
        {
            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;

            CompositeModel model = new CompositeModel(db);
            main str = db.Main.Find(id);
            try { model.id = str.id; } catch { }
            try { model.fam = db.Ist.Find(str.id_f).znach; } catch { }
            try { model.ima = db.Ist.Find(str.id_i).znach; } catch { }
            try { model.otch = db.Ist.Find(str.id_o).znach; } catch { }
            try { model.data_roj = str.data_rojd; } catch { }
            try { model.address_proj = db.Ist.Find(str.id_adr_progiv).znach; } catch { }
            try { model.address_reg = db.Ist.Find(str.id_adr_reg).znach; } catch { }
            try { model.tel = db.Ist.Find(str.id_tel).znach; } catch { }
            try { model.MO = str.id_mo; } catch { }

            try { model.Fio_rod = db.Ist.Find(str.id_fio_rod).znach; } catch { }
            try { model.Fio_rod_zp = db.Ist.Find(str.id_fio_rod_predst).znach; } catch { }

            try { model.prikaz = str.prik_o_zach_n; } catch { }
            try { model.klass = str.klass; } catch { }
            try { model.status = str.status; } catch { }
            try { model.tel = db.Ist.Find(str.id_tel).znach; } catch { }
            try { model.bvps = db.Bvp.Where(p => p.id_uo == str.id_uo).ToList(); } catch { }
            try { model.data_ust_oborud = db.To.Where(p => p.id == str.id_to).First().data_ust_o; } catch { }


            if (role == 1)
            {

                try { model.dvig_dogov_bvp = db.Ist.Find(model.urot.id_dvij_dog_bvp).znach; } catch { }
                try { model.kursi = db.Kurs.Where(p => p.id_main == str.id).ToList(); } catch { }
                try { model.internet = db.Inter.Where(p => p.id_to == str.id_to).ToList(); } catch { }
                try { model.remonti = db.Rem.Where(p => p.id_to == str.id_to).ToList(); } catch { }


                try { model.tip_kompl = str.tip_kompl; } catch { }


                try { model.soh_jit = db.Ist.Find(str.id_soh_jit).znach; } catch { }
                try { model.soh_baz = db.Ist.Find(str.id_soh_baz).znach; } catch { }
                try { model.tehot = db.To.Find(str.id_to); } catch { }
                try { model.urot = db.Uo.Find(str.id_uo); } catch { }

            }

            if (role == 2)
            {
                try { model.diag = str.diagn; } catch { }
                try { model.data_sprav = Convert.ToDateTime(db.Ist.Find(str.id_srok_mse).znach); } catch { }
                try { model.tip_kompl = str.tip_kompl; } catch { }
                try { model.soh_jit = db.Ist.Find(str.id_soh_jit).znach; } catch { }
                try { model.soh_baz = db.Ist.Find(str.id_soh_baz).znach; } catch { }
                try { model.tehot.inter = db.To.Where(p => p.id == str.id_to).First().inter; } catch { }
                //Достать паспортные только
                try { model.urot = db.Uo.Find(str.id_uo); } catch { }
            }
            if (role == 3)
            {

                try { model.soh_jit = db.Ist.Find(str.id_soh_jit).znach; } catch { }
                try { model.soh_baz = db.Ist.Find(str.id_soh_baz).znach; } catch { }
                try { model.tehot.inter = db.To.Where(p => p.id == str.id_to).First().inter; } catch { }
                try { model.tehot.kompl = db.To.Where(p => p.id == str.id_to).First().kompl; } catch { }
                try { model.kursi = db.Kurs.Where(p => p.id_main == str.id).ToList(); } catch { }
            }
            if (role == 4)
            {

                try { model.tehot = db.To.Find(str.id_to); } catch { }
                try { model.internet = db.Inter.Where(p => p.id_to == str.id_to).ToList(); } catch { }
                try { model.remonti = db.Rem.Where(p => p.id_to == str.id_to).ToList(); } catch { }
            }
            if (role == 5)
            {
                //4 поля для юр
                try { model.tehot = db.To.Find(str.id_to); } catch { }

                try { model.urot = db.Uo.Find(str.id_uo); } catch { }

            }
            if (role == 6)
            {
                //4 поля для бух
                try { model.tehot = db.To.Find(str.id_to); } catch { }

            }

            //try { model.nom_dogov_bvp = db.Ist.Find(model.urot.id_nom_dog_bvp).znach; } catch { }
            //try { model.dvig_dogov_bvp = db.Ist.Find(model.urot.id_dvij_dog_bvp).znach; } catch { }
            //try { model.kursi = db.Kurs.Where(p => p.id_main == str.id).ToList(); } catch { }
            //try { model.internet = db.Inter.Where(p => p.id_to == str.id_to).ToList(); } catch { }
            //try { model.remonti = db.Rem.Where(p => p.id_to == str.id_to).ToList(); } catch { }


            //try { model.tip_kompl = str.tip_kompl; } catch { }


            //try { model.soh_jit = db.Ist.Find(str.id_soh_jit).znach; } catch { }
            //try { model.soh_baz = db.Ist.Find(str.id_soh_baz).znach; } catch { }
            //try { model.tehot = db.To.Find(str.id_to); } catch { }
            //try { model.urot = db.Uo.Find(str.id_uo); } catch { }

            return View("ZayavEdit", model);
        }
    }
}
