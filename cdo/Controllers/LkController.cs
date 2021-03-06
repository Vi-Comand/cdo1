﻿using cdo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace cdo.Controllers
{
    public class LkController : Controller
    {

        private DataContext db = new DataContext();
        [Authorize]
        public async Task<IActionResult> Vgr_LKPP()
        {







            // query data from database  
            await Task.Yield();

            var stream = new MemoryStream();



            ListLK list = new ListLK();
            list.Listlk = (from main in db.Main

                           join mo in db.Mo on main.id_mo equals mo.Id into mo
                           from m in mo.DefaultIfEmpty()
                           join inv in db.Sklad_to on main.id_sklad equals inv.Id into inv
                           from inven in inv.DefaultIfEmpty()
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
                               inventr = inven == null ? 0 : inven.nov_inv,
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
                               prikaz_iskl_d = main.prik_o_iskl_d,
                               prikaz = main.prik_o_zach_n,
                               srok_mse = ms.znach,
                               klass = main.klass,
                               tip_kompl = main.tip_kompl,
                               status = main.status

                           }).ToList();




            using (var package = new ExcelPackage(stream))
            {

                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list.Listlk, true);
                workSheet.Column(7).Style.Numberformat.Format = "dd.mm.yyyy";
                workSheet.Column(12).Style.Numberformat.Format = "dd.mm.yyyy";
                workSheet.Column(14).Style.Numberformat.Format = "dd.mm.yyyy";
                workSheet.Cells[1, 1].Value = "№";
                workSheet.Cells[1, 2].Value = "Инвентарный номер";
                workSheet.Cells[1, 3].Value = "МО";
                workSheet.Cells[1, 4].Value = "Фамилия";
                workSheet.Cells[1, 5].Value = "Имя";
                workSheet.Cells[1, 6].Value = "Отчество";
                workSheet.Cells[1, 7].Value = "Дата рождения";
                workSheet.Cells[1, 8].Value = "Адрес проживания";
                workSheet.Cells[1, 9].Value = "Телефон";
                workSheet.Cells[1, 10].Value = "ФИО законного представителя";
                workSheet.Cells[1, 11].Value = "Диагноз";
                workSheet.Cells[1, 12].Value = "Дата приказа";
                workSheet.Cells[1, 13].Value = "Номер приказа";
                workSheet.Cells[1, 14].Value = "Дата МСЭ";
                workSheet.Cells[1, 15].Value = "Класс";
                workSheet.Cells[1, 16].Value = "Тип комплекта";
                workSheet.Cells[1, 17].Value = "Статус";

                package.Save();
            }
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        }
        public async Task<IActionResult> ExportExcelAsync()
        {

            await Task.Yield();

            var stream = new MemoryStream();
            List<sklad_to> list = db.Sklad_to.ToList();
            using (var package = new ExcelPackage(stream))
            {

                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                workSheet.Column(3).Style.Numberformat.Format = "dd.mm.yyyy";
                workSheet.Column(7).Style.Numberformat.Format = "dd.mm.yyyy";
                workSheet.Column(8).Style.Numberformat.Format = "dd.mm.yyyy";
                workSheet.Cells[1, 1].Value = "№";
                workSheet.Cells[1, 2].Value = "Название комплекта";
                workSheet.Cells[1, 3].Value = "Дата установки оборудования";
                workSheet.Cells[1, 4].Value = "Старый инвентарник";
                workSheet.Cells[1, 5].Value = "Новый инвентарник";
                workSheet.Cells[1, 6].Value = "Стоимость";
                workSheet.Cells[1, 7].Value = "Дата возврата комплекта";
                workSheet.Cells[1, 8].Value = "Дата ввода комплекта";
                workSheet.Cells[1, 9].Value = "Статус";
                workSheet.Cells[1, 10].Value = "Претензия";
                workSheet.Cells[1, 11].Value = "Примечание";

                package.Save();
            }
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        }

        public ActionResult LKIstor(ListLK6 list)
        {

            var query = from t in db.Ist
                        where list.Filt.DataN <= t.data_izm && list.Filt.DataK >= t.data_izm
                        select t;


            int[] mas = (from t in query
                         where (list.Filt.F != false ? t.kluch == "f" : t.kluch == "") || (list.Filt.I != false ? t.kluch == "i" : t.kluch == "") || (list.Filt.O != false ? t.kluch == "o" : t.kluch == "") || (list.Filt.sb != false ? t.kluch == "sbaz" : t.kluch == "") || (list.Filt.sj != false ? t.kluch == "sjit" : t.kluch == "") || (list.Filt.Tel != false ? t.kluch == "tel" : t.kluch == "") || (list.Filt.mse != false ? t.kluch == "srmse" : t.kluch == "") || (list.Filt.Fio_rod != false ? t.kluch == "fior" : t.kluch == "") || (list.Filt.Fio_rod_zp != false ? t.kluch == "fiozp" : t.kluch == "") || (list.Filt.add_p != false ? t.kluch == "adrp" : t.kluch == "") || (list.Filt.add_r != false ? t.kluch == "adrr" : t.kluch == "")
                         select t.id_main).ToArray();
            list.Listlk = (from main in db.Main.Where(p => mas.Distinct().Contains(p.id))

                           join mo in db.Mo on main.id_mo equals mo.Id into mo
                           from m in mo.DefaultIfEmpty()
                           join inv in db.Sklad_to on main.id_sklad equals inv.Id into inv
                           from inven in inv.DefaultIfEmpty()
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
                               inventr = (inven == null ? 0 : inven.nov_inv),
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
                               prikaz_iskl_d = main.prik_o_iskl_d,
                               prikaz = main.prik_o_zach_n,
                               srok_mse = ms.znach,
                               klass = main.klass,
                               tip_kompl = main.tip_kompl,
                               status = main.status

                           }).ToList();

            return View("Istor", list);
        }



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
                               join inv in db.Sklad_to on main.id_sklad equals inv.Id into inv
                               from inven in inv.DefaultIfEmpty()
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
                                   inventr = (inven == null ? 0 : inven.nov_inv),
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
                                   prikaz_iskl_d = main.prik_o_iskl_d,
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
                               join inv in db.Sklad_to on main.id_sklad equals inv.Id into inven
                               from inv in inven.DefaultIfEmpty()
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
                                   inventr = (inv == null ? 0 : inv.nov_inv),
                                   MO = (m == null ? String.Empty : m.name),
                                   fam = f.znach,
                                   ima = i.znach,
                                   otch = o.znach,
                                   data_roj = main.data_rojd.Date,
                                   address_proj = a.znach,
                                   tel = te.znach,
                                   Fio_rod_zp = r.znach,
                                   klass = main.klass,
                                   sch_jit = sj.znach,
                                   sch_baz = sb.znach,
                                   kurs = db.Kurs.Where(x => x.id_main == main.id).ToList(),
                                   FIO_ped = main.FIO_ped,
                                   status = main.status
                               }).ToList();
                list.Filt = new Filters();
                return View("LKUVR", list);

            }
            if (role == 4 || role == 7)
            {

                ListLK2 list = new ListLK2();
                list.Listlk = (from main in db.Main

                               join mo in db.Mo on main.id_mo equals mo.Id into mo
                               from m in mo.DefaultIfEmpty()
                               join inve in db.Sklad_to on main.id_sklad equals inve.Id into inven
                               from inv in inven.DefaultIfEmpty()
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


                               select new LKTO
                               {
                                   id = main.id,
                                   inventr = (inv == null ? 0 : inv.nov_inv),
                                   nazv_kompl = inv.nazv_komp,
                                   MO = (m == null ? String.Empty : m.name),
                                   fam = f.znach,
                                   ima = i.znach,
                                   otch = o.znach,

                                   address_proj = a.znach,
                                   tel = te.znach,
                                   Fio_rod_zp = r.znach,
                                   tip_kompl = main.tip_kompl


                               }).ToList();
                ViewBag.rl = role;
                list.Filt = new FilterLKTO();
                return View("LKTO", list);

            }
            if (role == 5)
            {
                ListLK list = new ListLK();
                list.Listlk = (from main in db.Main

                               join mo in db.Mo on main.id_mo equals mo.Id into mo
                               from m in mo.DefaultIfEmpty()
                               join inv in db.Sklad_to on main.id_sklad equals inv.Id into inven
                               from inv in inven.DefaultIfEmpty()
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
                                   inventr = (inv == null ? 0 : inv.nov_inv),
                                   MO = (m == null ? String.Empty : m.name),
                                   fam = f.znach,
                                   ima = i.znach,
                                   otch = o.znach,
                                   data_roj = main.data_rojd.Date,
                                   address_proj = a.znach,
                                   tel = te.znach,
                                   Fio_rod_zp = r.znach,
                                   prikaz_iskl_d = main.prik_o_iskl_d,
                                   prikazd = main.prik_o_zach_d,
                                   prikaz = main.prik_o_zach_n,
                                   srok_mse = ms.znach,
                                   klass = main.klass,
                                   tip_kompl = main.tip_kompl,
                                   status = main.status

                               }).ToList();
                list.Filt = new Filters();
                return View("LKUO", list);

            }
            if (role == 6)
            {
                ListLK list = new ListLK();
                list.Listlk = (from main in db.Main

                               join mo in db.Mo on main.id_mo equals mo.Id into mo
                               from m in mo.DefaultIfEmpty()
                               join inv in db.Sklad_to on main.id_sklad equals inv.Id into inven
                               from inv in inven.DefaultIfEmpty()
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
                                   inventr = (inv == null ? 0 : inv.nov_inv),
                                   MO = (m == null ? String.Empty : m.name),
                                   fam = f.znach,
                                   ima = i.znach,
                                   otch = o.znach,
                                   data_roj = main.data_rojd.Date,
                                   address_proj = a.znach,
                                   tel = te.znach,
                                   Fio_rod_zp = r.znach,
                                   prikaz_iskl_d = main.prik_o_iskl_d,
                                   prikazd = main.prik_o_zach_d,
                                   prikaz = main.prik_o_zach_n,
                                   srok_mse = ms.znach,
                                   klass = main.klass,
                                   tip_kompl = main.tip_kompl,
                                   status = main.status

                               }).ToList();
                list.Filt = new Filters();
                ViewBag.rl = role;
                return View("LKBO", list);

            }
            else
            {
                ListLK list = new ListLK();
                list.Listlk = (from main in db.Main

                               join mo in db.Mo on main.id_mo equals mo.Id into mo
                               from m in mo.DefaultIfEmpty()
                               join inv in db.Sklad_to on main.id_sklad equals inv.Id into inven
                               from inv in inven.DefaultIfEmpty()
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
                                   inventr = (inv == null ? 0 : inv.nov_inv),
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
                                   prikaz_iskl_d = main.prik_o_iskl_d,
                                   prikaz = main.prik_o_zach_n,
                                   srok_mse = ms.znach,
                                   klass = main.klass,
                                   tip_kompl = main.tip_kompl,
                                   status = main.status

                               }).ToList();
                list.Filt = new Filters();
                ViewBag.rl = role;
                return View("obch", list);

            }

        }
        public IActionResult LKTech(ListLK2 list)
        {
            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;
            if (list == null)
                list = new ListLK2();

            var query = (from main in (list.Filt.MO != 0 ? db.Main.Where(x => x.id_mo == list.Filt.MO) : db.Main)

                         join mo in db.Mo on main.id_mo equals mo.Id into mo
                         from m in mo.DefaultIfEmpty()
                         join inve in db.Sklad_to on main.id_sklad equals inve.Id into inven
                         from inv in inven.DefaultIfEmpty()
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


                         select new LKTO
                         {
                             id = main.id,
                             inventr = (inv == null ? 0 : inv.nov_inv),
                             nazv_kompl = inv.nazv_komp,
                             MO = (m == null ? String.Empty : m.name),
                             fam = f.znach,
                             ima = i.znach,
                             otch = o.znach,

                             address_proj = a.znach,
                             tel = te.znach,
                             Fio_rod_zp = r.znach,
                             tip_kompl = main.tip_kompl


                         });




            if (list.Filt == null)
                list.Filt = new FilterLKTO();
            else
            {


                if (list.Filt.Nom != 0)
                {
                    query = query.Where(p => p.id == list.Filt.Nom);


                }
                if (list.Filt.F != null)
                {
                    query = from t in query
                            where t.fam.Contains(list.Filt.F)
                            select t;

                }
                if (list.Filt.I != null)
                {
                    query = from t in query
                            where t.ima.Contains(list.Filt.I)
                            select t;

                }
                if (list.Filt.O != null)
                {
                    query = from t in query
                            where t.otch.Contains(list.Filt.O)
                            select t;

                }

                if (list.Filt.Fio_rod_zp != null)
                {
                    query = from t in query
                            where t.Fio_rod_zp.Contains(list.Filt.Fio_rod_zp)
                            select t;

                }

                if (list.Filt.Add_proj != null)
                {
                    query = from t in query
                            where t.address_proj.Contains(list.Filt.Add_proj)
                            select t;

                }
                if (list.Filt.Tel != null)
                {
                    query = from t in query
                            where t.tel.Contains(list.Filt.Tel)
                            select t;

                }
                if (list.Filt.inv != 0)
                {
                    query = from t in query
                            where t.inventr == list.Filt.inv
                            select t;

                }

                if (list.Filt.nazv_kompl != null)
                {
                    query = from t in query
                            where t.tip_kompl.Contains(list.Filt.nazv_kompl)
                            select t;

                }

            }
            ViewBag.rl = role;
            list.Listlk = query.ToList();
            return View("LKTO", list);
        }
        public IActionResult Rem(ListLK5 list)
        {
            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;
            var query = (from remon in db.Rem

                         join ma in db.Main on remon.id_to equals ma.id_to into mo
                         from m in mo.DefaultIfEmpty()
                         join moo in db.Mo on m.id_mo equals moo.Id into moo
                         from mm in moo.DefaultIfEmpty()
                         join ist in db.Ist on m.id_f equals ist.id into ist
                         from f in ist.DefaultIfEmpty()
                         join im in db.Ist on m.id_i equals im.id into im
                         from i in im.DefaultIfEmpty()
                         join ot in db.Ist on m.id_o equals ot.id into ot
                         from o in ot.DefaultIfEmpty()
                         join rod in db.Ist on m.id_fio_rod_predst equals rod.id into rod
                         from r in rod.DefaultIfEmpty()
                         join add in db.Ist on m.id_adr_progiv equals add.id into add
                         from a in add.DefaultIfEmpty()
                         select new LKRem
                         {
                             rem = remon,
                             id_main = (m == null ? 0 : m.id),
                             fam = f.znach,
                             ima = i.znach,
                             otch = o.znach,
                             Fio_rod_zp = r.znach,
                             MO = mm.name,
                             address_proj = a.znach,

                         });
            if (list == null)
                list = new ListLK5();

            if (list.Filt == null)
                list.Filt = new FilterRem();
            else
            {
                if (list.Filt.DatKoncZ.ToString() == "01.01.0001 0:00:00")
                { list.Filt.DatKoncZ = DateTime.Now.AddYears(10); }
                if (list.Filt.DatKoncV.ToString() == "01.01.0001 0:00:00")
                { list.Filt.DatKoncV = DateTime.Now.AddYears(10); }



                if (list.Filt.Nom != 0)
                {
                    query = query.Where(p => p.rem.Id == list.Filt.Nom);


                }

                if (list.Filt.FIO_prin != null)
                {
                    query = from t in query
                            where t.rem.fio_prin_r.Contains(list.Filt.FIO_prin)
                            select t;

                }
                if (list.Filt.FIO_vipol != null)
                {
                    query = from t in query
                            where t.rem.fio_vipol_r.Contains(list.Filt.FIO_vipol)
                            select t;

                }
                if (list.Filt.Status != null)
                {
                    query = from t in query
                            where t.rem.status_r.Contains(list.Filt.Status)
                            select t;

                }
                if (list.Filt.prim == true)
                {
                    query = from t in query
                            where t.rem.prim_r != "" && t.rem.prim_r != null
                            select t;

                }
                if (list.Filt.neisp == true)
                {
                    query = from t in query
                            where t.rem.prich_r != "" && t.rem.prich_r != null
                            select t;

                }
                if (list.Filt.zamena == true)
                {
                    query = from t in query
                            where t.rem.zamena != false
                            select t;

                }
                if (list.Filt.viezd == true)
                {
                    query = from t in query
                            where t.rem.viezd != false
                            select t;

                }
                if (list.Filt.DatNachZ != null || list.Filt.DatKoncZ != null)
                    query = query.Where(p => p.rem.data_z_r >= list.Filt.DatNachZ && p.rem.data_z_r <= list.Filt.DatKoncZ);


                if (list.Filt.DatNachV != null || list.Filt.DatKoncV != null)
                    query = query.Where(p => p.rem.data_v_r >= list.Filt.DatNachV && p.rem.data_v_r <= list.Filt.DatKoncV);


            }


            ViewBag.rl = role;
            list.Listlk = query.ToList();
            list.Listlk = list.Listlk.OrderByDescending(s => s.rem.data_z_r).ToList();
            return View("LKRem", list);

        }
        public IActionResult Inter(ListLK4 list)
        {

            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;



            var query = (from inter in db.Inter

                         join ma in db.Main on inter.id_to equals ma.id_to into mo
                         from m in mo.DefaultIfEmpty()
                         join moo in db.Mo on m.id_mo equals moo.Id into moo
                         from mm in moo.DefaultIfEmpty()
                         join ist in db.Ist on m.id_f equals ist.id into ist
                         from f in ist.DefaultIfEmpty()
                         join im in db.Ist on m.id_i equals im.id into im
                         from i in im.DefaultIfEmpty()
                         join ot in db.Ist on m.id_o equals ot.id into ot
                         from o in ot.DefaultIfEmpty()
                         join rod in db.Ist on m.id_fio_rod_predst equals rod.id into rod
                         from r in rod.DefaultIfEmpty()
                         join add in db.Ist on m.id_adr_progiv equals add.id into add
                         from a in add.DefaultIfEmpty()
                         select new LKInter
                         {
                             internet = inter,
                             id_main = (m == null ? 0 : m.id),
                             fam = f.znach,
                             ima = i.znach,
                             otch = o.znach,
                             Fio_rod_zp = r.znach,
                             MO = mm.name,
                             address_proj = a.znach,

                         });








            if (list == null)
                list = new ListLK4();

            if (list.Filt == null)
                list.Filt = new FilterInter();
            else
            {
                if (list.Filt.DatKoncZ.ToString() == "01.01.0001 0:00:00")
                { list.Filt.DatKoncZ = DateTime.Now.AddYears(10); }
                if (list.Filt.DatKoncV.ToString() == "01.01.0001 0:00:00")
                { list.Filt.DatKoncV = DateTime.Now.AddYears(10); }



                if (list.Filt.Nom != 0)
                {
                    query = query.Where(p => p.internet.Id == list.Filt.Nom);


                }

                if (list.Filt.FIO_prin != null)
                {
                    query = from t in query
                            where t.internet.fio_prin_i.Contains(list.Filt.FIO_prin)
                            select t;

                }

                if (list.Filt.prim == true)
                {
                    query = from t in query
                            where t.internet.prim_i != "" && t.internet.prim_i != null
                            select t;
                }

                if (list.Filt.neisp == true)
                {
                    query = from t in query
                            where t.internet.zayav_neisp_i != "" && t.internet.zayav_neisp_i != null
                            select t;
                }
                if (list.Filt.DatNachZ != null || list.Filt.DatKoncZ != null)
                    query = query.Where(p => p.internet.data_z_i >= list.Filt.DatNachZ && p.internet.data_z_i <= list.Filt.DatKoncZ);


                if (list.Filt.DatNachV != null || list.Filt.DatKoncV != null)
                    query = query.Where(p => p.internet.data_v_i >= list.Filt.DatNachV && p.internet.data_v_i <= list.Filt.DatKoncV);


            }



            list.Listlk = query.ToList();
            list.Listlk = list.Listlk.OrderByDescending(s => s.internet.data_z_i).ToList();
            ViewBag.rl = role;
            return View("LKInter", list);

        }
        public IActionResult Sklad(ListLK3 list)
        {
            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;


            var query = (from sk in db.Sklad_to

                         join ma in db.Main on sk.Id equals ma.id_sklad into mo
                         from m in mo.DefaultIfEmpty()

                         select new LKSKlad
                         {
                             sklad = sk,
                             id_main = (m == null ? 0 : m.id)

                         });

            if (list == null)
                list = new ListLK3();

            if (list.Filt == null)
                list.Filt = new FilterSklad();
            else
            {
                if (list.Filt.DatKoncU.ToString() == "01.01.0001 0:00:00")
                { list.Filt.DatKoncU = DateTime.Now.AddYears(10); }
                if (list.Filt.DatKoncV.ToString() == "01.01.0001 0:00:00")
                { list.Filt.DatKoncV = DateTime.Now.AddYears(10); }
                if (list.Filt.DatKoncVV.ToString() == "01.01.0001 0:00:00")
                { list.Filt.DatKoncVV = DateTime.Now.AddYears(10); }



                if (list.Filt.Nom != 0)
                {
                    query = query.Where(p => p.sklad.Id == list.Filt.Nom);


                }
                if (list.Filt.Nazv_kompl != null)
                {
                    query = from t in query
                            where t.sklad.nazv_komp.Contains(list.Filt.Nazv_kompl)
                            select t;

                }
                if (list.Filt.nov_inv != 0)
                {
                    query = query.Where(p => p.sklad.nov_inv == list.Filt.nov_inv);


                }
                if (list.Filt.star_inv != 0)
                {
                    query = from t in query
                            where t.sklad.star_inv == list.Filt.star_inv
                            select t;

                }
                if (list.Filt.Status != null)
                {
                    query = from t in query
                            where t.sklad.status == list.Filt.Status
                            select t;

                }
                if (list.Filt.prim == true)
                {
                    query = from t in query
                            where t.sklad.prim != "" && t.sklad.prim != null
                            select t;

                }
                if (list.Filt.pret == true)
                {
                    query = from t in query
                            where t.sklad.pritenz != "" && t.sklad.pritenz != null
                            select t;

                }
                if (list.Filt.DatNachU != null || list.Filt.DatKoncU != null)
                    query = query.Where(p => p.sklad.data_ust_o >= list.Filt.DatNachU && p.sklad.data_ust_o <= list.Filt.DatKoncU);


                if (list.Filt.DatNachV != null || list.Filt.DatKoncV != null)
                    query = query.Where(p => p.sklad.data_vozvr_kompl >= list.Filt.DatNachV && p.sklad.data_vozvr_kompl <= list.Filt.DatKoncV);

                if (list.Filt.DatNachVV != null || list.Filt.DatKoncVV != null)
                    query = query.Where(p => p.sklad.data_vvoda_kompl >= list.Filt.DatNachVV && p.sklad.data_vvoda_kompl <= list.Filt.DatKoncVV);


            }

            ViewBag.rl = role;
            list.Listlk = query.ToList();
            return View("LKSklad", list);

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
            try { model.data_sprav = db.Ist.Find(str.id_srok_mse).znach; } catch { }
            var login = HttpContext.User.Identity.Name;
            int id_user = db.User.Where(p => p.login == login).First().id;
            int role = db.User.Where(p => p.login == login).First().role;
            bool modif = false;
            if (role == 1 || role == 2)
            {
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
                    pole.znach = composit.data_sprav;
                    pole.role = role;
                    pole.data_izm = DateTime.Now;
                    db.Entry(pole).State = EntityState.Added;

                    db.SaveChanges();
                    str.id_srok_mse = pole.id;
                }
                if (str.prik_o_zach_d != composit.prikaz_d)
                    str.prik_o_zach_d = composit.prikaz_d;
                if (str.prik_o_iskl_d != composit.prikaz_iskl_d)
                    str.prik_o_iskl_d = composit.prikaz_iskl_d;
                if (str.prik_o_zach_n != composit.prikaz)
                    str.prik_o_zach_n = composit.prikaz;
                if (str.status != composit.status)
                    str.status = composit.status;
                if (str.tip_kompl != composit.tip_kompl)
                    str.tip_kompl = composit.tip_kompl;
                if (str.diagn != composit.diag)
                    str.diagn = composit.diag;
                if (str.id_mo != composit.MO)
                    str.id_mo = composit.MO;
                if (str.data_rojd != composit.data_roj)
                    str.data_rojd = composit.data_roj;
                if (composit.urot != null && role == 2)

                    try
                    {

                        uo row = composit.urot;
                        uo prov = db.Uo.Find(row.id);
                        if (row.id_dvij_dog_bvp != prov.id_dvij_dog_bvp || row.kod_p != prov.kod_p || row.nom_p != prov.nom_p || row.propis_p != prov.propis_p || row.rogd_p != prov.rogd_p || row.ser_p != prov.ser_p || row.vidan_p != prov.vidan_p || row.data_p != prov.data_p || row.data_roj != prov.data_roj || row.dop_sogl_bvp_d != prov.dop_sogl_bvp_d || row.dop_sogl_bvp_n != prov.dop_sogl_bvp_n)
                        {
                            row.id_dvij_dog_bvp = prov.id_dvij_dog_bvp;
                            row.dop_sogl_bvp_d = prov.dop_sogl_bvp_d;
                            row.dop_sogl_bvp_n = prov.dop_sogl_bvp_n;

                            db.Entry(prov).State = EntityState.Detached;
                            db.Entry(row).State = EntityState.Modified;
                            db.SaveChanges();
                            modif = true;
                        }
                    }
                    catch { }
            }
            if (role == 1 || role == 3)
            {
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
                if (str.klass != composit.klass)
                    str.klass = composit.klass;
                if (str.FIO_ped != composit.FIO_ped)
                    str.FIO_ped = composit.FIO_ped;
            }






            if (role == 1 || role == 4 || role == 7)
            {
                if (role == 1 || role == 4)
                {
                    if (composit.tehot != null)
                    {
                        try
                        {
                            to row = composit.tehot;
                            to prov = db.To.Find(row.id);
                            if (row.inter != prov.inter || row.kompl != prov.kompl || row.skype_l != prov.skype_l || row.skype_p != prov.skype_p || row.date_inter != prov.date_inter)
                            {

                                db.Entry(prov).State = EntityState.Detached;
                                db.Entry(row).State = EntityState.Modified;

                                db.SaveChanges();
                                modif = true;
                            }
                        }
                        catch { }
                    }
                }
                if (composit.remonti != null)
                {

                    foreach (rem row in composit.remonti)
                    {
                        rem prov = db.Rem.Find(row.Id);
                        if (row.id_to != prov.id_to || row.prich_r != prov.prich_r || row.prim_r != prov.prim_r || row.status_r != prov.status_r || row.viezd != prov.viezd || row.zamena != prov.zamena || row.data_v_r != prov.data_v_r || row.data_z_r != prov.data_z_r || row.fio_prin_r != prov.fio_prin_r || row.fio_vipol_r != prov.fio_vipol_r)
                        {

                            db.Entry(prov).State = EntityState.Detached;
                            db.Entry(row).State = EntityState.Modified;

                            db.SaveChanges();
                            modif = true;
                        }
                    }



                }
                if (composit.internet != null)
                {
                    try
                    {
                        foreach (inter row in composit.internet)
                        {


                            inter prov = db.Inter.Find(row.Id);
                            if (row.id_to != prov.id_to || row.zayav_neisp_i != prov.zayav_neisp_i || row.prim_i != prov.prim_i || row.data_v_i != prov.data_v_i || row.data_z_i != prov.data_z_i || row.fio_prin_i != prov.fio_prin_i)
                            {

                                db.Entry(prov).State = EntityState.Detached;


                                db.Entry(row).State = EntityState.Modified;
                                db.SaveChanges();
                                modif = true;
                            }
                        }

                    }
                    catch { }
                }

            }
            if (role == 1 || role == 5)
            {
                if (composit.urot != null)
                {
                    try
                    {

                        uo row = composit.urot;
                        uo prov = db.Uo.Find(row.id);
                        if (row.id_dvij_dog_bvp != prov.id_dvij_dog_bvp || row.kod_p != prov.kod_p || row.nom_p != prov.nom_p || row.propis_p != prov.propis_p || row.rogd_p != prov.rogd_p || row.ser_p != prov.ser_p || row.vidan_p != prov.vidan_p || row.data_p != prov.data_p || row.data_roj != prov.data_roj || row.dop_sogl_bvp_d != prov.dop_sogl_bvp_d || row.dop_sogl_bvp_n != prov.dop_sogl_bvp_n)
                        {

                            db.Entry(prov).State = EntityState.Detached;
                            db.Entry(row).State = EntityState.Modified;
                            db.SaveChanges();
                            modif = true;
                        }
                    }
                    catch { }
                }
                if (composit.bvps != null)
                {

                    foreach (bvp row in composit.bvps)
                    {
                        bvp prov = db.Bvp.Find(row.id);
                        if (row.prik_o_oborud != prov.prik_o_oborud || row.prik_o_oborud_d != prov.prik_o_oborud_d || row.data_ust_oborud != prov.data_ust_oborud || row.nom_dog_bvp != prov.nom_dog_bvp || row.nom_dog_bvp_d != prov.nom_dog_bvp_d || row.srok_dog_bvp != prov.srok_dog_bvp || row.akt_vozvr_oborud != prov.akt_vozvr_oborud || row.dop_obor != prov.dop_obor || row.data_u_dop_o != prov.data_u_dop_o || row.data_v_dop_o != prov.data_v_dop_o)
                        {

                            db.Entry(prov).State = EntityState.Detached;
                            db.Entry(row).State = EntityState.Modified;

                            db.SaveChanges();
                            modif = true;
                        }
                    }
                }
                if (composit.dop_Dogs != null)
                {

                    foreach (dop_dog row in composit.dop_Dogs)
                    {
                        dop_dog prov = db.Dop_Dog.Find(row.id);
                        if (row.dop_sogl_bvp_n != prov.dop_sogl_bvp_n || row.dop_sogl_bvp_d != prov.dop_sogl_bvp_d || row.soderg != prov.soderg)
                        {
                            db.Entry(prov).State = EntityState.Detached;
                            db.Entry(row).State = EntityState.Modified;
                            db.SaveChanges();
                            modif = true;
                        }
                    }
                }
            }

            if (modif)
            {
                str.data_izm = DateTime.Now;
            }


            if (db.Entry(str).State == EntityState.Modified)
            {
                str.data_izm = DateTime.Now;
                db.Entry(str).State = EntityState.Modified;
                db.SaveChanges();
            }


            //return Redirect("/Lk/kartochka?id=" + str.id  + "#j_rem");
            return Redirect("/Lk/Lk" + "#id_" + str.id);

        }
        
        private ListLK FiltSort(SortState sortOrder, ListLK filtr, string status, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17, string f18)
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
                    filtr.Filt.Fio_rod_zp = f12;
                filtr.Filt.Nom = f13;
                if (f14 != null)
                    filtr.Filt.O = f14;
                if (f15 != null)
                    filtr.Filt.prikaz = f15;
                if (f16 != null)
                    filtr.Filt.Tel = f16;
                if (f17 != null)
                    filtr.Filt.tip_kompl = f17;
                if (f18 != null)
                            filtr.Filt.Status = f18;
                
            }
           
           // else
            //if (filtr.Filt.Status != null)
            //{ filtr.Filt.Status = null; }

            ListLK model = new ListLK();
            var query = (from main in (filtr.Filt.MO != 0 ? db.Main.Where(x => x.id_mo == filtr.Filt.MO) : db.Main)

                         join mo in db.Mo on main.id_mo equals mo.Id into mo
                         from m in mo.DefaultIfEmpty()

                         join ist in db.Ist on main.id_f equals ist.id into ist
                         from f in ist.DefaultIfEmpty()
                         join inv in db.Sklad_to on main.id_sklad equals inv.Id into inv
                         from inven in inv.DefaultIfEmpty()
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
                             inventr = (inven == null ? 0 : inven.nov_inv),
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
                             prikaz_iskl_d = main.prik_o_iskl_d,
                             prikaz = main.prik_o_zach_n,
                             srok_mse = ms==null? "01.01.2000 0:00:00": ms.znach,
                klass = main.klass,
                 tip_kompl = main.tip_kompl,
                 status = main.status

             });


            if (filtr.Filt.Status != null)
            {
                query = query.Where(p => p.status == filtr.Filt.Status);
            }
            if (filtr.Filt.inv != 0)
            {
                query = query.Where(p => p.inventr == filtr.Filt.inv);
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

            if (filtr.Filt.Fio_rod_zp != null)
            {
                query = from t in query
                        where t.Fio_rod_zp.Contains(filtr.Filt.Fio_rod_zp)
                        select t;

            }
            if (filtr.Filt.DatKoncDR == Convert.ToDateTime("01.01.0001 0:00:00"))
                filtr.Filt.DatKoncDR = DateTime.Now;
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

            ViewData["Nom"] = sortOrder == SortState.NomAsc? SortState.NomDesc : SortState.NomAsc;
ViewData["In"] = sortOrder == SortState.InAsc? SortState.InDesc : SortState.InAsc;
ViewData["Mo"] = sortOrder == SortState.MoAsc? SortState.MoDesc : SortState.MoAsc;
ViewData["F"] = sortOrder == SortState.FAsc? SortState.FDesc : SortState.FAsc;
ViewData["I"] = sortOrder == SortState.IAsc? SortState.IDesc : SortState.IAsc;
ViewData["O"] = sortOrder == SortState.OAsc? SortState.ODesc : SortState.OAsc;
ViewData["DR"] = sortOrder == SortState.DRAsc? SortState.DRDesc : SortState.DRAsc;

ViewData["Add_p"] = sortOrder == SortState.Add_pAsc? SortState.Add_pDesc : SortState.Add_pAsc;
ViewData["D"] = sortOrder == SortState.DAsc? SortState.DDesc : SortState.DAsc;
ViewData["MSE"] = sortOrder == SortState.MSEAsc? SortState.MSEDesc : SortState.MSEAsc;
ViewData["Nom_p"] = sortOrder == SortState.Nom_pAsc? SortState.Nom_pDesc : SortState.Nom_pAsc;
ViewData["Data_p"] = sortOrder == SortState.Data_pAsc? SortState.Data_pDesc : SortState.Data_pAsc;
ViewData["Klass"] = sortOrder == SortState.KlassAsc? SortState.KlassDesc : SortState.KlassAsc;
ViewData["Tip_kompl"] = sortOrder == SortState.Tip_komplAsc? SortState.Tip_komplDesc : SortState.Tip_komplAsc;
ViewData["Status"] = sortOrder == SortState.StatusAsc? SortState.StatusDesc : SortState.StatusAsc;
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
            return model;
        }

        public async Task<IActionResult> Sort(SortState sortOrder, ListLK filtr, string status, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17, string f18)
        {
          
            return View("obch", FiltSort(sortOrder,  filtr,  status,  f1,  f2,  f3,  f4,  f5,  f6,  f7,  f8,  f9,  f10,  f11,  f12,  f13,  f14,  f15,  f16,  f17, f18));
        }

        public ActionResult LKUr(SortState sortOrder, ListLK filtr, string status, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17, string f18)
        {
           
            return View("LKUO", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
        }




        public ActionResult LKBuch(SortState sortOrder, ListLK filtr, string status, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17, string f18)
        {
          
            return View("LKBO", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
        }










        public async Task<IActionResult> Sort1(SortState sortOrder, ListLK filtr, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17, int f18, string f19, string f20, string f21, string f22)
        {

            if (filtr.Filt == null)
            {
                filtr.Filt = new Filters();
                if (f2.ToString() == "01.01.0001 0:00:00")
                    f2 = DateTime.Now;
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
                    filtr.Filt.Fio_rod_zp = f12;
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
            var query = (from main in (filtr.Filt.MO != 0 ? db.Main.Where(x => x.id_mo == filtr.Filt.MO) : db.Main)

                         join mo in db.Mo on main.id_mo equals mo.Id into mo
                         from m in mo.DefaultIfEmpty()
                         join inv in db.Sklad_to on main.id_sklad equals inv.Id into inv
                         from inven in inv.DefaultIfEmpty()
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
                             inventr = (inven == null ? 0 : inven.nov_inv),
                             MO = (m == null ? String.Empty : m.name),
                             fam = f.znach,
                             ima = i.znach,
                             otch = o.znach,
                             data_roj = main.data_rojd.Date,
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
            if (filtr.Filt.inv != 0)
            {
                query = query.Where(p => p.inventr == filtr.Filt.inv);

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

            if (filtr.Filt.Fio_rod_zp != null)
            {
                query = from t in query
                        where t.Fio_rod_zp.Contains(filtr.Filt.Fio_rod_zp)
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
        public IActionResult IstI(int id)
        {
            var query = from t in db.Ist
                        where t.id_main == id && t.kluch == "i"
                        select new { t.znach, t.data_izm };
            // Add user model
            return Json(query.ToArray());
        }
        public IActionResult SpisokPrivyaz(int id_komp)
        {

            var query = (from ist_kom in db.Ist_Kompl.Where(p => p.id_kompl == id_komp)




                         select new ist_kompl
                         {
                             id_kompl = ist_kom.id_kompl,
                             id_main = ist_kom.id_main,
                             data_p = ist_kom.data_p

                         });


            return Json(query.ToArray());
        }

        public IActionResult SpisokToSkald()
        {

            var query = (from main in db.Main.Where(p => p.id_sklad == 0)

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


                         select new LKPP
                         {
                             id = main.id,
                             MO = (m == null ? String.Empty : m.name),
                             fam = f.znach,
                             ima = i.znach,
                             otch = o.znach,

                             address_proj = a.znach,
                             tel = te.znach,
                             Fio_rod_zp = r.znach,
                             status = main.status

                         });


            return Json(query.ToArray());
        }
        public IActionResult SvyazSkladToUser(int id, int id_s)
        {
            main str = db.Main.Find(id);
            str.id_sklad = id_s;
            db.Entry(str).State = EntityState.Modified;
            db.SaveChanges();
            // Add user model
            sklad_to ss = db.Sklad_to.Find(id_s);
            ss.status = "Назначен";

            db.Entry(ss).State = EntityState.Modified;
            db.SaveChanges();

            ist_kompl istk = new ist_kompl();
            istk.id_kompl = id_s;
            istk.id_main = id;
            istk.data_p = DateTime.Now;
            db.Ist_Kompl.Add(istk);
            db.SaveChanges();
            return RedirectToAction("Sklad");
        }

        public IActionResult SvyazSkladToUser1(int id, int id_s)
        {
            main str = db.Main.Find(id);
            str.id_sklad = id_s;

            db.Entry(str).State = EntityState.Modified;
            db.SaveChanges();

            sklad_to ss = db.Sklad_to.Find(id_s);
            ss.status = "Назначен";

            db.Entry(ss).State = EntityState.Modified;
            db.SaveChanges();
            // Add user model

            return RedirectToAction("KartochKompl", new { id = id_s });
        }

        public IActionResult NewKompl()
        {
            sklad_to str = new sklad_to();
            db.Sklad_to.Add(str);
            db.Entry(str).State = EntityState.Added;

            db.SaveChanges();
            // Add user model
            sklad sklad = new sklad();
            sklad.sk = str;

            return View("SkladEdit", sklad);
        }

        public IActionResult SaveKompl(sklad composit)
        {
            sklad_to str = db.Sklad_to.Find(composit.sk.Id);
            str.nazv_komp = composit.sk.nazv_komp;
            str.data_ust_o = composit.sk.data_ust_o;
            str.star_inv = composit.sk.star_inv;
            str.nov_inv = composit.sk.nov_inv;
            str.stoim = composit.sk.stoim;
            str.data_vozvr_kompl = composit.sk.data_vozvr_kompl;
            str.data_vvoda_kompl = composit.sk.data_vvoda_kompl;
            str.status = composit.sk.status;
            str.pritenz = composit.sk.pritenz;
            str.prim = composit.sk.prim;
            db.Entry(str).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect("/Lk/Sklad");
        }

        public IActionResult KartochKompl(int id)
        {
            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;
            sklad model = new sklad();
            sklad_to str = db.Sklad_to.Find(id);

            model.sk = str;
            try { model.id_main = db.Main.Where(p => p.id_sklad == id).First().id; } catch { }
            ViewBag.rl = role;
            return View("SkladEdit", model);
        }

        public IActionResult SvyazSkladToUserDelete(int id, int id_s)
        {
            main str = db.Main.Find(id);
            id_s = str.id_sklad;
            str.id_sklad = 0;
            db.Entry(str).State = EntityState.Modified;
            db.SaveChanges();
            // Add user model
            sklad_to ss = db.Sklad_to.Find(id_s);
            ss.status = "На складе";

            db.Entry(ss).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Sklad");
        }
        public IActionResult SvyazSkladToUserDelete1(int id, int id_s)
        {
            main str = db.Main.Find(id);
            id_s = str.id_sklad;
            str.id_sklad = 0;
            db.Entry(str).State = EntityState.Modified;
            db.SaveChanges();
            // Add user model
            sklad_to ss = db.Sklad_to.Find(id_s);
            ss.status = "На складе";

            db.Entry(ss).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("KartochKompl", new { id = id_s });
        }
        public IActionResult IstO(int id)
        {
            var query = from t in db.Ist
                        where t.id_main == id && t.kluch == "o"
                        select new { t.znach, t.data_izm };
            // Add user model
            return Json(query.ToArray());
        }


        public IActionResult IstAddP(int id)
        {
            var query = from t in db.Ist
                        where t.id_main == id && t.kluch == "adrp"
                        select new { t.znach, t.data_izm };
            // Add user model
            return Json(query.ToArray());
        }
        public IActionResult IstTel(int id)
        {
            var query = from t in db.Ist
                        where t.id_main == id && t.kluch == "tel"
                        select new { t.znach, t.data_izm };
            // Add user model
            return Json(query.ToArray());
        }

        public IActionResult IstFioR(int id)
        {
            var query = from t in db.Ist
                        where t.id_main == id && t.kluch == "fior"
                        select new { t.znach, t.data_izm };
            // Add user model
            return Json(query.ToArray());
        }

        public IActionResult IstFioZP(int id)
        {
            var query = from t in db.Ist
                        where t.id_main == id && t.kluch == "fiozp"
                        select new { t.znach, t.data_izm };
            // Add user model
            return Json(query.ToArray());
        }


        public IActionResult IstSDmse(int id)
        {
            var query = from t in db.Ist
                        where t.id_main == id && t.kluch == "srmse"
                        select new { t.znach, t.data_izm };
            // Add user model
            return Json(query.ToArray());
        }

        public IActionResult IstSbaz(int id)
        {
            var query = from t in db.Ist
                        where t.id_main == id && t.kluch == "sbaz"
                        select new { t.znach, t.data_izm };
            // Add user model
            return Json(query.ToArray());
        }
        public IActionResult IstSjit(int id)
        {
            var query = from t in db.Ist
                        where t.id_main == id && t.kluch == "sjit"
                        select new { t.znach, t.data_izm };
            // Add user model
            return Json(query.ToArray());
        }





      


        public IActionResult Uch(SortState sortOrder, ListLK filtr, string status, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17,string f18)
        {
            filtr.Filt.Status = "учащийся";
            // Add user model

            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;
            if (role == 2 || role == 1)
                return View("obch", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            if (role == 5)
                return View("LKUO", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            if (role == 6)
                return View("LKBO", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            else
            return View("index");
        }
        public IActionResult Stud(SortState sortOrder, ListLK filtr, string status, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17, string f18)
        {
            filtr.Filt.Status = "студент";
            // Add user model
            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;
            if (role == 2 || role == 1)
                return View("obch", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            if (role == 5)
                return View("LKUO", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            if (role == 6)
                return View("LKBO", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            else
                return View("index");

        }
        public IActionResult Vibivsh(SortState sortOrder, ListLK filtr, string status, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17, string f18)
        {
            filtr.Filt.Status = "выбывший";
            // Add user model
            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;
            if (role == 2 || role == 1)
                return View("obch", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            if (role == 5)
                return View("LKUO", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            if (role == 6)
                return View("LKBO", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            else
                return View("index");
        }
        public IActionResult Vse(SortState sortOrder, ListLK filtr, string status, string f1, DateTime f2, DateTime f3, DateTime f4, DateTime f5, DateTime f6, DateTime f7, string f8, string f9, string f10, int f11, string f12, int f13, string f14, string f15, string f16, string f17,string f18)
        {
            filtr.Filt.Status = null;
            // Add user model
            var login = HttpContext.User.Identity.Name;
            int role = db.User.Where(p => p.login == login).First().role;
            if (role == 2 || role == 1)
                return View("obch", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            if (role == 5)
                return View("LKUO", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            if (role == 6)
                return View("LKBO", FiltSort(sortOrder, filtr, status, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18));
            else
                return View("index");
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
            try { model.data_roj = str.data_rojd.Date; } catch { }
            try { model.address_proj = db.Ist.Find(str.id_adr_progiv).znach; } catch { }
            try { model.address_reg = db.Ist.Find(str.id_adr_reg).znach; } catch { }
            try { model.tel = db.Ist.Find(str.id_tel).znach; } catch { }
            try { model.MO = str.id_mo; } catch { }
            try { model.diag = str.diagn; } catch { }
            try { model.Fio_rod = db.Ist.Find(str.id_fio_rod).znach; } catch { }
            try { model.Fio_rod_zp = db.Ist.Find(str.id_fio_rod_predst).znach; } catch { }
            try { model.data_sprav = db.Ist.Find(str.id_srok_mse).znach.Replace(" 0:00:00", ""); } catch { }
            try { model.sklad = db.Sklad_to.Find(str.id_sklad); } catch { }
            try { model.prikaz = str.prik_o_zach_n; } catch { }
            try { model.prikaz_d = str.prik_o_zach_d; } catch { }
            try { model.prikaz_iskl_d = str.prik_o_iskl_d; } catch { }
            try { model.klass = str.klass; } catch { }
            try { model.status = str.status; } catch { }
            try { model.tel = db.Ist.Find(str.id_tel).znach; } catch { }
            try { model.bvps = db.Bvp.Where(p => p.id_uo == str.id_uo).ToList(); } catch { }
            try { model.dop_Dogs = db.Dop_Dog.Where(p => p.id_uo == str.id_uo).ToList(); } catch { }
            //     try { model.data_ust_oborud = db.To.Where(p => p.id == str.id_to).First().data_ust_o; } catch { }
            try { model.role = role; } catch { }
            try { model.FIO_ped = str.FIO_ped; } catch { }
            if (role == 1)
            {

                try { model.dvig_dogov_bvp = db.Ist.Find(model.urot.id_dvij_dog_bvp).znach; } catch { }
                try { model.kursi = db.Kurs.Where(p => p.id_main == str.id).OrderByDescending(p => p.period).ToList(); } catch { }
                try
                {
                    model.kurs_per = db.Kurs.Where(p => p.id_main == str.id).OrderBy(p => p.period).GroupBy(p => p.period).Select(x => x.First()).ToList();

                }
                catch { }
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
                try { model.data_sprav = db.Ist.Find(str.id_srok_mse).znach.Replace(" 0:00:00", ""); } catch { }
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
                //  try { model.remonti = db.Rem.Where(p => p.id_to == str.id_to).ToList(); } catch { }
                try { model.internet = db.Inter.Where(p => p.id_to == str.id_to).ToList(); } catch { }
                try
                {
                    model.kurs_per = db.Kurs.Where(p => p.id_main == str.id).OrderBy(p => p.period).GroupBy(p => p.period).Select(x => x.First()).ToList();

                }
                catch { }
            }
            if (role == 4 || role == 7)
            {
                try { model.tip_kompl = str.tip_kompl; } catch { }
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
            ViewBag.rl = role;
            return View("ZayavEdit", model);
        }

        public IActionResult Save_Kurs(CompositeModel compositeModel)
        {
            int ye = DateTime.Now.Year;
            if (compositeModel.kursadd.kurs_do != null)
            {
                main mai = db.Main.Find(compositeModel.id);
                mai.data_izm = DateTime.Now;
                compositeModel.kursadd.id_main = compositeModel.id;
                if (DateTime.Now.Month > 8)
                {
                    compositeModel.kursadd.period = ye.ToString() + "-" + (ye + 1).ToString();
                }
                else
                {
                    compositeModel.kursadd.period = (ye - 1).ToString() + "-" + ye.ToString();
                }
                db.Entry(mai).State = EntityState.Modified;
                db.Entry(compositeModel.kursadd).State = EntityState.Added;
                db.SaveChanges();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id + "#j_kurs");
        }

        public IActionResult Save_Rem(CompositeModel compositeModel)
        {
            if (compositeModel.remadd.data_z_r != null)
            {
                main mai = db.Main.Find(compositeModel.id);
                mai.data_izm = DateTime.Now;
                compositeModel.remadd.id_to = compositeModel.tehot.id;

                db.Entry(mai).State = EntityState.Modified;
                db.Entry(compositeModel.remadd).State = EntityState.Added;
                db.SaveChanges();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id + "#j_rem");
        }

        public IActionResult Save_Int(CompositeModel compositeModel)
        {
            if (compositeModel.intadd.data_z_i != null)
            {
                main mai = db.Main.Find(compositeModel.id);
                mai.data_izm = DateTime.Now;
                compositeModel.intadd.id_to = compositeModel.tehot.id;
                db.Entry(mai).State = EntityState.Modified;
                db.Entry(compositeModel.intadd).State = EntityState.Added;
                db.SaveChanges();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id + "#j_int");
        }

        public IActionResult Save_Bvp(CompositeModel compositeModel)
        {
            if (compositeModel.bvpadd.nom_dog_bvp_d != null)
            {
                main mai = db.Main.Find(compositeModel.id);
                mai.data_izm = DateTime.Now;
                compositeModel.bvpadd.id_uo = compositeModel.urot.id;
                db.Entry(mai).State = EntityState.Modified;
                db.Entry(compositeModel.bvpadd).State = EntityState.Added;
                db.SaveChanges();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id + "#j_bvp");
        }

        public IActionResult Save_Dop(CompositeModel compositeModel)
        {
            if (compositeModel.dopadd.dop_sogl_bvp_n != null)
            {
                main mai = db.Main.Find(compositeModel.id);
                mai.data_izm = DateTime.Now;
                compositeModel.dopadd.id_uo = compositeModel.urot.id;
                db.Entry(mai).State = EntityState.Modified;
                db.Entry(compositeModel.dopadd).State = EntityState.Added;
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
                main mai = db.Main.Find(id_main);
                mai.data_izm = DateTime.Now;
                db.Entry(mai).State = EntityState.Modified;
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
                main mai = db.Main.Find(compositeModel.id);
                mai.data_izm = DateTime.Now;
                db.Rem.Remove(product);
                db.Entry(mai).State = EntityState.Modified;

                await db.SaveChangesAsync();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id.ToString());
        }

        public async Task<IActionResult> Del_Int(CompositeModel compositeModel)
        {
            var product = db.Inter.Find(compositeModel.intadd.Id);
            if (product != null)
            {
                main mai = db.Main.Find(compositeModel.id);
                mai.data_izm = DateTime.Now;
                db.Inter.Remove(product);
                db.Entry(mai).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id.ToString());
        }

        public async Task<IActionResult> Del_Bvp(CompositeModel compositeModel)
        {
            var product = db.Bvp.Find(compositeModel.bvpadd.id);
            if (product != null)
            {
                main mai = db.Main.Find(compositeModel.id);
                mai.data_izm = DateTime.Now;
                db.Bvp.Remove(product);
                db.Entry(mai).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id.ToString());
        }

        public async Task<IActionResult> Del_Dop(CompositeModel compositeModel)
        {
            var product = db.Dop_Dog.Find(compositeModel.dopadd.id);
            if (product != null)
            {
                main mai = db.Main.Find(compositeModel.id);
                mai.data_izm = DateTime.Now;
                db.Dop_Dog.Remove(product);
                db.Entry(mai).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return Redirect("/Lk/kartochka?id=" + compositeModel.id.ToString());
        }
        public async Task<IActionResult> Del_Main(int id)
        {
            var product = db.Main.Find(id);

            if (product != null)
            {
                var to = product.id_to;
                var uo = product.id_uo;

                db.Main.Remove(product);

                await db.SaveChangesAsync();
                db.To.Remove(db.To.Find(to));
                await db.SaveChangesAsync();
                db.Uo.Remove(db.Uo.Find(uo));
                await db.SaveChangesAsync();
            }
            return Redirect("/Lk/lk");
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
