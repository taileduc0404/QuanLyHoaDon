using HoaDon.Data;
using HoaDon.Models;
using HoaDon.ViewModels;
using Microsoft.EntityFrameworkCore;
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
	/// Interaction logic for HangHoaWindow.xaml
	/// </summary>
	public partial class HangHoaWindow : Window
	{
		public HangHoaWindow()
		{
			InitializeComponent();
		}

		hoadonContext context = new hoadonContext();

		private void show()
		{
			dgHangHoa.ItemsSource = context.Hanghoas.ToList();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			show();
		}

		private void lenhThem_CanExe(object sender, CanExecuteRoutedEventArgs e)
		{
			hoadonContext db = new hoadonContext();
			HangHoaVM chh = gridHangHoa.DataContext as HangHoaVM;
			if (chh == null || string.IsNullOrEmpty(chh.Mahang))
			{
				e.CanExecute = false;
				return;
			}
			double dg;
			if (Double.TryParse(chh.Dongia, out dg) == false)
			{
				e.CanExecute = false;
				return;
			}
			Hanghoa hh = db.Hanghoas.Find(chh.Mahang);

			if (hh != null)
			{
				e.CanExecute = false;
				return;
			}
			e.CanExecute = true;
		}

		private void lenhThem_Exe(object sender, ExecutedRoutedEventArgs e)
		{
			HangHoaVM chh = gridHangHoa.DataContext as HangHoaVM;
			Hanghoa hh = HangHoaVM.transferHangHoa(chh);
			context.Hanghoas.Add(hh);
			context.SaveChanges();
			show();
		}

		private void lenhXoa_CanExe(object sender, CanExecuteRoutedEventArgs e)
		{
			hoadonContext db = new hoadonContext();
			HangHoaVM chh = gridHangHoa.DataContext as HangHoaVM;
			if (chh == null || string.IsNullOrEmpty(chh.Mahang))
			{
				e.CanExecute = false;
				return;
			}
			Hanghoa hh = db.Hanghoas.Find(chh.Mahang);

			if (hh == null)
			{
				e.CanExecute = false;
				return;
			}
			if (db.Chitiethoadons.Count(t => t.Mahang == chh.Mahang) > 0)
			{
				e.CanExecute = false;
			}
			e.CanExecute = true;
		}

		private void lenhXoa_Exe(object sender, ExecutedRoutedEventArgs e)
		{
			HangHoaVM chh = gridHangHoa.DataContext as HangHoaVM;
			Hanghoa hh = context.Hanghoas.Find(chh.Mahang);
			if (hh != null)
			{
				context.Hanghoas.Remove(hh);
				context.SaveChanges();
				show();
			}
		}

		private void select(object sender, SelectionChangedEventArgs e)
		{
			if (dgHangHoa.SelectedValue != null)
			{
				string id = dgHangHoa.SelectedValue.ToString();
				Hanghoa hh = context.Hanghoas.Find(id);
				gridHangHoa.DataContext = HangHoaVM.transferHanghoaVM(hh);
			}
		}

		private void lenhSua_CanExe(object sender, CanExecuteRoutedEventArgs e)
		{
			hoadonContext db = new hoadonContext();
			HangHoaVM chh = gridHangHoa.DataContext as HangHoaVM;
			if (chh == null || string.IsNullOrEmpty(chh.Mahang))
			{
				e.CanExecute = false;
				return;
			}
			double dg;
			if (Double.TryParse(chh.Dongia, out dg) == false)
			{
				e.CanExecute = false;
				return;
			}
			Hanghoa hh = db.Hanghoas.Find(chh.Mahang);

			if (hh == null)
			{
				e.CanExecute = false;
				return;
			}
			e.CanExecute = true;
		}

		private void lenhSua_Exe(object sender, ExecutedRoutedEventArgs e)
		{
			HangHoaVM chh = gridHangHoa.DataContext as HangHoaVM;
			Hanghoa hh = HangHoaVM.transferHangHoa(chh);

			// Tách thực thể hiện tại nếu nó đang được theo dõi

			var existingEntity = context.ChangeTracker.Entries<Hanghoa>().FirstOrDefault(x => x.Entity.Mahang == hh.Mahang);
			if (existingEntity != null)
			{
				existingEntity.State = EntityState.Detached;
			}

			// Gắn và cập nhật thực thể

			context.Hanghoas.Attach(hh);
			context.Entry(hh).State = EntityState.Modified;

			context.SaveChanges();
			show();
		}
	}
}
