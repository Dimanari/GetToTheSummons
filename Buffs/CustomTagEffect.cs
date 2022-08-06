using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using GetToTheSummons.NPCs;
using GetToTheSummons.Players;

namespace GetToTheSummons.Buffs
{
    public class BasicTagEffect : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tag Damage");
            Description.SetDefault("Taking increased damage from minions.");
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GNPC>().summonTag = 3;
        }
    }

    public class StrongTagEffect : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tag Damage");
            Description.SetDefault("Taking increased damage from minions.");
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GNPC>().summonTag = 30;
        }
    }

    public class CustomTagEffect : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hurt Me More");
            Description.SetDefault("Taking increased damage from minions.");
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
        }
    }

    public class RegenCoolDown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Minion Regen Cooldown");
            Description.SetDefault("was Recently healed by minions.");
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
        }
    }

    public class Explosion : GlobalProjectile
	{
		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			bool summon = (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type] || ProjectileID.Sets.SentryShot[projectile.type] || projectile.sentry);
			if (summon && target.HasBuff(ModContent.BuffType<CustomTagEffect>()))
			{
                target.StrikeNPC((int)(damage * 4.5f), knockback, 0, false);
                var index = target.FindBuffIndex(ModContent.BuffType<CustomTagEffect>());
                target.DelBuff(index);
            }
            if(projectile.minion && Main.player[projectile.owner].lifeSteal > 0)
            {  
                Player p = Main.player[projectile.owner];
                
                if (!p.HasBuff<RegenCoolDown>() && p.GetModPlayer<GPlayer>().minionlifesteal > 0)
                {
                    p.HealEffect(p.GetModPlayer<GPlayer>().minionlifesteal);
                    p.statLife += p.GetModPlayer<GPlayer>().minionlifesteal;
                    p.AddBuff(ModContent.BuffType<RegenCoolDown>(), 60);
                }
            }
        }
	}
}