
namespace tdt4240.PowerUps
{
    class FreezePowerUp : PowerUp
    {

        public FreezePowerUp()
        {
            this.Target = Target.Enemy;
            this.effect += FreezeEffect;
            this.Title = "Freeze";
            this.Description = "Prevents the targeted player from using his turn";
        }

        void FreezeEffect(object sender, PowerUpEvent powerUpEvent)
        {
            powerUpEvent.Player.Effect = Effect.Freeze;
        }
    }
}
