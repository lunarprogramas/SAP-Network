using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace lordhelpme 
{
    public sealed partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        public const string bottomBarAboutMe = "This application has been developed by lunarprogramas - V.0.1b";
        public const Boolean signedIn = false;
        public const Boolean forceTestError = false;

        DataTable users = new DataTable();
       

        public MainWindow()
        {
            this.InitializeComponent();
            this.Title = "SAP Network Interface";
            this.BottomBar.Text = bottomBarAboutMe;
            Debug.Write("Started interface module!");
            this.StartClock();

            if (forceTestError)
            {
                this.errorPage.Visibility = Visibility.Visible;
                this.signedOutMenu.Visibility = Visibility.Collapsed;
            } else
            {
                this.signedOutMenu.Visibility = Visibility.Visible;
            }
        }

        private void initiateUsers()
        {
            users.Clear();
            users.Columns.Add("Id", typeof(String));
            users.Columns.Add("Password", typeof(String)); // sorry for no encryption i cba

            DataRow row = users.NewRow();
            row["Id"] = "admin";
            row["Password"] = "admin";
            users.Rows.Add(row);

            Debug.WriteLine("Successfully loaded UserAccountService!");
        }

        private void StartClock()
        {
            // Initialize the timer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1); // Update every second
            _timer.Tick += Timer_Tick; // Subscribe to the Tick event
            _timer.Start(); // Start the timer
            Debug.WriteLine("Started clock service!");
        }

        private void Timer_Tick(object sender, object e)
        {
            
            if (this.signedOutMenu.Visibility == Visibility.Visible)
            {
                this.ClockSignedOutMenu.Text = DateTime.Now.ToString("hh:mm tt");
                this.Clock.Visibility = Visibility.Collapsed;
            } else
            {
                this.Clock.Visibility = Visibility.Visible;
                this.Clock.Text = DateTime.Now.ToString("hh:mm:ss tt");
            }
        }

        private void ViewLoginPage(object sender, RoutedEventArgs e)
        {
            this.signedOutMenu.Visibility = Visibility.Collapsed;
            this.loginPage.Visibility = Visibility.Visible;
        }

        private void ReturnToMenu(object sender, RoutedEventArgs e)
        {
            if (this.errorPage.Visibility == Visibility.Visible)
            {
                this.signedOutMenu.Visibility = Visibility.Visible;
                this.errorPage.Visibility = Visibility.Collapsed;
            } else
            {
                this.signedOutMenu.Visibility = Visibility.Visible;
                this.loginPage.Visibility = Visibility.Collapsed;
            }
        }

        private async void LogIn(object sender, RoutedEventArgs e)
        {
                if (this.username.Text.Length == 0 || this.password.Password.Length == 0)
                {
                    Debug.WriteLine("No logged user!");
                    this.incorrectCredentialsNotice.Visibility = Visibility.Visible;
                    await Task.Delay(10000);
                    this.incorrectCredentialsNotice.Visibility = Visibility.Collapsed;
                }
        }
    }

}
