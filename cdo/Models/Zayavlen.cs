using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attest.Models
{
    public class Zayavlen
    {

        public int Id { get; set; }
        public int id_user { get; set; }
        public int mo { get; set; }
        public string oo { get; set; }
        public string dolgnost_imeyu { get; set; }
        public string dolgnost_att { get; set; }
        public string kategor { get; set; }
        public DateTime data_last_att { get; set; }
        public string kategor_rabot { get; set; }
        public string uch_stepen { get; set; }
        public DateTime data_podachi { get; set; }
        public DateTime data_obnovl { get; set; }
        public string status { get; set; }
        public string ball { get; set; }
        public int spec { get; set; }


    }
}
