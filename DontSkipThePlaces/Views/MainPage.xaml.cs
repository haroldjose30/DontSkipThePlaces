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
        public MainPage()
        {
            InitializeComponent();

            //initialize the map
			MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
            new Position(-23.581776, -46.700551),
                         Distance.FromMiles(0.5)));
			
        }


		RootPlace oRootPlace;


		Boolean isBusy = false;

		/// <summary>
		/// GetData  from Google place Api
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void BtnGetData_Clicked(object sender, System.EventArgs e)
		{

			try
			{
				isBusy = true;
				IndLoading.IsVisible = isBusy;
				IndLoading.IsRunning = isBusy;

                const string RestUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-23.581776,%20-46.700551&radius=5000&type=restaurant&keyword=cruise&key=AIzaSyDilcr5nJRznxSc10APEG8wgBzVqyQ_3wM";
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
