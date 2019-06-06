using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attest.Models
{
    public class ProfRazv
    {
        public int Id { get; set; }
        public int id_zayav { get; set; }
        public string uch_stepen { get; set; }
        public string uch_zvanie { get; set; }
        public string kod_nauc_spec { get; set; }
        public string nazv_doc { get; set; }
        public string org_d { get; set; }
        public string ser_d { get; set; }
        public string non_d { get; set; }
        public DateTime data_vid_d { get; set; }
    }
}
