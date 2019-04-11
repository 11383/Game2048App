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
        }

        private void GvRoot_SizeChanged(object sender, EventArgs e)
        {
            var size = Math.Min(gvRoot.Width, gvRoot.Height);
            Console.WriteLine(size);
            gvContainer.WidthRequest = gvContainer.HeightRequest = size - (padding * 2);
        }
    }
}