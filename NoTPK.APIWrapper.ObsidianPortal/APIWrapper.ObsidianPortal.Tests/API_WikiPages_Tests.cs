using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal;
using APIWrapper.ObsidianPortal.Tests.Support;
using ApprovalTests;
using ApprovalTests.Reporters;

namespace APIWrapper.ObsidianPortal.Tests
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class API_WikiPages_Tests
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
		public void Test_WikiPages_Index__ByCampaignId()
		{
			var campaignId = (string) _testVariables.Element("CampaignId");
			
			var result = API_WikiPages.IndexByCampaignId(_appId, _appSecret, _token, _tokenSecret, campaignId).Result;
			Approvals.Verify(result);
		}

		[TestMethod]
		public void Test_WikiPages_Show__ByWikiPageId()
		{
			var campaignId = (string) _testVariables.Element("CampaignId");
			var wikiPageId = (string) _testVariables.Element("WikiPageId");

			var result = API_WikiPages.ShowById(_appId, _appSecret, _token, _tokenSecret, campaignId, wikiPageId).Result;
			Approvals.Verify(result);
		}

		[TestMethod]
		/// There are two types of wiki pages: Post and WikiPage. ShowBySlug sometimes works with WikiPages, and not Posts; even then, it may not work.
		public void Test_WikiPages_Show__BySlug()
		{
			var campaignId = (string) _testVariables.Element("CampaignId");
			var wikiPageSlug = (string) _testVariables.Element("WikiPageSlug");

			var result = API_WikiPages.ShowBySlug(_appId, _appSecret, _token, _tokenSecret, campaignId, wikiPageSlug).Result;
			Approvals.Verify(result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps

		public void Test_WikiPages_Create__ByCampaignId()
		{
			var campaignId = (string) _testVariables.Element("ModifiableCampaign");
			var newWikiPage = (string) _testVariables.Element("NewWikiPage");

			var result = API_WikiPages.Create(_appId, _appSecret, _token, _tokenSecret, campaignId, newWikiPage).Result;
			Approvals.Verify(result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps
		public void Test_WikiPages_Update__ByWikiPageId()
		{
			var campaignId = (string) _testVariables.Element("ModifiableCampaign");
			var updateWikiPageId = (string) _testVariables.Element("UpdateWikiPageId");
			var updateWikiPageContent = (string) _testVariables.Element("UpdateWikiContent");

			var result = API_WikiPages.Update(_appId, _appSecret, _token, _tokenSecret, campaignId, updateWikiPageId, updateWikiPageContent).Result;
			Approvals.Verify(result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps
		public void Test_WikiPages_Delete__ByWikiPageId()
		{
			var campaignId = (string) _testVariables.Element("ModifiableCampaign");
			var deleteWikipageId = (string) _testVariables.Element("DeleteWikiPageId");

			var result = API_WikiPages.Delete(_appId, _appSecret, _token, _tokenSecret, campaignId, deleteWikipageId).Result;
			Approvals.Verify(result);
		}
	}
}