using System;
using System.ComponentModel;

namespace X_FirebaseVideo
{
    public class Elemento : INotifyPropertyChanged
    {

        private string uid;
        private string nombre;
        private string peso;
        private string l1;
        private string l2;
        private string l3;
        private string l4;
       

        public Elemento()
        {
        }

        public Elemento(string nombre, string peso ,string l1 , string l2 , string l3 , string l4)
        {
            L1 = l1;
            L2 = l2;
            L3 = l3;
            L4 = l4;
            Nombre = nombre;
            Peso = peso;
            Uid = "";
        }


        public string Nombre
        {
            set
            {
                if (nombre != value)
                {
                    nombre = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("Nombre"));
                    }
                }
            }
            get
            {
                return nombre;
            }
        }

        public string Peso
        {
            set
            {
                if (peso != value)
                {
                    peso = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("Peso"));
                    }
                }
            }
            get
            {
                return peso;
            }
        }

        

        public string L1
        {
            set
            {
                if(l1 != value)
                {
                    l1 = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("L1"));
                    }
                }
            }
            get
            {
                return l1;
            }
        }
        public string L2
        {
            set
            {
                if (l2 != value)
                {
                    l2 = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("L2"));
                    }
                }
            }
            get
            {
                return l2;
            }
        }

        public string L3
        {
            set
            {
                if (l3 != value)
                {
                    l3 = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("L3"));
                    }
                }
            }
            get
            {
                return l3;
            }
        }


        public string L4
        {
            set
            {
                if (l4 != value)
                {
                    l4 = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("L4"));
                    }
                }
            }
            get
            {
                return l4;
            }
        }


        public string Uid
        {
            set
            {
                if (uid != value)
                {
                    uid = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("Uid"));
                    }
                }
            }
            get
            {
                return uid;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
