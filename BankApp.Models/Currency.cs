
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using Newtonsoft.Json;

namespace BankApp.Models
{
    public class Currency
    {
        string url = "http://www.floatrates.com/daily/";
        public float exchangeRate;
        public string name { get; set; }
        public string currencyCode { get; set; }
        public Currency(string name ,string code)
        {
            url = url + code + "json";
            
            string json = new WebClient().DownloadString(url);
            var currency = JsonConvert.DeserializeObject<dynamic>(json);
            exchangeRate = Convert.ToSingle(currency.inr.rate);
            currencyCode = code;
             //Excbage rate With Respect to INR
             
        }
    }
}
