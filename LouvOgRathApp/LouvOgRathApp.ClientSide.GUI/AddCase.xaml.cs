using LouvOgRathApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
namespace LouvOgRathApp.ClientSide.GUI
{
    /// <summary>
    /// Interaction logic for AddCase.xaml
    /// </summary>
    public partial class AddCase : UserControl
    {
        ClientControllers.Client client;
        public AddCase()
        {
            List<Person> clients = new List<Person>();
            List<Person> secretary = new List<Person>();
            List<Person> lawyer = new List<Person>();
            this.client = new ClientControllers.Client(new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 65432));
            InitializeComponent();

            foreach (Person item2 in client.GetAllPersons())
            {
                if (item2.Rolekind == RoleKind.Client)
                {
                    clients.Add(item2);
                }
                else if (item2.Rolekind == RoleKind.Lawyer)
                {
                    lawyer.Add(item2);
                }
                else if (item2.Rolekind == RoleKind.Secretary)
                {
                    secretary.Add(item2);
                }
            }
            tbxCaseClient.ItemsSource = clients;
            tbxCaseLawyer.ItemsSource = lawyer;
            cmbSecretary.ItemsSource = secretary;
            cmbCaseKind.ItemsSource = Enum.GetValues(typeof(CaseKind));
        }
        internal void AssignUsercontrol(UserControl userControl)
        {
            this.Content = userControl;
        }
        private void btnAddCase_Click(object sender, RoutedEventArgs e)
        {
            client = new ClientControllers.Client(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 65432));
            List<Case> cases = new List<Case>();
            Case @case = new Case(tbxCaseTitle.Text, (CaseKind)cmbCaseKind.SelectedItem, (Person)cmbSecretary.SelectedItem, (Person)tbxCaseLawyer.SelectedItem, (Person)tbxCaseClient.SelectedItem, new MettingSummery(tbxResume.Text));
            cases.Add(@case);
            //Controller.SendData(SaveNewCase(),cases);
            client.SaveCase(cases.ToArray());
            MessageBox.Show("der er her med blevet oprettet en ny sag.");
            AssignUsercontrol(new SecretaryGUI());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AssignUsercontrol(new SecretaryGUI());
        }
    }
}
