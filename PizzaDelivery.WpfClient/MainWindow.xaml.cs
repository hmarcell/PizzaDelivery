using ODataService.Database;
using PizzaDelivery.Data;
using PizzaDelivery.Models;
using PizzaDelivery.Repository;
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

namespace PizzaDelivery.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //AddPizza();
        }

        async void AddPizza()
        {
            var serviceRoot = "https://odataservice20220428121656.azurewebsites.net/ODataService.svc/";
            ODataServiceService service = new ODataServiceService(new Uri(serviceRoot));
            ODataService.Database.Customer customer = new ODataService.Database.Customer()
            {
                Oid = 0,
                Name = "John Doe",
                Phone = "+36701234567",
                Addresses = new System.Collections.ObjectModel.Collection<ODataService.Database.Address>(),
                Orders = new System.Collections.ObjectModel.Collection<ODataService.Database.Order>()
            };
            ODataService.Database.Address address = new ODataService.Database.Address()
            {
                Oid = 0,
                City = "Budapest",
                Customer = customer,
                HouseNumber = "41",
                Street = "Vaci",
                StreetType = "Street"
            };
            service.AddToOrder(new ODataService.Database.Order()
            {
                Oid = 0,
                Address = address,
                Customer = customer,
                Description = "ground floor, please ring :)",
                OrderLines = new System.Collections.ObjectModel.Collection<OrderLine>()
            });
            service.EndSaveChanges(service.BeginSaveChanges((arg) => { }, null));
        }

    }
}
