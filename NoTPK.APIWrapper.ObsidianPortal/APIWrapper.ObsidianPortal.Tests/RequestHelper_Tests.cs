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
        private const string Location = "location";
        private const string Nonce = "nonce";
        private const string OauthTimeStamp = "1444009267";

        [TestMethod]
        public void TestGetAuthorizationHeader()
        {
            var webMethod = HttpMethod.Get;
            var mockClock = new MockClock(DateTime.Parse("6/28/3185 5:30:00"));
            var result = RequestHelpers.GetAuthorizationHeader(mockClock, AppId, AppSecret, AccessToken, AccessTokenSecret, Location, webMethod, Nonce);
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
    }
}