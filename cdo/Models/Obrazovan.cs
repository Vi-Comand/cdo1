using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attest.Models
{
    public class Obrazovan
    {

        public int Id { get; set; }
        public int id_zayavl { get; set; }
        public int tip_obr { get; set; }
        public string period { get; set; }
        public string mo { get; set; }
        public string oo { get; set; }
        public int kol_chas { get; set; }
        public string special { get; set; }
        public string kvalif { get; set; }
        public string nazv_doc { get; set; }
        public string ser_doc { get; set; }
        public string nom_doc { get; set; }
        public DateTime data_doc { get; set; }
        public string reg_nom { get; set; }
        public string vid_obuch { get; set; }

    }
}
