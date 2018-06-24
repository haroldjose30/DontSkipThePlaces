using System;
using System.Collections.Generic;

namespace DontSkipThePlaces.Models
{
	public class OpeningHours
    {
        public bool open_now { get; set; }
        public List<object> weekday_text { get; set; }
    }
}
