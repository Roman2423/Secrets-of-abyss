using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SoA.Content.Projectiles;

namespace SoA.Content.Tiles.Nature
{
    public class Tidesand_tile : ModTile
    {
        public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileBrick[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
            TileID.Sets.Falling[Type] = true;
            Main.tileSand[Type] = true;
			TileID.Sets.Conversion.Sand[Type] = true; // Allows Clentaminator solutions to convert this tile to their respective Sand tiles.
			TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true; // Allows Sandshark enemies to "swim" in this sand.
			TileID.Sets.CanBeDugByShovel[Type] = true;
			TileID.Sets.Falling[Type] = true;
			TileID.Sets.Suffocate[Type] = true;
            TileID.Sets.FallingBlockProjectile[Type] = new TileID.Sets.FallingBlockProjectileInfo(ModContent.ProjectileType<Tidesandball_Projectile>(), 10);
            TileID.Sets.GeneralPlacementTiles[Type] = false;
			TileID.Sets.ChecksForMerge[Type] = true;
            MinPick = 30;
            DustType = DustID.BubbleBlock;
            MineResist = 0.5f;
		}
        public override void NumDust(int i, int j, bool fail, ref int num) {
            num = fail ? 1 : 3; // Количество пыли при разрушении блока
        }

        public override void ChangeWaterfallStyle(ref int style) {
            style = 2; // Стиль водопада, если блок находится под водой
        }
    }
}