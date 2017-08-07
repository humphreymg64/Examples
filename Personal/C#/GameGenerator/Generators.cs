using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameGenerator
{
	class Generators
	{
		private static Random rand = new Random();

		internal static string dice(int numDice, int numFaces) {
			string rolls = "";
			numFaces++;
			for (int i = 0; i < numDice; i++) {
				rolls += rand.Next(1, numFaces).ToString() + "\n";
			}
			return rolls; 
		}

		internal static List<Loot> lootGeneration(string itemsString, string valuesString)
		{
			List<Loot> loots = new List<Loot>(3);
			List<string> items = itemsString.Split(',').ToList();
			List<string> values = valuesString.Split(',').ToList();
			List<string> probs = new List<string>(5);
			Loot l = new Loot();
			int currentItemProb;
			int numLength;
			bool probWasParsed;
			bool valueWasParsed;

			if (items[0].Equals(""))
			{
				return loots;
			}
			for (int i = 0; i < items.Count; i++)
			{
				numLength = items[i].IndexOf(')') - items[i].IndexOf('(');
				string temp = items[i].Substring(items[i].IndexOf('(')+1, numLength-1);
				probWasParsed = int.TryParse(temp, out currentItemProb);
				//the above line takes a value like "name(75)" and trys to parse out the 75
				if (probWasParsed && rand.Next(101) < currentItemProb)
				{
					l.name = items[i].Substring(0, items[i].IndexOf('('));
					//gets the name part of "name(75)"
					try
					{
						valueWasParsed = float.TryParse(values[i], out l.value);
						if (!valueWasParsed)
						{
							l.value = -2;
						}	
					}
					catch (IndexOutOfRangeException)
					{
						l.value = -1;
					}
					loots.Add(l);
				}
			}
			return loots;
		}

		internal static Monster generateMonster(string selectedAreaName)
		{
			StreamReader sr = new StreamReader(@"~\..\..\..\textFiles\Monsters\" + selectedAreaName + ".txt");
			List<string> lines = new List<string>(25);

			lines.Add(sr.ReadLine());
			while (lines[0].First() != '-')
			{
				lines[0] = sr.ReadLine();
			}
			lines[0] = sr.ReadLine();
			for (int i = 0; sr.Peek() > -1; i++)
			{
				lines.Add(sr.ReadLine());
			}
			sr.Close();
			return new Monster(lines[rand.Next(lines.Count-1)]);
		}

		internal static string generateMagic(string selectedAreaName)
		{
			StreamReader sr = new StreamReader(@"~\..\..\..\textFiles\Spells\" + selectedAreaName + ".txt");
			List<string> lines = new List<string>(25);
			string ret;
			int roll;

			lines.Add(sr.ReadLine());
			lines[0] = sr.ReadLine();
			for (int i = 0; sr.Peek() > -1; i++)
			{
				lines.Add(sr.ReadLine());
			}
			sr.Close();
			ret = lines[rand.Next(lines.Count)];
			lines = ret.Split('|').ToList();
			int x = int.Parse(lines[1].Substring(0, lines[1].IndexOf('d')));
			int y = int.Parse(lines[1].Substring(lines[1].IndexOf('d') + 1));
			roll = int.Parse(dice(x,y));
			ret = lines[0] + "\n------------\nRoll = " + roll;
			return ret;
		}
		internal static Loot generateRandomItem(string areaName) {
			Loot randItem;
			StreamReader sr = new StreamReader(@"~\..\..\..\textFiles\Items\" + areaName + ".txt");
			List<string> lines = new List<string>(50);
			int prob;
			int count;
			int lineNum;
			bool couldParse;

			lines.Add(sr.ReadLine());
			while (lines[0].First() != '-')
			{
				lines[0] = sr.ReadLine();
			}
			lines[0] = sr.ReadLine();
			for (int i = 0; sr.Peek() > -1; i++)
			{
				lines.Add(sr.ReadLine());
			}
			sr.Close();

			count = lines.Count;

			for (int i = 0; i < count; i++)
			{
				couldParse = int.TryParse(lines[i].Split('|')[1],out prob);

				if (couldParse && prob < 10)
				{
					for (int j = 0; j < prob; j++)
					{
						lines.Add(lines[i]);
					} 
				}
			}

			lineNum = rand.Next(lines.Count);
			randItem.name = lines[lineNum].Split('|')[0];
			randItem.value = float.Parse(lines[lineNum].Split('|')[2]);
			return randItem;
		}
	}
}
