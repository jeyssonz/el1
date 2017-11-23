using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X_FirebaseVideo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu2 : ContentPage
    {
        String a;
        String id;
        bool origen;
        public Menu2(String a,String id,bool origen)
        {
            this.origen = origen;
            this.id = id;
            this.a = a;
            Title = "Selección";
            InitializeComponent();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Estudiante(a,id,origen));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Eva(a));
        }

    }
}
