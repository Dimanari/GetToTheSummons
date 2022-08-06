using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GetToTheSummons.Buffs;

namespace GetToTheSummons.Items.Weapons
{
    public class SilkyWhip : UltraWhip
    {
        public override void SetStaticDefaults()
        {
            //Items needed to Journey Mode Research
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("3 summon tag damage\nYour summons will focus struck enemies\n\'It's kinky\'");
            DisplayName.SetDefault("Silky Whip");
        }
        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<SilkyWhipProjectile>(), 10, 3f, 3f);

            Item.rare = ItemRarityID.Red;

            Item.channel = false; // example whip's channel is not vanilla+
            Item.autoReuse = false;
        }
        public override void AddRecipes()
        {
            //The recipe
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddIngredient(ItemID.LeadBar, 5);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public class SilkyWhipProjectile : UltraWhipProjectile
        {
            public override void SetStaticDefaults()
            {
                //The Whip Projectile file
                DisplayName.SetDefault("SilkyWhipProjectile");
            }
            public override void SetDefaults()
            {
                Projectile.DefaultToWhip();
                tag = ModContent.BuffType<BasicTagEffect>();
                special_tag = -1;
                fallOff = 0.5f;
            }


        }
    }
}