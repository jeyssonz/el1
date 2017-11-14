using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;

namespace X_FirebaseVideo
{
   // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Formulario2 : ContentPage
    {
        public string a;
        private bool add;
        FirebaseClient firebase;
        public Formulario2 (bool add, String a)
        {
            this.a = a;
            this.add = add;
            firebase = new FirebaseClient("https://calificador-de-rubrica.firebaseio.com/");
            InitializeComponent();
            if (add)
            {
                Title = "Nuevo Estudiante" + a;
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
                .Child("Estudiante" + a)
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PostAsync(item);
            }
            else
            {
                await firebase
                .Child("Estudiante" + a)
                .Child(item.Uid)
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(item);


            }

            await Navigation.PopAsync();

        }
    }
}
