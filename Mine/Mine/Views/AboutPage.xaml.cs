using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mine.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {

        /// <summary>
        /// Constructor for AboutPage
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();

            CurrentDateTimeLabel.Text = System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss");

        }
    }
}