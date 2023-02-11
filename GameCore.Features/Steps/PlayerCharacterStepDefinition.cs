using GameCore.Features.Attributes;
using GameCore.Models;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace GameCore.Features.Steps;

[Binding]
public class PlayerCharacterStepDefinition
{
    private PlayerCharacter _playerCharacter;
    private readonly PlayerCharacterStepsContext _context;

    public PlayerCharacterStepDefinition(PlayerCharacterStepsContext context)
    {
        _context = context;
        _playerCharacter = _context.PlayerCharacter;
    }
    
    [Given(@"I'm a new player from race (.*)")]
    public void GivenImANewPlayerFromRace(PlayerCharacterRace race)
    {
        _playerCharacter = new PlayerCharacter(race);
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
        // var armor = table.Rows.FirstOrDefault(x => x["attribute"] == "Armor").Value();
        var attributes = table.CreateInstance<PlayerAttributes>();
        _playerCharacter.Armor += attributes.Armor;
    }

    [Given(@"My character class is set to (.*)")]
    public void GivenMyCharacterClassIsSetTo(CharacterClass characterClass)
    {
        _playerCharacter.CharacterClass = characterClass;
    }

    [When(@"Cast a healing spell")]
    public void WhenCastAHealingSpell()
    {
        _playerCharacter.CastHealingSpell();
    }

    [Given(@"I have the following magical items")]
    public void GivenIHaveTheFollowingMagicalItems(Table table)
    {
        // foreach (var tableRow in table.Rows)
        // {
        //     var name = tableRow["item"];
        //     var value = tableRow["value"];
        //     var power = tableRow["power"];
        //     
        //     _playerCharacter.MagicalItems.Add(new MagicalItem
        //     {
        //         Name = name,
        //         Value = int.Parse(value),
        //         Power = int.Parse(power)
        //     });
        // }

        var items = table.CreateSet<MagicalItem>();
        _playerCharacter.MagicalItems.AddRange(items);
    }

    [Then(@"My total magical power should be (.*)")]
    public void ThenMyTotalMagicalPowerShouldBe(int expectedMagicalPower)
    {
        Assert.Equal(expectedMagicalPower, _playerCharacter.MagicalPower);
    }

    [Given(@"I have slept (.* days ago)")]
    public void GivenIHaveSleptDaysAgo(DateTime lastSlept)
    {
        _playerCharacter = new PlayerCharacter(PlayerCharacterRace.Troll);
        _playerCharacter.LastSleepTime = lastSlept;
    }

    [When(@"I read a restore health scroll")]
    public void WhenIReadARestoreHealthScroll()
    {
        _playerCharacter.ReadHealthScroll();
    }

    [Given(@"I have the following weapons")]
    public void GivenIHaveTheFollowingWeapons(IEnumerable<Weapon> weapons)
    {
        _playerCharacter.Weapons.AddRange(weapons);
    }

    [Then(@"My weapons should be worth (.*)")]
    public void ThenMyWeaponsShouldBeWorth(int weaponsValue)
    {
        Assert.Equal(weaponsValue, _playerCharacter.WeaponsValue);
    }

    [Given(@"I have an (.*) with a power of (.*)")]
    public void GivenIHaveAnAmuletWithAPowerOf(string magicalItemName, int power)
    {
        _playerCharacter.MagicalItems.Add(new MagicalItem
        {
            Name = magicalItemName,
            Power = power,
            Value = 100
        });

        _context.StartingMagicalPower = power;
    }

    [When(@"I use my magical (.*)")]
    public void WhenIUseMyMagicalAmulet(string magicalItemName)
    {
        _playerCharacter.UseMagicalItem(magicalItemName);
    }

    [Then(@"The (.*) power should not be reduced")]
    public void ThenTheAmuletPowerShouldNotBeReduced(string magicalItemName)
    {
        var currentMagicalItemPower =
            _playerCharacter.MagicalItems.FirstOrDefault(x => x.Name == magicalItemName).Power;
        Assert.Equal(_context.StartingMagicalPower, currentMagicalItemPower);
    }
}