using System;
using System.ComponentModel;

namespace X_FirebaseVideo
{
    public class Contact : INotifyPropertyChanged
    {

        private string uid;
        private string fullName;
        private string notes;

        public Contact()
        {
        }

        public Contact(string fullName, string notes)
        {

            FullName = fullName;
            Notes = notes;
            Uid = "";
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


        public string FullName
        {
            set
            {
                if (fullName != value)
                {
                    fullName = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("FullName"));
                    }
                }
            }
            get
            {
                return fullName;
            }
        }

        public string Notes
        {
            set
            {
                if (notes != value)
                {
                    notes = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("Notes"));
                    }
                }
            }
            get
            {
                return notes;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
