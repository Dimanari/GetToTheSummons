using GetToTheSummons.Players;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GetToTheSummons.Items.Accessories
{
	public class TamerGauntlet : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("+15% Summon damage\n" +
                "+15% Whip Speed\n" +
                "+20% Whip Range\n" +
                "Increased Minion Knockback\n" +
                "Auto-Swing\n\'With the Blood of my enemies\'");
            DisplayName.SetDefault("Tamer\'s Gauntlet");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {

            player.GetDamage(DamageClass.Summon) *= 1.15f;

            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.15f;

            player.whipRangeMultiplier += 0.20f;

            player.GetKnockback(DamageClass.Summon) += 1f;

            player.autoReuseGlove = true;
        }
        public override void AddRecipes()
        {
            //The recipe
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SummonerEmblem);
            recipe.AddIngredient(ItemID.PowerGlove);
            recipe.AddIngredient(ItemID.Obsidian, 50);
            recipe.AddTile(TileID.LihzahrdFurnace);
            recipe.Register();
        }
    }
}