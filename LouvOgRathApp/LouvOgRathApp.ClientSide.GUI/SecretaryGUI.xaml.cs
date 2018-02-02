using LouvOgRathApp.Shared.Entities;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace LouvOgRathApp.ClientSide.GUI
{
    /// <summary>
    /// Interaction logic for Secretary.xaml
    /// </summary>
    public partial class SecretaryGUI : UserControl
    {
        ClientControllers.Client client;
        AddCase add;
        MainWindow main;
        public SecretaryGUI()
        {
            InitializeComponent();
            main = new MainWindow();
            this.client = new ClientControllers.Client(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 65432));
            if (client.ClientConnected())
            {
                IPersistable[] Iper = client.GetAllCases();
                List<Case> cases = new List<Case>();
                foreach (Case item in Iper)
                {
                    cases.Add(item);
                }
                dgCases.ItemsSource = cases;
            }
        }
        private void dgCases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCases.SelectedItem is Case @case)
            {
                client = new ClientControllers.Client(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 65432));
                List<MettingSummery> summerys = new List<MettingSummery>();
                foreach (MettingSummery item in client.GetAllSummerys(@case))
                {
                    summerys.Add(item);
                }
                dgExistsingSummery.ItemsSource = summerys;

            }
        }
        internal void AssignUsercontrol(UserControl userControl)
        {
            this.Content = userControl;
        }

        private void btnSaveSummery1_Click(object sender, RoutedEventArgs e)
        {
            client = new ClientControllers.Client(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 65432));
            if ((Case)dgCases.SelectedItem is Case @case)
            {
                List<MettingSummery> metting = new List<MettingSummery>();
                MettingSummery summery = new MettingSummery(tbxNewSummery.Text, @case.CaseName, @case);
                metting.Add(summery);
                client.SaveSummery(metting.ToArray());
                client = new ClientControllers.Client(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 65432));
                List<MettingSummery> summerys = new List<MettingSummery>();
                foreach (MettingSummery item in client.GetAllSummerys(@case))
                {
                    summerys.Add(item);
                }
                dgExistsingSummery.ItemsSource = summerys;
            }
            else
            {
                MessageBox.Show("you have to select a case before you can safe a summery");
            }
        }

        private void AddCaseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            add = new AddCase();
            AssignUsercontrol(add);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AssignUsercontrol(new LoginUserControl());
        }
    }
}