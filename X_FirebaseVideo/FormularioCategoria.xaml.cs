using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;


namespace X_FirebaseVideo
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormularioCategoria : ContentPage
    {
        private bool add;
        string b;
        FirebaseClient firebase;
        public FormularioCategoria(bool add,string a)
        {
            b = a;
            this.add = add;
            firebase = new FirebaseClient("https://calificador-de-rubrica.firebaseio.com/");
            InitializeComponent();
            if (add)
            {
                Title = "Nueva Categoria";
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
                .Child("Categoria" + b)
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PostAsync(item);
            }
            else
            {
                await firebase
                .Child("Categoria" + b)
                .Child(item.Uid)
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(item);


            }

            await Navigation.PopAsync();

        }
    }

}
