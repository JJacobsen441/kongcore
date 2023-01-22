using kongcore.dk.Core.Models.DB;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Web;

namespace kongcore.dk.Core._Statics
{
    [Serializable]
    public sealed class SessionSingleton
    {
        #region Singleton

        private const string SESSION_SINGLETON_NAME = "Singleton_502E69E5-668B-E011-951F-00155DF26207";

        private SessionSingleton()
        {

        }

        public static SessionSingleton Current
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_SINGLETON_NAME] == null)
                {
                    SessionSingleton _s = new SessionSingleton();
                    _s.Setup();

                    HttpContext.Current.Session[SESSION_SINGLETON_NAME] = _s;
                }


                return HttpContext.Current.Session[SESSION_SINGLETON_NAME] as SessionSingleton;
            }
        }

        private void Setup()
        {
            if (ShoppingCart.IsNull())
                ShoppingCart = new List<OrderItemOBJ>();

            if (Tokens.IsNull())
                Tokens = new List<string>();

            //if (MyOrder.IsNull())
            //    MyOrder = new MyOrder();

            if (OrderPhone.IsNull())
                OrderPhone = "";
            if (OrderFullName.IsNull())
                OrderFullName = "";
            if (OrderStreet.IsNull())
                OrderStreet = "";
            if (OrderArea.IsNull())
                OrderArea = "";
            if (OrderTown.IsNull())
                OrderTown = "";
            if (OrderCountry.IsNull())
                OrderCountry = "";
            if (OrderEmail.IsNull())
                OrderEmail = "";
        }

        #endregion

        public List<OrderItemOBJ> ShoppingCart { get; set; }
        public List<string> Tokens { get; set; }
        public string OrderPhone { get; set; }
        public string OrderFullName { get; set; }
        public string OrderStreet { get; set; }
        public string OrderArea { get; set; }
        public string OrderTown { get; set; }
        public string OrderCountry { get; set; }
        public string OrderEmail { get; set; }
        public MyOrder MyOrder { get; set; }

    }
}