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
    
    @playerCombat @gameOver
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
        
    @playerHeal
    Scenario: Healers restore all health
        Given My character class is set to Healer
        When I take 40 damage
            And Cast a healing spell
        Then My health should be 100
        
    #multi column step table data - automatically converted to strongly typed data
    @playerStats
    Scenario: Total magical power
        Given I have the following magical items
            | name   | value | power |
            | Ring   | 200   | 100   |
            | Amulet | 400   | 200   |
            | Gloves | 100   | 400   |
        Then My total magical power should be 700
        
    #custom data conversions
    @playerCombat @playerHeal
    Scenario: Reading a restore health scroll when the player is too tired has no effect
        Given I have slept 3 days ago
        When I take 40 damage
            And I read a restore health scroll
        Then My health should be 60
        
    #custom multi column table data conversion
    @playerStats
    Scenario: Weapons are worth money
        Given I have the following weapons
            | name  | value |
            | Sword | 50    |
            | Pick  | 40    |
            | Knife | 10    |
        Then My weapons should be worth 100
        
    Scenario: Elf race characters don't lose magical item power while casting spells
        Given I'm a new player from race Elf
            And I have an Amulet with a power of 200
        When I use my magical Amulet
        Then The Amulet power should not be reduced