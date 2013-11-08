using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace ObesityStudyWin8.Model
{
    public class PersonModel : ObservableObject
    {
        private string initials;

        private double height;
        private PersonModel c;

        private int age;
        private double bmi;

        private double weight;

        private SexType sex;

        public PersonModel()
        {
             
         //  this.initials = initials;
         //  this.height = height;
        //    this.CreateFileAsync();

            this.Sex = Enum.GetValues(typeof(SexType)).OfType<SexType>().ToArray(); 
        }

        //public PersonModel(PersonModel c)
        //{
        //    // TODO: Complete member initialization
        //  //  this.c = c;
        //    this.Initials = c.Initials;

        //}

        public string Initials
        {
            get
            {
                return this.initials;
            }

            set
            {
                this.initials = value;
                this.RaisePropertyChanged("Initials");
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
                this.RaisePropertyChanged("Height");
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                this.age = value;
                this.RaisePropertyChanged("Age");
            }
        }

        public double Bmi
        {
            get
            {
                return this.bmi;
            }

            set
            {
                this.bmi = value;
                this.RaisePropertyChanged("Bmi");
            }
        }

        public double Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                this.weight = value;
                this.RaisePropertyChanged("Weight");
            }
        }

        public IEnumerable<SexType> Sex
        {
            get;
            private set;
        }

        private SexType selectedSex = SexType.Female;

        public SexType SelectedSex
        {
            get { return this.selectedSex; }
            set
            {
                this.selectedSex = value;
                this.RaisePropertyChanged("IsMale");
                this.RaisePropertyChanged("IsFemale");   
                this.RaisePropertyChanged("SelectedSex");
                
            }
        }

        public bool IsMale
        {
            get { return this.SelectedSex == SexType.Male; }
            set { if (value) this.SelectedSex = SexType.Male; }
        }

        public bool IsFemale
        {
            get { return this.SelectedSex == SexType.Male; }
            set { if (value) this.SelectedSex = SexType.Female; }
        }

        //public SexType Sex
        //{
        //    get
        //    {
        //        return this.sex;
        //    }

        //    set
        //    {
        //        this.sex = value;
        //        this.RaisePropertyChanged("Sex");
        //    }
        //}

        

        public async Task SaveAsync()
        {
            StorageFolder sf = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Data", CreationCollisionOption.OpenIfExists);
            StorageFile st = await sf.CreateFileAsync("Data.xml", CreationCollisionOption.OpenIfExists);

            XmlDocument doc = new XmlDocument();
                            
            var content = await FileIO.ReadTextAsync(st);
            XmlElement root;
            if (!string.IsNullOrEmpty(content))
            {
                doc.LoadXml(content);
                root = doc.DocumentElement;
            }
            else
            {
                root = doc.CreateElement("persons");
                doc.AppendChild(root);

            }

            
            XmlElement item = doc.CreateElement("person");
           // item.InnerText = "item";
            root.AppendChild(item);

            XmlElement newElem = doc.CreateElement("Initials");
            newElem.InnerText = this.Initials;
            item.AppendChild(newElem);

            newElem = doc.CreateElement("Height");
            newElem.InnerText = this.Height.ToString();
            item.AppendChild(newElem);

            newElem = doc.CreateElement("Age");
            newElem.InnerText = this.Age.ToString();
            item.AppendChild(newElem);

            newElem = doc.CreateElement("Bmi");
            newElem.InnerText = ((double)this.Weight/(2*(this.Height)/100)).ToString();
            item.AppendChild(newElem);

            newElem = doc.CreateElement("Weight");
            newElem.InnerText = this.Weight.ToString();
            item.AppendChild(newElem);

            newElem = doc.CreateElement("Sex");
            newElem.InnerText = this.SelectedSex.ToString();
            item.AppendChild(newElem);

            await doc.SaveToFileAsync(st);
        }
    }
}
