using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Game2048App
{
    public partial class GameCell : ContentView
    {
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
        }

        private Color GetColor(string value, string type)
        {
            try
            {
                return (Color)Application.Current.Resources[$"GameCell.{type}:{value}"];
            }
            catch (KeyNotFoundException)
            {
                return (Color)Application.Current.Resources[$"GameCell.{type}:0"];
            }
        }
    }
}
