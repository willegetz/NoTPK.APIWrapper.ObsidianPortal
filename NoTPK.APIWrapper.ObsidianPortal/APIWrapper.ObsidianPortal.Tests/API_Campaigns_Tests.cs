using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal;
using APIWrapper.ObsidianPortal.Tests.Support;
using ApprovalTests.Reporters;
using ApprovalTests;

namespace APIWrapper.ObsidianPortal.Tests
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class API_Campaigns_Tests
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
		public void Test_Campaigns_Show__ById()
		{
			var campaignId = (string)_testVariables.Element("CampaignId");

			var result = API_Campaigns.ShowById(_appId, _appSecret, _token, _tokenSecret, campaignId).Result;
			Approvals.Verify(result);
		}

		[TestMethod]
		public void Test_Campaigns_Show__BySlug()
		{
			var campaignSlug = (string)_testVariables.Element("CampaignSlug");

			var result = API_Campaigns.ShowBySlug(_appId, _appSecret, _token, _tokenSecret, campaignSlug).Result;
			Approvals.Verify(result);
		}
	}
}