using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal;

namespace APIWrapper.ObsidianPortal.Tests
{
	[TestClass]
	public class API_Users_Tests
	{
		private static string _appId = "";
		private static string _appSecret = "";
		private static string _token = "";
		private static string _tokenSecret = "";

		private static XElement _approvedResults;

		[AssemblyInitialize]
		public static void LoadConstants(TestContext testContext)
		{
			var configPath = Path.GetFullPath(@"..\..\..\..\..\..\Configs\NoTPK.APIWrapper.ObsidianPortal.Tests.Config.xml");
			var tokenDoc = XDocument.Load(configPath);
			var tokens = (from t in tokenDoc.Descendants("TestTokens") select t).FirstOrDefault();

			if (tokens != null)
			{
				_appId = (string) tokens.Element("AppId");
				_appSecret = (string)tokens.Element("AppSecret");
				_token = (string)tokens.Element("AccessToken");
				_tokenSecret = (string)tokens.Element("AccessTokenSecret");
			}

			var approvedPath = Path.GetFullPath(@"..\..\..\..\..\..\Configs\NoTPK.APIWrapper.ObsidianPortal.Tests.Approved.xml");
			var approvedDOc = XDocument.Load(approvedPath);
			
			_approvedResults = (from a in approvedDOc.Descendants("ApprovedValues") select a).FirstOrDefault();
		}

		[TestMethod]
		public async Task Test_Users_Show__LoggedInUser()
		{
			var approved = (string) _approvedResults.Element("Show_LoggedInUser");
			var result = await ApiCalls.ShowMe(_appId, _appSecret, _token, _tokenSecret);
			Assert.AreEqual(approved, result);
		}
	}
}
