using ScoreoidPortable.Attributes;

namespace ScoreoidPortable.Entities
{
    public enum GameField
    {
        [Description("bonus")]
        Bonus,
        [Description("gold")]
        Gold,
        [Description("money")]
        Money,
        [Description("kills")]
        Kills,
        [Description("lives")]
        Lives,
        [Description("time_played")]
        TimePlayed,
        [Description("unlocked_levels")]
        UnlockedLevels
    }
}