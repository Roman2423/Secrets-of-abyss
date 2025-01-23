using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoA.Content.Items.Weapons
{
    public class ShardedSpear : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.DamageType = DamageClass.Melee;
            Item.width = 30;
            Item.height = 30;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(gold: 20);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>("SoA/Content/Projectiles/ShardedSpearProjectile").Value;

            // Настраиваем позицию и масштаб
            float customScale = 1.5f;
            Vector2 position = Item.Center - Main.screenPosition;
            Rectangle? sourceRectangle = null;

            spriteBatch.Draw(
                texture,
                position,
                sourceRectangle,
                lightColor,
                rotation,
                texture.Size() * 0.5f,
                scale * customScale,
                SpriteEffects.None,
                0f
            );
        }
    }
}