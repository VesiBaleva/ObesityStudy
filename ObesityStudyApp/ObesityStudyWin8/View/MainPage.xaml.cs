using Microsoft.Live;
using ObesityStudyWin8.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.OnlineId;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ObesityStudyWin8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public LiveConnectSession _session { get; set; }

        public LiveConnectSession Session
        {
            get
            {
                return _session;
            }
            set
            {
                _session = value;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
        }

        public MainViewModel Model
        {
            get
            {
                return this.DataContext as MainViewModel;
            }
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private async void InitAuth()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                var authClient = new LiveAuthClient();
                LiveLoginResult authResult = await authClient.LoginAsync(new List<string>() { "wl.signin", "wl.basic", "wl.skydrive" });

                if (authResult.Status == LiveConnectSessionStatus.Connected)
                {
                    this.Session = authResult.Session;
                }

            }
        }

        
    }
}
