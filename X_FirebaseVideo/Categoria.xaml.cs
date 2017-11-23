using Xamarin.Forms;
using System.Collections.ObjectModel;
using System;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace X_FirebaseVideo
{
   // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Categoria : ContentPage
    {
        ObservableCollection<Contact> people = new ObservableCollection<Contact>();
        FirebaseClient firebase;
        private bool _isRefreshing = false;
        string a,idCurso,idEstudiante;
        bool origen;
        public Categoria(string id,String idCurso ,String idEstudiante, bool origen,bool Inicio,double Resultado)
        {
            
            this.origen = origen;
            if (origen == true)
            {
                this.idCurso = idCurso;
                this.idEstudiante = idEstudiante;
            }
                a = id;
            Title = "Categoria";
            InitializeComponent();
            firebase = new FirebaseClient("https://calificador-de-rubrica.firebaseio.com/");

            BindingContext = this;

            laLista.ItemsSource = people;
        }
   


        public async Task<int> getList()
        {
            if (IsRefreshing == false)
            {
                indicator.IsRunning = true;
                indicator.IsVisible = true;
            }

            var list = (await firebase
            .Child("Categoria" + a)
            .OnceAsync<Contact>());

            people.Clear();

            Debug.WriteLine("Número de entradas en firebase " + list.Count);


            foreach (var item in list)
            {
                Contact c = item.Object as Contact;
                c.Uid = item.Key;
                people.Add(c);
            }

            indicator.IsRunning = false;
            indicator.IsVisible = false;

            return 0;

        }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(origen==true)
            {
                if (e.SelectedItem == null) return;
                Contact data = e.SelectedItem as Contact;

                string CID = data.Notes;
                var formulario1 = new SubCategoria(a,CID,idCurso,idEstudiante,origen,true,0);
                formulario1.BindingContext = data;
                await Navigation.PushAsync(formulario1);
            }
            else
            {

                if (e.SelectedItem == null) return;
                Contact data = e.SelectedItem as Contact;

                string CID = data.Notes;
                var formulario1 = new SubCategoria(a,CID,"","",origen,true,0);
                formulario1.BindingContext = data;
                await Navigation.PushAsync(formulario1);
            }

        }


        public async void Handle_Toolbar_Add(object sender, EventArgs e)
        {

            Contact c = new Contact();
            var formulario1 = new FormularioCategoria(true,a);
            formulario1.BindingContext = c;
            await Navigation.PushAsync(formulario1);
        }

        public async void Handle_Toolbar_DeleteAll(object sender, EventArgs e)
        {
            await firebase
                .Child("Categoria" + a).DeleteAsync();
            await getList();
        }


        async void OnDeleteItem(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Contact data = item.CommandParameter as Contact;
            await firebase
                .Child("Categoria" + a).Child(data.Uid).DeleteAsync();

            await getList();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await getList();
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }


        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {

                    await getList();

                    IsRefreshing = false;
                });
            }
        }

    }
}
