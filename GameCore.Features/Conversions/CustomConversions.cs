using GameCore.Models;
using TechTalk.SpecFlow.Assist;

namespace GameCore.Features.Conversions;

[Binding]
public class CustomConversions
{
    [StepArgumentTransformation(@"(\d+) days ago")]
    public DateTime DaysAgoTransformation(int daysAgo)
    {
        // return DateTime.UtcNow.AddDays(-daysAgo);
        return DateTime.Now.Subtract(TimeSpan.FromDays(daysAgo));
    }

    [StepArgumentTransformation]
    public IEnumerable<Weapon> WeaponsTransformation(Table table)
    {
        return table.CreateSet<Weapon>();
    }
}