﻿using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using VanArsdel.Inventory.ViewModels;

namespace VanArsdel.Inventory.Views
{
    public sealed partial class OrdersView : Page
    {
        public OrdersView()
        {
            ViewModel = ServiceLocator.Current.GetService<OrdersViewModel>();
            InitializeComponent();
        }

        public OrdersViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.LoadAsync(e.Parameter as OrdersViewState);
        }
    }
}
