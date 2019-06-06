using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Attest.Models
{
    public class Users
    {
        public  int Id { get; set; }
        public string FIO { get; set; }
        public string Email { get; set; }
        public string tel { get; set; }
        public string pass { get; set; }
        public string Snils { get; set; }
        public DateTime data_sozd { get; set; }
        public DateTime data_izm { get; set; }
        public string role { get; set; }
        public int mo { get; set; }
        public string special_exp { get; set; }


    }
}
