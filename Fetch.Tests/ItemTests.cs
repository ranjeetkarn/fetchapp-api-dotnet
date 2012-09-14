using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using Fetch.Api;
using NUnit.Framework;

namespace Fetch.Tests
{
    [TestFixture]
    public class ProductTests
    {

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            Config.Key = "pixallent";
            Config.Token = "pixallent";
            Config.Domain = "pixallent.myhost.dev:3000";
        }

        [Test]
        public void CreateRetrieveUpdateDelete()
        {
            string sku = DateTime.Now.ToString( "MMddHHmm" );
            string name = string.Format( "test product - {0}", DateTime.Now.ToString( "MMdd HHmm" ) );

            // create test
            Product product = new Product();
            product.Sku = sku;
            product.Name = name;
            product.Save();

            // retrieve test
            product = Product.Retrieve( sku );
            Assert.AreEqual( name, product.Name );

            // update test
            product.Name += " update test";
            product.Save();

            // delete test
            product.Delete();

        }

        [Test]
        public void RetrieveAllProducts()
        {
            ProductCollection products = Product.RetrieveAll();
            Assert.IsNotNull( products );
        }

        [Test]
        [ExpectedException( typeof( FetchException ) )]
        public void RetrieveInvalidProduct()
        {
            Product product = Product.Retrieve( "SomeCrazySkuThatWillNeverExistHopefully" );
        }

        [Test]
        [ExpectedException( typeof( FetchException ) )]
        public void CreateInvalidProduct()
        {
            Product product = new Product();
            product.Name = "Bad Product";
            product.Save();
        }


    }
}
