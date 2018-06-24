using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DontSkipThePlaces.Models;
using DontSkipThePlaces.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace DontSkipThePlaces
{
    public partial class MainPage : ContentPage
    {
		double mocklatitude = -23.581776;
		double mockLongitude = -46.700551;

        public MainPage()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			//initialize the map
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(mocklatitude, mockLongitude),
                         Distance.FromMiles(0.5)));
			
			GetDataFromNear();
		}


		RootPlace oRootPlace;


		Boolean isBusy = false;

		/// <summary>
		/// GetData  from Google place Api
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void GetDataFromNear()
		{

			try
			{
				isBusy = true;
				IndLoading.IsVisible = isBusy;
				IndLoading.IsRunning = isBusy;

				string RestUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={mocklatitude.ToString()},%20{mockLongitude.ToString()}&radius=5000&type=restaurant&keyword=cruise&key={Constants.ApiKey}";
                var uri = new Uri(string.Format(RestUrl, string.Empty));

                HttpClient client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    oRootPlace = JsonConvert.DeserializeObject<RootPlace>(content);

                    Place firstPlace = new Place();
                    Boolean fistPlaceAssigned = false;

                    foreach (var local in oRootPlace.results)
                    {
                        //get the first place
                        if (!fistPlaceAssigned)
                            firstPlace = local;

                        var pin = new Pin()
                        {
                            Type = PinType.SearchResult,
                            Position = new Position(local.geometry.location.lat, local.geometry.location.lng),
                            Label = local.name,
                            Address = local.vicinity
                        };

                        pin.Clicked += Pin_Clicked;

                        MyMap.Pins.Add(pin);
                    }

                    //move the map to first pin
                    if (firstPlace != null)
                    {
                        MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                            new Position(firstPlace.geometry.location.lat, firstPlace.geometry.location.lng),
                                 Distance.FromMiles(1)));
                    }

                }
			}
			finally
			{
				isBusy = false;
                IndLoading.IsVisible = isBusy;
                IndLoading.IsRunning = isBusy;
			}
		}

  
        /// <summary>
        /// Pins the clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
		void Pin_Clicked(object sender, EventArgs e)
		{
			if (sender is Pin)
			{
				var pin = (Pin)sender;

				//locate de data from pi
				Place place = oRootPlace.results.Where(p => p.name.Equals(pin.Label)).FirstOrDefault();

				if (place != null)
				{
					Navigation.PushAsync(new DetailPlacePage(place));

				}

				//open the new page showing de pin details
			}
		}

        /// <summary>
        /// Buttons the show filter stacklayout.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
		async void BtnShowFilter_Clicked(object sender, System.EventArgs e)
		{
			StcFilterOptions.IsVisible = !StcFilterOptions.IsVisible;

			if (StcFilterOptions.IsVisible)
				BtnFilterOptions.Text = "Hide Filter Options";
			else
				BtnFilterOptions.Text = "Show Filter Options";
				
		}


    }
}
