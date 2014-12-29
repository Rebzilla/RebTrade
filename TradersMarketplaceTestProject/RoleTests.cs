using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;

namespace TradersMarketplaceTestProject
{
    [TestClass]
    public class RoleTests
    {
        private RolesBL rolesBL;

        [TestInitialize]
        public void Initialize()
        {
            rolesBL = new RolesBL();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
