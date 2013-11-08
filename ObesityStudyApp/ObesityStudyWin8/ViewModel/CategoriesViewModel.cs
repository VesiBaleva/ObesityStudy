using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using ObesityStudyWin8.Model;
using ObesityStudyWin8.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObesityStudyWin8.ViewModel
{
    public class CategoriesViewModel : ViewModelBase
    {
        private IDataService dataService;

        private CategoryModel newItem;              

        public CategoryModel NewItem
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
                //    return null;
                }

                return this.updates;
            }

            set
            {
                this.updates = value;
                this.RaisePropertyChanged("Updates");
            }
        }

        public CategoriesViewModel()
        {
            this.NewItem = new CategoryModel(); 

            dataService = SimpleIoc.Default.GetInstance<IDataService>();

            this.SaveCommand = new RelayCommand(async () =>
            {

                this.NewItem.SaveCategory();
                this.NewItem = new CategoryModel();

                this.GetUpdates();
            });
        }

        public RelayCommand SaveCommand { get; private set; }

        private void GetUpdates()
        {
            IEnumerable data = this.dataService.GetUpdatesCategories();

            this.Updates = data;
        }

    }
}
