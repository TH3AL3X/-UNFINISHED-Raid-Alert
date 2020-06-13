using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RaidDiscord
{
    public class RaidDiscordConfig : IRocketPluginConfiguration
    {
        [XmlArrayItem(ElementName = "Discord")]
        public List<DiscordSettings> Discord;

        public void LoadDefaults()
        {
            Discord = new List<DiscordSettings>()
            {
                DiscordSettings.Create()
            };
        }
    }
}