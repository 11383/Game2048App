using Xamarin.Forms;
using GameLib;

namespace Game2048App
{
    public partial class MainPage : ContentPage
    {
        private Game game = new Game(3);

        public MainPage()
        {
            InitializeComponent();
            gvMain.Init(game);
        }
    }
}
