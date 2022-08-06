using GetToTheSummons.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GetToTheSummons
{
	public class GetToTheSummons : Mod
	{
        public override void Load()
        {

		}
        public override void Unload()
		{
			// All code below runs only if we're not loading on a server
			if (!Main.dedServ)
			{
				
			}

			// Unload static references

		}
	}
}