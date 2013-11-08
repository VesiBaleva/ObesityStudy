using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObesityStudyWin8.Model
{
    public class CategoryModel : ObservableObject
    {
        private int categoryId;

        public int CategoryId
        {
            get
            {
                if (this.categoryId == null)
                {
                    this.categoryId = 0;
                }
                return this.categoryId;
            }
            set
            {
                this.categoryId = value;
                this.RaisePropertyChanged("CategoryId");
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.RaisePropertyChanged("Title");
            }
        }

        public double From
        {
            get
            {
                return this.from;
            }

            set
            {
                this.from = value;
                this.RaisePropertyChanged("From");
            }
        }

        public double To
        {
            get
            {
                return this.to;
            }

            set
            {
                this.to = value;
                this.RaisePropertyChanged("To");
            }
        }

        public string title { get; set; }

        public double from { get; set; }

        public double to { get; set; }

        public void SaveCategory()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey("countCategories"))
            {
                this.CategoryId = int.Parse(localSettings.Values["countCategories"].ToString());
            }

            var category = new Windows.Storage.ApplicationDataCompositeValue();
            category["title"] = this.Title;
            category["from"] = this.From;
            category["to"] = this.To;

            localSettings.Values["category" + this.CategoryId] = category;
            
            this.CategoryId += 1;
            localSettings.Values["countCategories"] = this.CategoryId;
           
        }
    }
}
