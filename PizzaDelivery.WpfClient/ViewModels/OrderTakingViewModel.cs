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
    public class OrderTakingViewModel
    {
        public IMainRepository Repository { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public OrderTakingViewModel(IMainRepository Repository)
        {
            this.Repository = Repository;
        }
        public OrderTakingViewModel() :this(IsInDesignMode? null : Ioc.Default.GetService<IMainRepository>())
        {

        }
    }
}
