using ScoreoidPortable.Attributes;

namespace ScoreoidPortable.Entities
{
    public enum OrderBy
    {
        [Description("asc")]
        Ascending,
        [Description("desc")]
        Descending,
        [Description("asc,asc")]
        AscendingAscending,
        [Description("asc,desc")]
        AscendingDescending,
        [Description("desc,desc")]
        DescendingDescending,
        [Description("desc,asc")]
        DescendingAscending
    }
}