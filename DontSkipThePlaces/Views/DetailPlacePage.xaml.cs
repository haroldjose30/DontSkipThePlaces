using System;
using System.Collections.Generic;
using System.Net.Http;
using DontSkipThePlaces.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DontSkipThePlaces.Views
{
    public partial class DetailPlacePage : ContentPage
    {

		Place oPlace;
		RootPlaceDetail oRootPlaceDetail;

        public DetailPlacePage(Place _Place)
        {
		    InitializeComponent();
			oPlace = _Place;
		
        }


		Boolean isBusy = false;

        /// <summary>
		/// searche de place datail from Google Place Api
        /// </summary>
		async protected override void OnAppearing()
		{
			base.OnAppearing();



			if (oPlace == null)
				return;

			try
            {
				isBusy = true;
                IndLoading.IsRunning = isBusy;
                IndLoading.IsVisible = isBusy;




				string RestUrl = $"https://maps.googleapis.com/maps/api/place/details/json?placeid={oPlace.id}&key=AIzaSyDilcr5nJRznxSc10APEG8wgBzVqyQ_3wM\n";
                var uri = new Uri(string.Format(RestUrl, string.Empty));

                HttpClient client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
					oRootPlaceDetail = JsonConvert.DeserializeObject<RootPlaceDetail>(content);
					this.BindingContext = oRootPlaceDetail.result;
                }
            }
			finally
            {
                isBusy = false;
                IndLoading.IsVisible = isBusy;
                IndLoading.IsRunning = isBusy;
            }


		}
	}
}
