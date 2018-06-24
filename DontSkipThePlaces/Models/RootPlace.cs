using System;
using System.Collections.Generic;

namespace DontSkipThePlaces.Models
{
    public class RootPlace
    {
        public List<object> html_attributions { get; set; }
        public List<Place> results { get; set; }
        public string status { get; set; }
    }
}
