using Xamarin.Forms;
using GameLib;

namespace Game2048App
{
    public partial class GamePage : ContentPage
    {
        public GamePage(int gameSize = 3)
        {
            Game game = new Game((byte) gameSize);

            InitializeComponent();
            gvMain.Init(game);
        }
    }
}
