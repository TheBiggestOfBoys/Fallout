using Pip_Boy.Entities;
using Pip_Boy.Objects;

namespace PIP_Boy_Tests;

[TestClass]
public class PlayerTests
{
	public static Player player = new();

	public const string serializedPlayerFileFolder = "C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\Player\\";

	[TestMethod]
	public void PlayerSerialization()
	{
		string filePath = PipBoy.ToFile(serializedPlayerFileFolder, player);
		Assert.IsTrue(Directory.EnumerateFiles(serializedPlayerFileFolder).Contains(filePath));
		Assert.AreEqual(player.GetType(), PipBoy.GetTypeFromXML(filePath), "Deserialized type does not match expected Player type.");
	}

	[TestMethod]
	public void PlayerDeserialization()
	{
		string filePath = Directory.GetFiles(serializedPlayerFileFolder, "*.xml")[0];
		Player deserializedPlayer = PipBoy.FromFile<Player>(filePath);
		Assert.IsNotNull(deserializedPlayer, "Deserialized player should not be null.");
		Assert.AreEqual(player.GetType(), deserializedPlayer.GetType(), "Deserialized type does not match expected Player type.");
	}
}
