using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator
{
	class Monster
	{

		private static Random rand = new Random();
		public int health { get; set; }
		public int danger { get; set; }
		public int heightFt { get; set; }
		public int heightIn { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public List<Loot> loot { get; set; }

		public Monster(string generationLine)
		{
			List<string> lines = new List<string>(6);
			string[] heights = new string[2];
			lines = generationLine.Split('|').ToList();
			try
			{
				name = lines[0];
				description = lines[1];
				heights = lines[2].Split('\'');
				heightFt = int.Parse(heights[0]);
				heightIn = int.Parse(heights[1].Substring(0, heights[1].Length - 1));
				heightIn += rand.Next(-6, 6);
				if (heightIn > 12)
				{
					heightIn -= 12;
					heightFt++;
				}
				else if (heightIn < 0)
				{
					heightIn += 12;
					if (heightFt > 0)
					{
						heightFt--;
					}
					else
					{
						heightIn = 1;
					}
				}
				danger = int.Parse(lines[3]);
				health = danger * 2 + 3;
			}
			catch (IndexOutOfRangeException e)
			{
				name = "Error Beast";
				description = "The input did not have the right amount of \'|\'s or the height was formatted poorly.";
			}
			loot = Generators.lootGeneration(lines[4], lines[5]);
		}
	}
}
