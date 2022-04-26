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
        PizzaRepository cr;
        public List<Pizza> Customers { get; set; }
        public void Setup(DeliveryDbContext db)
        {
            this.db = db;
            cr = new PizzaRepository(db);
        }
        public MainWindow()
        {
            InitializeComponent();
            Setup(new DeliveryDbContext());
            Customers = cr.ReadAll().ToList();
            ;
        }
    }
}
