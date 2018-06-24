using System;
using System.Collections.Generic;

namespace DontSkipThePlaces.Models
{

	                

	public class Photo
    {
        public int height { get; set; }
        public List<string> html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
		public string photo_url {
			get
            {
				string PhotoUrl = $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference={this.photo_reference}&key={Constants.ApiKey}";
				return PhotoUrl;
            }
		}
    }
}
