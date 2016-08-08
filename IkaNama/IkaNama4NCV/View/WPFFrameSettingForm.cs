using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hapo31.IkaNama4NCV.View;

namespace Hapo31.IkaNama4NCV.View
{
	public partial class WPFFrameSettingForm : Form
	{
		public View.Setting View
		{
			get
			{
				return setting1;
			}
			private set
			{
				setting1 = value;
			}
		}
		public WPFFrameSettingForm()
		{
			InitializeComponent();
			setting1.Owner = this;
		}
		
	}
}
