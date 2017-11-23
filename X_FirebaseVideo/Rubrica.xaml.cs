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
    public partial class Rubrica : ContentPage
    {
        
        ObservableCollection<Contact> people = new ObservableCollection<Contact>();
        FirebaseClient firebase;
        private bool _isRefreshing = false;
        String a;
        private bool origen;
        public Rubrica(bool origen,string a)
        {
            this.a = a;
            this.origen = origen;
            Title = "Rubrica";
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
            .Child("Rubrica")
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
            
                if (e.SelectedItem == null) return;
                Contact data = e.SelectedItem as Contact;

                string dui = data.Notes;
            if (origen == true)
            {
                var formulario1 = new Categoria(dui,"","",false);
                formulario1.BindingContext = data;
                await Navigation.PushAsync(formulario1);
            }
            else
            {
                var formulario1 = new Menu2(a,dui,true);
                await Navigation.PushAsync(formulario1);
            }
        }


        public async void Handle_Toolbar_Add(object sender, EventArgs e)
        {
            if (origen == true)
            {

                Contact c = new Contact();
                var formulario1 = new FormularioRubrica(true);
                formulario1.BindingContext = c;
                await Navigation.PushAsync(formulario1);
            }
        }

        public async void Handle_Toolbar_DeleteAll(object sender, EventArgs e)
        {
            if (origen == true)
            {
                await firebase
                .Child("Rubrica").DeleteAsync();
                await getList();
            }
        }


        async void OnDeleteItem(object sender, EventArgs e)
        {
            if (origen == true)
            {
                MenuItem item = (MenuItem)sender;
                Contact data = item.CommandParameter as Contact;
                await firebase
                    .Child("Rubrica").Child(data.Uid).DeleteAsync();

                await getList();
            }
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