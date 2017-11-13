using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;


namespace X_FirebaseVideo
{
   // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Formulario1 : ContentPage
    {
        private bool add;
        FirebaseClient firebase;
        public Formulario1(bool add)
        {
            this.add = add;
            firebase = new FirebaseClient("https://calificador-de-rubrica.firebaseio.com/");
            InitializeComponent();
            if (add)
            {
                Title = "Nuevo Curso";
            }
            else
            {
                Title = "Edición";
            }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var item = (Contact)BindingContext;

            if (add)
            {
                await firebase
                .Child("Curso")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PostAsync(item);
            }
            else
            {
                await firebase
                .Child("Curso")
                .Child(item.Uid)
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(item);


            }

            await Navigation.PopAsync();

        }
    }
}
