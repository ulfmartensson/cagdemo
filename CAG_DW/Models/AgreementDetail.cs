using System;
namespace CAG_DW.Models
{
	public class AgreementDetail
    {
		public int ID { get; set; }

		public int Qty { get; set; }

		public int ProductID { get; set; }

		public int AgreementID { get; set; }


		public Agreement Agreement { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}

