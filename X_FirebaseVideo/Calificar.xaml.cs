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
    public partial class Calificar : ContentPage
    {
        bool Origen, Inicio;
        Double Resultado, Peso,CategoriaPeso;
        int b;
        String id, idCurso, idEstudiante, L1, L2, L3, L4;
        public Calificar(String id, String idCurso, String idEstudiante, bool origen, String l1, String l2, String l3, String l4, String peso, Double Resultado,Boolean Inicio)
        {
            if(Inicio==true)
            {
                Resultado = 0;
            }
            this.id = id;
            this.idCurso = idCurso;
            this.idEstudiante = idEstudiante;
            this.L2 = l2;
            this.L3 = l3;
            this.L4 = l4;
            this.L1 = l1;
            this.Resultado = Resultado;
            Peso = Double.Parse(peso);
            CategoriaPeso = Double.Parse(id);
            InitializeComponent();
            Title = "Califiacr";
            MainPicker.Items.Add(l1);
            MainPicker.Items.Add(l2);
            MainPicker.Items.Add(l3);
            MainPicker.Items.Add(l4);
        }

        private void Boton_Clicked(object sender, EventArgs e)
        {
            double S = Slider.Value ;
            Double ElPeso,CPeso;
            ElPeso = Peso / 100;
            CPeso = CategoriaPeso / 100;
            Double Total = S + b;
             Resultado = Resultado + (Total * ElPeso * CPeso);

            DisplayAlert("Nota","Nota:"+Total+" Peso: " + ElPeso +"Total Acomulado :"+Resultado,"ok");
            Navigation.PushAsync(new Categoria(id, idCurso, idEstudiante, true, false, Resultado));

        }

        private void MainPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var name = MainPicker.Items[MainPicker.SelectedIndex];

             b=MainPicker.SelectedIndex;
            b = b + 1;
            string a;
            a=""+name;
           
        }
        
    }
}
