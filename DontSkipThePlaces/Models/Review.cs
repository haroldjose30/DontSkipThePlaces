using System;
namespace DontSkipThePlaces.Models
{
	public class Review
    {
        public string author_name { get; set; }
        public string author_url { get; set; }
        public string language { get; set; }
        public string profile_photo_url { get; set; }
        public int rating { get; set; }
        public string relative_time_description { get; set; }
        public string text { get; set; }
        public int time { get; set; }
		public string text_formated
        {
            get
            {
				string ctext_formated = "";
				if (this.text.Length > 100)
					ctext_formated = this.text.Substring(0,100)+"...";
				return ctext_formated;
            }
        }
    }
}
