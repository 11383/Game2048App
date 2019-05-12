using System.Collections.Generic;
using Game2048App.Models;
using Xamarin.Forms;

namespace Game2048App
{
    public partial class MenuPage : ContentPage
    {
        private int gameSize = 3;
        private List<CarouselDataModel> carouselSlides = new List<CarouselDataModel>();

        public MenuPage()
        {
            InitializeComponent();

            carouselSlides = new List<CarouselDataModel> {
                new CarouselDataModel { Value = 3, Name = "3x3", Source = ImageSource.FromResource("Game2048App.Resources.grid-3x3.png")},
                new CarouselDataModel { Value = 4, Name = "4x4", Source = ImageSource.FromResource("Game2048App.Resources.grid-4x4.png")},
                new CarouselDataModel { Value = 5, Name = "5x5", Source = ImageSource.FromResource("Game2048App.Resources.grid-5x5.png")}
            };

            cvMain.ItemsSource = carouselSlides;
        }

        void OnCarouselPositionChanged(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            gameSize = carouselSlides[e.NewValue].Value;
        }

        async void OnStartGame(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new GamePage(gameSize));
        }
    }
}
