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
    public class OrderTakingViewModel : ObservableRecipient
    {
        public IMainRepository Repository { get; set; }
        public ICommand CheckPhoneNumber { get; set; }
        public ICommand ConfirmOrder { get; set; }
        public ObservableCollection<Address> Addresses { get; set; }
        public ObservableCollection<Address> CustomerAddresses { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Pizza> Pizzas { get; set; }
        public ObservableCollection<Courier> Couriers { get; set; }
        public Customer SelectedCustomer { get; set; }
        public string PhoneNumber { get; set; }

        Courier selectedCourier;
        public Courier SelectedCourier
        {
            get { return selectedCourier; }
            set
            {
                SetProperty(ref selectedCourier, value);
                (ConfirmOrder as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        Address selectedAddress;
        public Address SelectedAddress
        {
            get { return selectedAddress; }
            set
            {
                SetProperty(ref selectedAddress, value);
                (ConfirmOrder as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        Pizza selectedPizza;
        public Pizza SelectedPizza
        {
            get { return selectedPizza; }
            set
            {
                SetProperty(ref selectedPizza, value);
                (ConfirmOrder as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public OrderTakingViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IMainRepository>())
        {

        }
        public OrderTakingViewModel(IMainRepository Repository)
        {
            this.Repository = Repository;
            SetupCollections();
            SetupCommands();
        }
        public void SetupCollections()
        {
            Addresses = new ObservableCollection<Address>(Repository.AddressRepo.ReadAll());
            Customers = new ObservableCollection<Customer>(Repository.CustomerRepo.ReadAll());
            Pizzas = new ObservableCollection<Pizza>(Repository.PizzaRepo.ReadAll());
            Couriers  = new ObservableCollection<Courier>(Repository.CourierRepo.ReadAll());
            CustomerAddresses = new ObservableCollection<Address>();
        }
        public void RefreshCollections()
        {
            Addresses = new ObservableCollection<Address>(Repository.AddressRepo.ReadAll());
            Customers = new ObservableCollection<Customer>(Repository.CustomerRepo.ReadAll());
            //Pizzas = new ObservableCollection<Pizza>(Repository.PizzaRepo.ReadAll());          
        }
        public void SetupCommands()
        {
            CheckPhoneNumber = new RelayCommand(() =>
            {
                SelectedCustomer = Customers.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);
                if (SelectedCustomer != null)
                {
                    FillForm();
                    OnPropertyChanged(nameof(CustomerAddresses));
                    OnPropertyChanged("SelectedCustomer");
                }
                else
                {
                    if ((bool)new NewCustomerWindow(PhoneNumber).ShowDialog())
                    {
                        RefreshCollections();
                        SelectedCustomer = Customers.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);
                        FillForm();
                        OnPropertyChanged(nameof(CustomerAddresses));
                        OnPropertyChanged("SelectedCustomer");
                    }                   
                }
                ;
            });

            ConfirmOrder = new RelayCommand(() =>
            {
                Order order = new Order() { AddressId = SelectedAddress.Id, CustomerId = SelectedCustomer.Id, PizzaId = SelectedPizza.Id, Status = OrderStatus.Cooking, CourierId = SelectedCourier.Id };
                Repository.OrderRepo.Create(order);

            },
            () =>
            {
                return SelectedAddress != null && SelectedPizza != null && SelectedCourier?.Status == CourierStatus.Waiting;
            });
        }
        public void FillForm()
        {
            if (CustomerAddresses.Count == 0 || CustomerAddresses[0].CustomerId != SelectedCustomer.Id)
            {
                if (CustomerAddresses.Count != 0)
                {
                    CustomerAddresses = new ObservableCollection<Address>();
                }
                foreach (var address in Addresses)
                {
                    if (address.CustomerId == SelectedCustomer.Id)
                    {
                        CustomerAddresses.Add(address);
                    }
                }
            }
        }
    }
}
