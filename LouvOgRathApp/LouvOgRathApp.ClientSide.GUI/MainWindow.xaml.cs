using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LouvOgRathApp.Shared.Entities;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using LouvOgRathApp.ClientSide.ClientControllers;

namespace LouvOgRathApp.ClientSide.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        LoginUserControl loginUserControl;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        
        internal void AssignUsercontrol(UserControl userControl)
        {
           this.Content = userControl;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loginUserControl = new LoginUserControl();            
            AssignUsercontrol(loginUserControl);
        }
    }
}
