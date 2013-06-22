using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ScoreoidPortable;
using ScoreoidTest.Resources;

namespace ScoreoidTest
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Loaded += async (sender, args) =>
            {
                var client = new ScoreoidClient("1fb20791da97e2395364b19595aace22d727de60", "dfe4d56007");

                var response = await client.SignInAsync("a");

                MessageBox.Show(response.ToString());
            };
        }
    }
}