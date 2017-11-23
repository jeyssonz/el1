using System;
using System.ComponentModel;

namespace X_FirebaseVideo
{
    public class Medidor : INotifyPropertyChanged
    {
        private string categoria;
        private string subCategoria;

        public Medidor ()
        {
        }
        public Medidor(string categoria, string subCategoria)
        {
        Categoria = categoria;
        SubCategoria = subCategoria;
        }

        public string Categoria
        {
            set
            {
                if (categoria != value)
                {
                    categoria = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("Categoria"));
                    }
                }
            }
            get
            {
                return categoria;
            }
        }
        public string SubCategoria
        {
            set
            {
                if (subCategoria != value)
                {
                    subCategoria = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                                        new PropertyChangedEventArgs("SubCategoria"));
                    }
                }
            }
            get
            {
                return subCategoria;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
