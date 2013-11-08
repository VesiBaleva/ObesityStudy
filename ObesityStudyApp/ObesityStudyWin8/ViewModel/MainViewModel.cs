using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ObesityStudyWin8.Services;

namespace ObesityStudyWin8.ViewModel
{
    
    public class MainViewModel:ViewModelBase
    {
        private INavigationServices navigationServices;

        public MainViewModel(INavigationServices navigationServices)
        {
            this.navigationServices = navigationServices;

            this.NavigateCommand = new RelayCommand(()=>
            {
                this.navigationServices.Navigate(Views.Categories);
            });
        }

        public RelayCommand NavigateCommand { get; private set; }
    }
}
