using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using PizzaDelivery.Models;
using PizzaDelivery.Repository;
using PizzaDelivery.WpfClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PizzaDelivery.WpfClient.ViewModels
{
    public class NewCustomerViewModel : ObservableRecipient
    {
        IMainRepository Repository;
        public ICommand AddCustomerCommand { get; set; }
        private string phoneNumber;
        private string name;
        private string address;

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                (AddCustomerCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public string Address 
        { 
            get => address;
            set 
            {
                SetProperty(ref address, value);
                (AddCustomerCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public float Distance { get; set; }
        public int CustomerId { get; set; }
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                SetProperty(ref phoneNumber, value);
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        public NewCustomerViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IMainRepository>())
        {

        }
        public NewCustomerViewModel(IMainRepository Repository)
        {
            this.Repository = Repository;
            Distance = (float)Math.Round(Util.rng.NextDouble()*10, 2);

            AddCustomerCommand = new RelayCommand(() =>
            {
                if (Repository.CustomerRepo.ReadAll().FirstOrDefault(x => PhoneNumber == x.PhoneNumber) == null)
                {
                    Customer customer = new Customer() { Name = this.Name, PhoneNumber = this.PhoneNumber };
                    Repository.CustomerRepo.Create(customer);
                }
                CustomerId = Repository.CustomerRepo.ReadAll().FirstOrDefault(x => x.PhoneNumber == PhoneNumber).Id;
                if (Repository.AddressRepo.ReadAll().FirstOrDefault(x => x.Street == Address && x.Distance == Distance && x.CustomerId == CustomerId) == null)
                {
                    Address address = new Address() { Street = Address, Distance = this.Distance, CustomerId = this.CustomerId };
                    Repository.AddressRepo.Create(address);
                }
            },
            () =>
            {
                return Name != "" && Address != null;
            });
        }
    }
}
