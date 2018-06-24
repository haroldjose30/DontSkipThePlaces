using System;
using System.Collections.Generic;

namespace DontSkipThePlaces.Models
{
	public class RootPlaceDetail
    {
        public List<object> html_attributions { get; set; }
		public PlaceDetail result { get; set; }
        public string status { get; set; }
    }
}
