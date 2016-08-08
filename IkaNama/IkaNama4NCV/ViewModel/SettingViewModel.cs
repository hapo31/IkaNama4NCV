using Hapo31.IkaNama.Model.Config;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Hapo31.IkaNama.Utils;

//設定画面のViewModel
namespace Hapo31.IkaNama4NCV.ViewModel
{
	class SettingViewModel : INotifyPropertyChanged
	{
		#region SettingViewModel PropertyChangedHandler
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string name)
		{
			if(PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}
		#endregion

		private string config_filename;
		private ConfigData config_data;

		private string discription;
		public string Discription
		{
			get { return discription; }
			set
			{
				discription = value;
				OnPropertyChanged("Discription");
			}
		}

		private bool source_text_enable;
		public bool SourceTextEnable
		{
			get { return source_text_enable; }
			set
			{
				source_text_enable = value;
				OnPropertyChanged("SourceTextEnable");
			}
		}

		private string source_text;
		public string SourceText
		{
			get { return source_text; }
			set
			{
				source_text = value;
				if(source_text != null)
				{
					ItemList[SelectedItem.Key].PostSource = source_text;
				}
				OnPropertyChanged("SourceText");
			}
		}

		private Dictionary<string, SceneData> item_list;
		public Dictionary<string, SceneData> ItemList
		{
            get { return item_list; }
			set
			{
				item_list = value;
				OnPropertyChanged("ItemList");
			}
		}

		private KeyValuePair<string, SceneData> selected_item;
		public KeyValuePair<string, SceneData> SelectedItem
		{
			get { return selected_item; }
			set
			{
				selected_item = value;
				Discription = selected_item.Value.Discription;
				SourceText = selected_item.Value.PostSource;
				SourceTextEnable = true;
				VisibleDetail = Visibility.Visible;

				OnPropertyChanged("SelectedItem");
			}
		}

		private System.Windows.Visibility visible_detail;
		public System.Windows.Visibility VisibleDetail
		{
			get { return visible_detail; }
			set
			{
				visible_detail = value;
				OnPropertyChanged("VisibleDetail");
			}
		}
		
		public ICommand ItemSelectedCommand { get; private set; }
		public ICommand OKButtonCommand { get; private set; }
		public ICommand CancelButtonCommand	{ get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="configFileName">設定ファイルの名前</param>
		public SettingViewModel(string configFileName)
		{
			//コンフィグデータを読み込む
			config_filename = configFileName;
			var conv = new ConfigFileReaderWriter(config_filename);
			if (conv.FileExist())
				config_data = conv.Read();
			else
				config_data = ConfigData.CreateDefault();

			Dictionary<string, SceneData> tmp = new Dictionary<string, SceneData>();

			//アンダーバーが消えるバグ対策
			foreach (var pair in config_data.SceneCommentSources)
			{
				string newkey = pair.Key.Replace("_", "__");
				tmp.Add(newkey, pair.Value);
			}

			ItemList = tmp;

			Discription = "項目を選択してください。";

			OKButtonCommand = new Command(o => { OKButton(o); });
			CancelButtonCommand = new Command(o => { CloseCommand(o); });
			ItemSelectedCommand = new Command(o => { TransformConfigData(o); });

			VisibleDetail = Visibility.Visible;
			SourceTextEnable = false;
		}

		void TransformConfigData(object sender)
		{
			MessageBox.Show("selected " + sender.ToString());
		}

		void OKButton(object o)
		{
			Dictionary<string, SceneData> tmp = new Dictionary<string, SceneData>();

			//アンダーバーを元に戻す
			foreach (var pair in ItemList)
			{
				string newkey = pair.Key.Replace("__", "_");
				tmp.Add(newkey, pair.Value);
			}
			config_data.SceneCommentSources = tmp;

			var convv = new ConfigFileReaderWriter(config_filename);
			convv.Write(config_data);
			CloseCommand(o);
		}

		void CloseCommand(object o)
		{
			var window = (View.Setting)o;
			window.Owner.Close();
		}
	}
}
