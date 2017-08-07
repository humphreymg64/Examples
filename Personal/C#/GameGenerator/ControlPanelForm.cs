using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameGenerator
{
	public partial class ControlPanelForm : Form
	{
		internal Monster currentMonster;
		internal List<Loot> lootList;
		private Random rand = new Random();
		public ControlPanelForm()
		{
			InitializeComponent();
		}

		private void d20Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1,20);
			numFaces.Value = 20;
		}

		private void d10Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1, 10);
			numFaces.Value = 10;
		}

		private void d6Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1, 6);
			numFaces.Value = 6;
		}

		private void d5Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1, 5);
			numFaces.Value = 5;
		}

		private void d4Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1, 4);
			numFaces.Value = 4;
		}

		private void coinButton_Click(object sender, EventArgs e)
		{
			int roll = int.Parse(Generators.dice(1, 2));
			if (roll == 1)
			{
				mainTextBox.Text = "Heads";
			}
			else
			{
				mainTextBox.Text = "Tails";
			}
			numFaces.Value = 2;
		}

		private void d100Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1, 100);
			numFaces.Value = 100;
		}

		private void d50Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1, 50);
			numFaces.Value = 50;
		}

		private void d25Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1, 25);
			numFaces.Value = 25;
		}

		private void d200Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1, 200);
			numFaces.Value = 200;
		}

		private void d500Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1, 500);
			numFaces.Value = 500;
		}

		private void d1000Button_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice(1, 1000);
			numFaces.Value = 1000;
		}

		private void customDiceButton_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = Generators.dice((int)numDice.Value, (int)numFaces.Value);
		}

		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void label4_Click(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void monGenButton_Click(object sender, EventArgs e)
		{
			int numAreas = 2;
			string selectedAreaName;

			monItemList.Items.Clear();

			if (monAreaBox2.Text.Equals("None"))
			{
				numAreas = 1;
			}

			if(monAreaBox2.SelectedIndex == 1 || monAreaBox.SelectedIndex == 0) {
				selectedAreaName = monAreaBox.Items[rand.Next(1,monAreaBox.Items.Count)].ToString();
				//gets a random index name
			}
			else if (numAreas == 2)
			{
				if (rand.Next(2) == 1)
				{
					selectedAreaName = monAreaBox.SelectedItem.ToString();
				}
				else
				{
					selectedAreaName = monAreaBox2.SelectedItem.ToString();
				}
			}
			else
			{
				selectedAreaName = monAreaBox.SelectedItem.ToString();
			}
			currentMonster = Generators.generateMonster(selectedAreaName);
			lootList = currentMonster.loot;
			mainTextBox.Text = currentMonster.name + "\n-----------\n" + currentMonster.description + "\n-----------\nHealth: "
				+ currentMonster.health + "\n-----------\nDanger: " + currentMonster.danger + "\n-----------\nHeight: " 
				+ currentMonster.heightFt + "\'" + currentMonster.heightIn + "\"";
			for (int i = 0; i < currentMonster.loot.Count; i++)
			{
				monItemList.Items.Add(currentMonster.loot[i].name + "," + currentMonster.loot[i].value);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string selectedAreaName;
			
			if (magicTypeBox.SelectedIndex == 0)
			{
				selectedAreaName = magicTypeBox.Items[rand.Next(1, magicTypeBox.Items.Count)].ToString();
				//gets a random index name
			}
			else
			{
				selectedAreaName = magicTypeBox.SelectedItem.ToString();	
			}

			mainTextBox.Text = Generators.generateMagic(selectedAreaName);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			int numAreas = 2;
			string selectedAreaName;

			if (monAreaBox2.Text.Equals("None"))
			{
				numAreas = 1;
			}

			if (monAreaBox2.SelectedIndex == 1 || monAreaBox.SelectedIndex == 0)
			{
				selectedAreaName = monAreaBox.Items[rand.Next(1, monAreaBox.Items.Count)].ToString();
				//gets a random index name
			}
			else if (numAreas == 2)
			{
				if (rand.Next(2) == 1)
				{
					selectedAreaName = monAreaBox.SelectedItem.ToString();
				}
				else
				{
					selectedAreaName = monAreaBox2.SelectedItem.ToString();
				}
			}
			else
			{
				selectedAreaName = monAreaBox.SelectedItem.ToString();
			}
			lootList.Add(Generators.generateRandomItem(selectedAreaName));
			monItemList.Items.Add(lootList.Last().name + ", " + lootList.Last().value);
		}

		private void showMonInfo_Click(object sender, EventArgs e)
		{
			mainTextBox.Text = currentMonster.name + "\n-----------\n" + currentMonster.description + "\n-----------\nHealth: "
				+ currentMonster.health + "\n-----------\nDanger: " + currentMonster.danger + "\n-----------\nHeight: "
				+ currentMonster.heightFt + "\'" + currentMonster.heightIn + "\"";
		}

		private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
		{

		}

		private void monAddCustomItem_Click(object sender, EventArgs e)
		{
			CustomItemForm cusItemForm = new CustomItemForm(this);
			DialogResult dr = cusItemForm.ShowDialog();
			if (dr != DialogResult.Cancel)
			{
				monItemList.Items.Add(lootList.Last().name + "," + lootList.Last().value);
			}
		}

		private void monRemoveItem_Click(object sender, EventArgs e)
		{
			if (monItemList.SelectedIndex >= 0 && monItemList.SelectedIndex < monItemList.Items.Count)
			{

				lootList.RemoveAt(monItemList.SelectedIndex);//these two should be synced
				monItemList.Items.RemoveAt(monItemList.SelectedIndex);
			}
		}

		private void monItemList_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = monItemList.SelectedIndex;
			if (index > -1 && index < lootList.Count)
			{
				double gold = Math.Floor(lootList[index].value);
				double silver = (lootList[index].value - gold) * 100;
				double copper = Math.Round((silver - Math.Round(silver))*100);
				string desc = lootList[index].name + "\n\nValue\n";
				silver = Math.Round(silver);
				if (gold >= 1)
				{
					desc += "Gold: " + gold + "\n";
				}
				if (silver >= 1)
				{
					desc += "Silver: " + silver + "\n";
				}
				if (copper >= 1)
				{
					desc += "Copper: " + copper + "\n";
				}
				mainTextBox.Text = desc;
			}
			else
			{
				mainTextBox.Text = "";
			}
		}
	}
}
