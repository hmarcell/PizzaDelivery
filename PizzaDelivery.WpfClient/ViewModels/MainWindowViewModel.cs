using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using PizzaDelivery.Models;
using PizzaDelivery.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PizzaDelivery.WpfClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public IMainRepository Repository { get; set; }
        public ICommand UpdatePizzaCommand { get; set; }
        public ICommand OpenOrderTakingWindow { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Courier> Couriers { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IMainRepository>())
        {

        }
        public MainWindowViewModel(IMainRepository Repository)
        {
            this.Repository = Repository;
            RefreshCollections();
            SetupCommands();            
        }

        public void RefreshCollections()
        { 
            Orders = new ObservableCollection<Order>(Repository.OrderRepo.ReadAll());
            Couriers = new ObservableCollection<Courier>(Repository.CourierRepo.ReadAll());
        }

        public void SetupCommands()
        {
            //UpdatePizzaCommand = new RelayCommand(() =>
            //{
            //    Pizza newPizza = Repository.PizzaRepo.Read(1);
            //    newPizza.Price = 1234567;
            //    Repository.PizzaRepo.Update(newPizza);
            //    Pizzas = new ObservableCollection<Pizza>(Repository.PizzaRepo.ReadAll());

            //    OnPropertyChanged("Pizzas");
            //});
            OpenOrderTakingWindow = new RelayCommand(() =>
            {
                if ((bool)new OrderTakingWindow().ShowDialog())
                {
                    RefreshCollections();
                    OnPropertyChanged(nameof(Orders));
                }               
            });
        }
    }
}
