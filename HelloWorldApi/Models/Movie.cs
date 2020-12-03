using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldApi.Models
{
    public class Movie
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public int year { get; set; }
        public int runtime { get; set; }
        public string[] categories { get; set; }

        [JsonProperty("release-date")]
        public string releaseDate { get; set; }

        public string director { get; set; }
        public string[] writer { get; set; }
        public string[] actors { get; set; }
        public string storyline { get; set; }
    }
}
