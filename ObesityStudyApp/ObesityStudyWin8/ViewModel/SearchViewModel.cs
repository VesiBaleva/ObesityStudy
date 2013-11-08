using ObesityStudyWin8.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace ObesityStudyWin8.ViewModel
{
    public class SearchViewModel : Common.BindableBase
    {
        private string queryText = "";

        public string QueryText
        {
            get
            {
                return this.queryText;
            }
            set
            {
                this.queryText = value;
                this.OnPropertyChanged("QueryText");
                this.LoadResults();
            }
        }

        private ObservableCollection<PersonViewModel> results;
        public IEnumerable<PersonViewModel> Results
        {
            get
            {
                if (this.results == null)
                {
                    results = new ObservableCollection<PersonViewModel>();
                }

                return results;
            }
            set
            {
                this.results.Clear();

                foreach (var item in value)
                {
                    this.results.Add(item);
                }
            }
        }



        private async void LoadResults()
        {
            //Get all updates
            StorageFolder sf = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Data", CreationCollisionOption.OpenIfExists);
            StorageFile st = await sf.CreateFileAsync("Data.xml", CreationCollisionOption.OpenIfExists);

            var updates = new List<PersonModel>();

            XmlDocument doc = new XmlDocument();

            var content = await FileIO.ReadTextAsync(st);

            if (!string.IsNullOrEmpty(content))
            {
                doc.LoadXml(content);

                var items = doc.GetElementsByTagName("person");

                foreach (var item in items)
                {
                    var person = new PersonViewModel();

                    foreach (var itemChild in item.ChildNodes)
                    {
                        switch (itemChild.NodeName)
                        {
                            case "Initials":
                                person.Initials = itemChild.InnerText;
                                break;
                            case "Height":
                                person.Height = double.Parse(itemChild.InnerText);
                                break;
                            case "Age":
                                person.Age = int.Parse(itemChild.InnerText);
                                break;
                            case "Bmi":
                                person.Bmi = double.Parse(itemChild.InnerText);
                                break;
                            case "Weight":
                                person.Weight = double.Parse(itemChild.InnerText);
                                break;
                            case "Sex":
                                person.SelectedSex = (SexType)Enum.Parse(typeof(SexType), itemChild.InnerText);
                                break;
                            default:
                                break;
                        }
                    }

                    if (person.Initials.ToLower().Contains(this.QueryText.ToLower()))
                    {
                        this.results.Add(person);
                    }
                }

            }
        }
    }
}
