using HoaDon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoaDon.ViewModels
{
	public class HangHoaVM
	{
		public string? Mahang { get; set; }
		public string? Tenhang { get; set; }
		public string? Dvt { get; set; }
		public string? Dongia { get; set; }
		public static Hanghoa transferHangHoa(HangHoaVM c)
		{
			if (c == null) { return null!; }
			return new Hanghoa
			{
				Mahang = c.Mahang,
				Tenhang = c.Tenhang,
				Dvt = c.Dvt,
				Dongia = Double.Parse(c.Dongia!)
			};
		}

		public static HangHoaVM transferHanghoaVM(Hanghoa? c)
		{
			if (c == null) { return null!; }
			return new HangHoaVM
			{
				Mahang = c.Mahang,
				Tenhang = c.Tenhang,
				Dvt = c.Dvt,
				Dongia = c.Dongia!.Value.ToString()
			};
		}
	}
}
