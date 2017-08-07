namespace GameGenerator
{
	partial class CustomItemForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cusItemHeader = new System.Windows.Forms.Label();
			this.cusItemValue = new System.Windows.Forms.Label();
			this.cusItemNameLabel = new System.Windows.Forms.Label();
			this.cusItemNameBox = new System.Windows.Forms.TextBox();
			this.cusItemCreatButton = new System.Windows.Forms.Button();
			this.cusItemGoldValue = new System.Windows.Forms.NumericUpDown();
			this.cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.cusItemGoldValue)).BeginInit();
			this.SuspendLayout();
			// 
			// cusItemHeader
			// 
			this.cusItemHeader.AutoSize = true;
			this.cusItemHeader.Location = new System.Drawing.Point(50, 9);
			this.cusItemHeader.Name = "cusItemHeader";
			this.cusItemHeader.Size = new System.Drawing.Size(82, 13);
			this.cusItemHeader.TabIndex = 0;
			this.cusItemHeader.Text = "Item Information";
			// 
			// cusItemValue
			// 
			this.cusItemValue.AutoSize = true;
			this.cusItemValue.Location = new System.Drawing.Point(16, 55);
			this.cusItemValue.Name = "cusItemValue";
			this.cusItemValue.Size = new System.Drawing.Size(71, 13);
			this.cusItemValue.TabIndex = 1;
			this.cusItemValue.Text = "Value In Gold";
			// 
			// cusItemNameLabel
			// 
			this.cusItemNameLabel.AutoSize = true;
			this.cusItemNameLabel.Location = new System.Drawing.Point(29, 29);
			this.cusItemNameLabel.Name = "cusItemNameLabel";
			this.cusItemNameLabel.Size = new System.Drawing.Size(58, 13);
			this.cusItemNameLabel.TabIndex = 2;
			this.cusItemNameLabel.Text = "Item Name";
			// 
			// cusItemNameBox
			// 
			this.cusItemNameBox.Location = new System.Drawing.Point(93, 26);
			this.cusItemNameBox.Name = "cusItemNameBox";
			this.cusItemNameBox.Size = new System.Drawing.Size(100, 20);
			this.cusItemNameBox.TabIndex = 0;
			// 
			// cusItemCreatButton
			// 
			this.cusItemCreatButton.Location = new System.Drawing.Point(19, 90);
			this.cusItemCreatButton.Name = "cusItemCreatButton";
			this.cusItemCreatButton.Size = new System.Drawing.Size(79, 23);
			this.cusItemCreatButton.TabIndex = 2;
			this.cusItemCreatButton.Text = "Create Item";
			this.cusItemCreatButton.UseVisualStyleBackColor = true;
			this.cusItemCreatButton.Click += new System.EventHandler(this.cusItemCreatButton_Click);
			// 
			// cusItemGoldValue
			// 
			this.cusItemGoldValue.Location = new System.Drawing.Point(93, 53);
			this.cusItemGoldValue.Name = "cusItemGoldValue";
			this.cusItemGoldValue.Size = new System.Drawing.Size(100, 20);
			this.cusItemGoldValue.TabIndex = 1;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(104, 90);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(79, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// CustomItemForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(207, 125);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.cusItemGoldValue);
			this.Controls.Add(this.cusItemCreatButton);
			this.Controls.Add(this.cusItemNameBox);
			this.Controls.Add(this.cusItemNameLabel);
			this.Controls.Add(this.cusItemValue);
			this.Controls.Add(this.cusItemHeader);
			this.Name = "CustomItemForm";
			this.Text = "CustomItemForm";
			this.Load += new System.EventHandler(this.CustomItemForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.cusItemGoldValue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label cusItemHeader;
		private System.Windows.Forms.Label cusItemValue;
		private System.Windows.Forms.Label cusItemNameLabel;
		private System.Windows.Forms.TextBox cusItemNameBox;
		private System.Windows.Forms.Button cusItemCreatButton;
		private System.Windows.Forms.NumericUpDown cusItemGoldValue;
		private System.Windows.Forms.Button cancelButton;
	}
}