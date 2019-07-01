using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cdo.Models

{
    public enum SortState
    {
        NomAsc,    // по имени по возрастанию
        NomDesc,
        InAsc,
        InDesc,// по имени по убыванию
        MoAsc,
        MoDesc,
        FAsc,
        FDesc,
        IAsc,
        IDesc,
        OAsc,
        ODesc,
        DRAsc,
        DRDesc,

        Add_pAsc,
        Add_pDesc,
        DAsc,
        DDesc,
        MSEAsc,
        MSEDesc,
        Nom_pAsc,
        Nom_pDesc,
        Data_pAsc,
        Data_pDesc,
        KlassAsc,
        KlassDesc,
        Tip_komplAsc,
        Tip_komplDesc,
        StatusAsc,
        StatusDesc,
        BSAsc,
        BSDesc,
        MSAsc,
        MSDesc,
        Fio_pedAsc,
        Fio_pedDesc



    }







}
