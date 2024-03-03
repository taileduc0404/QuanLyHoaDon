using HoaDon.Data;
using HoaDon.Models;
using HoaDon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HoaDon.UI
{
	/// <summary>
	/// Interaction logic for HoaDonWindow.xaml
	/// </summary>
	public partial class HoaDonWindow : Window
	{

		private Hoadon _hoadon;

		public HoaDonWindow()
		{
			InitializeComponent();
		}

		hoadonContext context = new hoadonContext();
		private void hienthi()
		{
			hoadonContext db = new hoadonContext();
			dgHoadon.ItemsSource = db.Hoadons.Select(t => HoaDonVM.transferCHoadon(t)).ToList();
		}
		private void hienthiCTHD(DataGrid dg, Hoadon hd)
		{
			dg.ItemsSource = hd.Chitiethoadons
				.Select(t => ChiTietHoaDonView.transferChitietHDView(t)).ToList();
		}
		private void lenhChon_CanExe(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void lenhChon_Exe(object sender, ExecutedRoutedEventArgs e)
		{
			hoadonContext db = new hoadonContext();
			ChiTietHoaDonVM ct = gridLHD.DataContext as ChiTietHoaDonVM;
			_hoadon = new Hoadon();
			Chitiethoadon cttemp = _hoadon.Chitiethoadons.FirstOrDefault(t => t.Mahang == ct.Mahang);
			if (cttemp == null)
			{
				cttemp = new Chitiethoadon();
				cttemp.Mahang = ct.Mahang;
				cttemp.MahangNavigation = db.Hanghoas.Find(ct.Mahang);
				cttemp.Dongia = cttemp.MahangNavigation.Dongia;
				cttemp.Soluong = int.Parse(ct.Soluong);
				_hoadon.Chitiethoadons.Add(cttemp);
				hienthiCTHD(dgCTHD, _hoadon);
			}
			else
			{
				cttemp.Soluong += int.Parse(ct.Soluong);
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			hoadonContext db = new hoadonContext();
			cmbMahang.ItemsSource = db.Hanghoas.ToList();
			hienthi();
		}

		private void dgHoadon_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
		{
			hoadonContext db = new hoadonContext();

			if (dgHoadon.SelectedValue == null) return;
			string sohd = dgHoadon.SelectedValue.ToString();
			Hoadon hd = db.Hoadons.Find(sohd);
			hd.Chitiethoadons = db.Chitiethoadons.Where(t => t.Sohd == sohd).ToList();
			foreach (Chitiethoadon item in hd.Chitiethoadons)
			{
				item.MahangNavigation = db.Hanghoas.Find(item.Mahang);
			}
			DataGrid dgCTHD = e.DetailsElement.FindName("dgCTHD") as DataGrid;
			hienthiCTHD(dgCTHD, hd);
		}
	}
}
