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
        string id,idCurso,idEstudiante;
        bool origen, Inicio;
        double Resultado;

        public SubCategoria(String idRubrica, String id, String idCurso, String idEstudiante, bool origen,bool Inicio,Double Resultado)
        {
            this.origen = origen;
            if (origen == true)
            {
                this.Inicio = Inicio;
                this.idCurso = idCurso;
                this.idEstudiante = idEstudiante;
            }
            this.id = id;
            this.Resultado = Resultado;
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
            .Child("SubCategoria" + id)
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

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (origen == true)
            {
                if (e.SelectedItem == null) return;
                Elemento data = e.SelectedItem as Elemento;
                String peso = "" + data.Peso;
                String l1 = "" + data.L1;
                String l2 = "" + data.L2;
                String l3 = "" + data.L3;
                String l4 = "" + data.L4;
                var formulario1 = new Calificar(id, idCurso,idEstudiante, origen,l1,l2,l3,l4,peso,Resultado, Inicio);
                formulario1.BindingContext = data;
                await Navigation.PushAsync(formulario1);
            }
            else
            {

                if (e.SelectedItem == null) return;
                Elemento data = e.SelectedItem as Elemento;
                String peso = "" + data.Peso;
                String l1 = "" + data.L1;
                String l2 = "" + data.L2;
                String l3 = "" + data.L3;
                String l4 = "" + data.L4; 

                var formulario1 = new Calificar(id,"","",origen,l1,l2,l3,l4,peso,Resultado,Inicio);
                formulario1.BindingContext = data;
                await Navigation.PushAsync(formulario1);
            }
        }
       


        public async void Handle_Toolbar_Add(object sender, EventArgs e)
        {

            Elemento c = new Elemento();
            var formulario1 = new FormularioElemento(true,id);
            formulario1.BindingContext = c;
            await Navigation.PushAsync(formulario1);
        }

        public async void Handle_Toolbar_DeleteAll(object sender, EventArgs e)
        {
            await firebase
                .Child("SubCategoria" + id).DeleteAsync();
            await getList();
        }


        async void OnDeleteItem(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Elemento data = item.CommandParameter as Elemento;
            await firebase
                .Child("SubCategoria" + Id).Child(data.Uid).DeleteAsync();

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
