using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DucksVSGeese.Ducks;
using DucksVSGeese;
using DucksVSGeese.Attributes;

namespace DucksVSGeeseTest.Ducks
{
    [TestClass]
    public class DuckFighterTest
    {
        // test constructors
        [TestMethod]
        public void TestInit()
        {
            // setup
            string name = "Test";
            Duck duck = new DuckFighter(name);
            string expected = $"{DuckFighter.ClassName} Test (150/150)";

            // invoke
            string? actual = duck.ToString();

            // analyze
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInitRandomName()
        {
            // setup
            int seed = 1;
            Duck.RNG = new(seed);
            Duck duck = new DuckFighter();
            string expected = $"{DuckFighter.ClassName} Bread (150/150)";

            // invoke
            string? actual = duck.ToString();

            // analyze
            Assert.AreEqual(expected, actual);
        }

        // test attack

        [TestMethod]
        public void TestAttack()
        {
            // setup
            string name = "Test";
            Duck duck = new DuckFighter(name);
            string expected = "Eye Poke hits for [25] points of Physical damage!";

            // invoke
            Attack attack = duck.Attack();
            string? actual = attack.ToString();

            // analyze
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAttackScaling()
        {
            // setup
            string name = "Test";
            Duck duck = new DuckFighter(name);
            string expected = "Eye Poke hits for [50] points of Physical damage!";

            // invoke
            duck.LevelUp();
            duck.LevelUp();
            Attack attack = duck.Attack();
            string? actual = attack.ToString();

            // analyze
            Assert.AreEqual(expected, actual);
        }

        // test take damage
        [TestMethod]
        public void TestTakeDamageNormalHoly()
        {
            // setup
            string name = "Test";
            int[] hits = [10];
            DAttribute attribute = DAttribute.Holy;
            Attack attack = new(name, hits, attribute);

            Duck duck = new DuckFighter(name);
            string expectedDuck = $"{DuckFighter.ClassName} Test (140/150)";
            int expectedDamage = 10;

            // invoke
            int actualDamage = duck.TakeDamage(attack);
            string? actualDuck = duck.ToString();

            // analyze
            Assert.AreEqual(expectedDuck, actualDuck);
            Assert.AreEqual(expectedDamage, actualDamage);
        }

        [TestMethod]
        public void TestTakeDamageResistPhysical()
        {
            // setup
            string name = "Test";
            int[] hits = [10];
            DAttribute attribute = DAttribute.Physical;
            Attack attack = new(name, hits, attribute);

            Duck duck = new DuckFighter(name);
            string expectedDuck = $"{DuckFighter.ClassName} Test (142/150)";
            int expectedDamage = 8;

            // invoke
            int actualDamage = duck.TakeDamage(attack);
            string? actualDuck = duck.ToString();

            // analyze
            Assert.AreEqual(expectedDuck, actualDuck);
            Assert.AreEqual(expectedDamage, actualDamage);
        }

        [TestMethod]
        public void TestTakeDamageWeakMagical()
        {
            // setup
            string name = "Test";
            int[] hits = [10, 10];
            DAttribute attribute = DAttribute.Magical;
            Attack attack = new(name, hits, attribute);

            Duck duck = new DuckFighter(name);
            string expectedDuck = $"{DuckFighter.ClassName} Test (126/150)";
            int expectedDamage = 24;

            // invoke
            int actualDamage = duck.TakeDamage(attack);
            string? actualDuck = duck.ToString();

            // analyze
            Assert.AreEqual(expectedDuck, actualDuck);
            Assert.AreEqual(expectedDamage, actualDamage);
        }

        [TestMethod]
        public void TestTakeDamageWeakElemental()
        {
            // setup
            string name = "Test";
            int[] hits = [10];
            DAttribute attribute = DAttribute.Elemental;
            Attack attack = new(name, hits, attribute);

            Duck duck = new DuckFighter(name);
            string expectedDuck = $"{DuckFighter.ClassName} Test (138/150)";
            int expectedDamage = 12;

            // invoke
            int actualDamage = duck.TakeDamage(attack);
            string? actualDuck = duck.ToString();

            // analyze
            Assert.AreEqual(expectedDuck, actualDuck);
            Assert.AreEqual(expectedDamage, actualDamage);
        }

        // test is conscious
        [TestMethod]
        public void TestIsConsciousTrue1()
        {
            // setup
            string name = "Test";
            Duck duck = new DuckFighter(name);

            // analyze
            Assert.IsTrue(duck.IsConscious());
        }

        [TestMethod]
        public void TestIsConsciousTrue2()
        {
            // setup
            string name = "Test";
            Duck duck = new DuckFighter(name);

            // invoke
            duck.Heal(-10);

            // analyze
            Assert.IsTrue(duck.IsConscious());
        }

        [TestMethod]
        public void TestIsConsciousFalse()
        {
            // setup
            string name = "Test";
            Duck duck = new DuckFighter(name);

            // invoke
            duck.Heal(-duck.MaxHP);

            // analyze
            Assert.IsFalse(duck.IsConscious());
        }


        // test heal
        [TestMethod]
        public void TestHealAtFull()
        {
            // setup
            int amount = 100;
            string name = "Test";
            Duck duck = new DuckFighter(name);
            string expected = $"{DuckFighter.ClassName} Test (150/150)";

            // invoke
            duck.Heal(amount);
            string? actual = duck.ToString();

            // analyze
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHealNormal()
        {
            // setup
            int amount = 100;
            string name = "Test";
            Duck duck = new DuckFighter(name);
            string expected = $"{DuckFighter.ClassName} Test (149/150)";

            // invoke
            duck.Heal(-amount);
            duck.Heal(amount - 1);
            string? actual = duck.ToString();

            // analyze
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHealTo0()
        {
            // setup
            int amount = -10000;
            string name = "Test";
            Duck duck = new DuckFighter(name);
            string expected = $"{DuckFighter.ClassName} Test (0/150)";

            // invoke
            duck.Heal(amount);
            string? actual = duck.ToString();

            // analyze
            Assert.AreEqual(expected, actual);
        }

        // test heal all
        [TestMethod]
        public void TestHealAll()
        {
            // setup
            int amount = -100;
            string name = "Test";
            Duck duck = new DuckFighter(name);
            string expected = $"{DuckFighter.ClassName} Test (150/150)";

            // invoke
            duck.Heal(amount);
            duck.HealAll();
            string? actual = duck.ToString();

            // analyze
            Assert.AreEqual(expected, actual);
        }

        // test end turn
        [TestMethod]
        public void TestEndTurn()
        {
            // setup
            string name = "Test";
            Duck duck = new DuckFighter(name);
            int expected = 0;

            // invoke
            duck.EndTurn();
            int actual = duck.CursedTimer;

            // analyze
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCurseEndTurn()
        {
            // setup
            string name = "Test";
            Duck duck = new DuckFighter(name);
            int expected = 4;

            // invoke
            duck.Curse();
            duck.EndTurn();
            int actual = duck.CursedTimer;

            // analyze
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(duck.AttackAllies);
        }

        [TestMethod]
        public void TestRemoveCurseWithEndTurn()
        {
            // setup
            string name = "Test";
            int curseTime = 5;
            Duck duck = new DuckFighter(name);
            int expected = 0;

            // invoke
            duck.Curse();
            for (int i = 0; i < curseTime; i++) duck.EndTurn();
            int actual = duck.CursedTimer;

            // analyze
            Assert.AreEqual(expected, actual);
            Assert.IsFalse(duck.AttackAllies);
        }

        // test level up
        [TestMethod]
        public void TestLevelUp1()
        {
            // setup
            string name = "Test";
            Duck duck = new DuckFighter(name);
            string expected = $"{DuckFighter.ClassName} Test (225/225)";

            // invoke
            duck.LevelUp();
            string? actual = duck.ToString();

            // analyze
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLevelUp2()
        {
            // setup
            string name = "Test";
            Duck duck = new DuckFighter(name);
            string expected = $"{DuckFighter.ClassName} Test (337/337)";

            // invoke
            duck.LevelUp();
            duck.LevelUp();
            string? actual = duck.ToString();

            // analyze
            Assert.AreEqual(expected, actual);
        }

        // test set level
        // [TestMethod]
        // public void TestSetLevelUp()
        // {
        //     // setup
        //     int level = 3;
        //     string name = "Test";
        //     Duck duck = new DuckFighter(name);
        //     string expectedHP = $"{DuckFighter.ClassName} Test (337/337)";
        //     string expectedAttack = "Eye Poke hits for [50] points of Physical damage!";

        //     // invoke
        //     duck.SetLevel(level);
        //     string? actualHP = duck.ToString();
        //     string actualAttack = duck.Attack().ToString();

        //     // analyze
        //     Assert.AreEqual(expectedHP, actualHP);
        //     Assert.AreEqual(expectedAttack, actualAttack);
        // }
        
    }
}