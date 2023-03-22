using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace CAG_DW.Models
{
	public class Agreement
	{
		public int AgreementID { get; set; }

		public DateTime OrderDate { get; set; }

		public int CustomerID { get; set; }

		public ICollection<AgreementDetail>? AgreementDetails { get; set; }
	}
}

