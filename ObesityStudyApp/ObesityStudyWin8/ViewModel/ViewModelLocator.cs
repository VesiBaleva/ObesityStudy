/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ObesityStudyWin8"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using ObesityStudyWin8.Model;
using ObesityStudyWin8.Services;

namespace ObesityStudyWin8.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public INavigationServices DefaultNavigationService { get; private set; }
        public static ViewModelLocator Default { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<INavigationServices, NavigationServices>();
            SimpleIoc.Default.Register<IDataService, ExternalDataService>();

            SimpleIoc.Default.Register<UpdatesViewModel>();
            SimpleIoc.Default.Register<CategoriesViewModel>();
           // SimpleIoc.Default.Register<PersonModel>();
            SimpleIoc.Default.Register<NavigateViewModel>();
            SimpleIoc.Default.Register<PersonViewModel>();
            DefaultNavigationService = SimpleIoc.Default.GetInstance<INavigationServices>();
        }

        public ViewModelBase Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ViewModelBase UpdatesModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UpdatesViewModel>();
            }
        }

        public ViewModelBase CategoriesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoriesViewModel>();
            }
        }

        public ViewModelBase PersonViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PersonViewModel>();
            }
        }


        public ViewModelBase NavigateViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NavigateViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}