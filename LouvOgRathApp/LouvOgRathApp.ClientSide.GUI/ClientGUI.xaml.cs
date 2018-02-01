using LouvOgRathApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace LouvOgRathApp.ClientSide.GUI
{
    /// <summary>
    /// Interaction logic for ClientGUI.xaml
    /// </summary>
    public partial class ClientGUI : UserControl
    {
        public ClientGUI(UserCredentials uInfo)
        {
            InitializeComponent();
            ClientControllers.Client client = new ClientControllers.Client(new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 65432));
            List<Case> cases = new List<Case>();
            foreach (Case item in client.GetClientsCases(uInfo))
            {
                cases.Add(item);
            }
            dgCases.ItemsSource = cases;
        }

        private void dgCases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgCases.SelectedItem is Case selectedCase)
            {
                List<MettingSummery> sums = new List<MettingSummery>();
                ClientControllers.Client client = new ClientControllers.Client(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 65432));
                foreach (MettingSummery sum in client.GetClientSummerys(selectedCase))
                {
                    sums.Add(sum);
                }
                try
                {
                    tbkNewestSummery.Text = sums.First<MettingSummery>().Resumé;
                    sums.RemoveAt(0);
                    tbkSecondNewestSummery.Text = sums.First<MettingSummery>().Resumé;
                    sums.RemoveAt(0);
                    tbkThirdNewestSummery.Text = sums.First<MettingSummery>().Resumé;
                    sums.RemoveAt(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        internal void AssignUsercontrol(UserControl userControl)
        {
            this.Content = userControl;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AssignUsercontrol(new LoginUserControl());
        }
    }
}
