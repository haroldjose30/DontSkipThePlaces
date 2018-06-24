using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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


        /// <summary>
        /// GetData  from Google place Api
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void BtnGetData_Clicked(object sender, System.EventArgs e)
        {

            const string RestUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-23.581776,%20-46.700551&radius=5000&type=restaurant&keyword=cruise&key=AIzaSyDilcr5nJRznxSc10APEG8wgBzVqyQ_3wM";
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<RootObject>(content);

                foreach (var local in Items.results)
                {
                    var pin = new Pin
                    {
                        Type = PinType.SearchResult,
                        Position = new Position(local.geometry.location.lat, local.geometry.location.lng),
                        Label = local.name,
                        Address = local.vicinity,

                    };

                    MyMap.Pins.Add(pin);
                }


            }
        }


    }
}
