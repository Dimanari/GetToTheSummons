using Terraria;
using Terraria.ModLoader;


namespace GetToTheSummons.Buffs
{
    public class Frozen : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen");
            Description.SetDefault("Holy Crap I'm Dead");
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.controlUp = false;
            player.controlDown = false;
            player.controlLeft = false;
            player.controlRight = false;
            player.controlJump = false;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity.X = npc.velocity.X /2f;
            npc.velocity.Y = npc.velocity.Y /2f;
        }
    }
}