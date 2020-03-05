using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConsumeWebService.ConsumingService
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class APIDemo : ContentPage
    {
        public APIDemo()
        {
            InitializeComponent();
            BindingContext = new APIViewModel();
        }
    }

}