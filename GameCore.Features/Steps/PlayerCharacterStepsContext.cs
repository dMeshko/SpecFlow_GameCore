using GameCore.Models;

namespace GameCore.Features.Steps;

public class PlayerCharacterStepsContext
{
    public PlayerCharacter PlayerCharacter { get; set; } = new PlayerCharacter(PlayerCharacterRace.Elf);
    public int StartingMagicalPower { get; set; }
}