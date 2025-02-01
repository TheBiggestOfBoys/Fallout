using Pip_Boy.Entities;
using Pip_Boy.Items;
using Pip_Boy.Objects;

namespace PIP_Boy_Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			// Item Objects
			Weapon weapon = new("10mm Pistol", 5.5f, 55, [], Weapon.WeaponType.Gun, 3, 30, 10, 100);
			TorsoPiece torsoPiece = new("Vault 13 Jumpsuit", 5, 25, [], 3, false);
			HeadPiece headPiece = new("Nerd Goggles", 1, 25, [], 0, false);
			Ammo ammo = new("10mm Ammo", 1, [], Ammo.AmmoType.Bullet, Ammo.AmmoModification.Standard);
			Aid aid = new("Stimpack", 1f, 30, [], Aid.AidType.Syringe);
			Misc misc = new("Journal Entry", 1f, 15, Misc.MiscType.Other);

			string[] filePaths =
			[
				PipBoy.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\", weapon),
				PipBoy.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\", torsoPiece),
				PipBoy.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\", headPiece),
				PipBoy.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\", ammo),
				PipBoy.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\", aid),
				PipBoy.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\", misc),
			];

			string[] serializedFiles = Directory.GetFiles("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\");

			foreach (string filePath in filePaths)
			{
				Assert.IsTrue(serializedFiles.Contains(filePath));
			}

			// Check tag-to-type casting
			Type[] expectedTypes = [typeof(Weapon), typeof(TorsoPiece), typeof(HeadPiece), typeof(Ammo), typeof(Aid), typeof(Misc)];
			Type[] actualTypes = new Type[6];
			for (int i = 0; i < filePaths.Length; i++)
			{
				string filePath = filePaths[i];
				Type type = PipBoy.GetTypeFromXML(filePath);
				actualTypes[i] = type;
			}

			for (int i = 0; i < expectedTypes.Length; i++)
			{
				Assert.IsTrue(actualTypes.Contains(expectedTypes[i]));
			}

			Weapon weaponDeserializaed = PipBoy.FromFile<Weapon>(serializedFiles[0]);
			TorsoPiece torsoPieceDeserializaed = PipBoy.FromFile<TorsoPiece>(serializedFiles[1]);
			HeadPiece headPieceDeserializaed = PipBoy.FromFile<HeadPiece>(serializedFiles[2]);
			Ammo ammoDeserializaed = PipBoy.FromFile<Ammo>(serializedFiles[3]);
			Aid aidDeserializaed = PipBoy.FromFile<Aid>(serializedFiles[4]);
			Misc miscDeserializaed = PipBoy.FromFile<Misc>(serializedFiles[5]);

			Assert.AreEqual(weapon, weaponDeserializaed);
			Assert.AreEqual(torsoPiece, torsoPieceDeserializaed);
			Assert.AreEqual(headPiece, headPieceDeserializaed);
			Assert.AreEqual(ammo, ammoDeserializaed);
			Assert.AreEqual(aid, aidDeserializaed);
			Assert.AreEqual(misc, miscDeserializaed);

			// Entity objects
			Player testPlayer = new();
			string playerSerializedPath = PipBoy.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\", testPlayer);

			// Test serializing them back and forth
			Player deserializedTestPlayer = PipBoy.FromFile<Player>(playerSerializedPath);
			Assert.IsTrue(testPlayer == deserializedTestPlayer);
		}
	}
}