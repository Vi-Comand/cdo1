using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attest.Models
{
    public enum SortState
    {
        NomZayavAsc,    // по имени по возрастанию
        NomZayavDesc,   // по имени по убыванию
        DataPodachAsc,
        DataPodachDesc,
        DataObnovAsc,
        DataObnovDesc,
        StatusAsc,
        StatusDesc,
        BalAsc,
        BalDesc,
    }
}
