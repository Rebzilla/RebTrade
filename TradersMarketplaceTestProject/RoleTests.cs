using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Transactions;
using System.Collections.Generic;
using DataAccessLayer;
using BusinessLayer;
using Common;
using Common.CustomExceptions;


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

        public void AreListsEqual(List<Role> expected, List<Role> actual)
        {
            if (actual.Count != expected.Count)
            {
                Assert.Fail("List are not of the same size.");
            }
            expected = expected.OrderBy(r => r.RoleID).ToList<Role>();
            actual = actual.OrderBy(r => r.RoleID).ToList<Role>();
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].RoleID, actual[i].RoleID);
                Assert.AreEqual(expected[i].RoleName, actual[i].RoleName);
            }
        }
        
        #region CreateTests

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreateRole_Null_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();
            //add a new role to the db
            string roleName = null;
            rolesBL.AddRole(roleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateRole_EmptyRole_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();
            //add a new role to the db
            string roleName = "";
            rolesBL.AddRole(roleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        public void CreateRole_Success_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();
            //add a new role to the db
            string roleName = "aaa";
            rolesBL.AddRole(roleName);

            Role actualRole = rolesBL.GetRoleByRoleName(roleName);
            Role expectedRole = new Role()
            {
                RoleID = actualRole.RoleID,
                RoleName = roleName
            };

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list
            expectedRoles.Add(expectedRole); //add the newly added role to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        public void CreateRole_SuccessLimit_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();
            //add a new role to the db
            string roleName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            rolesBL.AddRole(roleName);

            Role actualRole = rolesBL.GetRoleByRoleName(roleName);
            Role expectedRole = new Role()
            {
                RoleID = actualRole.RoleID,
                RoleName = roleName
            };

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list
            expectedRoles.Add(expectedRole); //add the newly added role to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))] 
        public void CreateRole_OverLimit_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();
            //add a new role to the db
            string roleName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            rolesBL.AddRole(roleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }
       
        [TestMethod]
        public void CreateRole_InvalidString_Test()
        {
            //Cannot compile

            //get all the roles from the db
            //List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //string roleName = 3333;
            //rolesBL.AddRole(roleName);

            //List<Role> expectedRoles = new List<Role>();
            //expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            //List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            ////check if expected roles with the actual roles are equal
            //AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(RoleNameAlreadyExistsException))]
        public void CreateRole_ExistingRole_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();
            //add an existing role to the database
            string roleName = "Guest";
            rolesBL.AddRole(roleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list
            
            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CreateRole_RoleNumber_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();
            //add a new role to the db
            string roleName = "TestRole1";
            rolesBL.AddRole(roleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CreateRole_RoleCharacter_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();
            //add a new role to the db
            string roleName = "TestRoleж";
            rolesBL.AddRole(roleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        #endregion 

        #region DeleteTests
        [TestMethod]
        public void DeleteRole_Success_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //successfully delete role
            Role r = rolesBL.GetRole(13);
            rolesBL.DeleteRole(r.RoleID);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list
            expectedRoles.RemoveAll(x => x.RoleID == r.RoleID);

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreRoleException))]
        public void DeleteRole_CoreRole_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //delete admin, buyer, seller or guest
            int roleID = 1;
            rolesBL.DeleteRole(1);
            
            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteRole_ZeroID_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //delete admin, buyer, seller or guest
            int roleID = 0;
            rolesBL.DeleteRole(0);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteRole_Invalid_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            rolesBL.DeleteRole(-1);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        public void DeleteRole_NotAnInt_Test()
        {
            //Cannot compile

            //get all the roles from the db
            //List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //rolesBL.DeleteRole("one");

            //List<Role> expectedRoles = new List<Role>();
            //expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            //List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            ////check if expected roles with the actual roles are equal
            //AreListsEqual(expectedRoles, actualRoles);
        }
        #endregion

        #region ReadTest
        [TestMethod]
        public void ReadRole_Success_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //read single role successfully
            Role r = rolesBL.GetRole(1);
            
            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadRole_InvalidRoleID_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            Role r = rolesBL.GetRole(-1);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        public void ReadRole_NotAnInt_Test()
        {
            //Cannot compile

            //get all the roles from the db
            //List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //Role r = rolesBL.GetRole("one");
            //Assert.IsNotNull(rolesBL.GetRole("one"));

            //List<Role> expectedRoles = new List<Role>();
            //expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            //List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            ////check if expected roles with the actual roles are equal
            //AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadRole_EmptyRole_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            Role r = rolesBL.GetRole(0);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadRole_ZeroID_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            string roleName = "hello";
            Role r = rolesBL.GetRoleByRoleName(roleName);
            Role ro = rolesBL.GetRole(r.RoleID);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        #endregion

        #region UpdateTest
        [TestMethod]
        public void UpdateRole_Success_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //update rolename successfully
            string newRoleName = "aaa";

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            Role role = rolesBL.GetRole(13);
            expectedRoles.RemoveAll(x => x.RoleID == role.RoleID);
            role.RoleName = newRoleName;
            rolesBL.UpdateRole(13, newRoleName);
            expectedRoles.Add(role);

            Role updatedRole = rolesBL.GetRoleByRoleName(newRoleName);
            Role expectedRole = new Role()
            {
                RoleID = updatedRole.RoleID,
                RoleName = newRoleName
            };

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
            
        }

        [TestMethod]
        public void UpdateRole_SuccessLimit_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //update rolename successfully
            string newRoleName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            Role role = rolesBL.GetRole(13);
            expectedRoles.RemoveAll(x => x.RoleID == role.RoleID);
            role.RoleName = newRoleName;
            rolesBL.UpdateRole(13, newRoleName);
            expectedRoles.Add(role);

            Role updatedRole = rolesBL.GetRoleByRoleName(newRoleName);
            Role expectedRole = new Role()
            {
                RoleID = updatedRole.RoleID,
                RoleName = newRoleName
            };

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateRole_OverLimit_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            string newRoleName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            rolesBL.UpdateRole(13, newRoleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateRole_EmptyRoleName_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            string newRoleName = "";
            rolesBL.UpdateRole(-1, newRoleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateRole_NullRoleName_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            string newRoleName = null;
            rolesBL.UpdateRole(0, newRoleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        public void UpdateRole_InvalidRoleName_Test()
        {
            //does not compile
            //get all the roles from the db
            //List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //string newRoleName = 3333;
            //rolesBL.UpdateRole("one", newRoleName);

            //List<Role> expectedRoles = new List<Role>();
            //expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            //List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            ////check if expected roles with the actual roles are equal
            //AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(RoleNameAlreadyExistsException))]
        public void UpdateRole_ExistingRoleName_Test()
        {
            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //modify role name to an already existing rolename
            string oldRoleName = "RoleTest";
            string newRoleName = "Guest";
            Role r = rolesBL.GetRoleByRoleName(oldRoleName);
            r.RoleName = newRoleName;
            rolesBL.UpdateRole(r.RoleID, r.RoleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreRoleException))]
        public void UpdateRole_CoreRole_Test()
        {

            //get all the roles from the db
            List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //update core role test - guest, buyer, seller, administrator
            string oldRoleName = "Guest";
            string newRoleName = "Testing";
            Role r = rolesBL.GetRoleByRoleName(oldRoleName);
            r.RoleName = newRoleName;
            rolesBL.UpdateRole(r.RoleID, r.RoleName);

            List<Role> expectedRoles = new List<Role>();
            expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            //check if expected roles with the actual roles are equal
            AreListsEqual(expectedRoles, actualRoles);
        }

        [TestMethod]
        //[ExpectedException(typeof(FormatException))]
        public void UpdateRole_IntRoleName_Test()
        {
            //cannot compile
            //get all the roles from the db
            //List<Role> previousRoles = rolesBL.GetAllRoles().ToList<Role>();

            //string oldRoleName = "RoleTest";
            //string newRoleName = 3333;
            //Role r = rolesBL.GetRoleByRoleName(oldRoleName);
            //r.RoleName = newRoleName;
            //rolesBL.UpdateRole(r.RoleID, r.RoleName);

            //List<Role> expectedRoles = new List<Role>();
            //expectedRoles.AddRange(previousRoles); //add the previous Roles to the list

            //List<Role> actualRoles = rolesBL.GetAllRoles().ToList<Role>();
            ////check if expected roles with the actual roles are equal
            //AreListsEqual(expectedRoles, actualRoles);
        }
        #endregion
    }
}
