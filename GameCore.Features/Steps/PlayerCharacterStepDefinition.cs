using GameCore.Models;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace GameCore.Features.Steps;

[Binding]
public class PlayerCharacterStepDefinition
{
    private PlayerCharacter _playerCharacter;
    
    [Given(@"I'm a new player from race (.*)")]
    public void GivenImANewPlayerFromRace(string race)
    {
        var playerRace = (PlayerCharacterRace) Enum.Parse(typeof(PlayerCharacterRace), race);
        _playerCharacter = new PlayerCharacter(playerRace);
    }
    
    [When(@"I take (.*) damage")]
    public void WhenITakeDamage(int damage)
    {
        _playerCharacter.Hit(damage);
    }
    
    [Then(@"My health should be (.*)")]
    public void ThenMyHealthShouldBe(int expectedHealth)
    {
        Assert.Equal(expectedHealth, _playerCharacter.Health);
    }

    [Then(@"I should be dead")]
    public void ThenIShouldBeDead()
    {
        Assert.Equal(0, _playerCharacter.Health);
        Assert.True(_playerCharacter.IsDead);
    }

    [Given(@"I have the following attributes")]
    public void GivenIHaveTheFollowingAttributes(Table table)
    {
        var armor = table.Rows.FirstOrDefault(x => x["attribute"] == "Armor").Value();
        _playerCharacter.Armor += int.Parse(armor);
    }
}