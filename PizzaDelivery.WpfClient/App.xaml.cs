﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PizzaDelivery.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PizzaDelivery.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(      //repobuilder 
                    new ServiceCollection()
                    .AddTransient<ICourierRepository, CourierRepository>()
                    .AddTransient<ICustomerRepository, CustomerRepository>()
                    .AddTransient<IPizzaRepository, PizzaRepository>()
                    .AddTransient<IOrderRepository, OrderRepository>()
                    .BuildServiceProvider()
                );
        }
    }
}
