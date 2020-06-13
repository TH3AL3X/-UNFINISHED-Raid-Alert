using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Framework.Utilities;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace RaidDiscord
{
    public class PlayerComponent : UnturnedPlayerComponent
    {
        public static string health;

        public static string PlayerInfo(CSteamID id)
        {
            string lama = null;
            if (Main.IsDependencyLoaded("PlayerInfoLib"))
            {
                PlayerInfoLibrary.PlayerData data = PlayerInfoLibrary.PlayerInfoLib.Database.QueryById(id, true);
                lama = data.CharacterName;
            }
            return lama;
        }

        public static Transform Raycast(UnturnedPlayer player)
        {
            RaycastHit hit;
            if (Physics.Raycast(player.Player.look.aim.position, player.Player.look.aim.forward, out hit, 2000, RayMasks.BARRICADE_INTERACT))
            {
                Transform transform = hit.transform;


                return transform;
            }
            return null;
        }

        public void letmeclearcode(UnturnedPlayer player, ushort pendingTotalDamage)
        {
            while (true) // don't kill me please i was testing something
            {
                PlayerLook look = player.Player.look;
                Transform raycast = Raycast(player);

                Interactable2 component;

                component = raycast.GetComponent<Interactable2>();

                var updatedStructures = new List<StructureData>();

                CSteamID val2 = (CSteamID)component.owner;

                ushort id;
                string itemString = component.name.ToString();


                if (!ushort.TryParse(itemString, out id))
                {
                    List<ItemAsset> sortedAssets = new List<ItemAsset>(Assets.find(EAssetType.ITEM).Cast<ItemAsset>());
                    ItemAsset asset = sortedAssets.Where(i => i.itemName != null).OrderBy(i => i.itemName.Length).Where(i => i.itemName.ToLower().Contains(itemString.ToLower())).FirstOrDefault();
                    if (asset != null) id = asset.id;
                    if (String.IsNullOrEmpty(itemString.Trim()) || id == 0)
                    {
                        return;
                    }
                }
                Asset item = Assets.find(EAssetType.ITEM, id);
                string itemName = ((ItemAsset)item).itemName;
                // Testing... UnturnedChat.Say($"PLAYER {player} | OWNER {val2} | ITEM {itemName} | DAMAGE {pendingTotalDamage} | HEALTH {componentwo.hp}");

                health = raycast.GetComponent<Interactable2HP>().hp.ToString();
                StartCoroutine(Webhook(player, val2, itemName, pendingTotalDamage, health));
                break;
            }
        }

        public IEnumerator Webhook(UnturnedPlayer player, CSteamID ownerid, string structurename, int pendingTotalDamage, string hp)
        {
            // Uff ja, lets gonna wait some seconds if we don't want rape CPU
            yield return new WaitForSeconds(1f);
            string lama = null;
            UnturnedPlayer owner = UnturnedPlayer.FromCSteamID(ownerid);

            bool isOnline = owner.Player != null ? owner.Player.channel != null ? true : false : false;

            if (!isOnline)
                lama = PlayerInfo(ownerid);

            if (isOnline)
            {
                foreach (DiscordSettings Discord in Main.Instance.Configuration.Instance.Discord)
                    DiscordClass.SendSingle(DiscordClass.NiggaCanUAlertMyAss(player, owner.CharacterName, structurename, pendingTotalDamage, hp), Discord);
            }
            else if (lama != null)
            {
                foreach (DiscordSettings Discord in Main.Instance.Configuration.Instance.Discord)
                    DiscordClass.SendSingle(DiscordClass.NiggaCanUAlertMyAss(player, owner.CharacterName, structurename, pendingTotalDamage, hp), Discord);
            }
        }
    }
}
