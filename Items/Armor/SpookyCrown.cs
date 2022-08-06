using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using GetToTheSummons.Players;

namespace GetToTheSummons.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class SpookyCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spooky Crown");
			Tooltip.SetDefault("Increases your max number of minions by 1.\nIncreases your max number of sentries by 1.\n\'Fit for a King!\'");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;

			// If your head equipment should draw hair while drawn, use one of the following:
			// ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
			// ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
			// ArmorIDs.Head.Sets.DrawBackHair[Item.headSlot] = true;
			// ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true; 
		}

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Yellow; // The rarity of the item
			Item.defense = 12; // The amount of defense the item will give when equipped
		}
		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ItemID.SpookyBreastplate && legs.type == ItemID.SpookyLeggings;
		}
		public override void UpdateArmorSet(Player player) {
			player.setBonus = "Increases your max number of sentries by 2.\nYour fighting minions Rejuvenate you.";
			player.GetModPlayer<GPlayer>().minionlifesteal += 2;
			player.maxTurrets += 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.maxMinions += 1;
			player.maxTurrets += 1;
		}
		public override void AddRecipes() {
			Recipe recipe2 = CreateRecipe();
			recipe2.AddIngredient(ItemID.SpookyWood, 300);
			recipe2.AddTile(TileID.WorkBenches);
			recipe2.Register();
		}
	}
}
