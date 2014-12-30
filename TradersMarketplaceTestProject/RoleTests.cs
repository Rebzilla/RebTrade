using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using System.Transactions;

namespace TradersMarketplaceTestProject
{
    [TestClass]
    public class RoleTests
    {
        private TransactionScope tScope;
        private RolesBL rolesBL;

        [TestInitialize]
        public void Initialize()
        {
            tScope = new TransactionScope();
            rolesBL = new RolesBL();
        }

        [TestCleanup]
        public void Cleanup()
        {
            tScope.Dispose(); //This is to revert changes made in the db
        }

        [TestMethod]
        public void CreateRole_Success_Test()
        {
            //successfully add role
            string roleName = "TestRole";
            rolesBL.AddRole(roleName);
        }

        [TestMethod]
        public void CreateRole_ExistingRole_Test()
        {
            //add an existing role to the database
            string roleName = "TestRole";
            rolesBL.AddRole(roleName);
        }

        [TestMethod]
        public void DeleteRole_Success_Test()
        {
            //successfully delete role
        }

        [TestMethod]
        public void DeleteRole_CoreRole_Test()
        {
            //delete admin, buyer, seller or guest
        }

        [TestMethod]
        public void DeleteRole_Invalid_Test()
        {
            //delete wut
        }

        [TestMethod]
        public void ReadRole_Success_Test()
        {
            //read single successfully
        }

        [TestMethod]
        public void ReadRoles_Success_Test()
        {
            //read all roles sucessfully
        }

        [TestMethod]
        public void UpdateRole_Success_Test()
        {
            //update rolename successfully
        }

        [TestMethod]
        public void UpdateRole_ExistingRoleName_Test()
        {
            //modify role name to an already existing rolename
        }

        [TestMethod]
        public void UpdateRole_CoreRole_Test()
        {
            //update core(admin, buyer, guest, seller) role test
        }
    }
}
