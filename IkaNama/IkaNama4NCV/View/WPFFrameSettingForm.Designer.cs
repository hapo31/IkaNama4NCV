﻿namespace Hapo31.IkaNama4NCV.View
{
	partial class WPFFrameSettingForm
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
			this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
			this.setting1 = new Hapo31.IkaNama4NCV.View.Setting();
			this.SuspendLayout();
			// 
			// elementHost1
			// 
			this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.elementHost1.Location = new System.Drawing.Point(0, 0);
			this.elementHost1.Name = "elementHost1";
			this.elementHost1.Size = new System.Drawing.Size(707, 475);
			this.elementHost1.TabIndex = 1;
			this.elementHost1.Text = "elementHost2";
			this.elementHost1.Child = this.setting1;
			// 
			// WPFFrameSettingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(707, 475);
			this.Controls.Add(this.elementHost1);
			this.Name = "WPFFrameSettingForm";
			this.Text = "IkaNamaの設定";
			this.ResumeLayout(false);

		}

		#endregion
		
		private System.Windows.Forms.Integration.ElementHost elementHost1;
		private Setting setting1;
	}
}