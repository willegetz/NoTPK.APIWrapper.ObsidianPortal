using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;
using ApprovalTests;
using System.Net.Http;
using ApprovalUtilities.Utilities;

namespace APIWrapper.ObsidianPortal.Tests
{
    [TestClass]
    public class RequestHelper_Tests
    {
        private const string AppId = "appId";
        private const string AppSecret = "appSecret";
        private const string AccessToken = "accessToken";
        private const string AccessTokenSecret = "accessTokenSecret";
        private const string Location = @"http://api.obsidianportal.com/v1/campaigns/123456789.json";
        private const string Nonce = "nonce";
        private const string OauthTimeStamp = "38357152200";
        private const string ParameterString = "oauth_consumer_key=appId&oauth_nonce=nonce&oauth_signature_method=HMAC-SHA1&oauth_timestamp=38357152200&oauth_token=accessToken&oauth_version=1.0";
        private const string SignatureData = "GET&http%3A%2F%2Fapi.obsidianportal.com%2Fv1%2Fcampaigns%2F123456789.json&oauth_consumer_key%3DappId%26oauth_nonce%3Dnonce%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D38357152200%26oauth_token%3DaccessToken%26oauth_version%3D1.0";

        private MockClock _mockClock = new MockClock(DateTime.Parse("6/28/3185 5:30:00"));

        [TestMethod]
        public void TestGetAuthorizationHeader()
        {
            var webMethod = HttpMethod.Get;
            var result = RequestHelpers.GetAuthorizationHeader(_mockClock, AppId, AppSecret, AccessToken, AccessTokenSecret, Location, webMethod, Nonce);
            Approvals.Verify(result);
        }

        [TestMethod]
        public void TestGetAuthorizationParts()
        {
            var optionalParams = new Dictionary<string, string>()
            {
                {"optionalKey1", "optionalValue1" },
                {"optionalKey2", "optionalValue2" },
                {"optionalKey3", "optionalValue3" },
            };

            var result = RequestHelpers.GetAuthorizationParts(AppId, AccessToken, Nonce, OauthTimeStamp, optionalParams);
            Approvals.VerifyAll(result);
        }

        [TestMethod]
        public void TestGetAuthorizationParts_NoOptionalParams()
        {
            var result = RequestHelpers.GetAuthorizationParts(AppId, AccessToken, Nonce, OauthTimeStamp);
            Approvals.VerifyAll(result);
        }

        [TestMethod]
        public void TestBuildAuthorizationParameterString()
        {
            var authorizationParts = new SortedDictionary<string, string>()
            {
                {"oauth_consumer_key", AppId},
                {"oauth_nonce", Nonce},
                {"oauth_signature_method", "HMAC-SHA1"},
                {"oauth_token", AccessToken},
                {"oauth_timestamp", OauthTimeStamp},
                {"oauth_version", "1.0"}
            };

            var result = RequestHelpers.BuildAuthorizationParameterString(authorizationParts);
            Approvals.Verify(result);
        }

        [TestMethod]
        public void TestGenerateTimeStamp()
        {
            var result = RequestHelpers.GenerateTimeStamp(_mockClock);
            Approvals.Verify(result);
        }

        [TestMethod]
        public void TestBuildCanonicalizedRequest()
        {
            var webMethod = new HttpMethod("GET");
            var result = RequestHelpers.BuildCanonicalizedRequest(Location, webMethod, ParameterString);
            Approvals.Verify(result);
        }

        [TestMethod]
        public void TestComputeSignature()
        {
            var result = RequestHelpers.ComputeSignature(AppSecret, AccessTokenSecret, SignatureData);
            Approvals.Verify(result);
        }
    }
}