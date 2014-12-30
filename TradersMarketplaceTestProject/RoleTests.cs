using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using Common;
using Common.CustomExceptions;
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
        #region CreateTests

        [TestMethod]
        public void CreateRole_Success_Test()
        {
            //successfully add role
            string roleName = "TestRole";
            rolesBL.AddRole(roleName);
            Assert.IsNotNull(rolesBL.GetRoleByRoleName(roleName));
        }

        [TestMethod]
        [ExpectedException(typeof(RoleNameAlreadyExistsException))]
        public void CreateRole_ExistingRole_Test()
        {
            //add an existing role to the database
            string roleName = "Guest";
            rolesBL.AddRole(roleName);
            Assert.IsNotNull(rolesBL.GetRoleByRoleName(roleName));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreateRole_EmptyRole_Test()
        {
            string roleName = "";
            rolesBL.AddRole(roleName);
            Assert.IsNotNull(rolesBL.GetRoleByRoleName(roleName));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CreateRole_RoleNumber_Test()
        {
            string roleName = "TestRole1";
            rolesBL.AddRole(roleName);
            Assert.IsNotNull(rolesBL.GetRoleByRoleName(roleName));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CreateRole_RoleCharacter_Test()
        {
            string roleName = "TestRole$";
            rolesBL.AddRole(roleName);
            Assert.IsNotNull(rolesBL.GetRoleByRoleName(roleName));
        }

        #endregion 

        #region DeleteTests
        [TestMethod]
        public void DeleteRole_Success_Test()
        {
            //successfully delete role
            string roleName = "RoleTest";
            Role r = rolesBL.GetRoleByRoleName(roleName);
            rolesBL.DeleteRole(r.RoleID);
            Assert.IsNull(rolesBL.GetRole(r.RoleID));
            
        }

        [TestMethod]
        [ExpectedException(typeof(CoreRoleException))]
        public void DeleteRole_CoreRole_Test()
        {
            //delete admin, buyer, seller or guest
            int roleID = 1;
            rolesBL.DeleteRole(1);
            Assert.IsNull(rolesBL.GetRole(1));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteRole_Invalid_Test()
        {
            Role r = rolesBL.GetRole(0);
            rolesBL.DeleteRole(r.RoleID);
            Assert.IsNull(rolesBL.GetRole(r.RoleID));
        }

        #endregion

        #region ReadTest
        [TestMethod]
        public void ReadRole_Success_Test()
        {
            //read single role successfully
            Role r = rolesBL.GetRole(1);
            Assert.IsNotNull(rolesBL.GetRole(1));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadRole_EmptyRole_Test()
        {
            string roleName = "hello";
            Role r = rolesBL.GetRoleByRoleName(roleName);
            Role ro = rolesBL.GetRole(r.RoleID);
            Assert.IsNull(rolesBL.GetRole(r.RoleID));
        }

        #endregion

        #region UpdateTest
        [TestMethod]
        public void UpdateRole_Success_Test()
        {
            //update rolename successfully
            string oldRoleName = "RoleTest";
            string newRoleName = "TestingRole";
            Role r = rolesBL.GetRoleByRoleName(oldRoleName);
            r.RoleName = newRoleName;
            rolesBL.UpdateRole(r.RoleID, r.RoleName);
            Assert.IsNotNull(rolesBL.GetRoleByRoleName(newRoleName));
        }

        [TestMethod]
        [ExpectedException(typeof(RoleNameAlreadyExistsException))]
        public void UpdateRole_ExistingRoleName_Test()
        {
            //modify role name to an already existing rolename
            string oldRoleName = "RoleTest";
            string newRoleName = "Guest";
            Role r = rolesBL.GetRoleByRoleName(oldRoleName);
            r.RoleName = newRoleName;
            rolesBL.UpdateRole(r.RoleID, r.RoleName);
            Assert.IsNotNull(rolesBL.GetRoleByRoleName(newRoleName));
        }

        [TestMethod]
        [ExpectedException(typeof(CoreRoleException))]
        public void UpdateRole_CoreRole_Test()
        {
            //update core role test - guest, buyer, seller, administrator
            string oldRoleName = "Guest";
            string newRoleName = "Testing";
            Role r = rolesBL.GetRoleByRoleName(oldRoleName);
            r.RoleName = newRoleName;
            rolesBL.UpdateRole(r.RoleID, r.RoleName);
            Assert.IsNotNull(rolesBL.GetRoleByRoleName(newRoleName));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateRole_VoidRoleName_Test()
        {
            string oldRoleName = "RoleTest";
            string newRoleName = "";
            Role r = rolesBL.GetRoleByRoleName(oldRoleName);
            r.RoleName = newRoleName;
            rolesBL.UpdateRole(r.RoleID, r.RoleName);
            Assert.IsNotNull(rolesBL.GetRoleByRoleName(newRoleName));
        }
        #endregion
    }
}
