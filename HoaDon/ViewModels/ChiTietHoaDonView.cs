using HoaDon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoaDon.ViewModels
{
	public class ChiTietHoaDonView
	{
		public string? Mahang { get; set; }
		public string? Tenhang { get; set; }
					 
		public string? Dvt { get; set; }
		public string? Dongia { get; set; }
		public string? Soluong { get; set; }
		public string? Thanhtien { get; set; }

		public static ChiTietHoaDonView transferChitietHDView(Chitiethoadon cthd)
		{
			if (cthd == null) return null!;
			return new ChiTietHoaDonView()
			{
				Mahang = cthd.Mahang,
				Tenhang = cthd.MahangNavigation.Tenhang,
				Dvt = cthd.MahangNavigation.Dvt,
				Dongia = cthd.Dongia!.Value.ToString(),
				Soluong = cthd.Soluong!.Value.ToString(),
				Thanhtien = (cthd.Soluong * cthd.Dongia).Value.ToString()

			};
		}
	}
}
