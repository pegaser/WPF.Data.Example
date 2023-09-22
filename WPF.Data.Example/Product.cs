using System.ComponentModel;

namespace WPF.Data.Example
{
    public class Product : INotifyPropertyChanged
    {
        private int productID;
        public int ProductID 
        {
            get
            {
                return productID;
            }
            set
            {
                productID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProductID"));
            }
        }
        private string modelNumber;
        public string ModelNumber 
        {   get
            {
                return modelNumber;
            }
            set
            {
                modelNumber = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ModelNumber"));
            }
        }

        private string modelName;
        public string ModelName
        {
            get
            {
                return modelName;            }
            set
            {
                modelName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ModelName"));
            }
        }

        private decimal unitCost;
        public decimal UnitCost 
        {
            get
            {
                return unitCost;
            }
            set
            {
                unitCost = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UnitCost"));
            }
        }

        private string description;

        public string Description 
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Description"));
            }
        }

        public Product(int productID, string modelNumber, string modelName, decimal unitCost, string description)
        {
            this.ProductID = productID;
            this.modelNumber = modelNumber;
            this.modelName = modelName;
            this.unitCost = unitCost;
            this.description = description;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}