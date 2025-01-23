using SoA.Content.Items.Placebles;
using SoA.Content.Tiles.Nature;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoA.Content.Projectiles
{
    public abstract class Tidesandball_Projectile : ModProjectile
    {
		public override string Texture => "SoA/Content/Projectiles/Tidesandball_Projectile";

		public override void SetStaticDefaults() {
			ProjectileID.Sets.FallingBlockDoesNotFallThroughPlatforms[Type] = true;
			ProjectileID.Sets.ForcePlateDetection[Type] = true;
		}
	}

	public class Tidesandball_fallingProjectile : Tidesandball_Projectile
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<Tidesand_tile>(), ModContent.ItemType<Tidesand>());
		}

		public override void SetDefaults() {
			// The falling projectile when compared to the sandgun projectile is hostile.
			Projectile.CloneDefaults(ProjectileID.EbonsandBallFalling);
		}
	}

	public class ExampleSandBallGunProjectile : Tidesandball_Projectile
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<Tidesand_tile>());
		}

		public override void SetDefaults() {
			// The sandgun projectile when compared to the falling projectile has a ranged damage type, isn't hostile, and has extraupdates = 1.
			// Note that EbonsandBallGun has infinite penetration, unlike SandBallGun
			Projectile.CloneDefaults(ProjectileID.EbonsandBallGun);
			AIType = ProjectileID.EbonsandBallGun; // This is needed for some logic in the ProjAIStyleID.FallingTile code.
		}
	}
}