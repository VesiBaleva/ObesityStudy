using ObesityStudyWin8.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace ObesityStudyWin8.Services
{
    public class ExternalDataService : IDataService
    {
        public Task<IEnumerable> GetUpdates()
        {
            return GetUpdatesDataCore();
        }

        public IEnumerable GetUpdatesCategories()
        {
            return GetUpdatesCategoriesDataCore();
        }

        private IEnumerable GetUpdatesCategoriesDataCore()
        {
            var updates = new List<CategoryModel>();
            
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("countCategories"))
            {
                var countCat = int.Parse(localSettings.Values["countCategories"].ToString());
                for (int i = 0; i < countCat; i++)
                {
                    if (localSettings.Values.ContainsKey("category"+i))
                    {
                        ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)localSettings.Values["category"+i];
                        var cat = new CategoryModel();
                        cat.Title = composite["title"].ToString();
                        cat.From = double.Parse(composite["from"].ToString());
                        cat.To = double.Parse(composite["to"].ToString());

                        updates.Add(cat);
                    }
                }
            }

            
            //var categories = localSettings.Values["category"];
            //var cat = new CategoryModel();
            //if (localSettings.Values["category"])
            //cat.Title = categories["title"];
            //cat.From = categories["from"];
            //cat.To = categories["to"];

            

            var categoriesModel = from c in updates
                               select new CategoryModel()
                               {
                                   Title = c.Title,
                                   From = c.From,
                                   To = c.To,                                   
                               };

            var groupedData = from u in categoriesModel
                              group u by u.Title into groupData
                              select new
                              {
                                  Name = groupData.Key,
                                  Items = groupData
                              };

            ObservableCollection<GroupedData<CategoryModel>> data = new ObservableCollection<GroupedData<CategoryModel>>();
            foreach (var item in groupedData)
            {
                GroupedData<CategoryModel> list = new GroupedData<CategoryModel>();
                list.Key = item.Name;
                foreach (var itemInGroup in item.Items)
                {
                    list.Add(itemInGroup);
                }
                data.Add(list);
            }

            return data;
        }

        private async Task<IEnumerable> GetUpdatesDataCore()
        {
            //Get all updates
            StorageFolder sf = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Data", CreationCollisionOption.OpenIfExists);
            StorageFile st = await sf.CreateFileAsync("Data.xml", CreationCollisionOption.OpenIfExists);

            var updates=new List<PersonModel>();

            XmlDocument doc = new XmlDocument();

            var content = await FileIO.ReadTextAsync(st);

            if (!string.IsNullOrEmpty(content))
            {
                doc.LoadXml(content);

                var items = doc.GetElementsByTagName("person");

                foreach (var item in items)
                {
                    var person = new PersonModel();

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
                                person.SelectedSex =(SexType)Enum.Parse(typeof(SexType),itemChild.InnerText);
                                break;                            
                            default:
                                break;
                        }                         
                    }

                    updates.Add(person);
                }

            }

            var updatesModel = from c in updates
                               select new PersonModel()
                               { 
                                    Initials = c.Initials,
                                    Height=c.Height,
                                    Weight = c.Weight,
                                    Age = c.Age,
                                    Bmi = c.Bmi,
                                    SelectedSex = c.SelectedSex
                               };

            var groupedData = from u in updatesModel
                              group u by u.Initials into groupData
                              select new
                              {
                                  Name = groupData.Key,
                                  Items = groupData
                              };

            ObservableCollection<GroupedData<PersonModel>> data = new ObservableCollection<GroupedData<PersonModel>>();
            foreach (var item in groupedData)
            {
                GroupedData<PersonModel> list = new GroupedData<PersonModel>();
                list.Key = item.Name;
                foreach (var itemInGroup in item.Items)
                {
                    list.Add(itemInGroup);
                }
                data.Add(list);
            }

            return data;
        }
    }
}
