using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameLib;
using static GameLib.GameBoard;

namespace Game2048App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameBoard : ContentView
	{
        private int padding = 10;
        private Game game;
        private GameCell[,] gameCells;
        private bool endlessGameMode = false;

		public GameBoard ()
		{
			InitializeComponent ();
        }

        public void Init (Game game)
        {
            this.game = game;
            gameCells = new GameCell[game.Size, game.Size];
            RenderBackground();
            RefreshView();
            CheckGameState();
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

        public void RefreshView ()
        {
            Render();
            UpdateScore();
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

        private void UpdateScore()
        {
            lScore.Text = game.Score.ToString();
            lHighscore.Text = game.Highscore.ToString();
        }

        void OnButtonRestart(object sender, System.EventArgs e)
        {
            game.Restart();
            ClearComponent();
            RefreshView();
        }

        void OnButtonUndo(object sender, System.EventArgs e)
        {
            game.Undo();
            ClearComponent();
            RefreshView();
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
            UpdateScore();
            CheckGameState();
        }

        async void CheckGameState ()
        {
            if (!game.IsPlaying && !game.IsWin)
            {
                bool retry = await Application.Current.MainPage.DisplayAlert("Game Over!", $"Your score {game.Score}", "Retry", "Cancel");

                if (retry)
                {
                    game.Restart();
                    RefreshView();
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
            else if (game.IsWin && !endlessGameMode)
            {
                bool continueGame = await Application.Current.MainPage.DisplayAlert("You Win!", "Do you want to continue?", "Continue", "Restart");
            
                if (continueGame)
                {
                    endlessGameMode = true;
                } else
                {
                    game.Restart();
                    RefreshView();
                }
            }

        }

        private void GvRoot_SizeChanged(object sender, EventArgs e)
        {
            var size = Math.Min(gvRoot.Width, gvRoot.Height);
            gvContainerFrame.WidthRequest = gvContainerFrame.HeightRequest = size - (padding * 2);
        }
    }
}