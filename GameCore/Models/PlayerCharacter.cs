using System.Security.Cryptography;

namespace GameCore.Models;

public class PlayerCharacter
{
    public PlayerCharacter(PlayerCharacterRace race)
    {
        Health = 100;
        Race = race;
        IsDead = false;

        Armor = Race == PlayerCharacterRace.Elf ? 20 : 0;
    }

    public int Health { get; private set; } = 100;
    public int Armor { get; set; }
    public PlayerCharacterRace Race { get; private set; }
    public bool IsDead { get; private set; }

    public List<MagicalItem> MagicalItems { get; set; } = new();
    public int MagicalPower => MagicalItems.Sum(x => x.Power);

    public List<Weapon> Weapons { get; set; } = new();
    public int WeaponsValue => Weapons.Sum(x => x.Value);
    
    public CharacterClass CharacterClass { get; set; }

    public DateTime LastSleepTime { get; set; }
    
    public void Hit(int damage)
    {
        var damageToBeInflicted = damage - Armor;
        Health -= damageToBeInflicted > 0 ? damageToBeInflicted : 0;

        if (Health <= 0)
        {
            Health = 0;
            IsDead = true;
        }
    }

    public void CastHealingSpell()
    {
        if (CharacterClass == CharacterClass.Healer)
        {
            Health = 100;
        }
        else
        {
            Health += 10;
        }
    }

    public void ReadHealthScroll()
    {
        var daysSinceLastSlept = DateTime.Now.Subtract(LastSleepTime).Days;
        if (daysSinceLastSlept <= 2)
        {
            Health = 100;
        }
    }

    public void UseMagicalItem(string itemName)
    {
        int powerReduction = 10;

        if (Race == PlayerCharacterRace.Elf)
        {
            powerReduction = 0;
        }

        var itemToReduce = MagicalItems.FirstOrDefault(x => x.Name == itemName);
        itemToReduce.Power -= powerReduction;
    }
}

public enum PlayerCharacterRace
{
    Elf,
    Troll,
    Human
}

public enum CharacterClass
{
    None,
    Healer,
    Warrior
}