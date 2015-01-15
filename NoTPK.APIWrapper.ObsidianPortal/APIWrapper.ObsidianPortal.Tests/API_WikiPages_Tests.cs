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
		public async Task Test_WikiPages_Index__ByCampaignId()
		{
			var approved = Helpers.GetApprovedResults("Index_WikiPages");
			var campaignId = (string) _testVariables.Element("CampaignId");
			
			var result = await API_WikiPages.IndexByCampaignId(_appId, _appSecret, _token, _tokenSecret, campaignId);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_WikiPages_Show__ByWikiPageId()
		{
			var approved = Helpers.GetApprovedResults("Show_WikiPageById");
			var campaignId = (string) _testVariables.Element("CampaignId");
			var wikiPageId = (string) _testVariables.Element("WikiPageId");

			var result = await API_WikiPages.ShowById(_appId, _appSecret, _token, _tokenSecret, campaignId, wikiPageId);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		/// There are two types of wiki pages: Post and WikiPage. ShowBySlug sometimes works with WikiPages, and not Posts; even then, it may not work.
		public async Task Test_WikiPages_Show__BySlug()
		{
			var approved = Helpers.GetApprovedResults("Show_WikiPageBySlug");
			var campaignId = (string) _testVariables.Element("CampaignId");
			var wikiPageSlug = (string) _testVariables.Element("WikiPageSlug");

			var result = await API_WikiPages.ShowBySlug(_appId, _appSecret, _token, _tokenSecret, campaignId, wikiPageSlug);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps

		public async Task Test_WikiPages_Create__ByCampaignId()
		{
			var approved = Helpers.GetApprovedResults("Create_WikiPage");
			var campaignId = (string) _testVariables.Element("ModifiableCampaign");
			var newWikiPage = (string) _testVariables.Element("NewWikiPage");

			var result = await API_WikiPages.Create(_appId, _appSecret, _token, _tokenSecret, campaignId, newWikiPage);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps
		public async Task Test_WikiPages_Update__ByWikiPageId()
		{
			var approved = Helpers.GetApprovedResults("Update_WikiPage");
			var campaignId = (string) _testVariables.Element("ModifiableCampaign");
			var updateWikiPageId = (string) _testVariables.Element("UpdateWikiPageId");
			var updateWikiPageContent = (string) _testVariables.Element("UpdateWikiContent");

			var result = await API_WikiPages.Update(_appId, _appSecret, _token, _tokenSecret, campaignId, updateWikiPageId, updateWikiPageContent);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps
		public async Task Test_WikiPages_Delete__ByWikiPageId()
		{
			var approved = Helpers.GetApprovedResults("Delete_WikiPage");
			var campaignId = (string) _testVariables.Element("ModifiableCampaign");
			var deleteWikipageId = (string) _testVariables.Element("DeleteWikiPageId");

			var result = await API_WikiPages.Delete(_appId, _appSecret, _token, _tokenSecret, campaignId, deleteWikipageId);
			Assert.AreEqual(approved, result);
		}
	}
}