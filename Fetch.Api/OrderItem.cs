using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Fetch.Api
{
    /// <summary>
    /// object that is used to describe products included in an order
    /// </summary>
    [Serializable]
    [XmlType("order_product")]
    public class OrderItem
    {
        /// <summary>
        /// unique ID of an product in an order
        /// </summary>
        [XmlElement("sku")]
        public string Sku
        {
            get { return sku; }
            set { sku = value; }
        }
        private string sku;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("id")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int id;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("order_id")]
        public string Order_id
        {
            get { return order_id; }
            set { order_id = value; }
        }
        private string order_id;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("product_name")]
        public string Product_name
        {
            get { return product_name; }
            set { product_name = value; }
        }
        private string product_name;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("price")]
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        private float price;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("download_count")]
        public int Download_count
        {
            get { return download_count; }
            set { download_count = value; }
        }
        private int download_count;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("custom_1")]
        public string Custom_1
        {
            get { return custom_1; }
            set { custom_1 = value; }
        }
        private string custom_1;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("custom_2")]
        public string Custom_2
        {
            get { return custom_2; }
            set { custom_2 = value; }
        }
        private string custom_2;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("custom_3")]
        public string Custom_3
        {
            get { return custom_3; }
            set { custom_3 = value; }
        }
        private string custom_3;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("created_at")]
        public DateTime Created_at
        {
            get { return created_at; }
            set { created_at = value; }
        }
        private DateTime created_at;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("files_uri")]
        public string Files_uri
        {
            get { return files_uri; }
            set { files_uri = value; }
        }
        private string files_uri;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("downloads_uri")]
        public string Downloads_uri
        {
            get { return downloads_uri; }
            set { downloads_uri = value; }
        }
        private string downloads_uri;
    }
}
