﻿using Xamarin.Forms;
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
            Title = "Elija el curso";
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
            .Child("Curso")
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

            string dui = data.Notes;
            var secondPage = new El_Estudiante(dui);
            secondPage.BindingContext = data;
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
