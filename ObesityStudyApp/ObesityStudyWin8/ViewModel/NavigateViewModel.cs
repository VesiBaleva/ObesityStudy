using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ObesityStudyWin8.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ObesityStudyWin8.ViewModel
{
    public class NavigateViewModel : ViewModelBase
    {
        private INavigationServices navigationServices;

        public NavigateViewModel(INavigationServices navigationServices)
        {
            this.navigationServices = navigationServices;

            this.NavigateCommand = new RelayCommand<string>((s) =>
            {
                var view = (Views)Enum.Parse(typeof(Views), s);

                this.navigationServices.Navigate(view);
            });
        }

        public ICommand NavigateCommand { get; private set; }
    }
}
