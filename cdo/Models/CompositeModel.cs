using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attest.Models

{
    public class CompositeModel : PageModel
    {
        private readonly DataContext _context;
        public List<FileModel> listFile { get; set; }
        public FileModel FileModel { get; set; }
        public List<Obrazovan> listObrazovan { get; set; }
        public Obrazovan Obrazovan { get; set; }
        public Zayavlen Zayavlen { get; set; }
        public List<Zayavlen> ListZayavlen { get; set; }
        public List<Nauch_deyat> listNauch_deyat { get; set; }
        public Nauch_deyat Nauch_Deyat { get; set; }
        public Users Users { get; set; }
        public List<Users> ListUsers { get; set; }
        public ProfRazv ProfRazv { get; set; }
        public List<ProfRazv> listProfRazv { get; set; }

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

}
