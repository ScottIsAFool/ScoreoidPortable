namespace ScoreoidPortable.Entities
{
    public enum SortBy
    {
        [Description("date")]
        Date,
        [Description("score")]
        Score,
        [Description("date,score")]
        DateThenScore,
        [Description("score,date")]
        ScoreThenDate
    }
}