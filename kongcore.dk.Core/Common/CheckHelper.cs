using kongcore.dk.Core.Models.VM;
using System.Collections.Generic;
using System.Web;

namespace kongcore.dk.Core.Common
{
    class CheckHelper
    {
        public static string IsValidEmail(string email, out bool _ok)
        {
            _ok = false;
            if (string.IsNullOrEmpty(email))
                return "";

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                _ok = addr.Address == email;
                if (_ok)
                    return email;
                return "";
            }
            catch
            {
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
    }
}