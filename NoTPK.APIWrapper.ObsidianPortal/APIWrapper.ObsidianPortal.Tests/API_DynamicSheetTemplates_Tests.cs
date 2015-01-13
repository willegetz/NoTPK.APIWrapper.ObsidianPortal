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
	public class API_DynamicSheetTemplates_Tests
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
		public async Task Test_DynamicSheetTemplates_Index()
		{
			var approved = Helpers.GetApprovedResults("Index_DSTs");

			var result = await API_DynamicSheetTemplates.Index(_appId, _appSecret, _token, _tokenSecret);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_DynamicSheetTemplates_Show__ById()
		{
			var approved = Helpers.GetApprovedResults("Show_DST");
			var dstId = (string) _testVariables.Element("DST_Id");

			var result = await API_DynamicSheetTemplates.ShowById(_appId, _appSecret, _token, _tokenSecret, dstId);
			Assert.AreEqual(approved, result);
		}

		[TestMethod]
		public async Task Test_DynamicSheetTemplates_Show__BySlug()
		{
			var approved = Helpers.GetApprovedResults("Show_DST_BySlug");
			var dstSlug = (string) _testVariables.Element("DST_Slug");

			var result = await API_DynamicSheetTemplates.ShowBySlug(_appId, _appSecret, _token, _tokenSecret, dstSlug);
			Assert.AreEqual(approved, result);
		}
	}
}