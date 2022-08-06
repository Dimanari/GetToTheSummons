using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using GetToTheSummons.Buffs;
using Terraria.GameContent.Achievements;

namespace GetToTheSummons.Items.Weapons
{
    public class UltraWhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("30 summon tag damage\nMinion Strike will echo significant damage\nYour summons will focus struck enemies\n\'Ultimate destruction\'");
            DisplayName.SetDefault("Ultra Whip");
        }

        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DefaultToWhip(ModContent.ProjectileType<UltraWhipProjectile>(), 190, 4f, 13f);

            Item.rare = ItemRarityID.Red;

            Item.channel = false; // example whip's channel is not vanilla+
        }
        public override bool? CanAutoReuseItem(Player player)
        {
            return player.autoReuseGlove || Item.autoReuse;
        }
        
        public override void AddRecipes()
        {
            //The recipe
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class UltraWhipProjectile : ModProjectile
        {
            //The color of the line in the middle of the whip
            protected Color originalColor = Color.White;
            protected int tag;
            protected int special_tag;
            protected float fallOff;

            public override void SetStaticDefaults()
            {
                //The Whip Projectile file
                DisplayName.SetDefault("UltraWhipProjectile");
                ProjectileID.Sets.IsAWhip[Type] = true;

            }

            public override void SetDefaults()
            {
                // This method quickly sets the whip's properties.
                Projectile.DefaultToWhip();

                Projectile.WhipSettings.Segments = 50;
                tag = ModContent.BuffType<StrongTagEffect>();
                special_tag = ModContent.BuffType<CustomTagEffect>();
                fallOff = 0.3f;
            }

            private float Timer
            {
                get => Projectile.ai[0];
                set => Projectile.ai[0] = value;
            }

            private float ChargeTime
            {
                get => Projectile.ai[1];
                set => Projectile.ai[1] = value;
            }

            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
            {
                Projectile.damage = (int)(Projectile.damage * (1f - fallOff));
                if (tag != -1)
                {
                    target.AddBuff(tag, 300);
                }
                if (special_tag != -1)
                {
                    target.AddBuff(special_tag, 300);
                    special_tag = -1;
                }
                Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
            }

            public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
            {
                List<Vector2> list = new List<Vector2>();
                Projectile.FillWhipControlPoints(Projectile, list);
                for (int n = 0; n < list.Count; n++)
                {
                    Point point = list[n].ToPoint();
                    projHitbox.Location = new Point(point.X - projHitbox.Width / 2, point.Y - projHitbox.Height / 2);
                    if (projHitbox.Intersects(targetHitbox))
                    {
                        return true;
                    }
                }
                return false;
                //return base.Colliding(projHitbox, targetHitbox);
            }

            public override void CutTiles()
            {
                AchievementsHelper.CurrentlyMining = false;
                List<Vector2> list = new List<Vector2>();
                Projectile.FillWhipControlPoints(Projectile, list);
                Vector2 vector = new Vector2((float)Projectile.width * Projectile.scale / 2f, 0f);
                for (int i = 0; i < list.Count; i++)
                {
                    DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
                    Utils.PlotTileLine(list[i] - vector, list[i] + vector, (float)Projectile.width * Projectile.scale, DelegateMethods.CutTiles);
                }
            }

            // This method draws a line between all points of the whip, in case there's empty space between the sprites.
            private void DrawLine(List<Vector2> list)
			{
				Texture2D texture = TextureAssets.FishingLine.Value;
				Rectangle frame = texture.Frame();
				Vector2 origin = new Vector2(frame.Width / 2, 2);

				Vector2 pos = list[0];
				for (int i = 0; i < list.Count - 1; i++)
				{
					Vector2 element = list[i];
					Vector2 diff = list[i + 1] - element;

					float rotation = diff.ToRotation() - MathHelper.PiOver2;
					Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.White);
					Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

					Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

					pos += diff;
				}
			}

			public override bool PreDraw(ref Color lightColor)
			{
				List<Vector2> list = new List<Vector2>();
				Projectile.FillWhipControlPoints(Projectile, list);

				//DrawLine(list);

				Main.DrawWhip_WhipBland(Projectile, list);
                return false;
            }
        }
	}
}