using Terraria.ModLoader;

namespace GetToTheSummons.Players
{
    public class GPlayer : ModPlayer
    {
        public int minionlifesteal;
        public bool watcher_minion;
        public override void UpdateEquips()
        {
            minionlifesteal = 0;
        }

        public override void Initialize()
        {
            minionlifesteal = 0;
            watcher_minion = false;
        }
    }
}