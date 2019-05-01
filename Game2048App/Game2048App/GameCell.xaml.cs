using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using static GameLib.GameBoard;

namespace Game2048App
{
    public partial class GameCell : ContentView
    {
        public bool IsEmpty = false;

        public GameCell()
        {
            InitializeComponent();
        }

        public GameCell(String value = "")
        {
            InitializeComponent();
            gcTile.Text = value;

            bvTile.BackgroundColor = GetColor(value, "Background");
            gcTile.TextColor = GetColor(value, "Text");

            IsEmpty |= value == "";
        }

        public Animation AnimateTranslation(GameTranform transform)
        {
            var offsetX = transform.X - transform.LastX;
            var offsetY = transform.Y - transform.LastY;

            var xAnimation = new Animation(v => gcRoot.TranslationX = v, 0, offsetX * Size());
            var yAnimation = new Animation(v => gcRoot.TranslationY = v, 0, offsetY * Size());

            return new Animation
            {
                { 0, 1, xAnimation },
                { 0, 1, yAnimation }
            };
        }

        public Animation AnimateNew()
        {
            gcRoot.Scale = 0;
            var scaleUp = new Animation(v => gcRoot.Scale = v, 1, 1.15);
            var scaleDown = new Animation(v => gcRoot.Scale = v, 1.15, 1);

            return new Animation
            {
                { 0, 0.4, scaleUp },
                { 0.4, 1, scaleDown}
            };
        }

        private double Size() => gcTile.Width + 4; // width + grid spacing

        private Color GetColor(string value, string type)
        {
            try
            {
                return (Color)Application.Current.Resources[$"GameCell.{type}:{value}"];
            }
            catch (KeyNotFoundException)
            {
                return (Color)Application.Current.Resources[$"GameCell.{type}:fallback"];
            }
        }
    }
}
