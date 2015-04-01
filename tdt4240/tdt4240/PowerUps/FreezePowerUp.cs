
namespace tdt4240.PowerUps
{
    class FreezePowerUp : PowerUp
    {

        public FreezePowerUp()
        {
            Target = Target.Enemy;
            effect += FreezeEffect;
            Title = "Freeze";
            Description = "Prevents the targeted player from using his turn";
        }

        void FreezeEffect(object sender, PowerUpEvent powerUpEvent)
        {
            powerUpEvent.Player.Effect = Effect.Freeze;
        }
    }
}
