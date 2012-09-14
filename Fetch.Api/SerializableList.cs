using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Specialized;
using System.Xml.Schema;
using System.Xml;

namespace Fetch.Api
{
    /// <summary>
    /// SerializableList is a wrapper for the generic List&lt;&gt; that performs its own serialization,
    /// to allow the user to add custom attributes to the list.  The .NET Framework List overrides
    /// any serialization of properties of a custom list class, so they don't get serialized.
    /// </summary>
    /// <typeparam name="ProductType">type of the product in the list</typeparam>
    [Serializable]
    public class SerializableList<ProductType> : ICollection<ProductType>, IEnumerable<ProductType>, IXmlSerializable
    {
        private List<ProductType> list;

        /// <summary>
        /// default constructor
        /// </summary>
        public SerializableList()
        {
            list = new List<ProductType>();
            attributes = new NameValueCollection();
        }

        #region ICollection<ProductType> Members

        /// <summary>
        /// Add an product to the list
        /// </summary>
        public void Add(ProductType product)
        {
            list.Add(product);
        }

        /// <summary>
        /// Clear all products from the list
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }

        /// <summary>
        /// Returns true if the given product exists in the list
        /// </summary>
        public bool Contains(ProductType product)
        {
            return list.Contains(product);
        }

        /// <summary>
        /// Copies the contents of the list into the given array, 
        /// at the given index
        /// </summary>
        public void CopyTo(ProductType[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of products in the list
        /// </summary>
        public int Count
        {
            get { return list.Count; }
        }

        /// <summary>
        /// Returns false
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove the given product from the list
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool Remove(ProductType product)
        {
            return list.Remove(product);
        }

        /// <summary>
        /// Get the enumerator object from the list
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ProductType> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// Get the enumerator object from the list
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// A collection of name/value pairs to be written as attributes of the root
        /// element of this list
        /// </summary>
        public NameValueCollection Attributes
        {
            get { return attributes; }
        }
        private NameValueCollection attributes;

        #region IXmlSerializable Members

        /// <summary>
        /// Get the Schema for the serialization of this list.  Not used.
        /// </summary>
        public XmlSchema GetSchema()
        {
            // only used by the DataSet
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads data for this list in from the given XmlReader.
        /// May be overridden in a derived class to populate custom properties
        /// </summary>
        public virtual void ReadXml(XmlReader reader)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(reader);

            if (xDoc.HasChildNodes)
            {
                XmlNode listNode = xDoc.FirstChild;
                if (listNode.HasChildNodes)
                {
                    XmlNode productNode = listNode.FirstChild;
                    do
                    {
                        ProductType product = Utility.DeserializeFromXml<ProductType>(productNode.OuterXml);
                        list.Add(product);

                        productNode = productNode.NextSibling;
                    }
                    while (productNode != null);
                }
            }

        }

        /// <summary>
        /// Writes data from this list into the given XmlWriter.
        /// May be overridden in a derived class to write custom properties
        /// </summary>
        public virtual void WriteXml(XmlWriter writer)
        {

            foreach (string key in Attributes.Keys)
            {
                writer.WriteAttributeString(key, Attributes[key]);
            }

            string docDec = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            foreach (ProductType product in list)
            {
                string xml = Utility.SerializeToXml(product);
                xml = xml.Replace(docDec, string.Empty);
                writer.WriteRaw(xml);
            }

        }

        #endregion
    }
}
