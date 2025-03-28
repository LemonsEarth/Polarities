﻿using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Polarities.Content.Items.Consumables.Summons.Hardmode
{
    public class BetsySummonItem : ModItem
    {
        public override void Load()
        {
            Terraria.GameContent.Events.On_DD2Event.DropMedals += DD2Event_DropMedals;
        }

        private void DD2Event_DropMedals(Terraria.GameContent.Events.On_DD2Event.orig_DropMedals orig, int numberOfMedals)
        {
            orig(numberOfMedals);

            if (numberOfMedals == 25)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type == NPCID.DD2EterniaCrystal)
                    {
                        Main.npc[i].DropItemInstanced(Main.npc[i].position, Main.npc[i].Size, ItemType<BetsySummonItem>(), 1, interactionRequired: false);
                    }
                }
            }
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = (1);

            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            return !(DD2Event.Ongoing || NPC.AnyNPCs(NPCID.DD2Betsy));
        }

        public override bool? UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2Betsy);
            Main.NewText(Language.GetTextValue("Announcement.HasAwoken", Main.npc[NPC.FindFirstNPC(NPCID.DD2Betsy)].TypeName), 171, 64, 255);
            SoundEngine.PlaySound(SoundID.Roar, player.Center);
            return true;
        }
    }
}