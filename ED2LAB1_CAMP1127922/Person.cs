using ED2LAB1_CAMP1127922.CMP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED2LAB1_CAMP1127922
{
    public class Person
    {
        public string name { get; set; }
        public string dpi { get; set; }
        public DateTime datebirth { get; set; }
        public string address { get; set; }       
        public List<string> companies { get; set;}
        [JsonIgnore]
        public List<string> letters { get; set;}
        [JsonIgnore]
        public List<Dictionary<string, int>> dictio { get; set;}
        [JsonIgnore]
        public List<string> conversations { get; set; }
        public string recluiter { get; set; }
        [JsonIgnore]
        public long private1 { get; set; }
        [JsonIgnore]
        public long private2 { get; set; }
    }
}
