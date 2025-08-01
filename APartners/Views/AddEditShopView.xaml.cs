﻿using APartners.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace APartners.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditShopView.xaml
    /// </summary>
    public partial class AddEditShopView : UserControl
    {
        public AddEditShopView()
        {
            InitializeComponent();
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image && DataContext is AddEditProductViewModel vm)
            {
                vm.SelectedImage = image.Source;
            }
        }
    }
}
