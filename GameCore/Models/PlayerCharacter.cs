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
}

public enum PlayerCharacterRace
{
    Elf,
    Troll,
    Human
}