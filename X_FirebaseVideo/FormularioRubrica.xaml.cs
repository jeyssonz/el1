using System;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Xamarin.Forms;


namespace X_FirebaseVideo
{
   // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormularioRubrica : ContentPage
    {
        private bool add;
        FirebaseClient firebase;
        public FormularioRubrica(bool add)
        {
      
            this.add = add;
            firebase = new FirebaseClient("https://calificador-de-rubrica.firebaseio.com/");
            InitializeComponent();
            if (add)
            {
                Title = "Nueva Rubrica";
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
                .Child("Rubrica")
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PostAsync(item);
            }
            else
            {
                await firebase
                .Child("Rubrica")
                .Child(item.Uid)
                //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                .PutAsync(item);


            }

            await Navigation.PopAsync();

        }
    }
}
