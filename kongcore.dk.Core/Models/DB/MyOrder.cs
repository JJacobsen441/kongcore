using Lucene.Net.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Umbraco.Web.PublishedModels;

namespace kongcore.dk.Core.Models.DB
{
    [Table("MyOrder")]
    public class MyOrder
    {
        [Key]
        public int Id { get; set; }
        //public string Name { get; set; }
        //public string Email { get; set; }


        public string Reference { get; set; }
        public string Chargeid { get; set; }
        public string Paymentid { get; set; }
        public string Mask { get; set; }

        public DateTime Date { get; set; }

        public string ShopName { get; set; }
        public string ShopEmail { get; set; }
        public int ShopPhone { get; set; }
        public string ShopAddress { get; set; }

        public string ProductName { get; set; }
        public int ProductAmount { get; set; }
        public int ProductQty { get; set; }

        public string CustId { get; set; }
        public string CustEmail { get; set; }
        public string CustName { get; set; }

        public bool Sent { get; set; }
        public bool Done { get; set; }
        public bool Receipt { get; set; }
        public bool Refund { get; set; }
        public bool Paid { get; set; }
        
        public float ShippingPrice { get; set; }

        //public string ShippingAddress { get; set; }
        //public string ShippingFullname { get; set; }
        //public int ShippingPhone { get; set; }
        //public string Trackingnumber { get; set; }
        //public string Trackingservice { get; set; }
        //public int ExpectedDelivery { get; set; }
        //public string ProductDesc { get; set; }
        
    }
}