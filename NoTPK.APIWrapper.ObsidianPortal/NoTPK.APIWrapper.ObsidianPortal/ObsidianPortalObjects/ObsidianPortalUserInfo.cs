using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;

namespace NoTPK.APIWrapper.ObsidianPortal.ObsidianPortalObjects
{
	public class ObsidianPortalUserInfo
	{
		public ObsidianPortalUserInfo(Dictionary<string, object> userInfo)
		{
			UserId = userInfo.Where(ui => ui.Key == "id").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			UserName = userInfo.Where(ui => ui.Key == "username").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			AvatarImageUrl = userInfo.Where(ui => ui.Key == "avatar_image_url").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			ProfileUrl = userInfo.Where(ui => ui.Key == "profile_url").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			IsAscendant = userInfo.Where(ui => ui.Key == "is_ascendant").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			Created = userInfo.Where(ui => ui.Key == "created_at").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			LastSeen = userInfo.Where(ui => ui.Key == "last_seen_at").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			Updated = userInfo.Where(ui => ui.Key == "updated_at").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			UtcOffset = userInfo.Where(ui => ui.Key == "utc_offset").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			Locale = userInfo.Where(ui => ui.Key == "locale").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			
			var userHasCampaigns = userInfo.Any(ui => ui.Key == "campaigns");
			if (userHasCampaigns)
			{
				var campaigns = userInfo.Where(ui => ui.Key == "campaigns").Select(ui => ui.Value).FirstOrDefault() as ArrayList;

				if (campaigns != null)
				{
					Campaigns = GetUserCampaigns(campaigns);
				}
			}
			else
			{
				Campaigns = new List<Campaign>();
			}
		}

		private List<Campaign> GetUserCampaigns(ArrayList campaigns)
		{
			var userCampagins = new List<Campaign>();

			foreach (Dictionary<string, object> campaign in campaigns)
			{
				userCampagins.Add(new Campaign(campaign));
			}
			return userCampagins;
		}

		public string UserId { get; set; }
		public string UserName { get; set; }
		public string AvatarImageUrl { get; set; }
		public string ProfileUrl { get; set; }
		public string Created { get; set; }
		public string LastSeen { get; set; }
		public string Updated { get; set; }
		public string UtcOffset { get; set; }
		public string Locale { get; set; }
		public string IsAscendant { get; set; }
		public List<Campaign> Campaigns { get; set; }

	}
}