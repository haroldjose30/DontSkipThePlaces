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
    }
}
