using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SoA.Content.Items.Materials;
using SoA.Content.Buffs;

namespace SoA.Content.Items.Weapons
{
    public class ClawsOfLavaShadow : ModItem
    {
        private int dashCooldown = 120;
        private int dashCooldownCounter = 0;

        public override void SetDefaults() {
            Item.damage = 69;
            Item.DamageType = DamageClass.Melee;
            Item.width = 30;
            Item.scale = 2;
            Item.height = 20;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LavaShard>(), 10);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers) {
            target.AddBuff(ModContent.BuffType<LavaExplosionDebuff>(), 30);
            LavaExplosionGlobalNPC modNPC = target.GetGlobalNPC<LavaExplosionGlobalNPC>();
            modNPC.cumulativeDamage += Item.damage; // Add the weapon's damage to the cumulative damage
        }

        public override void MeleeEffects(Player player, Rectangle hitbox) {
            if (Main.rand.NextBool(2)) {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.SolarFlare, 0, 0, 200);
            }
        }

        public override bool AltFunctionUse(Player player) {
            return true; // Allow right-click functionality
        }

        public override bool? UseItem(Player player) {
            if (player.altFunctionUse == 2 && dashCooldownCounter == 0) {
                DashTowardsCursor(player);
                return true;
            }
            return base.UseItem(player);
        }

        private void DashTowardsCursor(Player player) {
            LavaShadowPlayer modPlayer = player.GetModPlayer<LavaShadowPlayer>();
            modPlayer.StartDash();
            Vector2 dashDirection = Vector2.Normalize(Main.MouseWorld - player.Center);
            player.velocity = dashDirection * 15f; // Adjust dash speed here

            // Apply the buff with a higher counter (x3 multiplier in this case)

            // Start cooldown
            dashCooldownCounter = dashCooldown;
        }

        public override void UpdateInventory(Player player) {
            // Handle dash cooldown
            if (dashCooldownCounter > 0) {
                dashCooldownCounter--;
            }
        }
    }

    public class LavaShadowPlayer : ModPlayer
    {
        public bool isDashing;
        private int dashDuration = 25; 
        private Vector2 dashVelocity;
        private int dashFrameCounter; // Counter to track dash frames

        public override void PostUpdate() {
            if (isDashing) {
                // Continue visual effects during the dash
                dashFrameCounter++;
                if (dashFrameCounter <= dashDuration) {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.IchorTorch, dashVelocity.X * 0.5f, dashVelocity.Y * 0.5f);

                    // Apply invincibility frames
                    Player.immune = true;
                    Player.immuneTime = 15; // Number of frames of invincibility (adjust as needed)
                } else {
                    // Stop dashing and reset counter after duration
                    isDashing = false;
                    dashFrameCounter = 0;
                    
                    // Remove invincibility frames after dash ends
                    Player.immune = false;
                }
            }
        }

        public void StartDash() {
            isDashing = true;
            dashFrameCounter = 0;
            dashVelocity = Main.MouseWorld - Player.Center;
            dashVelocity.Normalize();
            dashVelocity *= 20f; // Adjust speed
            Player.velocity = dashVelocity;

            // Apply initial invincibility frames
            Player.immune = true;
            Player.immuneTime = 15; // Number of frames of invincibility (adjust as needed)
        }
    }

    
}