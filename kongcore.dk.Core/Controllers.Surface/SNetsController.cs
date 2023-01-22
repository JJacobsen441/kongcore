using kongcore.dk.Core._Common;
using kongcore.dk.Core._Statics;
using kongcore.dk.Core.Models.DB;
using kongcore.dk.Core.Models.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace kongcore.dk.Core.Controllers.Surface
{
    public class SNetsController : SurfaceController
    {
        SessionSingleton session;
        ContentHelper helper;

        bool is_test = false;

        //string _netsbase = "https://api.dibspayment.eu";
        string _netsbase = "https://test.api.dibspayment.eu";
        string _netsapi = "";

        public SNetsController()
        {
            session = SessionSingleton.Current;

            helper = new ContentHelper(Umbraco);
            IPublishedContent root = helper.Root();
            //IPublishedContent current = helper.RootCurrent();

            //is_test = true;
        }

        [AllowAnonymous]
        [HttpPost]
        //[Route("nets/netspaymentid", Name = "pay")]
        public JsonResult NetsPaymentID(string guid, string method)
        {
            try
            {
                StaticsHelper.Log("Nets, PaymentID START");

                ShoppingCart sc = new ShoppingCart();
                OrderItemOBJ model = sc.GetOrderByToken(guid);
                if (model.IsNull())
                    return Json(new { success = false });

                double ship_fee = 0.0d;
                if (method == "Home")
                    ship_fee = double.Parse(DeliveryHelper.PostNord.GetPriceNormal(model.p_qty * model.p_weight, "home"));
                else if (method == "Collect")
                    ship_fee = double.Parse(DeliveryHelper.PostNord.GetPriceNormal(model.p_qty * model.p_weight, "collect"));
                else if (method == "Value")
                    ship_fee = double.Parse(DeliveryHelper.PostNord.GetPriceValue(model.p_qty * model.p_weight));
                else if (method == "Free")
                    ship_fee = 0.0;
                else
                    return Json(new { success = false });

                //byte[] _crypt = StaticsHelper.AppSettings("crypt").Split('-').Select(x => Convert.ToByte(x, 16)).ToArray();
                //byte[] _auth = StaticsHelper.AppSettings("auth").Split('-').Select(x => Convert.ToByte(x, 16)).ToArray();
                //string _secret = AESThenHMAC.SimpleDecrypt(StaticsHelper.AppSettings("secret"), _crypt, _auth);

                GetData gd = new GetData();
                BaseSum bs = new BaseSum();
                bs = bs.BaseCalc("" + model.p_amt, "" + model.p_qty, "" + ship_fee);

                
                //string return_url = "http://localhost:9140/umbraco/surface/snets/netssuccess";
                string return_url = "https://www.kongcore.dk/umbraco/surface/snets/netssuccess";
                //string terms_url = "http://localhost:9140/contact";
                string terms_url = "https://www.kongcore.dk/contact";
                string _path = "v1/payments";
                string _params = "";
                string _data = gd.PayID(session, guid, model, bs, method, return_url, terms_url);
                string _secret = StaticsHelper.AppSettings("secret").Replace("test-secret-key-", "");
                string _token = "";
                string _apikey = "";

                StaticsHelper.Log("Nets, PaymentID body:");
                StaticsHelper.Log("" + _data);

                string res = RestHelper.Send(
                    HttpMethod.Post,
                    _data,
                    _netsbase,
                    _path,
                    _params,
                    "application/json",
                    "application/json",
                    _apikey,
                    _token,
                    _secret
                    );                

                StaticsHelper.Log("Nets, PaymentID res: " + (res == null ? "NULL" : "'" + res + "'"));
                if (res.IsNull())
                    return Json(new { success = false });

                Dictionary<string, string> _r = JsonConvert.DeserializeObject<Dictionary<string, string>>(res);
                string payment_id = _r["paymentId"];
                string hosted = _r["hostedPaymentPageUrl"];
                //session.OrderShopID = "" + model.s_id;
                //session.OrderReferense = guid;
                //session.OrderPaymentID = payment_id;
                model.ship_fee = ship_fee;
                model.payment_id = payment_id;

                sc.RemoveItem(model);
                sc.AddItemAndToken(model);

                StaticsHelper.Log("Nets, PaymentID OK");

                return Json(new { success = true, msg = payment_id, host = hosted });
            }
            catch (Exception e)
            {
                return Json(new { success = false, res = "Beklager, der skete en fejl!" });
            }
        }

        public string NetsInfo(OrderItemOBJ o)
        {
            try
            {
                if (is_test)
                    return "masked_pan";

                StaticsHelper.Log("Nets, Nets_Info START, paymentid: " + o.payment_id);

                string _path = "v1/payments/" + o.payment_id;
                string _params = "";
                string _data = GetData.Info();

                string _secret = StaticsHelper.AppSettings("secret").Replace("test-secret-key-", "");
                string _token = "";
                string _apikey = "";

                string res = RestHelper.Send(
                    HttpMethod.Get,
                    _data,
                    _netsbase,
                    _path,
                    _params,
                    "application/json",
                    "",
                    _apikey,
                    _token,
                    _secret
                    );

                
                dynamic d = JObject.Parse(res);
                string masked_pan = d.payment.paymentDetails.cardDetails.maskedPan;
                StaticsHelper.Log("Nets, Nets_Info res: " + (res == null ? "NULL" : "'" + res + "'"));
                if (masked_pan == null)
                    return null;

                return masked_pan;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string NetsCharge(OrderItemOBJ o)
        {
            try
            {
                //return "" + -1;

                StaticsHelper.Log("Nets, Nets_Charges START, paymentid: " + o.payment_id);

                //double vat;
                //double t_vat;
                //int qty = o.p_qty;
                //int b_price = o.p_amt;
                //double n_price = Statics.Vat("" + b_price, out vat);
                //double b_total = qty * b_price;
                //double n_total = qty * n_price;
                //Statics.Vat("" + b_total, out t_vat);

                BaseSum bs = new BaseSum();
                bs = bs.BaseCalc("" + o.p_amt, "" + o.p_qty, "" + o.ship_fee);

                string _path = "v1/payments/" + o.payment_id + "/charges";
                string _params = "";
                string _data = GetData.Charge(bs, o);

                string _secret = StaticsHelper.AppSettings("secret").Replace("test-secret-key-", "");
                string _token = "";
                string _apikey = "";

                string res = RestHelper.Send(
                    HttpMethod.Post,
                    _data,
                    _netsbase,
                    _path,
                    _params,
                    "application/json",
                    "application/json",
                    _apikey,
                    _token,
                    _secret
                    );

                StaticsHelper.Log("Nets, Nets_Charges res: " + (res == null ? "NULL" : "'" + res + "'"));

                if (res == null)
                    return null;
                                
                Dictionary<string, string> _d = JsonConvert.DeserializeObject<Dictionary<string, string>>(res);
                string c_id = _d["chargeId"];

                StaticsHelper.Log("Nets, Nets_Charges OK");
                return c_id;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public JsonResult NetsRefund(string paymentid)
        {
            try
            {
                using (DBContext db = new DBContext())
                {

                    StaticsHelper.Log("Nets, Nets_Refund START, paymentid: " + paymentid);

                    MyOrder o = db.myorder.Where(x => x.Paymentid == paymentid).FirstOrDefault();
                    if (o == null)
                        return Json(new { success = false });
                    if (string.IsNullOrEmpty(o.Chargeid))
                        return Json(new { success = false });

                    BaseSum bs = new BaseSum();
                    bs.BaseCalc("" + o.ProductAmount, "" + o.ProductQty, "" + o.ShippingPrice);

                    string _path = "v1/charges/" + o.Chargeid + "/refunds";
                    string _params = "";
                    string _data = GetData.Refund(bs, o);

                    string _secret = StaticsHelper.AppSettings("secret").Replace("test-secret-key-", "");
                    string _token = "";
                    string _apikey = "";

                    string res = RestHelper.Send(
                        HttpMethod.Post,
                        _data,
                        _netsbase,
                        _path,
                        _params,
                        "application/json",
                        "application/json",
                        _apikey,
                        _token,
                        _secret
                        );

                    StaticsHelper.Log("Nets, Nets_Refund res: " + (res == null ? "NULL" : "'" + res + "'"));
                    if (res == null)
                        return Json(new { success = false });

                    o.Refund = true;
                    db.SaveChanges();
                    StaticsHelper.Log("Nets, Nets_Refund OK");
                    return Json(new { success = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, res = "Beklager, der skete en fejl!" });
            }
        }

        //[AllowAnonymous]
        public ActionResult NetsSuccess(string paymentid)
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    //BadMail2(paymentid);
                    //throw new Exception("Køb afbrudt0!");

                    //throw new Exception("Køb afbrudt!");
                    //TempData["MSG"] = "tag for besøget :)";
                    //Response.Redirect("/success");
                    //return;

                    StaticsHelper.Log("Nets, Nets_Success START, paymentid: " + paymentid);

                    if(!is_test && paymentid.IsNullOrEmpty())
                    {
                        BadMail2("-1");
                        throw new Exception("Køb afbrudt1!");
                    }

                    ShoppingCart sc = new ShoppingCart();
                    OrderItemOBJ o = sc.GetOrderByPaymentID(paymentid);
                    if (o.IsNull())
                    {
                        BadMail2(paymentid);
                        throw new Exception("Køb afbrudt2!");
                    }

                    string masked_pan = NetsInfo(o);
                    if (masked_pan.IsNull())
                    {
                        BadMail2(paymentid);
                        throw new Exception("Køb afbrudt3!");
                    }

                    MyOrder or = PlaceOrder(db, o, masked_pan);
                    if (or.IsNull())
                    {
                        BadMail2(paymentid);
                        throw new Exception("Køb afbrudt4!");
                    }

                    //dynamic stuff = JObject.Parse(res);
                    //string num1 = stuff.payment.summary.reservedAmount;
                    //string num2 = stuff.payment.summary.chargedAmount;
                    //bool ok = !string.IsNullOrEmpty(num1) && !string.IsNullOrEmpty(num2);
                    //ok &= num1 == num2;
                    //if(!ok)
                    //    return View("Fail", "Fejl 2");

                    //StaticsHelper.Log("Nets, Nets_Success masked_pan: " + (masked_pan == null ? "NULL" : "'" + masked_pan + "'"));

                    //if (string.IsNullOrEmpty(session.OrderEmail))
                    //    throw new Exception("Køb afbrudt!");

                    string c_id = NetsCharge(o);
                    if (c_id.IsNull())
                    {
                        BadMail1(or);
                        throw new Exception("Køb afbrudt5!");
                    }
                    or.Chargeid = c_id;
                    or.Paid = true;
                    db.SaveChanges();

                    

                    StaticsHelper.Log("Nets, Nets_Success OK");

                    TempData["MSG"] = "tak for besøget :)";
                    return Redirect("/success");
                    //return;
                }
            }
            catch (Exception _e)
            {
                //Response.Redirect("/fail");

                if (helper.IsNull())
                    helper = new ContentHelper(Umbraco, CurrentPage);

                TempData["MSG"] = _e.Message;
                TempData["IS_MSG"] = "true";

                var fail = helper.NodeName(helper.Root(), "Fail");
                int failPageId = fail.Id;

                var redirectPage = Umbraco.Content(failPageId); //page id here

                return Redirect(redirectPage.Url());
            }
        }

        private void OkMail(MyOrder or, string masked_pan)
        {
            BaseSum bs = new BaseSum();
            bs.BaseCalc("" + or.ProductAmount, "" + or.ProductQty, "" + or.ShippingPrice);

            string subject = "Ordre Bekræftelse, " + SettingsHelper.Basic.SITENAME_SHORT();
            string body = "Hermed følger en ordrebekræftelse.<br /><br />" +
                "PaymentID: " + or.Paymentid + "<br />" +
                "OrderID: " + or.Id + "<br />" +
                "Dags dato: " + or.Date.ToString("dd/MM/yyyy hh:mm") + "<br />" +
                //"Forventet levering:" + or.Date.AddDays(or.ExpectedDelivery).ToString("dd/MM/yyyy") + "<br /><br />" +
                "Forventet levering:" + System.DateTime.Now.ToString("dd/MM/yyyy") + "<br /><br />" +
                or.ProductName + ", af " + bs.str_b_price + " kr antal: " + bs.str_qty + "<br />" +
                "Beskrivelse:<br />en kort tekst<br /><br />" +
                "KortNo: " + masked_pan + "<br />" +
                "Forsendelse: " + (bs.str_ship == "0.0" ? "GRATIS" : bs.str_ship + " kr.") + "<br />" +
                "Total ex. moms: " + bs.str_n_total + " kr." + "<br />" +
                "Moms: " + bs.str_t_vat + " kr." + "<br />" +
                "Total inkl. moms: " + bs.str_b_total + " kr." + "<br />" +
                "<br /><br />" +
                "<br /><br />" +
                "Kontakt oplysninger:" +
                or.ShopName + "<br />" +
                or.ShopAddress + "<br />" +
                //or.Region.Zip + " " + s.Region.Town + "<br />" +
                //"Danmark" + "<br />" +
                "Email: " + or.ShopEmail + "<br />" +
                "Tel: " + or.ShopPhone + "<br /><br />" +
                "Med venlig hilsen<br />" +
                SettingsHelper.Basic.SITENAME_SHORT_CAP();

            NotificationHelper.Run(SettingsHelper.Basic.EMAIL_NO_REPLY(), session.OrderEmail, SettingsHelper.Basic.EMAIL_NO_REPLY(), subject, body);
        }

        private void BadMail1(MyOrder or)
        {
            string subject = "Besked fra " + SettingsHelper.Basic.SITENAME_SHORT_CAP();
            string body = "Der skete en fejl.<br /><br />" +
                "PaymentID: " + or.Paymentid + "<br />" +
                "OrderID: " + or.Id + "<br />" +
                "Dags dato: " + or.Date.ToString("dd/MM/yyyy hh:mm") + "<br />" +
                "Kontakt venligst:" +
                SettingsHelper.Basic.SITENAME_SHORT_CAP() + "<br />" +
                SettingsHelper.Basic.STREET() + "<br />" +
                SettingsHelper.Basic.AREA() + " " + SettingsHelper.Basic.TOWN() + "<br /><br />" +
                //or.Region.Zip + " " + s.Region.Town + "<br />" +
                //"Danmark" + "<br />" +
                "Email: " + SettingsHelper.Basic.EMAIL_MAIL() + "<br />" +
                "Tel: " + SettingsHelper.Basic.PHONE() + "<br /><br />" +
                "Med venlig hilsen<br />" +
                SettingsHelper.Basic.SITENAME_SHORT_CAP();

            NotificationHelper.Run(SettingsHelper.Basic.EMAIL_ADMIN(), session.OrderEmail, SettingsHelper.Basic.EMAIL_ADMIN(), subject, body);
        }

        private void BadMail2(string payment_id)
        {
            string subject = "Besked fra " + SettingsHelper.Basic.SITENAME_SHORT_CAP();
            string body = "Der skete en fejl.<br /><br />" +
                (payment_id != "-1" ? "PaymentID: " + payment_id + "<br />" : "") +
                "Dags dato: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm") + "<br />" +
                "Kontakt venligst:" +
                SettingsHelper.Basic.SITENAME_SHORT_CAP() + "<br />" +
                SettingsHelper.Basic.STREET() + "<br />" +
                SettingsHelper.Basic.AREA() + " " + SettingsHelper.Basic.TOWN() + "<br /><br />" +
                //or.Region.Zip + " " + s.Region.Town + "<br />" +
                //"Danmark" + "<br />" +
                "Email: " + SettingsHelper.Basic.EMAIL_MAIL() + "<br />" +
                "Tel: " + SettingsHelper.Basic.PHONE() + "<br /><br />" +
                "Med venlig hilsen<br />" +
                SettingsHelper.Basic.SITENAME_SHORT_CAP();

            NotificationHelper.Run(SettingsHelper.Basic.EMAIL_ADMIN(), session.OrderEmail, SettingsHelper.Basic.EMAIL_ADMIN(), subject, body);
        }

        private MyOrder PlaceOrder(DBContext db, OrderItemOBJ o, string masked_pan)
        {
            try
            {
                if (db.IsNull())
                    throw new Exception();

                if (o.IsNull())
                    throw new Exception();

                if (masked_pan.IsNullOrEmpty())
                    throw new Exception();

                if (!CheckHelper.CheckOrderItemOBJ2(o, 2))//state should be 2, and advanced false
                    return null;

                if (!CheckHelper.CheckSessionOrder(session))
                    return null;

                //List<string> tokens = session.Tokens;
                //tokens.Add(o.token);
                //session.Tokens = tokens;

                DateTime date = System.DateTime.Now;

                MyOrder or = new MyOrder()
                {
                    Reference = o.token,
                    Paymentid = o.payment_id,
                    Date = date,
                    ShopName = o.s_name,
                    ShopEmail = o.s_email,
                    ShopPhone = o.s_phone,
                    ShopAddress = o.s_address,
                    ProductName = o.p_name,
                    ProductAmount = o.p_amt,
                    ProductQty = o.p_qty,

                    CustId = null,//model.c_ref,
                    CustEmail = session.OrderEmail,
                    CustName = session.OrderFullName,

                    Mask = masked_pan,

                    ShippingPrice = (float)o.ship_fee,

                    Sent = false,
                    Done = false,
                    Refund = false,
                    Paid = false

                    //ShippingAddress = OrderStreet + ", " + OrderArea + " " + OrderTown + ", " + OrderCountry,
                    //ShippingFullname = OrderFullName,
                    //ShippingPhone = int.Parse(OrderPhone),

                    //ShopId = model.s_id,
                    //ProductId = long.Parse(model.p_id),
                    //ProductType = model.type,
                    //ProductDesc = desc,
                    //ExpectedDelivery = model.expected,
                };

                db.myorder.Add(or);
                db.SaveChanges();

                return or;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private class GetData
        {
            public string PayID(SessionSingleton session, string guid, OrderItemOBJ model, BaseSum bs, string method, string return_url, string terms_url)
            {
                string name = session.OrderFullName;
                string street = session.OrderStreet;
                string area = session.OrderArea;
                string town = session.OrderTown;
                string country = session.OrderCountry;
                string email = session.OrderEmail;
                string phone = session.OrderPhone;

                string[] arr = name.Split(' ');
                string fname = arr[0];
                string lname = "";
                for (int i = 1; i < arr.Length; i++)
                    lname += arr[i] + " ";

                if (model.IsNull() || bs.IsNull() || guid.IsNullOrEmpty() || method.IsNullOrEmpty() ||
                    email.IsNullOrEmpty() || street.IsNullOrEmpty() || area.IsNullOrEmpty() || town.IsNullOrEmpty() || phone.IsNullOrEmpty() || country.IsNullOrEmpty() || fname.IsNullOrEmpty() || lname.IsNullOrEmpty() ||
                    return_url.IsNullOrEmpty() || terms_url.IsNullOrEmpty())
                    throw new Exception();


                string data = "{" +
                "\"order\": {" +
                        "\"items\": [{" +
                            "\"reference\": \"" + guid + "\"," +
                            "\"name\": \"" + model.p_name + "\"," +
                            "\"quantity\": \"" + bs.str_qty + "\"," +
                            "\"unit\": \"pcs\"," +
                            "\"unitPrice\":\"" + bs.str_n_price_100 + "\"," +
                            "\"taxRate\":\"2500\"," +
                            "\"taxAmount\":\"" + bs.str_t_vat_100 + "\"," +
                            "\"grossTotalAmount\":\"" + bs.str_b_total_noship_100 + "\"," +
                            "\"netTotalAmount\":\"" + bs.str_n_total_noship_100 + "\"" +
                "}" +
                (method == "Free" ? "" :
                        ",{" +
                            "\"reference\": \"" + guid + "\"," +
                            "\"name\": \"SHIPPING\"," +
                            "\"quantity\": \"1\"," +
                            "\"unit\": \"pcs\"," +
                            "\"unitPrice\":\"" + bs.str_ship_100 + "\"," +
                            "\"taxRate\":\"0\"," +
                            "\"taxAmount\":\"000\"," +
                            "\"grossTotalAmount\":\"" + bs.str_ship_100 + "\"," +
                            "\"netTotalAmount\":\"" + bs.str_ship_100 + "\"" +
                        "}"
                        ) +
                        "]," +
                        "\"amount\":\"" + bs.str_b_total_100 + "\"," +
                        "\"currency\": \"DKK\"," +
                        "\"reference\": \"" + model.p_name + "\"" +
                    "}," +
                    "\"checkout\": {" +
                        "\"charge\":\"false\"," +
                        //"\"url\": \"https://www.centr.dk/Order/Nets_Donation\"," +
                        "\"returnUrl\": \"" + return_url + "\"," +
                        "\"termsUrl\": \"" + terms_url + "\"," +
                        "\"integrationType\": \"HostedPaymentPage\"," +
                        "\"merchantHandlesConsumerData\":\"true\"," +
                        "\"consumer\":{" +
                            "\"email\":\"" + email + "\"," +
                            "\"shippingAddress\":{" +
                                "\"addressLine1\":\"" + street + "\"," +
                                "\"addressLine2\":\"\"," +
                                "\"postalCode\":\"" + area + "\"," +
                                "\"city\":\"" + town + "\"," +
                                //"\"country\":\"" + country + "\"" +
                                "\"country\":\"DNK\"" +
                            "}," +
                            "\"phoneNumber\":{" +
                                "\"prefix\":\"+45\"," +
                                "\"number\":\"" + phone + "\"" +
                "}," +
                            "\"privatePerson\":{" +
                "\"firstName\":\"" + fname + "\"," +
                                "\"lastName\":\"" + lname + "\"" +
                            "}" +
                        "}," +
                        "\"shipping\": {" +
                            "\"countries\": [" +
                            "{" +
                                "\"countryCode\": \"DNK\"" +
                            "}" +
                            "]," +
                            "\"merchantHandlesShippingCost\": " + (method == "Free" ? "\"true\"" : "\"false\"") +
                            (method == "Free" ? ",\"costSpecified\":\"true\"" : "") +
                        "}," +
                        "\"consumerType\": {" +
                        "\"supportedTypes\": [ \"B2C\", \"B2B\" ]," +
                        "\"default\": \"B2C\"" +
                    "}" +
                    "}" +

                    //You can extend the datastring with optional webhook-parameters for status such as "payment.created". Click for more info
                    //"\"notifications\": {" +
                    //    "\"webhooks\": [{" +
                    //        "\"eventName\": \"payment.created\"," +
                    //        "\"url\": \"https://www.centr.dk/administration/rediger-butik/" + s_id + "\"," +
                    //        "\"authorization\": \"authorizationKey\"" +
                    //    "}" +
                    //    "]" +
                    //"}," +//This enables the merchant to charge an invoice fee towards the customer when invoice is used as paymentmethod
                    //"\"paymentMethods\": [" +
                    //"{" +
                    //    "\"name\": \"easyinvoice\"," +
                    //    "\"fee\": {" +
                    //        "\"reference\": \"invFee\"," +
                    //        "\"name\": \"fee\"," +
                    //        "\"quantity\": 1," +
                    //        "\"unit\": \"ct\"," +
                    //        "\"unitPrice\": 1000," +
                    //        "\"taxRate\": 2500," +
                    //        "\"taxAmount\": 250," +
                    //        "\"grossTotalAmount\": 1250," +
                    //        "\"netTotalAmount\": 1000" +
                    //    "}" +
                    //"}" +
                    //"]" +
                    "}";

                return data;
            }

            public static string Info() { return ""; }

            public static string Charge(BaseSum bs, OrderItemOBJ o)
            {
                if (bs.IsNull())
                    throw new Exception();

                if (o.IsNull())
                    throw new Exception();

                string data = "{" +
                        "\"amount\":\"" + bs.str_b_total_100 + "\"," +
                        "\"orderItems\":" +
                        "[{" +
                            "\"reference\":\"" + o.token + "\"," +
                            "\"name\":\"" + o.p_name + "\"," +
                            "\"quantity\":\"" + o.p_qty + "\"," +
                            "\"unit\":\"pcs\"," +
                            "\"unitPrice\":\"" + bs.str_n_price_100 + "\"," +
                            "\"taxRate\":\"2500\"," +
                            "\"taxAmount\":\"" + bs.str_t_vat_100 + "\"," +
                            "\"grossTotalAmount\":\"" + bs.str_b_total_noship_100 + "\"," +
                            "\"netTotalAmount\":\"" + bs.str_n_total_noship_100 + "\"" +
                        "}" +
                        (o.ship_fee == 0.0 ? "" :
                        ",{" +
                            "\"reference\":\"" + o.token + "\"," +
                            "\"name\":\"SHIPPING\"," +
                            "\"quantity\":\"1\"," +
                            "\"unit\":\"pcs\"," +
                            "\"unitPrice\":\"" + bs.str_ship_100 + "\"," +
                            "\"taxRate\":\"0\"," +
                            "\"taxAmount\":\"0\"," +
                            "\"grossTotalAmount\":\"" + bs.str_ship_100 + "\"," +
                            "\"netTotalAmount\":\"" + bs.str_ship_100 + "\"" +
                        "}"
                        ) +
                        "]" +
                    "}";

                return data;
            }

            public static string Refund(BaseSum bs, MyOrder o)
            {
                if (bs.IsNull())
                    throw new Exception();

                if (o.IsNull())
                    throw new Exception();

                string data = "{" +
                            "\"amount\":" + bs.str_b_price_100 + "," +
                            "\"orderItems\":" +
                            "[{" +
                                "\"reference\":\"" + o.Reference + "\"," +
                                "\"name\":\"" + o.ProductName + "\"," +
                                "\"quantity\":" + bs.str_qty + "," +
                                "\"unit\":\"pcs\"," +
                                "\"unitPrice\":" + bs.str_b_price_100 + "," +
                                "\"taxRate\":2500," +
                                "\"taxAmount\":" + bs.str_t_vat_100 + "," +
                                "\"grossTotalAmount\":" + bs.str_b_total_100 + "," +
                                "\"netTotalAmount\":" + bs.str_n_price_100 + "" +
                            "}]" +
                        "}";

                return data;
            }
        }
    }
}
