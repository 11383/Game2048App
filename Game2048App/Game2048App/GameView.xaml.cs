using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game2048App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameView : ContentView
	{
        private int padding = 10;
		public GameView ()
		{
			InitializeComponent ();

            //cell1
            //var cell = new GameCell();
            //AbsoluteLayout.SetLayoutBounds(cell, new Rectangle(0, 50, 1, 1));

            //cell2
            //var cell2 = new GameCell();
            //AbsoluteLayout.SetLayoutBounds(cell2, new Rectangle(52, 50, 1, 1));

            //gvContainer.Children.Add(cell);
            //gvContainer.Children.Add(cell2);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                var cell = (GameCell)s;
                cell.TranslateTo(0, 50, 1000, Easing.CubicInOut);
            };

            gcTest.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void GvRoot_SizeChanged(object sender, EventArgs e)
        {
            var size = Math.Min(gvRoot.Width, gvRoot.Height);
            gvContainer.WidthRequest = gvContainer.HeightRequest = size - (padding * 2);
        }
    }
}