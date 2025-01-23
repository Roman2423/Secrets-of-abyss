using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SoA.Content.Projectiles;
using SoA.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoA.Content.Items.Weapons
{
    public class InfernoShuriken : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 30;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(gold: 6);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<InfernoShurikenProjectile>();
            Item.shootSpeed = 16f;
            Item.consumable = false;
            Item.maxStack = 1;

            // Указываем стандартное смещение (см. ниже HoldoutOffset)
            Item.scale = 1.5f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<LavaShard>(), 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 20);
            recipe.AddIngredient(ItemID.Bone, 100);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void HoldItem(Player player)
        {
            if (player.itemAnimation > 0)
            {
                // Здесь можно добавлять дополнительные визуальные эффекты
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            // Настраиваем позицию и масштаб
            float customScale = 1.5f;
            Vector2 position = Item.Center - Main.screenPosition;
            Rectangle? sourceRectangle = null;

            spriteBatch.Draw(
                texture,
                position,
                sourceRectangle,
                lightColor, // Цвет освещения
                rotation, // Вращение предмета
                texture.Size() * 0.5f, // Центр для вращения
                scale * customScale, // Увеличенный масштаб
                SpriteEffects.None,
                0f
            );
        }

        public override void PostUpdate()
        {
            // Добавляем освещение вокруг предмета
            Lighting.AddLight(Item.Center, 1f, 0.5f, 0f);
        }
    }
}
