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
	public partial class CustomItemForm : Form
	{
		private ControlPanelForm cpf;
		public CustomItemForm()
		{
			InitializeComponent();
		}

		internal CustomItemForm(Form caller) {
			cpf = caller as ControlPanelForm;
			InitializeComponent();
		}

		private void CustomItemForm_Load(object sender, EventArgs e)
		{

		}

		private void cusItemCreatButton_Click(object sender, EventArgs e)
		{
			Loot l = new Loot();
			l.name = cusItemNameBox.Text;
			l.value = (float)cusItemGoldValue.Value;
			cpf.lootList.Add(l);
			cpf.Focus();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
