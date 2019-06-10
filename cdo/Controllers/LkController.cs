﻿using cdo.Models;
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


            var Email = HttpContext.User.Identity.Name;
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
            try { model.tip_kompl = str.tip_kompl; } catch { }
            try { model.Fio_rod = db.Ist.Find(str.id_fio_rod).znach; } catch { }
            try { model.Fio_rod_zp = db.Ist.Find(str.id_fio_rod_predst).znach; } catch { }
            try { model.diag = str.diagn; } catch { }
            try { model.data_sprav = Convert.ToDateTime(db.Ist.Find(str.id_srok_mse).znach); } catch { }
            try { model.prikaz = str.prik_o_zach_n; } catch { }
            try { model.klass = str.klass; } catch { }
            try { model.soh_jit = db.Ist.Find(str.id_soh_jit).znach; } catch { }
            try { model.soh_baz = db.Ist.Find(str.id_soh_baz).znach; } catch { }
            try { model.tel = db.Ist.Find(str.id_tel).znach; } catch { }
            try { model.tehot = db.To.Find(str.id_to); } catch { }
            try { model.urot = db.Uo.Find(str.id_uo); } catch { }
            try { model.prik_o_obrud = db.Ist.Find(model.urot.id_prik_o_oborud).znach; } catch { }
            try { model.nom_dogov_bvp = db.Ist.Find(model.urot.id_nom_dog_bvp).znach; } catch { }
            try { model.dvig_dogov_bvp = db.Ist.Find(model.urot.id_dvij_dog_bvp).znach; } catch { }
            try { model.kursi = db.Kurs.Where(p => p.id_main == str.id).ToList(); } catch { }
            try { model.internet = db.Inter.Where(p => p.id_to == str.id_to).ToList(); } catch { }
            try { model.remonti = db.Rem.Where(p => p.id_to == str.id_to).ToList(); } catch { }





            return View("ZayavEdit", model);
        }
    }
}
