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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        ClientControllers.Client client;
        public Register(ClientControllers.Client client)
        {
            this.client = client;
            InitializeComponent();
            cmbRoleKind.ItemsSource = Enum.GetValues(typeof(RoleKind)).Cast<RoleKind>();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            List<UserCredentials> usercres = new List<UserCredentials>();
            UserCredentials user = new UserCredentials((RoleKind)cmbRoleKind.SelectedValue, pbxPassword.Password, tbxUsername.Text, new Person(tbxFullname.Text, tbxPhoneNumber.Text, tbxEmail.Text, (RoleKind)cmbRoleKind.SelectedValue));
            usercres.Add(user);
            client.CreateNewUser(usercres);
            LoginUserControl loginUserControl = new LoginUserControl();
            AssignUsercontrol(loginUserControl);
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
