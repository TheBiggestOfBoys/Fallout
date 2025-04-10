using System;
using System.Xml;

namespace Map
{
	internal class Program
	{
		static void Main()
		{
			if (MapService.IsInternetAvailable().Result)
			{
				MapService mapService = new(new(40.45919275749208f, -85.49533550018319f), 5f);
				XmlDocument xmlDoc = mapService.GetMapDataAsync(Amenities.restaurant).Result;

				XmlNodeList xmlNodeList = xmlDoc.GetElementsByTagName("node");

				for (int index = 0; index < xmlNodeList.Count; index++)
				{
					XmlNode node = xmlNodeList[index];

					// Iterate through child nodes to find the <tag> with k="name"
					string name = null;
					foreach (XmlNode child in node.ChildNodes)
					{
						if (child.Name == "tag" && child.Attributes?["k"]?.Value == "name")
						{
							name = child.Attributes["v"]?.Value;
							break;
						}
					}

					// Print the name if found
					if (!string.IsNullOrEmpty(name))
					{
						Console.WriteLine($"Node Name: {name}");
					}
					else
					{
						Console.WriteLine("Node does not have a 'name' tag.");
					}
				}
			}
		}
	}
}
