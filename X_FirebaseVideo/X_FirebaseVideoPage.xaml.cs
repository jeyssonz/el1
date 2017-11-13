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
    public partial class X_FirebaseVideoPage : ContentPage
    {
        ObservableCollection<Contact> people = new ObservableCollection<Contact>();
        FirebaseClient firebase;
        private bool _isRefreshing = false;

        public X_FirebaseVideoPage()
        {
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
            .Child("yourentity")
            .OnceAsync<Contact>());

            people.Clear();

            Debug.WriteLine("Número de entradas en firebase "+list.Count);

 
            foreach (var item in list){

                Contact c = item.Object as Contact;
                c.Uid = item.Key;
                people.Add(c);

            }

            indicator.IsRunning = false;
            indicator.IsVisible = false;

            return 0;

        }


        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            Contact data = e.SelectedItem as Contact;

            var secondPage = new SecondPage(false);
            secondPage.BindingContext = data;
            await Navigation.PushAsync(secondPage);
        }

        async void OnDeleteItem(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Contact data = item.CommandParameter as Contact;

            await firebase
                .Child("yourentity").Child(data.Uid).DeleteAsync();

            await getList();
        }

        public async void Handle_Toolbar_DeleteAll(object sender, System.EventArgs e)
        {
            await firebase
                .Child("yourentity").DeleteAsync();

            await getList();
        }

        public async void Handle_Toolbar_Add(object sender, System.EventArgs e)
        {
            Contact c = new Contact();
            var secondPage = new SecondPage(true);
            secondPage.BindingContext = c;
            await Navigation.PushAsync(secondPage);
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
