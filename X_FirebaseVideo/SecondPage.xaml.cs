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
        String a,b;

        public SecondPage(bool add,String a, String b)
        {
            this.a = a;
            this.b = b;
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
                .Child("Evaluación" + a + b)
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PostAsync(item);
            } else 
            {
                await firebase
                .Child("Evaluación" + a + b)
                .Child(item.Uid)
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(item);


            }

            await Navigation.PopAsync();

        }
    }
}
