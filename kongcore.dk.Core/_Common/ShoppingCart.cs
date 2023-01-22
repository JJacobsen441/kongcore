using kongcore.dk.Core._Statics;
using kongcore.dk.Core.Models.BIZ;
using kongcore.dk.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core._Common
{
    public class ShoppingCart
    {
        ContentHelper helper;
        SessionSingleton session;
        bool is_test = false;

        public ShoppingCart()
        {
            session = SessionSingleton.Current;
            //is_test = true;
        }

        public bool AddItemByID(ContentHelper helper, long pro_id)
        {
            if (helper.IsNull())
                return false;

            IPublishedContent root = helper.Root();
            //IPublishedContent current = helper.RootCurrent();

            List<OrderItemOBJ> items = session.ShoppingCart;
            List<string> tokens = session.Tokens;

            BIZ_ShopItem biz_shop = new BIZ_ShopItem();
            DTO_ShopItem dto = biz_shop.ToDTO(helper, (int)pro_id);
            if (dto.IsNull())
                return false;

            foreach (OrderItemOBJ i in items)
            {
                if (!CheckHelper.CheckOrderItemOBJ2(i))
                    return false;
                if (i.p_id == pro_id)
                    return false;
            }

            string _token = Guid.NewGuid().ToString();
            OrderItemOBJ _item = new OrderItemOBJ()
            {
                //expected = pro.ExpectedDelivery, 
                //p_value = pro.ValueDelivery, 
                //p_free = pro.FreeDelivery, 
                //s_id = s.Id, 
                //type = "PRODUCT",
                token = _token,
                s_address = "Søborg Hovedgade 221, 2860 Søborg, Danmark",
                s_email = "info@kongcore.dk",
                s_phone = 29358577,
                p_weight = 0.0d,
                //s_owner = "info@kongcore.dk",
                s_name = "KongCore",
                p_name = dto.product_name,
                p_id = long.Parse(dto.id),
                p_amt = int.Parse(dto.product_price),
                p_qty = 1
            };


            tokens.Add(_token);
            items.Add(_item);

            session.ShoppingCart = null;
            session.Tokens = null;
            session.ShoppingCart = items;
            session.Tokens = tokens;

            return true;
        }

        public void AddItemAndToken(OrderItemOBJ obj)
        {
            if (obj.IsNull())
                return;

            if (!CheckHelper.CheckOrderItemOBJ2(obj))
                return;

            List<OrderItemOBJ> items = session.ShoppingCart;
            List<string> tokens = session.Tokens;

            items.Add(obj);
            tokens.Add(obj.token);

            session.ShoppingCart = null;
            session.ShoppingCart = items;
            session.Tokens = null;
            session.Tokens = tokens;
        }


        public void RemoveItemByToken(string guid)
        {
            if (guid.IsNullOrEmpty())
                return;

            List<OrderItemOBJ> items = session.ShoppingCart;
            List<OrderItemOBJ> res = new List<OrderItemOBJ>();
            List<string> tokens = new List<string>();


            foreach (OrderItemOBJ i in items)
            {
                if (!CheckHelper.CheckOrderItemOBJ2(i))
                    return;
                if (!string.IsNullOrEmpty(i.token) && i.token != guid)
                {
                    res.Add(new OrderItemOBJ() { token = i.token, s_address = i.s_address, s_email = i.s_email, s_phone = i.s_phone, p_weight = i.p_weight, s_name = i.s_name, p_id = i.p_id, p_name = i.p_name, p_amt = i.p_amt, p_qty = i.p_qty });
                    tokens.Add(i.token);
                }
            }

            session.ShoppingCart = null;
            session.Tokens = null;
            session.ShoppingCart = res;
            session.Tokens = tokens;
        }

        public void RemoveItem(OrderItemOBJ obj)
        {
            if (obj.IsNull())
                return;

            List<OrderItemOBJ> items = session.ShoppingCart;
            List<OrderItemOBJ> res = new List<OrderItemOBJ>();

            foreach (OrderItemOBJ i in items)
            {
                if (!CheckHelper.CheckOrderItemOBJ2(i))
                    return;
                if (!string.IsNullOrEmpty(i.token) && i.token != obj.token)
                    res.Add(new OrderItemOBJ() { token = i.token, s_address = i.s_address, s_email = i.s_email, s_phone = i.s_phone, p_weight = i.p_weight, s_name = i.s_name, p_id = i.p_id, p_name = i.p_name, p_amt = i.p_amt, p_qty = i.p_qty });
            }

            session.ShoppingCart = null;
            session.ShoppingCart = res;
        }

        public OrderItemOBJ GetOrderByToken(string _t)
        {
            if (_t.IsNullOrEmpty())
                return null;

            List<OrderItemOBJ> items = session.ShoppingCart;

            foreach (OrderItemOBJ i in items)
            {
                if (i.token == _t)
                    return i;
            }

            return null;
        }

        public OrderItemOBJ GetOrderByPaymentID(string pay_id)
        {
            if (is_test)
                pay_id = "-1";

            if (pay_id.IsNullOrEmpty())
                return null;

            List<OrderItemOBJ> items = session.ShoppingCart;

            foreach (OrderItemOBJ i in items)
            {
                if (pay_id == "-1")
                    return i;

                if (i.payment_id == pay_id)
                    return i;
            }

            return null;
        }

        public void ChangeNumbers(string token, string dir)
        {
            if (token.IsNullOrEmpty())
                return;
            if (dir.IsNullOrEmpty())
                return;

            List<OrderItemOBJ> items = session.ShoppingCart;
            List<OrderItemOBJ> res = new List<OrderItemOBJ>();

            foreach (OrderItemOBJ i in items)
            {
                if (!CheckHelper.CheckOrderItemOBJ2(i))
                    return;

                if (i.token == token)
                {
                    int n = i.p_qty;

                    if (dir == "up")
                        n++;
                    else
                        n--;

                    n = n < 1 ? 1 : n;

                    res.Add(new OrderItemOBJ() { token = i.token, s_address = i.s_address, s_email = i.s_email, s_phone = i.s_phone, p_weight = i.p_weight, s_name = i.s_name, p_id = i.p_id, p_name = i.p_name, p_amt = i.p_amt, p_qty = n });
                }
                else
                    res.Add(new OrderItemOBJ() { token = i.token, s_address = i.s_address, s_email = i.s_email, s_phone = i.s_phone, p_weight = i.p_weight, s_name = i.s_name, p_id = i.p_id, p_name = i.p_name, p_amt = i.p_amt, p_qty = i.p_qty });
            }

            session.ShoppingCart = null;
            session.ShoppingCart = res;
        }
    }
}
