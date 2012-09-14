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
    [XmlType( "product" )]
    public class Product
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Product()
        {
            this.isNew = true;
        }

        /// <summary>
        /// unique identifier for this product
        /// </summary>
        [XmlElement( "sku" )]
        public string Sku
        {
            get { return sku; }
            set { sku = value; }
        }
        private string sku;

        /// <summary>
        /// friendly name of the product
        /// </summary>
        [XmlElement( "name" )]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string name;

        /// <summary>
        /// number of times this Product has been downloaded from Fetch
        /// </summary>
        [XmlElement( "download-count" )]
        public int DownloadCount
        {
            get { return downloadCount; }
            set { downloadCount = value; }
        }
        private int downloadCount;

        /// <summary>
        /// date this product was created
        /// </summary>
        [XmlElement( "created-at" )]
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }
        private DateTime createdAt;

        /// <summary>
        /// The name of the file attached to this product
        /// </summary>
        [XmlElement( "filename" )]
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        private string filename;

        /// <summary>
        /// size in bytes of the file attached to this product
        /// </summary>
        [XmlElement( "size-bytes" )]
        public long SizeBytes
        {
            get { return size; }
            set { size = value; }
        }
        private long size;

        /// <summary>
        /// type of file attached to this product
        /// </summary>
        [XmlElement( "content-type" )]
        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }
        private string contentType;

        /// <summary>
        /// Gets true if this product has not been committed to Fetch.  Gets false if this
        /// product already exists in Fetch.
        /// </summary>
        [XmlIgnore]
        public bool IsNew
        {
            get { return isNew; }
            private set { isNew = value; }
        }
        private bool isNew;

        #region Instance Methods

        /// <summary>
        /// Commits the current product to Fetch.  Product is created if new, 
        /// or updated if existing.
        /// </summary>
        public void Save()
        {
            if ( IsNew )
            {
                Product response = Create( this );
                this.IsNew = false;
            }
            else
            {
                Update( this );
            }
        }

        /// <summary>
        /// Deletes the current product from Fetch.
        /// </summary>
        public void Delete()
        {
            Delete( this.Sku );
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Retrieve all the existing products from Fetch
        /// </summary>
        public static ProductCollection RetrieveAll()
        {
            RestConnection<ProductCollection> conn = new RestConnection<ProductCollection>();
            ProductCollection products = conn.InvokeGet( "products" );
            products.ForEach( delegate( Product i ) { i.IsNew = false; } );
            return products;
        }

        /// <summary>
        /// Retrieve a specific product from Fetch
        /// </summary>
        /// <param name="sku">The SKU of the Product to retrieve</param>
        public static Product Retrieve( string sku )
        {
            RestConnection<Product> conn = new RestConnection<Product>();
            Product product = conn.InvokeGet( "products", sku );
            product.IsNew = false;
            return product;
        }

        /// <summary>
        /// Deletes an product from Fetch
        /// </summary>
        /// <param name="sku">the SKU of the Product to delete</param>
        public static void Delete( string sku )
        {
            RestConnection<Message> conn = new RestConnection<Message>();
            Message response = conn.InvokeGet( "products", sku, "delete" );
            if ( !response.Text.Equals( Message.OkResponse ) )
            {
                throw new FetchException( string.Format( "Delete operation not successful: {0}", response.Text ) );
            }
        }

        /// <summary>
        /// Creates a new product in Fetch
        /// </summary>
        /// <returns>the actual product object that was created by Fetch</returns>
        internal static Product Create( Product product )
        {
            RestConnection<Product> conn = new RestConnection<Product>();
            Product response = conn.InvokePut( "products", product, "create" );
            return response;
        }

        /// <summary>
        /// Updates the given product in Fetch
        /// </summary>
        /// <returns>the actual product object that was updated in Fetch</returns>
        internal static Product Update( Product product )
        {
            RestConnection<Product> conn = new RestConnection<Product>();
            Product response = conn.InvokePut( "products", product, product.Sku, "update" );
            return response;
        }

        #endregion

    }

    /// <summary>
    /// Represents a collection of products that serializes to match the XML 
    /// returned by Fetch
    /// </summary>
    [Serializable]
    [XmlRoot( "products" )]
    public class ProductCollection : List<Product>
    {
    }
}
