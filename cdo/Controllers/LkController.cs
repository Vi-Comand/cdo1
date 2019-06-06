using Attest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attest.Controllers
{
    public class LkController : Controller
    {
        private DataContext db = new DataContext();
        [Authorize]
        public async Task<IActionResult> Lk(SortState sortOrder = SortState.NomZayavAsc)
        {


            var Email = HttpContext.User.Identity.Name;
            Users user = db.Users.Where(p => p.Email == Email).First();

            //CompositeModel compositeModel=new CompositeModel(db);

            try
            {
                if (user.role == "1")
                {
                    ViewBag.LK = db.Zayavlen.Where(p => p.id_user == user.Id).OrderByDescending(p => p.data_podachi)
                        .ToList();

                    return View("user");
                }

                if (user.role == "2")
                {




                    var listotv = new ListOtv();
                    listotv.ListOtvetstven = (from zaya in db.Zayavlen.Where(p => p.mo == user.mo)
                                              join usr in db.Users on zaya.id_user equals usr.Id
                                              join fl in db.File.Where(p => p.kategor_f == "PrevAttestCopy") on zaya.Id equals fl.id_zayavl
                                                  into gj
                                              from x in gj.DefaultIfEmpty()
                                              select new Otvetstven
                                              {
                                                  Id = zaya.Id,
                                                  name = usr.FIO,
                                                  data_podachi = zaya.data_podachi,
                                                  oo = zaya.oo,
                                                  dolgnost = zaya.dolgnost_att,
                                                  kategor = zaya.kategor,
                                                  file = (x == null ? String.Empty : x.name_f),
                                                  status = zaya.status
                                              }).ToList();
                    return View("otv", listotv);





                }




                if (user.role == "3")
                {




                    var listotv = new ListOtv();
                    listotv.ListOtvetstven =
                        (from zaya in db.Zayavlen.Where(p => p.spec == user.Id && p.status == "Заявление подлинное" || p.status == "Анализ проведен")
                         join usr in db.Users on zaya.id_user equals usr.Id
                         select new Otvetstven
                         {
                             Id = zaya.Id,
                             name = usr.FIO,
                             data_podachi = zaya.data_podachi,
                             oo = zaya.oo,
                             dolgnost = zaya.dolgnost_att,
                             kategor = zaya.kategor,
                             ball = zaya.ball,
                             status = zaya.status
                         }).ToList();
                    return View("spec", listotv);






                }
            }
            catch
            {

            }

            return View("index");

            /* ViewBag.LK = db.Zayavlen.Where(p=>p.mo==1).ToList();
           return View("otv");*/
            /*  ViewBag.LK = db.Zayavlen.Where(p => p.spec == 1).ToList();
              return View("spec");*/
        }





        /*    ViewData["NomZayavSort"] = sortOrder == SortState.NomZayavAsc ? SortState.NomZayavDesc : SortState.NomZayavAsc;
            ViewData["DataPodachSort"] = sortOrder == SortState.DataPodachAsc ? SortState.DataPodachDesc : SortState.DataPodachAsc;
            ViewData["DataObnovSort"] = sortOrder == SortState.DataObnovAsc ? SortState.DataObnovDesc : SortState.DataObnovAsc;

            switch (sortOrder)
            {
                case SortState.NomZayavAsc:
                    ViewBag.LK = db.Zayavlen.Where(p => p.id_user == user.Id).OrderByDescending(p => p.Id).ToList();
                    break;
                case SortState.NomZayavDesc:
                    ViewBag.LK = db.Zayavlen.Where(p => p.id_user == user.Id).OrderByDescending(p => p.Id).ToList();
                    break;
                case SortState.DataPodachAsc:
                    ViewBag.LK = db.Zayavlen.Where(p => p.id_user == user.Id).OrderByDescending(p => p.data_podachi).ToList();
                    break;

                case SortState.DataObnovAsc:
                    ViewBag.LK = db.Zayavlen.Where(p => p.id_user == user.Id).OrderByDescending(p => p.data_obnovl).ToList();
                    break;
                case SortState.DataObnovDesc:
                    ViewBag.LK = db.Zayavlen.Where(p => p.id_user == user.Id).OrderByDescending(p => p.data_obnovl).ToList();
                    break;
                default:
                    ViewBag.LK = db.Zayavlen.Where(p => p.id_user == user.Id).OrderByDescending(p => p.data_podachi).ToList();
                    break;
            }
            //return View(await zayavlen.AsNoTracking().ToListAsync());
            */
    }
}
