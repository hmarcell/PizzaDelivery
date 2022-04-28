using Microsoft.Toolkit.Mvvm.DependencyInjection;
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
    public class NewCustomerViewModel
    {
        IMainRepository Repository;
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public NewCustomerViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IMainRepository>())
        {

        }
        public NewCustomerViewModel(IMainRepository Repository)
        {
            this.Repository = Repository;
        }
    }
}
