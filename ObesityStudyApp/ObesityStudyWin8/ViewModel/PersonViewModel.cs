using GalaSoft.MvvmLight;
using ObesityStudyWin8.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObesityStudyWin8.ViewModel
{
    public class PersonViewModel:ViewModelBase
    {
       // public PersonModel Person { get; set; }

        public PersonViewModel()
        {
           // this.Person = new PersonModel();
          //  this.Sex = Enum.GetValues(typeof(SexType)).OfType<SexType>().ToArray();   
         //  this.initials = initials;
         //  this.height = height;
        //    this.CreateFileAsync();
        }

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

        private SexType selectedSex = SexType.Male;

        public SexType SelectedSex
        {
            get { return this.selectedSex; }
            set
            {
                this.selectedSex = value;
                this.RaisePropertyChanged("Sex");
            }
        }

        public string initials { get; set; }

        public double height { get; set; }

        public int age { get; set; }

        public double bmi { get; set; }

        public double weight { get; set; }
    }
}
