using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace X_FirebaseVideo
{
    public partial class SecondPage : ContentPage
    {
        private bool add;
        FirebaseClient firebase;

        public SecondPage(bool add)
        {
            this.add = add;
            firebase = new FirebaseClient("https://calificador-de-rubrica.firebaseio.com/");
            InitializeComponent();
            if (add){
                Title = "Nuevo elemento";
            } else {
                Title = "Edición";
            }

        }


        async void Save_Clicked(object sender, EventArgs e)
        {

            var item = (Contact)BindingContext;

            if (add)
            {
                await firebase
                .Child("yourentity")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PostAsync(item);
            } else 
            {
                await firebase
                .Child("yourentity")
                .Child(item.Uid)
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(item);


            }

            await Navigation.PopAsync();

        }
    }
}
