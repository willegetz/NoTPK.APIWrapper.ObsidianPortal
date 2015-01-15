using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal;
using APIWrapper.ObsidianPortal.Tests.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ApprovalTests.Reporters;
using ApprovalTests;

namespace APIWrapper.ObsidianPortal.Tests
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class API_Users_Tests
    {
        private static string _appId = "";
        private static string _appSecret = "";
        private static string _token = "";
        private static string _tokenSecret = "";

        private static XElement _testVariables;

        [ClassInitialize]
        public static void LoadConstants(TestContext testContext)
        {
            var configPath = Path.GetFullPath(@"..\..\..\..\..\..\Configs\NoTPK.APIWrapper.ObsidianPortal.Tests.Config.xml");
            var configDoc = XDocument.Load(configPath);
            var tokens = (from t in configDoc.Descendants("TestTokens") select t).FirstOrDefault();

            if (tokens != null)
            {
                _appId = (string)tokens.Element("AppId");
                _appSecret = (string)tokens.Element("AppSecret");
                _token = (string)tokens.Element("AccessToken");
                _tokenSecret = (string)tokens.Element("AccessTokenSecret");
            }

            _testVariables = (from v in configDoc.Descendants("TestVariables") select v).FirstOrDefault();
        }

        [TestMethod]
        [Ignore] // Timestamp is created for "updated_at" value whenever a get request is made.
        public void Test_Users_Show__LoggedInUser()
        {
            var result = API_Users.ShowMe(_appId, _appSecret, _token, _tokenSecret).Result;
            Approvals.Verify(result);
        }

        [TestMethod]
        public void Test_Users_Show__ById()
        {
            var userId = (string)_testVariables.Element("UserId");

            var result = API_Users.ShowById(_appId, _appSecret, _token, _tokenSecret, userId).Result;
            Approvals.Verify(result);
        }

        [TestMethod]
        public void Test_Users_Show__ByName()
        {
            var userName = (string)_testVariables.Element("UserName");

            var result = API_Users.ShowByName(_appId, _appSecret, _token, _tokenSecret, userName).Result;
            Approvals.Verify(result);
        }
    }
}
