using Rocket.Unturned;
using Rocket.Core.Plugins;
using SDG.Unturned;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using Steamworks;
using System;
using System.Linq;
using Rocket.Core;
using Rocket.Unturned.Chat;
using System.Timers;
using System.Net;
using System.Collections;
using UnityEngine;
using Rocket.API;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Globalization;
using SDG.Framework.Utilities;

namespace RaidDiscord
{
    public class Main : RocketPlugin<RaidDiscordConfig>
    {
        public static Main Instance;

        protected override void Load()
        {
            Instance = this;

            BarricadeManager.onDamageBarricadeRequested += onDamageStructureRequested;
            StructureManager.onDamageStructureRequested += onDamageStructureRequested;
        }


        private void onDamageStructureRequested(CSteamID instigatorSteamID, Transform structureTransform, ref ushort pendingTotalDamage, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            UnturnedPlayer player = UnturnedPlayer.FromCSteamID(instigatorSteamID);
            UnturnedChat.Say($" DAMAGE {pendingTotalDamage}");
            player.GetComponent<PlayerComponent>().letmeclearcode(player, pendingTotalDamage);
        }

        protected override void Unload()
        {
            BarricadeManager.onDamageBarricadeRequested -= onDamageStructureRequested;
            StructureManager.onDamageStructureRequested -= onDamageStructureRequested;
        }
    }
}
