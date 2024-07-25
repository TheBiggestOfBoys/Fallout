using Pip_Boy.Entities;
using Pip_Boy.Items;

namespace PIP_Boy_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(1 + 1 == 2);
            Assert.IsFalse(1 * 1 == 2);

            // Item Objects
            Weapon testWeapon = new("10mm Pistol", 5.5f, 55, [], Weapon.WeaponType.Gun, 3, 30, 10, 100);
            TorsoPiece testTorsoPiece = new("Vault 13 Jumpsuit", 5, 25, [], 3, false);
            HeadPiece testHeadPiece = new("Nerd Goggles", 1, 25, [], 0, false);
            Ammo testAmmo = new("10mm Ammo", 1, [], Ammo.AmmoType.Bullet, Ammo.AmmoModification.Standard);
            Aid testAid = new("Stimpack", 1f, 30, [], Aid.AidType.Syringe);
            Misc testMisc = new("Journal Entry", 1f, 15, Misc.MiscType.Other);

            // Test serializing them back and forth
            Weapon deserializedTestWeapon = Item.FromFile<Weapon>(testWeapon.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\"));
            Assert.IsTrue(testWeapon.Equals(deserializedTestWeapon));

            TorsoPiece deserializedTestTorsoPiece = Item.FromFile<TorsoPiece>(testHeadPiece.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\"));
            Assert.IsTrue(testTorsoPiece.Equals(deserializedTestTorsoPiece));

            HeadPiece deserializedTestHeadPiece = Item.FromFile<HeadPiece>(testTorsoPiece.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\"));
            Assert.IsTrue(testHeadPiece.Equals(deserializedTestHeadPiece));

            Ammo deserializedTestAmmo = Item.FromFile<Ammo>(testAmmo.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\"));
            Assert.IsTrue(testAmmo.Equals(deserializedTestAmmo));

            Aid deserializedTestAid = Item.FromFile<Aid>(testAid.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\"));
            Assert.IsTrue(testAid.Equals(deserializedTestAid));

            Misc deserializedTestMisc = Item.FromFile<Misc>(testMisc.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\"));
            Assert.IsTrue(testMisc.Equals(deserializedTestMisc));

            // Entity objects
            Player testPlayer = new();

            // Test serializing them back and forth
            Player deserializedTestPlayer = Entity.FromFile<Player>(testPlayer.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\"));
            Assert.IsTrue(testPlayer == deserializedTestPlayer);
        }
    }
}