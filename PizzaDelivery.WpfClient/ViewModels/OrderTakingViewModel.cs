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
        public ObservableCollection<Address> Addresses { get; set; }
        public ObservableCollection<Address> CustomerAddresses { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public Customer SelectedCustomer { get; set; }
        public string PhoneNumber { get; set; }

        string selectedAddress;
        public string SelectedAddress
        {
            get { return selectedAddress; }
            set
            {
                SetProperty(ref selectedAddress, value);
                //(command as RelayCommand).NotifyCanExecuteChanged();
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
            CustomerAddresses = new ObservableCollection<Address>();
        }
        public void SetupCommands()
        {
            CheckPhoneNumber = new RelayCommand(() =>
            {
                SelectedCustomer = Customers.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);
                if (SelectedCustomer != null)
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
                    OnPropertyChanged(nameof(CustomerAddresses));
                    OnPropertyChanged("SelectedCustomer");
                }
                else
                {
                    //uj customer felvétele
                }
            });
        }

    }
}
