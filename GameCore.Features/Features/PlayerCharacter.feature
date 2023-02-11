Feature: PlayerCharacter
In order to play the game
As a player from a certain race
I want my character attributes to be correctly represented

#parametrized shared step definitions

    @playerCombat
    Scenario: Taking no damage when hit doesn't affect health
        Given I'm a new player from race Human
        When I take 0 damage
        Then My health should be 100

    @playerCombat
    Scenario: Starting health is reduced when hit (damage mitigation of 20 is applied for Elf race)
        Given I'm a new player from race Elf
        When I take 40 damage
        Then My health should be 80
    @playerCombat
    Scenario: Starting health is reduced without damage mitigation when hit for non-elf classes
        Given I'm a new player from race Human
        When I take 40 damage
        Then My health should be 60
    
    @playerCombat
    Scenario: Taking too much damage results in player death
        Given I'm a new player from race Elf
        When I take 130 damage
        Then I should be dead

    #parametrized tests

    @playerCombat
    Scenario Outline: Damage mitigation and effect
        Given I'm a new player from race <race>
        When I take <damage> damage
        Then My health should be <remainingHealth>

        Examples:
          | race  | damage | remainingHealth |
          | Elf   | 35     | 85              |
          | Human | 50     | 50              |
          | Troll | 30     | 70              |
          | Elf   | 110    | 10              |
          | Human | 110    | 0               |

    #data table

    @playerCombat
    Scenario: Elf race characters get additional 20 damage mitigation using data table
        Given I'm a new player from race Elf
        And I have the following attributes
          | attribute | value |
          | Armor     | 30    |
        When I take 40 damage
        Then My health should be 100

    #scenario background - step executed before each scenario (as a prep?)

#    Background:
#        Given I'm a new player from race Elf