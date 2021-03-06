﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stardome.Controllers;
using Stardome.DomainObjects;
using Stardome.Models;
using Stardome.Services.Domain;

namespace Stardome.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        private Mock<IUserAuthCredentialService> aMockUserAuthCredentialService;
        private Mock<ISiteSettingsService> aMockSiteSettingsService;
        private Mock<IRoleService> aMockRoleService;
        private Mock<ControllerContext> controllerContext;
        private Mock<IPrincipal> principal;

        private AdminController controller;
        private UserAuthCredential userAuthCredentialAdmin;
        private UserAuthCredential userAuthCredentialProducer;
        private UserAuthCredential userAuthCredentialUser;
        private UserAuthCredential userAuthCredentialInActive;

        private string successMessage;
        private string failureMessage;

        private SiteSetting siteSetting;
        private SiteSetting siteSetting1;
        private SiteSetting siteSetting2;
        private SiteSetting siteSettingNew;
        private IEnumerable<SiteSetting> siteSettings;
        private IEnumerable<SiteSetting> siteSettingsNew;

        private UserAuthCredential userAuthCredential1;
        private UserAuthCredential userAuthCredential2;
        private UserAuthCredential userAuthCredentialNoUserInfo;
        private UserAuthCredential userAuthCredentialNoUserInfo1;
        private UserAuthCredential userAuthCredentialInactive;
        private UserAuthCredential userAuthCredentialInactive1;

        private User userUpdate;
        private User userUpdate1;

        [TestInitialize]
        public void Init()
        {
            aMockUserAuthCredentialService = new Mock<IUserAuthCredentialService>();
            aMockSiteSettingsService = new Mock<ISiteSettingsService>();
            aMockRoleService = new Mock<IRoleService>();
            controllerContext = new Mock<ControllerContext>();
            principal = new Mock<IPrincipal>();
            controller = new AdminController(aMockUserAuthCredentialService.Object, aMockSiteSettingsService.Object, aMockRoleService.Object);
            principal.SetupGet(x => x.Identity.Name).Returns("username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;
            
            userAuthCredentialAdmin = new UserAuthCredential
            {
                RoleId = (int)Enums.Roles.Admin
            };

            userAuthCredentialProducer = new UserAuthCredential
            {
                RoleId = (int)Enums.Roles.Producer
            };
            userAuthCredentialUser = new UserAuthCredential
            {
                RoleId = (int)Enums.Roles.User
            };
            userAuthCredentialInActive = new UserAuthCredential
            {
                RoleId = (int)Enums.Roles.InActive
            };


            aMockUserAuthCredentialService.Setup(aService => aService.GetByUsername("username")).Returns(userAuthCredentialAdmin);

            successMessage = "Success";
            failureMessage = "No changes were made.";

            siteSetting = new SiteSetting {Category = "category", Name = "name", Value = "value", Id = 0};
            siteSetting1 = new SiteSetting{ Category = "category1", Name = "name1", Value = "value1", Id = 1 };
            siteSetting2 = new SiteSetting{ Category = "category2", Name = "name2", Value = "value2", Id = 2 };
            siteSettingNew = new SiteSetting{ Category = "category2New", Name = "name2", Value = "value2", Id = 2 };

            siteSettings = new List<SiteSetting>
            {
                siteSetting2,
                siteSetting1,
                siteSetting
            };

            siteSettingsNew = new List<SiteSetting>
            {
                siteSettingNew,
                siteSetting,
                siteSetting1
            };


            userAuthCredential1 = new UserAuthCredential
            {
                Id = 4,
                Role = new Role { Id = (int)Enums.Roles.Producer },
                Username = "username1",
                UserInformations = new Collection<UserInformation>
                {
                    new UserInformation
                    {
                        Email = "email1",
                        FirstName = "Jane",
                        LastName = "Doe"
                    }
                }
            };
            userAuthCredential2 = new UserAuthCredential
            {
                Id = 5,
                Role = new Role { Id = (int)Enums.Roles.User },
                Username = "username2",
                UserInformations = new Collection<UserInformation>
                {
                    new UserInformation
                    {
                        Email = "email2",
                        FirstName = "John",
                        LastName = "Doe"
                    }
                }
            };
            userAuthCredentialNoUserInfo = new UserAuthCredential
            {
                Id = 3,
                Role = new Role { Id = (int)Enums.Roles.User },
                Username = "username3"
            };
            userAuthCredentialInactive = new UserAuthCredential
            {
                Id = 2,
                Role = new Role { Id = (int)Enums.Roles.InActive },
                Username = "username4",
                UserInformations = new Collection<UserInformation>
                {
                    new UserInformation
                    {
                        Email = "email3",
                        FirstName = "Not",
                        LastName = "Active"
                    }
                }
            };
            userAuthCredentialNoUserInfo1 = new UserAuthCredential
            {
                Id = 6,
                Role = new Role { Id = (int)Enums.Roles.User },
                Username = "username6"
            };
            userAuthCredentialInactive1 = new UserAuthCredential
            {
                Id = 7,
                Role = new Role { Id = (int)Enums.Roles.InActive },
                Username = "username7",
                UserInformations = new Collection<UserInformation>
                {
                    new UserInformation
                    {
                        Email = "email3",
                        FirstName = "Not",
                        LastName = "Active"
                    }
                }
            };

            userUpdate = new User
            {
                Id = 4,
                FirstName = "fname",
                LastName = "lname",
                EmailAddress = "newEmail",
                RoleId = (int)Enums.Roles.User
            };
            userUpdate1 = new User
            {
                Id = 3,
                FirstName = "fname",
                LastName = "lname",
                EmailAddress = "newEmail",
                RoleId = (int)Enums.Roles.User
            };
            
        }

        [TestMethod]
        public void Users_Admin()
        {
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Users)).Returns(new SiteSetting() { Value = "users" });
            ViewResult result = controller.Users() as ViewResult;
            Assert.IsTrue(result.ViewBag.Message.Equals("users") && result.ViewBag.showAdminMenu);
        }

        [TestMethod]
        public void Users_Producer()
        {
            aMockUserAuthCredentialService.Setup(aService => aService.GetByUsername("username")).Returns(userAuthCredentialProducer);
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Users)).Returns(new SiteSetting() { Value = "users" });
            ViewResult result = controller.Users() as ViewResult;
            Assert.IsTrue(result.ViewBag.Message.Equals("users") && !result.ViewBag.showAdminMenu);
        }

        [TestMethod]
        public void Users_User()
        {
            aMockUserAuthCredentialService.Setup(aService => aService.GetByUsername("username")).Returns(userAuthCredentialUser);
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Users)).Returns(new SiteSetting() { Value = "users" });
            ViewResult result = controller.Users() as ViewResult;
            Assert.IsTrue(result.ViewBag.Message.Equals("users") && !result.ViewBag.showAdminMenu);
        }

        [TestMethod]
        public void Users_InActive()
        {
            aMockUserAuthCredentialService.Setup(aService => aService.GetByUsername("username")).Returns(userAuthCredentialInActive);
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Users)).Returns(new SiteSetting() { Value = "users" });
            ViewResult result = controller.Users() as ViewResult;
            Assert.IsTrue(result.ViewBag.Message.Equals("users") && !result.ViewBag.showAdminMenu);
        }

        [TestMethod]
        public void Content_Admin()
        {
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Content)).Returns(new SiteSetting() { Value = "content" });
            ViewResult result = controller.Content() as ViewResult;

            Assert.IsTrue(result.ViewBag.Message.Equals("content") && result.ViewBag.showAdminMenu);
        }

        [TestMethod]
        public void Content_Producer()
        {
            aMockUserAuthCredentialService.Setup(aService => aService.GetByUsername("username")).Returns(userAuthCredentialProducer);
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Content)).Returns(new SiteSetting() { Value = "content" });
            ViewResult result = controller.Content() as ViewResult;

            Assert.IsTrue(result.ViewBag.Message.Equals("content") && !result.ViewBag.showAdminMenu);
        }

        [TestMethod]
        public void Content_User()
        {
            aMockUserAuthCredentialService.Setup(aService => aService.GetByUsername("username")).Returns(userAuthCredentialUser);
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Content)).Returns(new SiteSetting() { Value = "content" });
            ViewResult result = controller.Content() as ViewResult;

            Assert.IsTrue(result.ViewBag.Message.Equals("content") && !result.ViewBag.showAdminMenu);
        }

        [TestMethod]
        public void Content_InActive()
        {
            aMockUserAuthCredentialService.Setup(aService => aService.GetByUsername("username")).Returns(userAuthCredentialInActive);
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Content)).Returns(new SiteSetting() { Value = "content" });
            ViewResult result = controller.Content() as ViewResult;

            Assert.IsTrue(result.ViewBag.Message.Equals("content") && !result.ViewBag.showAdminMenu);
        }

        [TestMethod]
        public void Settings()
        {
            aMockSiteSettingsService.Setup(aService => aService.GetAll()).Returns(siteSettings);
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Settings)).Returns(new SiteSetting() { Value = "settings" });

            ViewResult result = controller.Settings() as ViewResult;
            SettingModel resultsModel = result.Model as SettingModel;
            List<SiteSetting> resultList = resultsModel.Settings;

            Boolean isSame = resultList[0].Category == "category" && resultList[0].Name == "name" && resultList[0].Value == "value";
            isSame = isSame && resultList[1].Category == "category1" && resultList[1].Name == "name1" && resultList[1].Value == "value1";
            isSame = isSame && resultList[2].Category == "category2" && resultList[2].Name == "name2" && resultList[2].Value == "value2";
            isSame = isSame && result.ViewBag.showAdminMenu;
            isSame = isSame && String.IsNullOrEmpty(result.ViewBag.UpdateMessage);
            Assert.IsTrue(isSame);
        }

        [TestMethod]
        public void Settings_NoSettings()
        {
            IEnumerable<SiteSetting> siteSettings = new List<SiteSetting>();
            aMockSiteSettingsService.Setup(aService => aService.GetAll()).Returns(siteSettings);
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Settings)).Returns(new SiteSetting() { Value = "settings" });

            ViewResult result = controller.Settings() as ViewResult;
            SettingModel resultsModel = result.Model as SettingModel;
            List<SiteSetting> resultList = resultsModel.Settings;
            Assert.IsTrue(resultList.Count == 0);
        }

        [TestMethod]
        public void Settings_SettingModelParam()
        {
            SettingModel settingModelNew = new SettingModel
            {
                Settings = siteSettingsNew.ToList()
            };
            aMockSiteSettingsService.Setup(aService => aService.UpdateSiteSettings(settingModelNew.Settings)).Returns(successMessage);
            aMockSiteSettingsService.Setup(aService => aService.GetAll()).Returns(siteSettingsNew);
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Settings)).Returns(new SiteSetting() { Value = "settings" });

            ViewResult result = controller.Settings(settingModelNew) as ViewResult;
            SettingModel resultsModel = result.Model as SettingModel;
            List<SiteSetting> resultList = resultsModel.Settings;

            Assert.IsTrue(resultList[2].Category.Equals("category2New") && result.ViewBag.UpdateMessage.Equals(successMessage));
        }

        [TestMethod]
        public void Settings_SettingModelParamNull()
        {
            SettingModel settingModel = new SettingModel
            {
                Settings = null
            };
            aMockSiteSettingsService.Setup(aService => aService.UpdateSiteSettings(null)).Returns(failureMessage);
            aMockSiteSettingsService.Setup(aService => aService.GetAll()).Returns(new List<SiteSetting>());
            aMockSiteSettingsService.Setup(aService => aService.FindSiteSetting(SiteSettings.Settings)).Returns(new SiteSetting() { Value = "settings" });

            ViewResult result = controller.Settings(settingModel) as ViewResult;
            SettingModel resultsModel = result.Model as SettingModel;
            List<SiteSetting> resultList = resultsModel.Settings;

            Assert.IsTrue(resultList.Count == 0 && result.ViewBag.UpdateMessage.Equals(failureMessage));
        }

        [TestMethod]
        public void GetActiveUsers()
        {
            IEnumerable<UserAuthCredential> userAuthCredentials = new List<UserAuthCredential>
            {
                userAuthCredential1,
                userAuthCredentialInactive,
                userAuthCredential2,
                userAuthCredentialNoUserInfo
            };
            aMockUserAuthCredentialService.Setup(aService => aService.GetUserAuthCredentials()).Returns(userAuthCredentials);

            var data = Json.Decode(Json.Encode(controller.GetActiveUsers().Data));
            Boolean isTrue = data["TotalRecordCount"] == 2;
            isTrue = isTrue && data["Records"][0]["Id"] == 4;
            isTrue = isTrue && String.Equals(data["Records"][0]["EmailAddress"], "email1");
            isTrue = isTrue && String.Equals(data["Records"][0]["FirstName"], "Jane");
            isTrue = isTrue && String.Equals(data["Records"][0]["LastName"], "Doe");
            isTrue = isTrue && data["Records"][0]["RoleId"] == 2;
            isTrue = isTrue && String.Equals(data["Records"][0]["Username"], "username1");
            isTrue = isTrue && data["Records"][1]["Id"] == 5;

            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void GetActiveUsers_AllInactive()
        {
            IEnumerable<UserAuthCredential> userAuthCredentials = new List<UserAuthCredential>
            {
                
                userAuthCredentialInactive,
                userAuthCredentialInactive1
            };
            aMockUserAuthCredentialService.Setup(aService => aService.GetUserAuthCredentials()).Returns(userAuthCredentials);

            var data = Json.Decode(Json.Encode(controller.GetActiveUsers().Data));

            Assert.IsTrue(data["TotalRecordCount"] == 0);
        }

        [TestMethod]
        public void GetActiveUsers_AllNoUserInfo()
        {
            IEnumerable<UserAuthCredential> userAuthCredentials = new List<UserAuthCredential>
            {
                userAuthCredentialNoUserInfo,
                userAuthCredentialNoUserInfo1
            };
            aMockUserAuthCredentialService.Setup(aService => aService.GetUserAuthCredentials()).Returns(userAuthCredentials);

            var data = Json.Decode(Json.Encode(controller.GetActiveUsers().Data));

            Assert.IsTrue(data["TotalRecordCount"] == 0);
        }

        [TestMethod]
        public void GetActiveUsers_NoneReturned()
        {
            IEnumerable<UserAuthCredential> userAuthCredentials = new List<UserAuthCredential>
            {
                userAuthCredentialNoUserInfo,
                userAuthCredentialNoUserInfo1,
                userAuthCredentialInactive,
                userAuthCredentialInactive1
            };
            aMockUserAuthCredentialService.Setup(aService => aService.GetUserAuthCredentials()).Returns(userAuthCredentials);

            var data = Json.Decode(Json.Encode(controller.GetActiveUsers().Data));

            Assert.IsTrue(data["TotalRecordCount"] == 0);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            IEnumerable<UserAuthCredential> userAuthCredentials = new List<UserAuthCredential>
            {
                userAuthCredential1,
                userAuthCredentialInactive,
                userAuthCredential2,
                userAuthCredentialNoUserInfo
            };
            aMockUserAuthCredentialService.Setup(aService => aService.GetUserAuthCredentials()).Returns(userAuthCredentials);

            var data = Json.Decode(Json.Encode(controller.GetAllUsers().Data));
            Boolean isTrue = data["TotalRecordCount"] == 3;
            // userAuthCredential1
            isTrue = isTrue && data["Records"][0]["Id"] == 4;
            isTrue = isTrue && String.Equals(data["Records"][0]["EmailAddress"], "email1");
            isTrue = isTrue && String.Equals(data["Records"][0]["FirstName"], "Jane");
            isTrue = isTrue && String.Equals(data["Records"][0]["LastName"], "Doe");
            isTrue = isTrue && data["Records"][0]["RoleId"] == 2;
            isTrue = isTrue && String.Equals(data["Records"][0]["Username"], "username1");
            // userAuthCredentialInactive
            isTrue = isTrue && data["Records"][1]["Id"] == 2;
            isTrue = isTrue && String.Equals(data["Records"][1]["EmailAddress"], "email3");
            isTrue = isTrue && String.Equals(data["Records"][1]["FirstName"], "Not");
            isTrue = isTrue && String.Equals(data["Records"][1]["LastName"], "Active");
            isTrue = isTrue && data["Records"][1]["RoleId"] == (int)Enums.Roles.InActive;
            isTrue = isTrue && String.Equals(data["Records"][1]["Username"], "username4");
            // userAuthCredential2
            isTrue = isTrue && data["Records"][2]["Id"] == 5;
            isTrue = isTrue && String.Equals(data["Records"][2]["EmailAddress"], "email2");
            isTrue = isTrue && String.Equals(data["Records"][2]["FirstName"], "John");
            isTrue = isTrue && String.Equals(data["Records"][2]["LastName"], "Doe");
            isTrue = isTrue && data["Records"][2]["RoleId"] == (int)Enums.Roles.User;
            isTrue = isTrue && String.Equals(data["Records"][2]["Username"], "username2");

            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void GetAllUsers_AllInactive()
        {
            IEnumerable<UserAuthCredential> userAuthCredentials = new List<UserAuthCredential>
            {
                
                userAuthCredentialInactive,
                userAuthCredentialInactive1
            };
            aMockUserAuthCredentialService.Setup(aService => aService.GetUserAuthCredentials()).Returns(userAuthCredentials);

            var data = Json.Decode(Json.Encode(controller.GetAllUsers().Data));

            Assert.IsTrue(data["TotalRecordCount"] == 2);
        }

        [TestMethod]
        public void GetAllUsers_AllNoUserInfo()
        {
            IEnumerable<UserAuthCredential> userAuthCredentials = new List<UserAuthCredential>
            {
                userAuthCredentialNoUserInfo,
                userAuthCredentialNoUserInfo1
            };
            aMockUserAuthCredentialService.Setup(aService => aService.GetUserAuthCredentials()).Returns(userAuthCredentials);

            var data = Json.Decode(Json.Encode(controller.GetAllUsers().Data));

            Assert.IsTrue(data["TotalRecordCount"] == 0);
        }

        [TestMethod]
        public void GetAllUsers_AllInActiveOrNoUserInfo()
        {
            IEnumerable<UserAuthCredential> userAuthCredentials = new List<UserAuthCredential>
            {
                userAuthCredentialNoUserInfo,
                userAuthCredentialNoUserInfo1,
                userAuthCredentialInactive,
                userAuthCredentialInactive1
            };
            aMockUserAuthCredentialService.Setup(aService => aService.GetUserAuthCredentials()).Returns(userAuthCredentials);

            var data = Json.Decode(Json.Encode(controller.GetAllUsers().Data));

            Assert.IsTrue(data["TotalRecordCount"] == 2);
        }
        
        [TestMethod]
        public void UpdateUser()
        {
            aMockUserAuthCredentialService.Setup(aService => aService.GetById(userUpdate.Id)).Returns(userAuthCredential1);
            var data = Json.Decode(Json.Encode(controller.UpdateUser(userUpdate).Data));

            Assert.IsTrue(String.Equals(data["Result"], "OK"));

        }

        [TestMethod]
        public void UpdateUser_NoUserInfo()
        {
            aMockUserAuthCredentialService.Setup(aService => aService.GetById(userUpdate1.Id)).Returns(userAuthCredentialNoUserInfo);
            var data = Json.Decode(Json.Encode(controller.UpdateUser(userUpdate1).Data));

            Assert.IsTrue(String.Equals(data["Result"], "ERROR"));
        }

        [TestMethod]
        public void UpdateUser_EmptyUserAuthCredential()
        {
            User userUpdate = new User
            {
                Id = 3,
                FirstName = "fname",
                LastName = "lname",
                EmailAddress = "newEmail",
                RoleId = (int)Enums.Roles.User
            };

            aMockUserAuthCredentialService.Setup(aService => aService.GetById(userUpdate.Id)).Returns(new UserAuthCredential());
            var data = Json.Decode(Json.Encode(controller.UpdateUser(userUpdate).Data));

            Assert.IsTrue(String.Equals(data["Result"], "ERROR"));
        }
        
        [TestMethod]
        public void DeleteUser()
        {
            aMockUserAuthCredentialService.Setup(aService => aService.GetById(userUpdate.Id)).Returns(userAuthCredential1);
            var data = Json.Decode(Json.Encode(controller.DeleteUser(userUpdate).Data));

            Assert.IsTrue(String.Equals(data["Result"], "OK"));   
        }

        [TestMethod]
        public void GetRoles()
        {
            IEnumerable<Role> roles = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Role1 = "1"
                },
                new Role
                {
                    Id = 2,
                    Role1 = "2"
                }
            };
            aMockRoleService.Setup(aService => aService.GetRoles()).Returns(roles);
            var data = Json.Decode(Json.Encode(controller.GetRoles().Data));

            Assert.IsTrue(String.Equals(data["Options"][0]["DisplayText"], "1"));
            Assert.IsTrue(String.Equals(data["Options"][1]["DisplayText"], "2"));
        }

        [TestMethod]
        public void GetRoles_None()
        {
            aMockRoleService.Setup(aService => aService.GetRoles()).Returns(new List<Role>());
            var data = Json.Decode(Json.Encode(controller.GetRoles().Data));

            Assert.IsTrue(data["Options"].Length == 0);
        }

        [TestMethod]
        public void GetMainPath_InActive()
        {
            int roleId = (int)Enums.Roles.InActive;
            string result = controller.GetMainPath(roleId);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetMainPath_Admin()
        {
            int roleId = (int)Enums.Roles.Admin;
            aMockSiteSettingsService.Setup(aService => aService.GetFilePath()).Returns("C:\\test\\123_4\\");
            string result = controller.GetMainPath(roleId);

            Assert.IsTrue(String.Equals(result, "C:/test/123_4/"));
        }

        [TestMethod]
        public void GetMainPath_Admin_NoReplaceNeeded()
        {
            int roleId = (int)Enums.Roles.Admin;
            aMockSiteSettingsService.Setup(aService => aService.GetFilePath()).Returns("C:/test/123_4");
            string result = controller.GetMainPath(roleId);

            Assert.IsTrue(String.Equals(result, "C:/test/123_4/"));
        }

        [TestMethod]
        public void GetMainPath_Producer()
        {
            int roleId = (int)Enums.Roles.Producer;
            aMockSiteSettingsService.Setup(aService => aService.GetFilePath()).Returns("C:\\test\\123_4\\");
            string result = controller.GetMainPath(roleId);

            Assert.IsTrue(String.Equals(result, "C:/test/123_4/"));
        }

        [TestMethod]
        public void GetMainPath_User()
        {
            int roleId = (int)Enums.Roles.User;
            aMockSiteSettingsService.Setup(aService => aService.GetFilePath()).Returns("C:\\test\\123_4\\");
            string result = controller.GetMainPath(roleId);

            Assert.IsTrue(String.Equals(result, "C:/test/123_4/"));
        }
    }
}
