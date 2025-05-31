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

			// Deserialize each file by determining its type at runtime using a switch statement
			Weapon? weaponDeserialized = null;
			TorsoPiece? torsoPieceDeserialized = null;
			HeadPiece? headPieceDeserialized = null;
			Ammo? ammoDeserialized = null;
			Aid? aidDeserialized = null;
			Misc? miscDeserialized = null;

			for (int i = 0; i < serializedFiles.Length; i++)
			{
				Type type = PipBoy.GetTypeFromXML(serializedFiles[i]);
				switch (type.Name)
				{
					case nameof(Weapon):
						weaponDeserialized = PipBoy.FromFile<Weapon>(serializedFiles[i]);
						break;
					case nameof(TorsoPiece):
						torsoPieceDeserialized = PipBoy.FromFile<TorsoPiece>(serializedFiles[i]);
						break;
					case nameof(HeadPiece):
						headPieceDeserialized = PipBoy.FromFile<HeadPiece>(serializedFiles[i]);
						break;
					case nameof(Ammo):
						ammoDeserialized = PipBoy.FromFile<Ammo>(serializedFiles[i]);
						break;
					case nameof(Aid):
						aidDeserialized = PipBoy.FromFile<Aid>(serializedFiles[i]);
						break;
					case nameof(Misc):
						miscDeserialized = PipBoy.FromFile<Misc>(serializedFiles[i]);
						break;
					default:
						throw new InvalidOperationException($"Unknown type: {type.Name}");
				}
			}

			Assert.AreEqual(weapon, weaponDeserialized);
			Assert.AreEqual(torsoPiece, torsoPieceDeserialized);
			Assert.AreEqual(headPiece, headPieceDeserialized);
			Assert.AreEqual(ammo, ammoDeserialized);
			Assert.AreEqual(aid, aidDeserialized);
			Assert.AreEqual(misc, miscDeserialized);

			// Entity objects
			Player testPlayer = new();
			string playerSerializedPath = PipBoy.ToFile("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\", testPlayer);

			// Test serializing them back and forth
			Player deserializedTestPlayer = PipBoy.FromFile<Player>(playerSerializedPath);
			Assert.IsTrue(testPlayer == deserializedTestPlayer);
		}
	}
}