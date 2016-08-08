using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NicoLibrary.NicoLiveAPI;
using NicoLibrary.NicoLiveData;
using NicoLibrary.NicoScraper;
using Plugin;

namespace Hapo31.IkaNama4NCV
{
	public class PluginCore : Plugin.IPluginEx
	{
		IPluginHost host = null;
		View.WPFFrameForm wform = null;
		View.WPFFrameSettingForm sform = null;

		private readonly string FileName = @"ikanama_config.xml";

		string IPlugin.Description
		{
			get
			{
				return "IkaLogデータ連携プラグイン for NCV";
			}
		}

		IPluginHost IPlugin.Host
		{
			get
			{
				return host;
			}

			set
			{
				host = value;
			}
		}

		bool IPlugin.IsAutoRun
		{
			get
			{
				return true;
			}
		}

		string IPlugin.Name
		{
			get
			{
				return "Ikanama4NCV";
			}
		}

		string IPlugin.Version
		{
			get
			{
				return "0.1";
			}
		}

		public bool HasSettingForm
		{
			get
			{
				return true;
			}
		}

		public void ShowSettingForm()
		{
			if(sform == null)
			{
				sform = new View.WPFFrameSettingForm();
				sform.Owner = host.MainForm;
				sform.Location = host.MainForm.Location;
				var data =  new ViewModel.SettingViewModel(FileName);
				sform.View.DataContext = data;
				sform.View.SettingDetailView.DataContext = data;
				sform.Closed += (s, e) => { sform = null; };
				sform.Show();
			}
		}
		
		void IPlugin.AutoRun()
		{

		}

		void IPlugin.Run()
		{
			if (wform == null)
			{
				wform = new View.WPFFrameForm();
				wform.View.DataContext = new ViewModel.IkaNamaViewModel(host, FileName);
				wform.Owner = host.MainForm;
				wform.Location = host.MainForm.Location;
				wform.Closed += (s, e) => { wform = null; };
				wform.Show();
			}
		}
	}
}
