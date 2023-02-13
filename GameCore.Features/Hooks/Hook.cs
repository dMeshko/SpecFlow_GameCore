using System;
using GameCore.Features.Steps;
using GameCore.Models;
using TechTalk.SpecFlow;

namespace GameCore.Features.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly PlayerCharacterStepsContext _context;

        public Hooks(PlayerCharacterStepsContext context)
        {
            _context = context;
        }

        [BeforeScenario]
        public void InitPlayerContext()
        {
            _context.PlayerCharacter = new PlayerCharacter(PlayerCharacterRace.Elf);
        }
        
        [BeforeScenario("playerCombat", Order = 1)]
        public void BeforePlayerCombarScenario()
        {
            
        }

        [BeforeScenario("playerStats", Order = 2)] // the order is just for showcase, applicable only if there were more BeforeScenarios for the same tag/general tag
        public void BeforePlayerStatsScenario()
        {
            
        }

        [AfterScenario]
        public void AfterScenario()
        {
            
        }
    }
}