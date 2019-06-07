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
        public async Task<IActionResult> Lk()
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
    }
}
