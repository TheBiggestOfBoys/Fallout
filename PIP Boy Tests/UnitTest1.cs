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
            // Base line tests
            Assert.IsTrue(1 + 1 == 2);
            Assert.IsFalse(1 * 1 == 2);

            // Item Objects
            Item[] items = [
                new Weapon("10mm Pistol", 5.5f, 55, [], Weapon.WeaponType.Gun, 3, 30, 10, 100),
                new TorsoPiece("Vault 13 Jumpsuit", 5, 25, [], 3, false),
                new HeadPiece("Nerd Goggles", 1, 25, [], 0, false),
                new Ammo("10mm Ammo", 1, [], Ammo.AmmoType.Bullet, Ammo.AmmoModification.Standard),
                new Aid("Stimpack", 1f, 30, [], Aid.AidType.Syringe),
                new Misc("Journal Entry", 1f, 15, Misc.MiscType.Other)
            ];

            // Serialize to file
            foreach (Item item in items)
            {
                item.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\");
            }

            // Check tag-to-type casting
            Type[] expectedTypes = [typeof(Weapon), typeof(TorsoPiece), typeof(HeadPiece), typeof(Ammo), typeof(Aid), typeof(Misc)];
            Type[] actualTypes = new Type[5];
            string[] filePaths = Directory.GetFiles("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\");
            for (int i = 0; i < filePaths.Length; i++)
            {
                string filePath = filePaths[i];
                Type type = Item.GetTypeFromXML(filePath);
                actualTypes[i] = type;
            }

            for (int i = 0; i < expectedTypes.Length; i++)
            {
                Assert.IsTrue(expectedTypes[i] == actualTypes[i]);
            }

            string[] serializedFiles = Directory.GetFiles("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\");

            Assert.IsTrue(items.Length == serializedFiles.Length);

            Item[] deserializedItems = new Item[6];

            // Entity objects
            Player testPlayer = new();

            // Test serializing them back and forth
            Player deserializedTestPlayer = Entity.FromFile<Player>(testPlayer.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\"));
            Assert.IsTrue(testPlayer == deserializedTestPlayer);
        }
    }
}