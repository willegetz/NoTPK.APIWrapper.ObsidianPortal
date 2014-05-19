using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal;

namespace APIWrapper.ObsidianPortal.Tests
{
	[TestClass]
	public class API_WikiPages_Tests
	{
		private static string _appId = "";
		private static string _appSecret = "";
		private static string _token = "";
		private static string _tokenSecret = "";

		private static XElement _approvedResults;
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

			var approvedPath = Path.GetFullPath(@"..\..\..\..\..\..\Configs\NoTPK.APIWrapper.ObsidianPortal.Tests.Approved.xml");
			var approvedDOc = XDocument.Load(approvedPath);

			_approvedResults = (from a in approvedDOc.Descendants("ApprovedValues").Descendants("WikiPages") select a).FirstOrDefault();
			_testVariables = (from v in configDoc.Descendants("TestVariables") select v).FirstOrDefault();
		}

		[TestMethod]
		public async Task Test_WikiPages_Index__ByCampaignId()
		{
			var approved = (string) _approvedResults.Element("Index_WikiPages");
			var campaignId = (string) _testVariables.Element("CampaignId");
			
			var result = await API_WikiPages.IndexByCampaignId(_appId, _appSecret, _token, _tokenSecret, campaignId);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_WikiPages_Show__ByWikiPageId()
		{
			var approved = (string) _approvedResults.Element("Show_WikiPageById");
			var campaignId = (string) _testVariables.Element("CampaignId");
			var wikiPageId = (string) _testVariables.Element("WikiPageId");

			var result = await API_WikiPages.ShowById(_appId, _appSecret, _token, _tokenSecret, campaignId, wikiPageId);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		[Ignore] // Recieving a 504 Gate-way timeout.
		public async Task Test_WikiPages_Show__BySlug()
		{
			var approved = (string) _approvedResults.Element("Show_WikiPageBySlug");
			var campaignId = (string) _testVariables.Element("CampaignId");
			var wikiPageSlug = (string) _testVariables.Element("WikiPageSlug");

			var result = await API_WikiPages.ShowBySlug(_appId, _appSecret, _token, _tokenSecret, campaignId, wikiPageSlug);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps

		public async Task Test_WikiPages_Create__ByCampaignId()
		{
			var approved = (string) _approvedResults.Element("Create_WikiPage");
			var campaignId = (string) _testVariables.Element("ModifiableCampaign");
			var newWikiPage = (string) _testVariables.Element("NewWikiPage");

			var result = await API_WikiPages.Create(_appId, _appSecret, _token, _tokenSecret, campaignId, newWikiPage);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		[Ignore] // Destructive -- Need mocking, perhaps
		public async Task Test_WikiPages_Update__ByWikiPageId()
		{
			var approved = (string) _approvedResults.Element("Update_WikiPage");
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
			var approved = (string) _approvedResults.Element("Delete_WikiPage");
			var campaignId = (string) _testVariables.Element("ModifiableCampaign");
			var deleteWikipageId = (string) _testVariables.Element("DeleteWikiPageId");

			var result = await API_WikiPages.Delete(_appId, _appSecret, _token, _tokenSecret, campaignId, deleteWikipageId);
			Assert.AreEqual(approved, result);
		}
	}
}