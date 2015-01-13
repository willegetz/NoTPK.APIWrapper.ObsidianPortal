using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal;
using APIWrapper.ObsidianPortal.Tests.Support;

namespace APIWrapper.ObsidianPortal.Tests
{
	[TestClass]
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
				_appId = (string) tokens.Element("AppId");
				_appSecret = (string)tokens.Element("AppSecret");
				_token = (string)tokens.Element("AccessToken");
				_tokenSecret = (string)tokens.Element("AccessTokenSecret");
			}

			_testVariables = (from v in configDoc.Descendants("TestVariables") select v).FirstOrDefault();
		}

		[TestMethod]
		[Ignore] // Timestamp is created for "updated_at" value whenever a get request is made.
		public async Task Test_Users_Show__LoggedInUser()
		{
			var approved = Helpers.GetApprovedResults("Show_LoggedInUser");
			var result = await API_Users.ShowMe(_appId, _appSecret, _token, _tokenSecret);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_Users_Show__ById()
		{
			var approved = Helpers.GetApprovedResults("Show_UserById");
			var userId = (string) _testVariables.Element("UserId");

			var result = await API_Users.ShowById(_appId, _appSecret, _token, _tokenSecret, userId);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_Users_Show__ByName()
		{
			var approved = Helpers.GetApprovedResults("Show_UserName");
			var userName = (string) _testVariables.Element("UserName");

			var result = await API_Users.ShowByName(_appId, _appSecret, _token, _tokenSecret, userName);
			Assert.AreEqual(approved, result);
		}
	}
}
