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
        DeliveryDbContext db;
        CustomerRepository cr;
        public List<Customer> Customers { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Setup(new DeliveryDbContext());
            Customers = cr.ReadAll().ToList();
            List<List<Address>> addresses = new List<List<Address>>();
            Customers.ForEach(x => addresses.Add(x.Addresses.ToList()));         
            ;
        }

        public void Setup(DeliveryDbContext db)         
        {
            this.db = db;
            cr = new CustomerRepository(db);
        }
    }
}
