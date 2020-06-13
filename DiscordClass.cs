using Newtonsoft.Json.Linq;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RaidDiscord
{
    public class DiscordClass
    {
        public static JObject NiggaCanUAlertMyAss(UnturnedPlayer player, string otherPlayer, string structurename, int pendingTotalDamage, string hp)
        {
            if (player == null)
                return null;

            JObject obj = new JObject();
            JArray arrEmbeds = new JArray();
            JObject objEmbed = new JObject();
            JObject objAuthor = new JObject();

            var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

            objAuthor.Add("name", player.CharacterName);
            objAuthor.Add("url", "http://steamcommunity.com/profiles/" + player.CSteamID.ToString());
            objAuthor.Add("icon_url", player.SteamProfile.AvatarFull.AbsoluteUri);
            objEmbed.Add("title", structurename);
            objEmbed.Add("description", "**Owner Structure: **" + otherPlayer + "@here" + System.Environment.NewLine + "**Total Damage: **" + pendingTotalDamage + System.Environment.NewLine + "**Total HP: **" + hp + "/100" + System.Environment.NewLine + "**Date: **" + DateTime.Now);
            objEmbed.Add("color", int.Parse("FF0000", NumberStyles.HexNumber));
            objEmbed.Add("author", objAuthor);
            arrEmbeds.Add(objEmbed);

            obj.Add("username", "Raid Alert");
            obj.Add("tts", false);
            obj.Add("embeds", arrEmbeds);

            return obj;
        }

        public static bool SendSingle(JObject WebHook, DiscordSettings Discord)
        {
            if (WebHook == null)
                return false;

            try
            {
                using (WebClient weebClient = new WebClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (o, certificate, chain, errors) => true;
                    weebClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    weebClient.UploadStringAsync(new Uri(Discord.Webhook), WebHook.ToString(Newtonsoft.Json.Formatting.None));
                }

                return true;
            }
            catch (Exception) { return false; }
        }

    }
}
