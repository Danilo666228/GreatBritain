using GreatBritain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GreatBritain.Pages;

namespace GreatBritain.Pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogOfGoodsPage.xaml
    /// </summary>
    public partial class CatalogOfGoodsPage : Page
    {
        int _itemcount = 0;
        public CatalogOfGoodsPage()
        {
            InitializeComponent();
            using (var context = new EnglishStoreEntities())
            {
                var developers = context.Manufacturers.OrderBy(p => p.Name).ToList();
                developers.Insert(0, new Manufacturer
                {
                    Name = "Все типы"
                });
                ComboDeveloper.ItemsSource = developers;
                ComboDeveloper.SelectedIndex = 0;
                LViewGoods.ItemsSource = context.Products.OrderBy(p => p.Name).ToList();
                _itemcount = LViewGoods.Items.Count;
                TextBlockCount.Text = $"Результат запроса {_itemcount} записей из {_itemcount}";
            }
        }
        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            using (var context = new EnglishStoreEntities())
            {
                if (Visibility == Visibility.Visible)
                {
                    context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    LViewGoods.ItemsSource = context.Products.OrderBy(p => p.Name).ToList();
                }
            }
        }
        private void txbSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }
        private void ComboTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }
        private void UpdateData()
        {
            using (var context = new EnglishStoreEntities())
            {
                var currentGoods = context.Products.OrderBy(c => c.Name).ToList();
                if (ComboDeveloper.SelectedIndex > 0)
                {
                    currentGoods = currentGoods.Where(c => c.ManufacturerID == (ComboDeveloper.SelectedItem as Manufacturer).ID).ToList();
                }
                currentGoods = currentGoods.Where(c => c.Name.ToLower().Contains(txbSearch.Text.ToLower())).ToList();
                if (ComboSort.SelectedIndex >= 0)
                {
                    if (ComboSort.SelectedIndex == 0)
                    {
                        currentGoods = currentGoods.OrderBy(c => c.Price).ToList();
                    }
                    if (ComboSort.SelectedIndex == 1)
                    {
                        currentGoods = currentGoods.OrderByDescending(p => p.Price).ToList();
                    }
                }
                LViewGoods.ItemsSource = currentGoods;
                TextBlockCount.Text = $"Результат запроса: {currentGoods.Count} записей из {_itemcount}";
            }
        }
        private void ComboSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }
        
    }
}
