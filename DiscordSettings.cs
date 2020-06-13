using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaidDiscord
{
    public class DiscordSettings
    {
        public string Webhook;

        public static DiscordSettings Create()
        {
            DiscordSettings ss = new DiscordSettings();

            ss.Webhook = "https://discordapp.com/api/webhooks/";
            return ss;
        }
    }
}