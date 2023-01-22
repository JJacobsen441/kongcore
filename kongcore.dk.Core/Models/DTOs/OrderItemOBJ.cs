using Newtonsoft.Json.Linq;
using System;
using static kongcore.dk.Core._Common.Enums;

namespace kongcore.dk.Core.Models.DTOs
{
    public class OrderItemOBJ
    {
        public string payment_id = "";
        public string token = "";
        public bool tokenexists = false;

        public string Total
        {
            get
            {
                return "" + (p_amt * p_qty);
            }
        }

        //public long s_id { get; set; }
        //public bool p_free { get; set; }
        //public bool p_value { get; set; }
        //public string type { get; set; }
        //public int expected { get; set; }

        public double ship_fee { get; set; }
        
        //public string s_owner { get; set; }
        public string s_name { get; set; }
        public string s_email { get; set; }
        public int s_phone { get; set; }
        public string s_address { get; set; }

        public long p_id { get; set; }//bliver ikke gemt i DB
        public double p_weight { get; set; }//bliver ikke gemt i DB

        public string p_name { get; set; }
        public int p_amt { get; set; }
        public int p_qty { get; set; }

        
        public string ToString(FORMAT type)
        {
            string s1 = "<table><tr><td>vare:</td><td>" + p_name + "</td></tr><tr><td>butik:</td><td>" + s_name + "</td></tr><tr><td>beløb:</td><td>" + p_amt + ",00 kr</td></tr><tr><td>antal:</td><td>" + p_qty + "</td></tr></table>";
            string s2 = "vare: " + p_name + "<br />butik: " + s_name + "<br />beløb: " + p_amt + ",00 kr<br />antal: " + p_qty;

            switch (type)
            {
                case FORMAT.TABLE: return s1;
                case FORMAT.RAW: return s2;
                default: throw new Exception(); ;
            }
        }

        public void FillFromJSON(string json)
        {
            dynamic stuff = JObject.Parse(json);

            this.token = stuff.token;

            //this.s_id = stuff.s_id;
            //if (stuff.p_free != null)
            //    this.p_free = stuff.p_free;
            //if (stuff.p_value != null)
            //    this.p_value = stuff.p_value;
            //this.expected = stuff.expected;
            //this.type = stuff.type;

            this.ship_fee = stuff.ship_fee;

            //this.s_owner = stuff.s_owner;
            this.s_name = stuff.s_name;
            this.s_email = stuff.s_email;
            this.s_phone = stuff.s_phone;
            this.s_address = stuff.s_address;

            this.p_id = stuff.p_id;
            this.p_weight = stuff.p_weight;

            this.p_name = stuff.p_name;
            this.p_amt = stuff.p_amount;
            this.p_qty = stuff.p_quantity;
        }
    }
}
