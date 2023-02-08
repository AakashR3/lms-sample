using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class Invoices
    {
        public string type { get; set; }
        public string description { get; set; }
        public bool partial_payment { get; set; }
        public Customer customer { get; set; }
        public List<LineItem> line_items { get; set; }
        public int sms_notify { get; set; }
        public int email_notify { get; set; }
        public string draft { get; set; }
        public int date { get; set; }
        public int expire_by { get; set; }
        public string receipt { get; set; }
        public string comment { get; set; }
        public string terms { get; set; }
        public Notes notes { get; set; }
    }

    public class BillingAddress
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }
    public class ShippingAddress
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }
    public class Customer
    {
        public string name { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public BillingAddress billing_address { get; set; }
        public ShippingAddress shipping_address { get; set; }
    }
    public class LineItem
    {
        public string name { get; set; }
        public string description { get; set; }
        public float amount { get; set; }
        public string currency { get; set; }
        public int quantity { get; set; }
        public string item_id { get; set; }
    }
    public class Notes
    {
        public string notes_key_1 { get; set; }
        public string notes_key_2 { get; set; }
    }


    public class InvoiceDetails
    {
        public string billing_address_line1 { get; set; }
        public string billing_address_line2 { get; set; }
        public string billing_address_zipcode { get; set; }
        public string billing_address_city { get; set; }
        public string billing_address_state { get; set; }
        public string billing_address_country { get; set; }
        public string shipping_address_line1 { get; set; }
        public string shipping_address_line2 { get; set; }
        public string shipping_address_city { get; set; }
        public string shipping_address_state { get; set; }
        public string shipping_address_zipcode { get; set; }
        public string shipping_address_country { get; set; }
        public string customer_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string receipt { get; set; }
        public string subscription_name { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public int Quantity { get; set; }
        public string comment { get; set; }
        public string terms { get; set; }
        public int expire_by { get; set; }
        public int date { get; set; }
    }

    public class InvoiceResponse
    {
        public string InvoiceType { get; set; }
        public string ShortURL { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileContent { get; set; }

    }


}
