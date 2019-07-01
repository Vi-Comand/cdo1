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




            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;
            if (role == 2)
            {
                ListLK list = new ListLK();
                list.Listlk = (from main in db.Main

                               join mo in db.Mo on main.id_mo equals mo.Id into mo
                               from m in mo.DefaultIfEmpty()
                               join ist in db.Ist on main.id_f equals ist.id into ist
                               from f in ist.DefaultIfEmpty()
                               join tel in db.Ist on main.id_tel equals tel.id into tel
                               from te in tel.DefaultIfEmpty()
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
                               join mse in db.Ist on main.id_srok_mse equals mse.id into mse
                               from ms in mse.DefaultIfEmpty()

                               select new LKPP
                               {
                                   id = main.id,
                                   inventr = t.nov_inv,
                                   MO = (m == null ? String.Empty : m.name),
                                   fam = f.znach,
                                   ima = i.znach,
                                   otch = o.znach,
                                   data_roj = main.data_rojd.Date,
                                   address_proj = a.znach,
                                   tel = te.znach,
                                   Fio_rod_zp = r.znach,
                                   diagn = main.diagn,
                                   prikazd = main.prik_o_zach_d,
                                   prikaz = main.prik_o_zach_n,
                                   srok_mse = ms.znach,
                                   klass = main.klass,
                                   tip_kompl = main.tip_kompl,
                                   status = main.status

                               }).ToList();
                list.Filt = new Filters();
                return View("obch", list);

            }
            if (role == 3)
            {

                ListLK1 list = new ListLK1();
                list.Listlk = (from main in db.Main

                               join mo in db.Mo on main.id_mo equals mo.Id into mo
                               from m in mo.DefaultIfEmpty()
                               join ist in db.Ist on main.id_f equals ist.id into ist
                               from f in ist.DefaultIfEmpty()
                               join tel in db.Ist on main.id_tel equals tel.id into tel
                               from te in tel.DefaultIfEmpty()
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
                               join mse in db.Ist on main.id_srok_mse equals mse.id into mse
                               from ms in mse.DefaultIfEmpty()
                               join s_j in db.Ist on main.id_soh_jit equals s_j.id into s_j
                               from sj in s_j.DefaultIfEmpty()
                               join s_b in db.Ist on main.id_soh_baz equals s_b.id into s_b
                               from sb in s_b.DefaultIfEmpty()
                               select new LKUVR
                               {
                                   id = main.id,
                                   inventr = t.nov_inv,
                                   MO = (m == null ? String.Empty : m.name),
                                   fam = f.znach,
                                   ima = i.znach,
                                   otch = o.znach,
                                   data_roj = main.data_rojd,
                                   address_proj = a.znach,
                                   tel = te.znach,
                                   Fio_rod_zp = r.znach,
                                   klass = main.klass,
                                   sch_jit = sj.znach,
                                   sch_baz = sb.znach,
                                   kurs = db.Kurs.Where(x => x.id_main == main.id).ToList(),
                                   FIO_ped = main.FIO_ped
                               }).ToList();
                list.Filt = new Filters();
                return View("LKUVR", list);

            }
            else
            {
                ListLK list = new ListLK();
                list.Listlk = (from main in db.Main

                               join mo in db.Mo on main.id_mo equals mo.Id into mo
                               from m in mo.DefaultIfEmpty()
                               join ist in db.Ist on main.id_f equals ist.id into ist
                               from f in ist.DefaultIfEmpty()
                               join tel in db.Ist on main.id_tel equals tel.id into tel
                               from te in tel.DefaultIfEmpty()
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
                               join mse in db.Ist on main.id_srok_mse equals mse.id into mse
                               from ms in mse.DefaultIfEmpty()

                               select new LKPP
                               {
                                   id = main.id,
                                   inventr = t.nov_inv,
                                   MO = (m == null ? String.Empty : m.name),
                                   fam = f.znach,
                                   ima = i.znach,
                                   otch = o.znach,
                                   data_roj = main.data_rojd.Date,
                                   address_proj = a.znach,
                                   tel = te.znach,
                                   Fio_rod_zp = r.znach,
                                   diagn = main.diagn,
                                   prikazd = main.prik_o_zach_d,
                                   prikaz = main.prik_o_zach_n,
                                   srok_mse = ms.znach,
                                   klass = main.klass,
                                   tip_kompl = main.tip_kompl,
                                   status = main.status

                               }).ToList();
                return View("obch", list);

            }


            //CompositeModel compositeModel=new CompositeModel(db);
            string remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            /* ViewData["Message"] = remoteIpAddress;
       if (remoteIpAddress == "193.242.149.177" || remoteIpAddress == "193.242.149.14" || remoteIpAddress == "::1")
           /*{
              return View("obch", list);
          }
          else
          {
              return View("dost");
          }*/
            return View("obch");

        }
        public IActionResult save(CompositeModel composit)
        {
            CompositeModel model = new CompositeModel();
            main str = db.Main.Find(composit.id);
            try { model.id = str.id; } catch { }
            try { model.fam = db.Ist.Find(str.id_f).znach; } catch { }
            try { model.ima = db.Ist.Find(str.id_i).znach; } catch { }
            try { model.otch = db.Ist.Find(str.id_o).znach; } catch { }
            try { model.address_proj = db.Ist.Find(str.id_adr_progiv).znach; } catch { }
            try { model.address_reg = db.Ist.Find(str.id_adr_reg).znach; } catch { }
            try { model.Fio_rod = db.Ist.Find(str.id_fio_rod).znach; } catch { }
            try { model.Fio_rod_zp = db.Ist.Find(str.id_fio_rod_predst).znach; } catch { }
            try { model.tel = db.Ist.Find(str.id_tel).znach; } catch { }
            try { model.soh_jit = db.Ist.Find(str.id_soh_jit).znach; } catch { }
            try { model.soh_baz = db.Ist.Find(str.id_soh_baz).znach; } catch { }
            try { model.data_sprav = Convert.ToDateTime(db.Ist.Find(str.id_srok_mse).znach); } catch { }
            var login = HttpContext.User.Identity.Name;
            int id_user = db.User.Where(p => p.login == login).First().id;
            int role = db.User.Where(p => p.login == login).First().role;


            if (composit.fam != model.fam)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "f";
                pole.znach = composit.fam;
                pole.role = role;
                pole.data_izm = DateTime.Now;
                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_f = pole.id;
            }
            if (composit.ima != model.ima)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "i";
                pole.znach = composit.ima;
                pole.role = role;
                pole.data_izm = DateTime.Now;
                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_i = pole.id;
            }


            if (composit.otch != model.otch)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "o";
                pole.znach = composit.otch;
                pole.role = role;
                pole.data_izm = DateTime.Now;
                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_o = pole.id;
            }

            if (composit.address_proj != model.address_proj)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "adrp";
                pole.znach = composit.address_proj;
                pole.role = role;
                pole.data_izm = DateTime.Now;
                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_adr_progiv = pole.id;
            }

            if (composit.address_reg != model.address_reg)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "adrr";
                pole.znach = composit.address_reg;
                pole.role = role;
                pole.data_izm = DateTime.Now;
                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_adr_reg = pole.id;
            }

            if (composit.tel != model.tel)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "tel";
                pole.znach = composit.tel;
                pole.role = role;
                pole.data_izm = DateTime.Now;
                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_tel = pole.id;
            }
            if (composit.Fio_rod != model.Fio_rod)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "fior";
                pole.znach = composit.Fio_rod;
                pole.role = role;
                pole.data_izm = DateTime.Now;
                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_fio_rod = pole.id;
            }
            if (composit.Fio_rod_zp != model.Fio_rod_zp)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "fiozp";
                pole.znach = composit.Fio_rod_zp;
                pole.role = role;
                pole.data_izm = DateTime.Now;
                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_fio_rod_predst = pole.id;
            }

            if (composit.data_sprav != model.data_sprav)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "srmse";
                pole.znach = composit.data_sprav.ToString();
                pole.role = role;
                pole.data_izm = DateTime.Now;
                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_srok_mse = pole.id;
            }

            if (composit.soh_jit != model.soh_jit)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "sjit";
                pole.znach = composit.soh_jit;
                pole.role = role;
                pole.data_izm = DateTime.Now;
                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_soh_jit = pole.id;
            }

            if (composit.soh_baz != model.soh_baz)
            {

                ist pole = new ist();
                pole.id_main = composit.id;
                pole.id_user = id_user;
                pole.kluch = "sbaz";
                pole.role = role;
                pole.data_izm = DateTime.Now;
                pole.znach = composit.soh_baz;

                db.Entry(pole).State = EntityState.Added;

                db.SaveChanges();
                str.id_soh_baz = pole.id;
            }

            str.klass = composit.klass;
            str.prik_o_zach_d = composit.prikaz_d;
            str.prik_o_zach_n = composit.prikaz;
            str.status = composit.status;
            str.tip_kompl = composit.tip_kompl;
            str.diagn = composit.diag;
            str.id_mo = composit.MO;
            str.data_rojd = composit.data_roj;


            db.Entry(str).State = EntityState.Modified;
            db.SaveChanges();
            if (composit.tehot != null)
            {
                try
                {
                    db.Entry(composit.tehot).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch { }
            }
            if (composit.urot != null)
            {
                try
                {
                    db.Entry(composit.urot).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch { }
            }
            return Redirect("/Lk/Lk");

        }
        public async Task<IActionResult> Sort(SortState sortOrder, ListLK filtr, string status, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17)
        {
            if (filtr.Filt == null)
            {
                filtr.Filt = new Filters();


                if (f2.ToString() == "01.01.0001 0:00:00")
                    f2 = DateTime.Now;
                if (f3.ToString() == "01.01.0001 0:00:00")
                    f3 = (DateTime.Now).AddYears(5);
                if (f4.ToString() == "01.01.0001 0:00:00")
                    f4 = (DateTime.Now).AddYears(5);

                if (f1 != null)
                    filtr.Filt.Add_proj = f1;
                if (f2 != null)
                    filtr.Filt.DatKoncDR = f2;
                if (f3 != null)
                    filtr.Filt.DatKoncP = f3;
                if (f4 != null)
                    filtr.Filt.DatKoncSD = f4;
                if (f5 != null)
                    filtr.Filt.DatNachDR = f5;
                if (f6 != null)
                    filtr.Filt.DatNachP = f6;
                if (f7 != null)
                    filtr.Filt.DatNachSD = f7;
                if (f8 != null)
                    filtr.Filt.Diagn = f8;
                if (f9 != null)
                    filtr.Filt.F = f9;
                if (f10 != null)
                    filtr.Filt.I = f10;
                filtr.Filt.MO = f11;
                if (f12 != null)
                    filtr.Filt.Fio_rod = f12;
                filtr.Filt.Nom = f13;
                if (f14 != null)
                    filtr.Filt.O = f14;
                if (f15 != null)
                    filtr.Filt.prikaz = f15;
                if (f16 != null)
                    filtr.Filt.Tel = f16;
                if (f17 != null)
                    filtr.Filt.tip_kompl = f17;
            }
            if (status != null)
            {
                filtr.Filt.Status = status;
            }
            else
            if (filtr.Filt.Status != null)
            { filtr.Filt.Status = null; }

            ListLK model = new ListLK();
            var query = (from main in (filtr.Filt.MO != 0 ? db.Main.Where(x => x.id_mo == filtr.Filt.MO) : db.Main)

                         join mo in db.Mo on main.id_mo equals mo.Id into mo
                         from m in mo.DefaultIfEmpty()
                         join ist in db.Ist on main.id_f equals ist.id into ist
                         from f in ist.DefaultIfEmpty()
                         join tel in db.Ist on main.id_tel equals tel.id into tel
                         from te in tel.DefaultIfEmpty()
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
                         join mse in db.Ist on main.id_srok_mse equals mse.id into mse
                         from ms in mse.DefaultIfEmpty()

                         select new LKPP
                         {
                             id = main.id,
                             inventr = t.nov_inv,
                             MO = (m == null ? String.Empty : m.name),
                             fam = f.znach,
                             ima = i.znach,
                             otch = o.znach,
                             data_roj = main.data_rojd,
                             address_proj = a.znach,
                             tel = te.znach,
                             Fio_rod_zp = r.znach,
                             diagn = main.diagn,
                             prikazd = main.prik_o_zach_d,
                             prikaz = main.prik_o_zach_n,
                             srok_mse = ms.znach,
                             klass = main.klass,
                             tip_kompl = main.tip_kompl,
                             status = main.status

                         });


            if (filtr.Filt.Status != null)
            {
                query = query.Where(p => p.status == filtr.Filt.Status);


            }
            if (filtr.Filt.Nom != 0)
            {
                query = query.Where(p => p.id == filtr.Filt.Nom);


            }
            if (filtr.Filt.F != null)
            {
                query = from t in query
                        where t.fam.Contains(filtr.Filt.F)
                        select t;

            }
            if (filtr.Filt.I != null)
            {
                query = from t in query
                        where t.ima.Contains(filtr.Filt.I)
                        select t;

            }
            if (filtr.Filt.O != null)
            {
                query = from t in query
                        where t.otch.Contains(filtr.Filt.O)
                        select t;

            }

            if (filtr.Filt.DatNachDR != null || filtr.Filt.DatKoncDR != null)
                query = query.Where(p => p.data_roj >= filtr.Filt.DatNachDR && p.data_roj <= filtr.Filt.DatKoncDR);

            if (filtr.Filt.Add_proj != null)
            {
                query = from t in query
                        where t.address_proj.Contains(filtr.Filt.Add_proj)
                        select t;

            }
            if (filtr.Filt.Tel != null)
            {
                query = from t in query
                        where t.tel.Contains(filtr.Filt.Tel)
                        select t;

            }
            if (filtr.Filt.Diagn != null)
            {
                query = from t in query
                        where t.diagn.Contains(filtr.Filt.Diagn)
                        select t;

            }
            if (filtr.Filt.DatNachSD != null || filtr.Filt.DatKoncSD != null)
                query = query.Where(p => Convert.ToDateTime(p.srok_mse) >= filtr.Filt.DatNachSD && Convert.ToDateTime(p.srok_mse) <= filtr.Filt.DatKoncSD);
            if (filtr.Filt.prikaz != null)
            {
                query = from t in query
                        where t.prikaz.Contains(filtr.Filt.prikaz)
                        select t;

            }
            if (filtr.Filt.DatNachP != null || filtr.Filt.DatKoncP != null)
                query = query.Where(p => p.prikazd >= filtr.Filt.DatNachP && p.prikazd <= filtr.Filt.DatKoncP);
            if (filtr.Filt.tip_kompl != null)
            {
                query = from t in query
                        where t.tip_kompl.Contains(filtr.Filt.tip_kompl)
                        select t;

            }

            ViewData["Nom"] = sortOrder == SortState.NomAsc ? SortState.NomDesc : SortState.NomAsc;
            ViewData["In"] = sortOrder == SortState.InAsc ? SortState.InDesc : SortState.InAsc;
            ViewData["Mo"] = sortOrder == SortState.MoAsc ? SortState.MoDesc : SortState.MoAsc;
            ViewData["F"] = sortOrder == SortState.FAsc ? SortState.FDesc : SortState.FAsc;
            ViewData["I"] = sortOrder == SortState.IAsc ? SortState.IDesc : SortState.IAsc;
            ViewData["O"] = sortOrder == SortState.OAsc ? SortState.ODesc : SortState.OAsc;
            ViewData["DR"] = sortOrder == SortState.DRAsc ? SortState.DRDesc : SortState.DRAsc;

            ViewData["Add_p"] = sortOrder == SortState.Add_pAsc ? SortState.Add_pDesc : SortState.Add_pAsc;
            ViewData["D"] = sortOrder == SortState.DAsc ? SortState.DDesc : SortState.DAsc;
            ViewData["MSE"] = sortOrder == SortState.MSEAsc ? SortState.MSEDesc : SortState.MSEAsc;
            ViewData["Nom_p"] = sortOrder == SortState.Nom_pAsc ? SortState.Nom_pDesc : SortState.Nom_pAsc;
            ViewData["Data_p"] = sortOrder == SortState.Data_pAsc ? SortState.Data_pDesc : SortState.Data_pAsc;
            ViewData["Klass"] = sortOrder == SortState.KlassAsc ? SortState.KlassDesc : SortState.KlassAsc;
            ViewData["Tip_kompl"] = sortOrder == SortState.Tip_komplAsc ? SortState.Tip_komplDesc : SortState.Tip_komplAsc;
            ViewData["Status"] = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            model.Listlk = query.ToList();
            switch (sortOrder)
            {
                /* case "1":
                     listotv.ListOtvetstvn = query.OrderBy(s => s.Id).ToList();
                     break;*/


                case SortState.NomAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.id).ToList();
                    break;
                case SortState.NomDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.id).ToList();
                    break;



                case SortState.InAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.inventr).ToList();
                    break;
                case SortState.InDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.inventr).ToList();
                    break;
                case SortState.MoAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.MO).ToList();
                    break;
                case SortState.MoDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.MO).ToList();
                    break;
                case SortState.FAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.fam).ToList();
                    break;
                case SortState.FDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.fam).ToList();
                    break;
                case SortState.IAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.ima).ToList();
                    break;
                case SortState.IDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.ima).ToList();
                    break;
                case SortState.OAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.otch).ToList();
                    break;
                case SortState.ODesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.otch).ToList();
                    break;
                case SortState.DRAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.data_roj).ToList();
                    break;
                case SortState.DRDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.data_roj).ToList();
                    break;
                case SortState.Add_pAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.address_proj).ToList();
                    break;
                case SortState.Add_pDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.address_proj).ToList();
                    break;
                case SortState.DAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.diagn).ToList();
                    break;
                case SortState.DDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.diagn).ToList();
                    break;
                case SortState.MSEAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.srok_mse).ToList();
                    break;
                case SortState.MSEDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.srok_mse).ToList();
                    break;
                case SortState.Nom_pAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.prikaz).ToList();
                    break;
                case SortState.Nom_pDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.prikaz).ToList();
                    break;
                case SortState.Data_pAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.prikazd).ToList();
                    break;
                case SortState.Data_pDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.prikazd).ToList();
                    break;
                case SortState.KlassAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.klass).ToList();
                    break;
                case SortState.KlassDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.klass).ToList();
                    break;
                case SortState.Tip_komplAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.tip_kompl).ToList();
                    break;
                case SortState.Tip_komplDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.tip_kompl).ToList();
                    break;
                case SortState.StatusAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.status).ToList();
                    break;
                case SortState.StatusDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.status).ToList();
                    break;


            }
            model.Filt = filtr.Filt;
            return View("obch", model);
        }
        public async Task<IActionResult> Sort1(SortState sortOrder, ListLK filtr, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17, int f18, string f19, string f20, string f21, string f22)
        {

            if (filtr.Filt == null)
            {
                filtr.Filt = new Filters();

                if (f1 != null)
                    filtr.Filt.Add_proj = f1;
                if (f2 != null)
                    filtr.Filt.DatKoncDR = f2;


                if (f5 != null)
                    filtr.Filt.DatNachDR = f5;


                if (f9 != null)
                    filtr.Filt.F = f9;
                if (f10 != null)
                    filtr.Filt.I = f10;
                filtr.Filt.MO = f11;
                if (f12 != null)
                    filtr.Filt.Fio_rod = f12;
                filtr.Filt.Nom = f13;
                if (f14 != null)
                    filtr.Filt.O = f14;

                if (f16 != null)
                    filtr.Filt.Tel = f16;
                if (f18 != 0)
                    filtr.Filt.Klass = f18;
                if (f19 != null)
                    filtr.Filt.BS = f19;
                if (f20 != null)
                    filtr.Filt.MS = f20;
                if (f21 != null)
                    filtr.Filt.Fio_ped = f21;

            }


            ListLK1 model = new ListLK1();
            var query = (from main in db.Main

                         join mo in db.Mo on main.id_mo equals mo.Id into mo
                         from m in mo.DefaultIfEmpty()
                         join ist in db.Ist on main.id_f equals ist.id into ist
                         from f in ist.DefaultIfEmpty()
                         join tel in db.Ist on main.id_tel equals tel.id into tel
                         from te in tel.DefaultIfEmpty()
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
                         join mse in db.Ist on main.id_srok_mse equals mse.id into mse
                         from ms in mse.DefaultIfEmpty()
                         join s_j in db.Ist on main.id_soh_jit equals s_j.id into s_j
                         from sj in s_j.DefaultIfEmpty()
                         join s_b in db.Ist on main.id_soh_baz equals s_b.id into s_b
                         from sb in s_b.DefaultIfEmpty()
                         select new LKUVR
                         {
                             id = main.id,
                             inventr = t.nov_inv,
                             MO = (m == null ? String.Empty : m.name),
                             fam = f.znach,
                             ima = i.znach,
                             otch = o.znach,
                             data_roj = main.data_rojd,
                             address_proj = a.znach,
                             tel = te.znach,
                             Fio_rod_zp = r.znach,
                             klass = main.klass,
                             sch_jit = sj.znach,
                             sch_baz = sb.znach,
                             kurs = db.Kurs.Where(x => x.id_main == main.id).ToList(),
                             FIO_ped = main.FIO_ped
                         });

            if (filtr.Filt.Nom != 0)
            {
                query = query.Where(p => p.id == filtr.Filt.Nom);


            }
            if (filtr.Filt.F != null)
            {
                query = from t in query
                        where t.fam.Contains(filtr.Filt.F)
                        select t;

            }
            if (filtr.Filt.I != null)
            {
                query = from t in query
                        where t.ima.Contains(filtr.Filt.I)
                        select t;

            }
            if (filtr.Filt.O != null)
            {
                query = from t in query
                        where t.otch.Contains(filtr.Filt.O)
                        select t;

            }

            if (filtr.Filt.DatNachDR != null || filtr.Filt.DatKoncDR != null)
                query = query.Where(p => p.data_roj >= filtr.Filt.DatNachDR && p.data_roj <= filtr.Filt.DatKoncDR);

            if (filtr.Filt.Add_proj != null)
            {
                query = from t in query
                        where t.address_proj.Contains(filtr.Filt.Add_proj)
                        select t;

            }
            if (filtr.Filt.Tel != null)
            {
                query = from t in query
                        where t.tel.Contains(filtr.Filt.Tel)
                        select t;

            }
            if (filtr.Filt.Klass != 0)
            {
                query = query.Where(p => p.klass == filtr.Filt.Klass);

            }
            if (filtr.Filt.BS != null)
            {
                query = from t in query
                        where t.sch_baz.Contains(filtr.Filt.BS)
                        select t;

            }
            if (filtr.Filt.MS != null)
            {
                query = from t in query
                        where t.sch_jit.Contains(filtr.Filt.MS)
                        select t;

            }

            if (filtr.Filt.Fio_ped != null)
            {
                query = from t in query
                        where t.FIO_ped.Contains(filtr.Filt.Fio_ped)
                        select t;

            }



            ViewData["Nom"] = sortOrder == SortState.NomAsc ? SortState.NomDesc : SortState.NomAsc;
            ViewData["In"] = sortOrder == SortState.InAsc ? SortState.InDesc : SortState.InAsc;
            ViewData["Mo"] = sortOrder == SortState.MoAsc ? SortState.MoDesc : SortState.MoAsc;
            ViewData["F"] = sortOrder == SortState.FAsc ? SortState.FDesc : SortState.FAsc;
            ViewData["I"] = sortOrder == SortState.IAsc ? SortState.IDesc : SortState.IAsc;
            ViewData["O"] = sortOrder == SortState.OAsc ? SortState.ODesc : SortState.OAsc;
            ViewData["DR"] = sortOrder == SortState.DRAsc ? SortState.DRDesc : SortState.DRAsc;

            ViewData["Add_p"] = sortOrder == SortState.Add_pAsc ? SortState.Add_pDesc : SortState.Add_pAsc;
            ViewData["Klass"] = sortOrder == SortState.KlassAsc ? SortState.KlassDesc : SortState.KlassAsc;
            ViewData["BS"] = sortOrder == SortState.BSAsc ? SortState.BSDesc : SortState.BSAsc;
            ViewData["MS"] = sortOrder == SortState.MSAsc ? SortState.MSDesc : SortState.MSAsc;
            ViewData["Fio_ped"] = sortOrder == SortState.Fio_pedAsc ? SortState.Fio_pedDesc : SortState.Fio_pedAsc;

            model.Listlk = query.ToList();

            switch (sortOrder)
            {
                /* case "1":
                     listotv.ListOtvetstvn = query.OrderBy(s => s.Id).ToList();
                     break;*/


                case SortState.NomAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.id).ToList();
                    break;
                case SortState.NomDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.id).ToList();
                    break;



                case SortState.InAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.inventr).ToList();
                    break;
                case SortState.InDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.inventr).ToList();
                    break;
                case SortState.MoAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.MO).ToList();
                    break;
                case SortState.MoDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.MO).ToList();
                    break;
                case SortState.FAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.fam).ToList();
                    break;
                case SortState.FDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.fam).ToList();
                    break;
                case SortState.IAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.ima).ToList();
                    break;
                case SortState.IDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.ima).ToList();
                    break;
                case SortState.OAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.otch).ToList();
                    break;
                case SortState.ODesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.otch).ToList();
                    break;
                case SortState.DRAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.data_roj).ToList();
                    break;
                case SortState.DRDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.data_roj).ToList();
                    break;
                case SortState.Add_pAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.address_proj).ToList();
                    break;
                case SortState.Add_pDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.address_proj).ToList();
                    break;
                case SortState.BSAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.sch_baz).ToList();
                    break;
                case SortState.BSDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.sch_baz).ToList();
                    break;
                case SortState.MSAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.sch_jit).ToList();
                    break;
                case SortState.MSDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.sch_jit).ToList();
                    break;
                case SortState.Fio_pedAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.FIO_ped).ToList();
                    break;
                case SortState.Fio_pedDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.FIO_ped).ToList();
                    break;
                case SortState.KlassAsc:
                    model.Listlk = model.Listlk.OrderBy(s => s.klass).ToList();
                    break;
                case SortState.KlassDesc:
                    model.Listlk = model.Listlk.OrderByDescending(s => s.klass).ToList();
                    break;

            }
            model.Filt = filtr.Filt;
            return View("LKUVR", model);
        }


        public IActionResult IstF(int id)
        {
            var query = from t in db.Ist
                        where t.id_main == id && t.kluch == "f"
                        select new { t.znach, t.data_izm };
            // Add user model
            return Json(query.ToArray());
        }
        public IActionResult Uch()
        {
            string Status = "учащийся";
            // Add user model
            return RedirectToAction("Sort", "Lk", new { status = Status });
        }
        public IActionResult Stud()
        {
            string Status = "студент";
            // Add user model
            return RedirectToAction("Sort", "Lk", new { status = Status });
        }
        public IActionResult Vibivsh()
        {
            string Status = "выбывший";
            // Add user model
            return RedirectToAction("Sort", "Lk", new { status = Status });
        }
        public IActionResult Vse()
        {
            string Status = null;
            // Add user model
            return RedirectToAction("Sort", "Lk", new { status = Status });
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
            try { model.diag = str.diagn; } catch { }
            try { model.Fio_rod = db.Ist.Find(str.id_fio_rod).znach; } catch { }
            try { model.Fio_rod_zp = db.Ist.Find(str.id_fio_rod_predst).znach; } catch { }
            try { model.data_sprav = Convert.ToDateTime(db.Ist.Find(str.id_srok_mse).znach); } catch { }
            try { model.prikaz = str.prik_o_zach_n; } catch { }
            try { model.prikaz_d = str.prik_o_zach_d; } catch { }
            try { model.klass = str.klass; } catch { }
            try { model.status = str.status; } catch { }
            try { model.tel = db.Ist.Find(str.id_tel).znach; } catch { }
            try { model.bvps = db.Bvp.Where(p => p.id_uo == str.id_uo).ToList(); } catch { }
            try { model.data_ust_oborud = db.To.Where(p => p.id == str.id_to).First().data_ust_o; } catch { }
            try { model.role = role; } catch { }

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
                try { model.tehot = db.To.Find(str.id_to); } catch { }
                //Достать паспортные только
                try { model.urot = db.Uo.Find(str.id_uo); } catch { }
            }
            if (role == 3)
            {

                try { model.soh_jit = db.Ist.Find(str.id_soh_jit).znach; } catch { }
                try { model.soh_baz = db.Ist.Find(str.id_soh_baz).znach; } catch { }
                try { model.tehot = db.To.Where(p => p.id == str.id_to).First(); } catch { }
                try { model.urot = db.Uo.Find(str.id_uo); } catch { }
                try { model.kursi = db.Kurs.Where(p => p.id_main == str.id).ToList(); } catch { }
                try { model.remonti = db.Rem.Where(p => p.id_to == str.id_to).ToList(); } catch { }
                try { model.internet = db.Inter.Where(p => p.id_to == str.id_to).ToList(); } catch { }
            }
            if (role == 4)
            {

                try { model.tehot = db.To.Find(str.id_to); } catch { }
                try { model.internet = db.Inter.Where(p => p.id_to == str.id_to).ToList(); } catch { }
                try { model.remonti = db.Rem.Where(p => p.id_to == str.id_to).ToList(); } catch { }
                try { model.urot = db.Uo.Find(str.id_uo); } catch { }
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
                try { model.urot = db.Uo.Find(str.id_uo); } catch { }
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

            string remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            /* ViewData["Message"] = remoteIpAddress;
             if (remoteIpAddress == "193.242.149.177" || remoteIpAddress == "193.242.149.14"|| remoteIpAddress == "::1")
             {
                 return View("ZayavEdit", model);

             }
             else
             {
                 return View("dost");
             }*/
            return View("ZayavEdit", model);
        }

        public IActionResult Save_Kurs(CompositeModel compositeModel)
        {
            if (compositeModel.kursadd.kurs_do != null)
            {
                compositeModel.kursadd.id_main = compositeModel.id;
                db.Entry(compositeModel.kursadd).State = EntityState.Added;
                db.SaveChanges();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id + "#j_kurs");
        }

        public IActionResult Save_Rem(CompositeModel compositeModel)
        {
            if (compositeModel.remadd.data_z_r != null)
            {
                compositeModel.remadd.id_to = compositeModel.tehot.id;
                db.Entry(compositeModel.remadd).State = EntityState.Added;
                db.SaveChanges();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id + "#j_rem");
        }

        public IActionResult Save_Int(CompositeModel compositeModel)
        {
            if (compositeModel.intadd.data_z_i != null)
            {
                compositeModel.intadd.id_to = compositeModel.tehot.id;
                db.Entry(compositeModel.intadd).State = EntityState.Added;
                db.SaveChanges();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id + "#j_int");
        }

        public IActionResult Save_Bvp(CompositeModel compositeModel)
        {
            if (compositeModel.bvpadd.nom_dog_bvp_d != null)
            {
                compositeModel.bvpadd.id_uo = compositeModel.urot.id;
                db.Entry(compositeModel.bvpadd).State = EntityState.Added;
                db.SaveChanges();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id + "#j_bvp");
        }
        [Route("Lk/Del_Kurs")]
        public async Task<IActionResult> Del_Kurs(int id, int id_main)
        {
            var product = db.Kurs.Find(id);
            if (product != null)
            {
                db.Kurs.Remove(product);
                await db.SaveChangesAsync();
            }
            return Redirect("/Lk/kartochka?id=" + id_main);
        }

        public async Task<IActionResult> Del_Rem(CompositeModel compositeModel)
        {
            var product = db.Rem.Find(compositeModel.remadd.Id);
            if (product != null)
            {
                db.Rem.Remove(product);
                await db.SaveChangesAsync();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id.ToString());
        }

        public async Task<IActionResult> Del_Int(CompositeModel compositeModel)
        {
            var product = db.Inter.Find(compositeModel.intadd.Id);
            if (product != null)
            {
                db.Inter.Remove(product);
                await db.SaveChangesAsync();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id.ToString());
        }

        public async Task<IActionResult> Del_Bvp(CompositeModel compositeModel)
        {
            var product = db.Bvp.Find(compositeModel.bvpadd.id);
            if (product != null)
            {
                db.Bvp.Remove(product);
                await db.SaveChangesAsync();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id.ToString());
        }

        public IActionResult Creat()
        {
            /*  var Login = HttpContext.User.Identity.Name;
              user user = db.User.Where(p => p.login == Login).First();*/
            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;
            if (role == 1 || role == 2)
            {
                to To = new to();
                db.Entry(To).State = EntityState.Added;
                uo Uo = new uo();
                db.Entry(Uo).State = EntityState.Added;
                db.SaveChanges();

                main Main = new main();
                Main.data_izm = DateTime.Now;
                Main.id_to = To.id;
                Main.id_uo = Uo.id;
                if (Main.data_sozd == Convert.ToDateTime("0001-01-01 00:00:00"))
                {
                    Main.data_sozd = DateTime.Now;
                }
                db.Entry(Main).State = EntityState.Added;
                db.SaveChanges();

                return Redirect("/Lk/kartochka?id=" + Main.id);
            }
            else
            {
                return Redirect("/Lk/Lk");
            }
        }
    }
}
