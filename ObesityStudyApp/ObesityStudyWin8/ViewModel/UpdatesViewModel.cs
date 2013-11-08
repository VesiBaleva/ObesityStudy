using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ObesityStudyWin8.Model;
using ObesityStudyWin8.Services;
using GalaSoft.MvvmLight.Ioc;
using System.Collections;

namespace ObesityStudyWin8.ViewModel
{
    public class UpdatesViewModel:ViewModelBase
    {
        private IDataService dataService;

        private PersonModel newItem;

        public PersonModel NewItem
        {
            get { return newItem; }
            set
            {
                newItem = value;
                this.RaisePropertyChanged("NewItem");
            }
        }

        private IEnumerable updates;

        public IEnumerable Updates
        {
            get
            {
                if (this.updates == null)
                {
                    this.GetUpdates();
                    return null;
                }

                return this.updates;
            }

            set
            {
                this.updates = value;
                this.RaisePropertyChanged("Updates");
            }
        }

        public UpdatesViewModel()
        {
            this.NewItem = new PersonModel(); //{ UserId = ((App)App.Current).AuthenticatedUser.ObjectId, User = new UserModel(((App)App.Current).AuthenticatedUser) };

            dataService = SimpleIoc.Default.GetInstance<IDataService>();

            this.SaveCommand = new RelayCommand(async () =>
            {

                await this.NewItem.SaveAsync();
                this.NewItem = new PersonModel();

                this.GetUpdates();
            });
        }

        public RelayCommand SaveCommand { get; private set; }

        private async void GetUpdates()
        {
            IEnumerable data = await this.dataService.GetUpdates();

            this.Updates = data;
        }

    }
}
