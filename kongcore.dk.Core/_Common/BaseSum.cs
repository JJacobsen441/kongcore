using kongcore.dk.Core._Statics;

namespace kongcore.dk.Core._Common
{
    public class BaseSum
    {
        public int qty;
        public double ship;
        public double vat;
        public double t_vat;
        public int b_price;
        public double n_price;
        public double b_total;
        public double n_total;
        public double b_total_noship;
        public double n_total_noship;

        //public string vat;
        public string str_qty;
        public string str_ship;
        public string str_vat;
        public string str_t_vat;
        public string str_b_price;
        public string str_n_price;
        public string str_b_total;
        public string str_n_total;
        public string str_b_total_noship;
        public string str_n_total_noship;

        public string str_ship_100;
        public string str_t_vat_100;
        public string str_b_price_100;
        public string str_n_price_100;
        public string str_b_total_100;
        public string str_n_total_100;
        public string str_b_total_noship_100;
        public string str_n_total_noship_100;

        private string DecimalNumber(double num, bool _100)
        {
            string[] arr;
            if (("" + num).Contains("."))
                arr = ("" + num).Split('.');
            else if (("" + num).Contains(","))
                arr = ("" + num).Split(',');
            else
                arr = new string[] { "" + num, "00" };

            string first = arr[0];
            string dec;

            if (arr.Length > 0)
                dec = arr[1];
            else
                dec = "0";

            if (dec.Length == 1)
                dec = dec.Substring(0, 1) + "0";
            else if (dec.Length > 1)
                dec = dec.Substring(0, 2);
            string div = ".";
            if (_100)
                div = "";
            return first + div + dec;
        }

        public static double Vat(string price, out double vat)
        {
            double pr = double.Parse(price);
            double non = StaticsHelper.Round(pr * 0.8, 100, false);
            vat = StaticsHelper.Round(pr - non, 100, true);
            return non;
        }

        public void Set()
        {
            str_qty = "" + qty;
            str_ship = "" + ship + ".00";
            str_vat = DecimalNumber(vat, false);
            str_t_vat = DecimalNumber(t_vat, false);
            str_b_price = b_price + ".00";
            str_n_price = DecimalNumber(n_price, false);
            str_b_total = DecimalNumber(b_total, false);
            str_n_total = DecimalNumber(n_total, false);
            str_b_total_noship = DecimalNumber(b_total_noship, false);
            str_n_total_noship = DecimalNumber(n_total_noship, false);

            str_t_vat_100 = DecimalNumber(t_vat, true);
            str_b_price_100 = b_price + "00";
            str_n_price_100 = DecimalNumber(n_price, true);
            str_b_total_100 = DecimalNumber(b_total, true);
            str_n_total_100 = DecimalNumber(n_total, true);
            str_b_total_noship_100 = DecimalNumber(b_total_noship, true);
            str_n_total_noship_100 = DecimalNumber(n_total_noship, true);
            str_ship_100 = ship + "00";
        }

        public BaseSum BaseCalc(string amount, string quantity, string shipping)
        {
            BaseSum bs = new BaseSum();
            bs.qty = int.Parse(quantity);
            bs.ship = double.Parse(shipping);

            bs.b_price = int.Parse(amount);

            bs.n_price = Vat("" + bs.b_price, out bs.vat);
            bs.b_total = bs.qty * bs.b_price + bs.ship;
            bs.n_total = bs.qty * bs.n_price + bs.ship;
            bs.b_total_noship = bs.qty * bs.b_price;
            bs.n_total_noship = bs.qty * bs.n_price;
            Vat("" + bs.b_price * bs.qty, out bs.t_vat);
            bs.Set();

            return bs;
        }
    }
}
