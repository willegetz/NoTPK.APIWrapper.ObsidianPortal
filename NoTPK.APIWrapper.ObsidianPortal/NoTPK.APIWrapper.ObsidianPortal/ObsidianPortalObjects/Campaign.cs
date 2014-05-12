using System.Collections.Generic;
using System.Linq;
using NoTPK.APIWrapper.ObsidianPortal.Helpers;

namespace NoTPK.APIWrapper.ObsidianPortal.ObsidianPortalObjects
{
	public class Campaign
	{
		public Campaign(Dictionary<string, object> campaign)
		{
			CampaignId = campaign.Where(ui => ui.Key == "id").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			CampaignSlug = campaign.Where(ui => ui.Key == "slug").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			CampaignName = campaign.Where(ui => ui.Key == "name").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			CampaignUrl = campaign.Where(ui => ui.Key == "campaign_url").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			BannerImageUrl = campaign.Where(ui => ui.Key == "banner_image_url").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			Visibility = campaign.Where(ui => ui.Key == "visibility").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			var roleRaw = campaign.Where(ui => ui.Key == "role").Select(ui => HelperMethods.ValidateEntry(ui.Value)).FirstOrDefault();
			if (roleRaw == "game_master")
			{
				UserRole = "Game Master";
			}else if (roleRaw == "player")
			{
				UserRole = "Player";
			}
		}

		public string CampaignId { get; set; }
		public string CampaignSlug { get; set; }
		public string CampaignName { get; set; }
		public string CampaignUrl { get; set; }
		public string BannerImageUrl { get; set; }
		public string Visibility { get; set; }
		public string UserRole { get; set; }

	}
}