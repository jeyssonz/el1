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
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubCategoria : ContentPage
    {
        ObservableCollection<Elemento> people = new ObservableCollection<Elemento>();
        FirebaseClient firebase;
        private bool _isRefreshing = false;
        string a;

        public SubCategoria(String id)
        {
            a = id;
            Title = "SubCategoria";
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
            .Child("SubCategoria" + a)
            .OnceAsync<Elemento>());

            people.Clear();

            Debug.WriteLine("Número de entradas en firebase " + list.Count);


            foreach (var item in list)
            {
                Elemento c = item.Object as Elemento;
                c.Uid = item.Key;
                people.Add(c);
            }

            indicator.IsRunning = false;
            indicator.IsVisible = false;

            return 0;

        }

       /* async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem == null) return;
            Elemento data = e.SelectedItem as Elemento;

            string dui = data.Peso;
            var formulario1 = new FormularioElemento(false);
            formulario1.BindingContext = data;
            await Navigation.PushAsync(formulario1);
        }
        */


        public async void Handle_Toolbar_Add(object sender, EventArgs e)
        {

            Elemento c = new Elemento();
            var formulario1 = new FormularioElemento(true,a);
            formulario1.BindingContext = c;
            await Navigation.PushAsync(formulario1);
        }

        public async void Handle_Toolbar_DeleteAll(object sender, EventArgs e)
        {
            await firebase
                .Child("SubCategoria" + a).DeleteAsync();
            await getList();
        }


        async void OnDeleteItem(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Elemento data = item.CommandParameter as Elemento;
            await firebase
                .Child("SubCategoria" + a).Child(data.Uid).DeleteAsync();

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
