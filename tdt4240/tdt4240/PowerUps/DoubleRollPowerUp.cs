namespace tdt4240.PowerUps
{
    class DoubleRollPowerUp : PowerUp
    {
        public DoubleRollPowerUp()
        {
            Target = Target.Self;
            
            effect += DoubleRollEffect;
            Title = "Double Roll";
            Description = "Gives the player an extra roll";
            IconPath = "powerups/double_dice";
        }

        void DoubleRollEffect(object sender, PowerUpEvent powerUpEvent)
        {
            powerUpEvent.Player.Effect = Effect.DoubleRoll;
        }
    }
}
