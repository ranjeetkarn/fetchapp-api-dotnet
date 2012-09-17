using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Fetch.Api
{
    /// <summary>
    /// Product is a model object that mirrors the type used by the Fetch REST API, and contains
    /// all the properties and methods offered by the API.
    /// </summary>
    [Serializable]
    [XmlType("upload")]
    public class Upload
    {

        /// <summary>
        /// unique identifier for this product
        /// </summary>
        [XmlElement("id")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int id;

        /// <summary>
        /// Associated order (if present)
        /// </summary>
        [XmlElement("order_id")]
        public string OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        private string orderId;

        /// <summary>
        /// Product Sku
        /// </summary>
        [XmlElement("product_sku")]
        public string ProductSku
        {
            get { return productSku; }
            set { productSku = value; }
        }
        private string productSku;

        /// <summary>
        /// IP Address of client
        /// </summary>
        [XmlElement("ip_address")]
        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }
        private string ipAddress;

        /// <summary>
        /// date this product was uploaded
        /// </summary>
        [XmlElement("uploaded-at")]
        public DateTime UploadedAt
        {
            get { return uploadedAt; }
            set { uploadedAt = value; }
        }
        private DateTime uploadedAt;

        /// <summary>
        /// size in bytes of the file attached to this upload
        /// </summary>
        [XmlElement("size-bytes")]
        public long SizeBytes
        {
            get { return size; }
            set { size = value; }
        }
        private long size;

        #region Static Methods

        /// <summary>
        /// Retrieve all the existing products from Fetch
        /// </summary>
        public static UploadCollection RetrieveAll()
        {
            RestConnection<UploadCollection> conn = new RestConnection<UploadCollection>();
            UploadCollection uploads = conn.InvokeGet("uploads");
            return uploads;
        }

        /// <summary>
        /// Retrieve a specific product from Fetch
        /// </summary>
        /// <param name="sku">The SKU of the Product to retrieve</param>
        public static Upload Retrieve(int id)
        {
            RestConnection<Upload> conn = new RestConnection<Upload>();
            Upload upload = conn.InvokeGet("uploads", id.ToString());
            return upload;
        }

        #endregion

    }

    /// <summary>
    /// Represents a collection of products that serializes to match the XML 
    /// returned by Fetch
    /// </summary>
    [Serializable]
    [XmlRoot("uploads")]
    public class UploadCollection : List<Upload>
    {
    }
}
