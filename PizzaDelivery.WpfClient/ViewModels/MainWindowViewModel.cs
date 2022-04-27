using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PizzaDelivery.Models;
using PizzaDelivery.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PizzaDelivery.WpfClient.ViewModels
{
    public class MainWindowViewModel
    {
        public IMainRepository Repository { get; set; }
        public List<Pizza> Pizzas { get; set; }
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
            Pizzas = Repository.PizzaRepo.ReadAll().ToList();
            ;

            
        }
    }
}
