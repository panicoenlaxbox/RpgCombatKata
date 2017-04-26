using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace RpgCombatKata
{
    public class CharacterShould
    {
        private static Character CreateCharacter(int health)
        {
            return new Character(health);
        }
        private static Character CreateCharacter()
        {
            return new Character();
        }

        [Fact]
        public void StartWithExpectedHealth()
        {
            var character = CreateCharacter();
            // Si usamos aquí Character.InitialHealth y mañana cambia a 999, nadie probará esto
            character.Health.Should().Be(Character.InitialHealth); 
        }

        [Fact]
        public void StartWithExpectedLevel()
        {
            var character = CreateCharacter();
            character.Level.Should().Be(1);
        }

        [Fact]
        public void ReceiveDamageFromAnotherCharacter()
        {
            var character = CreateCharacter();
            character.ReceiveDamage(1);
            character.Health.Should().Be(999);
        }

        [Fact]
        public void DealDamageToAnotherCharacter()
        {
            var attacker = CreateCharacter();
            var defender = CreateCharacter();

            attacker.DealDamage(defender, 1);

            defender.Health.Should().Be(999);
        }

        [Fact]
        public void HealHimSelf()
        {
            var character = CreateCharacter(health:999);

            character.Heal(1);

            character.Health.Should().Be(Character.InitialHealth);
        }

        [Fact]
        public void NotHealAboveMaximumHealth()
        {
            var character = CreateCharacter();
            character.Heal(1);
            character.Health.Should().Be(Character.InitialHealth);
        }

        [Fact]
        public void DieWhenLooseHimLife()
        {
            var character = CreateCharacter();
            character.ReceiveDamage(Character.InitialHealth);
            character.IsDead.Should().Be(true);
        }
    }

    public class Character
    {
        public const int InitialHealth = 1100;
        public int Health { get; private set; }
        public int Level { get; }
        public object IsDead { get; private set; }

        public Character(int initialHealth = InitialHealth)
        {
            Health = InitialHealth;
            Level = 1;
        }

        public void ReceiveDamage(int damage)
        {
            Health -= damage;
            if (Health == 0)
            {
                IsDead = true;
            }
        }

        public void DealDamage(Character defender, int damage)
        {
            defender.ReceiveDamage(damage);
        }

        public void Heal(int healthPoints)
        {
            Health += healthPoints;
            if (Health > InitialHealth)
            {
                Health = InitialHealth;
            }
        }
    }
}
