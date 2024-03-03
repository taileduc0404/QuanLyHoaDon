using HoaDon.Data;
using HoaDon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoaDon.ViewModels
{
	public class HoaDonVM
	{
		public string? Sohd { get; set; }
		public String? Ngaylaphd { get; set; }
		public string ?Tenkh { get; set; }
		public string ?Thanhtien { get; set; }

		public static HoaDonVM transferCHoadon(Hoadon hoadon)
		{
			if (hoadon == null) return null!;
			double tt = 0;
			hoadonContext db = new hoadonContext();
			tt = db.Chitiethoadons
				.Where(t => t.Sohd == hoadon.Sohd)
				.Sum(s => s.Dongia!.Value * s.Soluong!.Value);
			return new HoaDonVM()
			{
				Sohd = hoadon.Sohd,
				Ngaylaphd = hoadon.Ngaylaphd!.Value.ToShortDateString(),
				Tenkh = hoadon.Tenkh,
				Thanhtien = tt.ToString(),
			};
		}
	}
}
