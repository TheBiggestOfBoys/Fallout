using Pip_Boy.Entities;
using Pip_Boy.Entities.Creatures;
using Pip_Boy.Entities.Mutants;
using Pip_Boy.Entities.Robots;
using Pip_Boy.Objects;

namespace PIP_Boy_Tests;

[TestClass]
public class EntityTests
{
	public static Entity[] Entities =
	[
		new Human(),
		new Player(),

		new Robot(),

		new Ghoul(),
		new Feral(),

		new SuperMutant(),
		new Nightkin(),

		new DeathClaw(),

		new Dog(),
		new NightStalker(),

		new BloatFly()
	];

	public const string serializedEntitiesFilesFolder = "C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\PIP Boy Tests\\Serialized Files\\Entities\\";

	public static string[] serializedEntityFiles => Directory.GetFiles(serializedEntitiesFilesFolder, "*.xml");

	[TestMethod]
	public void EntitySerialization()
	{
		foreach (Entity entity in Entities)
		{
			// Check that serialization occurred and that the types match
			Assert.AreEqual(entity.GetType(), PipBoy.GetTypeFromXML(PipBoy.ToFile(serializedEntitiesFilesFolder, entity)));
		}
	}

	[TestMethod]
	public void EntityDeserialization()
	{
		foreach (string filePath in serializedEntityFiles)
		{
			Type type = PipBoy.GetTypeFromXML(filePath);
			Entity? deserialized = (Entity?)Activator.CreateInstance(type);
			Assert.IsNotNull(deserialized);
			Assert.AreEqual(type, deserialized.GetType());
		}
	}
}
