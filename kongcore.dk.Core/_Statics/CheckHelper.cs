using kongcore.dk.Core.Models.DTOs;
using kongcore.dk.Core.Models.VM;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kongcore.dk.Core._Statics
{
    public class CheckHelper
    {
        public static string IsValidFullName(string name, out bool _ok)
        {
            _ok = false;
            if (string.IsNullOrEmpty(name))
                return "";
            _ok = true;

            string[] na = name.Split(' ');
            bool ok;
            ValidateHelper.Sanitize(name, false, true, true, new List<string>() { "notag" }, CharacterHelper.All(false), out ok);
            
            _ok &= na.Length > 1;
            _ok &= ok;

            return _ok ? name : "";
        }

        public static string IsValidAddress(string address, out bool _ok)
        {
            _ok = false;
            if (string.IsNullOrEmpty(address))
                return "";
            _ok = true;

            return _ok ? address : "";
        }

        public static string IsValidPhonenumber(string nr, out bool _ok)
        {
            _ok = false;
            if (nr.IsNullOrEmpty())
                return "";
            _ok = true;

            int n;
            if (int.TryParse(nr, out n))
                _ok &= (_ok && n > 0 && n <= 99999999 && n.ToString().Count() == 8);

            return _ok ? nr : "";
        }

        public static string IsValidEmail(string email, out bool _ok)
        {
            try
            {
                _ok = false;
                if (email.IsNullOrEmpty())
                    return "";
                _ok = true;

                var addr = new System.Net.Mail.MailAddress(email);
                _ok &= addr.Address == email;
                
                return _ok ? email : "";
            }
            catch
            {
                _ok = false;
                return "";
            }
        }

        private static string Check(string val, bool allow_empty, bool decode, bool allow_upper, int len_lower, int len_upper, bool allow_newline, bool allow_numeric, List<string> allow_tag, char[] allowed, out bool _ok)
        {
            _ok = true;
            
            if (val.IsNullOrEmpty() && allow_empty)
                val = "";

            if (!val.IsNull())
            {
                if (val.Length >= len_lower && val.Length <= len_upper)
                {
                    val = val.Trim();

                    if (decode)
                        val = HttpUtility.UrlDecode(val);

                    if (!allow_upper)
                        val = val.ToLower();

                    val = ValidateHelper.Sanitize(val, allow_newline, allow_upper, allow_numeric, allow_tag, allowed, out _ok);
                    if (_ok)
                        return val;
                }
            }
            _ok = false;
            return val;
        }

        private static string CheckMail(string val, out bool _ok)
        {
            val = Check(val, false, false, true, 1, 100, false, true, new List<string>() { "no_tag" }, CharacterHelper.All(false), out _ok);
            if (_ok)
            {
                val = IsValidEmail(val, out _ok);
                if (_ok)
                    return val;
            }

            return "";
        }

        private static long CheckID(long val, out bool _ok)
        {
            _ok = true;
            if (val == 0)
                _ok = false;

            if (val < 0)
                _ok = false;

            return val;
        }
        
        private static long CheckDate(long val, out bool _ok)
        {
            /*
             * some checks for dates
             * */
            _ok = true;

            return val;
        }

        public static bool CheckSearch(SearchViewModel model)
        {
            if (model.IsNull())
                return false;

            /*
             * use fx 'no_tag' for no tags
             * */
            bool _ok_a;
            model.search_string = Check(model.search_string, true, false, false, 0, 20, false, true, new List<string>() { "no_tag" }, CharacterHelper.VeryLimited(false), out _ok_a);

            return _ok_a;
        }

        public static bool CheckContactForm(ContactFormViewModel model)
        {
            if (model.IsNull())
                return false;

            /*
             * use fx 'no_tag' for no tags
             * */
            bool _ok_a, _ok_b, _ok_c, _ok_d;
            model.name = Check(model.name, false, false, true, 1, 100, false, false, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_a);
            model.email = CheckMail(model.email, out _ok_b);
            model.subject = Check(model.subject, false, false, true, 1, 100, false, true, new List<string>() { "no_tag" }, CharacterHelper.All(false), out _ok_c);
            model.message = Check(model.message, false, false, true, 1, 5000, true, true, new List<string>() { "no_tag" }, CharacterHelper.All(true), out _ok_d);

            return _ok_a && _ok_b && _ok_c && _ok_d;
        }

        public static bool _CheckOrderItemOBJ(OrderItemOBJ obj)
        {
            bool ok1, ok2, ok3, ok4, ok5;

            ok1 = !string.IsNullOrEmpty(obj.token) && SessionSingleton.Current.Tokens.Contains(obj.token);

            ValidateHelper.Sanitize(obj.s_name, false, true, true, new List<string>() { "notag" }, CharacterHelper.Name(), out ok2);
            ValidateHelper.Sanitize(obj.p_name, false, true, true, new List<string>() { "notag" }, CharacterHelper.Product(), out ok3);

            ok4 = obj.p_id > 0;
            ok5 = obj.p_qty > 0;
            //ok &= obj.p_amt > 0;
            //ok &= obj.p_weight > 0;

            return ok1 && ok2 && ok3 && ok4 && ok5;
        }

        public static bool CheckOrderItemOBJ1(OrderItemOBJ obj)
        {
            //if (this.s_id < 0) return false;
            //if (string.IsNullOrEmpty(this.type)) return false;
            //if (this.expected < 0) return false;
            //if (string.IsNullOrEmpty(this.s_owner)) return false;

            //ok8 = !string.IsNullOrEmpty(obj.p_name);
            //ok2 = !string.IsNullOrEmpty(obj.s_name);

            bool ok1, ok2, ok3, ok4, ok5, ok6, ok7, ok8, ok9, ok10, ok11;

            ok1 = !string.IsNullOrEmpty(obj.token) && SessionSingleton.Current.Tokens.Contains(obj.token);

            ValidateHelper.Sanitize(obj.s_name, false, true, true, new List<string>() { "notag" }, CharacterHelper.Name(), out ok2);
            ValidateHelper.Sanitize(obj.p_name, false, true, true, new List<string>() { "notag" }, CharacterHelper.Product(), out ok3);

            CheckHelper.IsValidEmail(obj.s_email, out ok4);
            CheckHelper.IsValidPhonenumber("" + obj.s_phone, out ok5);
            CheckHelper.IsValidAddress(obj.s_address, out ok6);

            ok7 = obj.ship_fee > 0;
            ok8 = obj.p_id > 0;
            ok9 = obj.p_qty > 0;
            //ok7 = obj.p_weight > 0;
            //ok9 = obj.p_amt > 0;

            return ok1 && ok2 && ok3 && ok4 && ok5 && ok6 && ok7 && ok8 && ok9;
        }
        
        public static bool CheckOrderItemOBJ2(OrderItemOBJ obj, int state = 1, bool is_adv = false)
        {
            //if (this.s_id < 0) return false;
            //if (string.IsNullOrEmpty(this.type)) return false;
            //if (this.expected < 0) return false;
            //if (string.IsNullOrEmpty(this.s_owner)) return false;
            
            //ok8 = !string.IsNullOrEmpty(obj.p_name);
            //ok2 = !string.IsNullOrEmpty(obj.s_name);

            bool ok1, ok2, ok3, ok4, ok5, ok6, ok7, ok8, ok9 = true, ok10 = true, ok11 = true, ok12 = true;

            ok1 = !string.IsNullOrEmpty(obj.token) && SessionSingleton.Current.Tokens.Contains(obj.token);

            ValidateHelper.Sanitize(obj.s_name, false, true, true, new List<string>() { "notag" }, CharacterHelper.Name(), out ok2);
            ValidateHelper.Sanitize(obj.p_name, false, true, true, new List<string>() { "notag" }, CharacterHelper.Product(), out ok3);

            CheckHelper.IsValidEmail(obj.s_email, out ok4);
            CheckHelper.IsValidPhonenumber("" + obj.s_phone, out ok5);
            CheckHelper.IsValidAddress(obj.s_address, out ok6);

            ok7 = obj.p_id > 0;
            ok8 = obj.p_qty > 0;

            if (state > 1)
                ok9 = obj.ship_fee > 0;
            if (state > 2)
                ok10 = !obj.payment_id.IsNullOrEmpty();

            if (is_adv)
                ok11 = obj.p_weight > 0;
            if (is_adv)
                ok12 = obj.p_amt > 0;

            return ok1 && ok2 && ok3 && ok4 && ok5 && ok6 && ok7 && ok8 && ok9 && ok10 && ok11 && ok12;
        }

        public static bool CheckSessionOrder(SessionSingleton session)
        {
            string OrderPhone = session.OrderPhone;
            string OrderFullName = session.OrderFullName;
            string OrderStreet = session.OrderStreet;
            string OrderArea = session.OrderArea;
            string OrderTown = session.OrderTown;
            string OrderCountry = session.OrderCountry;
            string OrderEmail = session.OrderEmail;

            bool ok1, ok2, ok3, ok4, ok5, ok6, ok7;
            int _test;

            ok1 = !string.IsNullOrEmpty(OrderPhone) || int.TryParse(OrderPhone, out _test) || OrderPhone.Count() == 8;
            ok2 = !string.IsNullOrEmpty(OrderFullName);
            ok3 = !string.IsNullOrEmpty(OrderStreet);
            ok4 = !string.IsNullOrEmpty(OrderArea);
            ok5 = !string.IsNullOrEmpty(OrderTown);
            ok6 = !string.IsNullOrEmpty(OrderCountry);
            session.OrderEmail = string.IsNullOrEmpty(OrderEmail) ? "tracking ikke ønsket." : OrderEmail;
            ok7 = !string.IsNullOrEmpty(OrderEmail);

            return ok1 && ok2 && ok3 && ok3 && ok4 && ok5 && ok6 && ok7;
        }
    }
}