using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace CAG_DW.Models
{
    public class Customer
    {

        public int CustomerID { get; set; }

        public string? CustomerName { get; set; }

        public ICollection<Agreement>? Agreements { get; set; }
    }
}