using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameLib;
using static GameLib.GameBoard;

namespace Game2048App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameView : ContentView
	{
        private int padding = 10;
        private Game game;
        private GameCell[,] gameCells;

		public GameView ()
		{
			InitializeComponent ();
        }

        public void Init (Game game)
        {
            this.game = game;
            game.Restart();
            gameCells = new GameCell[game.Size, game.Size];
            RenderBackground();
            Render();
        }

        public void RenderAnimation()
        {
            GameCell gameCell = null;
            var animation = new Animation();

            game.LastTransforms.ForEach(transform =>
            {
                //translate && merge
                if (transform.Type != TransformType.New)
                {
                    gameCell = gameCells[transform.LastY, transform.LastX];
                    animation.Add(0, 0.3, gameCell.AnimateTranslation(transform));
                }

                if (transform.Type == TransformType.Merge)
                {
                    gameCell = AddGameCell(game.GameBoard[transform.Y, transform.X], transform.X, transform.Y);
                    animation.Add(0.2, 0.6, gameCell.AnimateNew());
                }

                if (transform.Type == TransformType.New)
                {
                    gameCell = AddGameCell(game.GameBoard[transform.Y, transform.X], transform.X, transform.Y);
                    animation.Add(0.6, 1, gameCell.AnimateNew());
                }
            });

            animation.Commit(this, "gameCellsTransforms", 16, 400, null,(arg1, arg2) => Render());
        }

        private void RenderBackground()
        {
            for (int y = 0; y < game.Size; y++)
            {
                for (int x = 0; x < game.Size; x++)
                {
                    var cell = new GameCell("");
                    gvContainer.Children.Add(cell, x, y);
                }
            }
        }

        public void Render ()
        {
            ClearComponent();

            for (int y = 0; y < game.Size; y++)
            {
                for (int x = 0; x < game.Size; x++)
                {  
                    var value = game.GameBoard[y, x];

                    if (value != 0)
                    {
                        AddGameCell(value, x, y);
                    }
                }
            }
        }

        private GameCell AddGameCell(int value, int x, int y)
        {
            GameCell gc = new GameCell(value.ToString());
            gvContainer.Children.Add(gc, x, y);
            gameCells[y, x] = gc;

            return gc;
        }

        private void ClearComponent()
        {   
            foreach(GameCell item in gvContainer.Children.Reverse())
            {
                if (!item.IsEmpty)
                {
                    gvContainer.Children.Remove(item);
                }
            }

            gameCells = new GameCell[game.Size, game.Size];
        }

        void Handle_Swiped(object sender, Xamarin.Forms.SwipedEventArgs e)
        {
            switch(e.Direction)
            {
                case SwipeDirection.Up:
                    game.MoveTop();
                    break;
                case SwipeDirection.Right:
                    game.MoveRight();
                    break;
                case SwipeDirection.Down:
                    game.MoveBottom();
                    break;
                case SwipeDirection.Left:
                    game.MoveLeft();
                    break;
            }

            RenderAnimation();
        }

        private void GvRoot_SizeChanged(object sender, EventArgs e)
        {
            var size = Math.Min(gvRoot.Width, gvRoot.Height);
            gvContainer.WidthRequest = gvContainer.HeightRequest = size - (padding * 2);
        }
    }
}