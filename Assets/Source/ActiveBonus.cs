namespace Assets.Source
{
    /// <summary>
    /// Bonuses picked up by player
    /// </summary>
    public class ActiveBonus
    {
        public BonusType BonusType;

        public float MaxDuration;
        public float Duration;

        public ActiveBonus(BonusType bonusType, float maxDuration)
        {
            BonusType = bonusType;
            MaxDuration = maxDuration;
            Duration = MaxDuration;
        }
    }
}
