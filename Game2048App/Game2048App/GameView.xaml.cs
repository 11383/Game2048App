using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameLib;

namespace Game2048App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameView : ContentView
	{
        private int padding = 10;
        private int size;
        private Game game;
        private List<GameCell> gameCells = new List<GameCell>();

		public GameView ()
		{
			InitializeComponent ();
        }

        public void Init (int size = 3)
        {
            this.size = size;
            this.game = new Game((byte) size);
            this.RenderBackground();
            this.Render();
        }

        private void RenderBackground()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    var cell = new GameCell("");
                    gvContainer.Children.Add(cell, x, y);
                }
            }
        }

        private void Render ()
        {
            gameCells.ForEach(cell => gvContainer.Children.Remove(cell));

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    var value = game.GameBoard[x, y];

                    if (value != 0)
                    {
                        var cell = new GameCell(value.ToString());
                        gvContainer.Children.Add(cell, x, y);
                        gameCells.Add(cell);
                    }
                }
            }
        }

        private void GvRoot_SizeChanged(object sender, EventArgs e)
        {
            var size = Math.Min(gvRoot.Width, gvRoot.Height);
            gvContainer.WidthRequest = gvContainer.HeightRequest = size - (padding * 2);
        }
    }
}