using LouvOgRathApp.Shared.Entities;
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

namespace LouvOgRathApp.ClientSide.GUI
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        SecretaryGUI secretary;
        ClientGUI clientGui;
        MainWindow mainWindow;
        public ClientControllers.Client client;
        public LoginUserControl()
        {
            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.client = new ClientControllers.Client(new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 65432));
            UserCredentials user = new UserCredentials(tbxUsername.Text, pbxPassword.Password, null);
            (bool isUser, UserCredentials uInfo) = client.LoginAttempt(user);
            if (isUser)
            {
                switch (uInfo.RoleKind_)
                {
                    case RoleKind.Client:
                        clientGui = new ClientGUI(uInfo);
                        AssignUsercontrol(clientGui);
                        break;
                        
                    case RoleKind.Secretary:
                        secretary = new SecretaryGUI();
                        AssignUsercontrol(secretary);


                        mainWindow = new MainWindow();
                        break;
                    default:
                        MessageBox.Show("wrong input");
                        break;
                }
            }
        }
        internal void AssignUsercontrol(UserControl userControl)
        {
            this.Content = userControl;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register(client);
            AssignUsercontrol(register);
        }
    }
}
