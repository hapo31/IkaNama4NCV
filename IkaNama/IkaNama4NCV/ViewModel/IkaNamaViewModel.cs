using System;
using System.Windows.Input;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Hapo31.IkaNama;
using Hapo31.IkaLogProxy.Core;
using Hapo31.IkaNama.Utils;
using Hapo31.IkaNama.Model.Config;

using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading;

namespace Hapo31.IkaNama4NCV.ViewModel
{
	class IkaNamaViewModel : INotifyPropertyChanged
	{
		private Connecter ikanama = null;
		private IkaNama.Model.Config.ConfigData config;

		private Plugin.IPluginHost host;

		private string FileName;
		

		/// <summary>
		/// ボタンがクリックされた時のコマンド
		/// </summary>
		public ICommand ButtonClickCommand { get; private set; }

		struct UIText
		{
			public const string Connectingbutton = "IkaLogを待機しています...";
			public const string ConnectedButton = "IkaLog接続済み";
			public const string DisConnectedButton = "未接続";
			public const string ConnectedLog = "IkaLogへ接続しました";
			public const string DisconnectedLog = "IkaLogから切断されました...";
			public const string LoadedConfigfileLog = "設定ファイルを読み込みました。";
			public const string CreatedDefalutConfigLog = "設定ファイルが読み込めなかったため、デフォルトの設定を適用します。";
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="host">このプラグインを呼び出したホストのIPluginHost型のインスタンス</param>
		/// <param name="configFileName">設定ファイルの名前</param>
		public IkaNamaViewModel(Plugin.IPluginHost host, string configFileName)
		{
			this.host = host;
			FileName = configFileName;
			ButtonClickCommand = new Command(o => { buttonClickCommand(o); });

			UpdateData();
		}

		#region IkaNamaViewModel Properties

		/// <summary>
		/// IkaNamaのInstanceが生成済みかどうか
		/// </summary>
		public bool IkaNamaCreated
		{
			get
			{
				return ikanama != null;
			}
		}

		/// <summary>
		/// IkaNamaが接続待機中かどうか
		/// </summary>
		public bool IkaNamaWaiting
		{
			get
			{
				return IkaNamaCreated && ikanama.Waiting;
			}
		}

		/// <summary>
		/// IkaNamaがIkaLogへ接続済みかどうか
		/// </summary>
		public bool IkaNamaConnected
		{
			get
			{
				return IkaNamaCreated && ikanama.Opened;
			}
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}


		#region IkaNamViewModel BindingMembers

		/// <summary>
		/// ボタンのテキスト
		/// </summary>
		private string connection_button_text;
		public string ConnectionButtonText
		{
			get { return connection_button_text; }
			set
			{
				connection_button_text = value;
				OnPropertyChanged("ConnectionButtonText");
			}
		}
		/// <summary>
		/// ボタンの状態
		/// </summary>
		private bool connection_button_enabled = true;
		public bool ConnectionButtonEnabled
		{
			get { return connection_button_enabled; }
			set
			{
				connection_button_enabled = value;
				OnPropertyChanged("ConnectionButtonEnabled");
			}
		}

		/// <summary>
		/// IkaNamaのLogが出力されるテキストボックス
		/// </summary>
		private string log_text;
		public string LogText
		{
			get { return log_text; }
			set
			{
				log_text = value;
				OnPropertyChanged("LogText");
			}
		}
		/// <summary>
		/// Log TextBoxの状態
		/// </summary>
		private bool log_text_enabled = false;
		public bool LogTextEnabled
		{
			get { return log_text_enabled; }
			set
			{
				log_text_enabled = value;
				OnPropertyChanged("LogTextEnabled");
			}
		}

		#endregion

		/// <summary>
		/// プラグインのウインドウ破棄時に行われる処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void wnd_closed(object sender, EventArgs e)
		{
			if (ikanama != null && !ikanama.Disposed)
				ikanama.Dispose();
		}

		/// <summary>
		/// ウインドウ破棄中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void wnd_closing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if (ikanama != null) ikanama.Dispose();
			//closing = true;
		}

		/// <summary>
		/// Logウインドウにログを出力する
		/// </summary>
		/// <param name="str">文字列</param>
		private void AppendLogText(string str)
		{
			LogText += str + Environment.NewLine;
		}

		/// <summary>
		/// データの状態を更新
		/// </summary>
		private void UpdateData()
		{
			//IkaNamaが接続待機中でなければ有効化
			ConnectionButtonEnabled = !IkaNamaWaiting;
			//IkaNamaが生成済みならば有効
			LogTextEnabled = IkaNamaCreated;

			if (!IkaNamaCreated)
				ConnectionButtonText = UIText.DisConnectedButton;
			else if (IkaNamaWaiting)
				ConnectionButtonText = UIText.Connectingbutton;
			else
				ConnectionButtonText = UIText.ConnectedButton;

		}
		

		private void buttonClickCommand(object parameter)
		{
			if (ikanama == null)
			{
				try
				{
					ikanama = new Connecter("127.0.0.1", 9090);

					//設定ファイルを読み込む
					var conv = new ConfigFileReaderWriter(FileName);

					if (conv.FileExist())
					{
						//設定ファイルがあればそれを読み込む
						config = conv.Read();
						AppendLogText(UIText.LoadedConfigfileLog);
					}
					else
					{
						//なければデフォルトデータを適用
						config = ConfigData.CreateDefault();
						conv.Write(config);
						AppendLogText(UIText.CreatedDefalutConfigLog);
					}
					
					//
					// WebSocketハンドラの設定
					//

					//接続に成功した時
					ikanama.WebSocket.Opened += (s, m) =>
					{
						UpdateData();
						AppendLogText(UIText.ConnectedLog);
					};

					//IkaLogから切断された時
					ikanama.WebSocket.Closed += (s, m) =>
					{
						ikanama = null;
						UpdateData();
						AppendLogText(UIText.DisconnectedLog);
					};

                    ////メッセージを受信した時
                    ikanama.WebSocket.MessageReceived += (s, m) =>
                    {
                        AppendLogText(m.Message);
                    };

                    //エラーが発生した時
                    ikanama.WebSocket.Error += (s, m) =>
					{
						MessageBox.Show("IkaLogへの接続時にエラーが発生しました。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						ikanama = null;
						UpdateData();
					};

                    //
                    // IkaLogイベントハンドラの設定
                    //

                    //TODO LINQで書いてすっきりさせたい
                    foreach (var c in config.SceneCommentSources)
					{
                        //コンフィグを見て、イベント発生時にコメント投稿を行うかどうかを設定
						if (config[c.Key].IsEnabled)
						{
							ikanama.Events.Add(c.Key, (s, m)=>
							{
								var result = (IkaLogProxy.Util.SplatoonMatchResult)s;
								string format = c.Value.PostSource;
								string str = CommentFormater.Format(format, result);
                                //放送に接続されていれば投稿
                                //されていなくても、IkaNamaウインドウのConsoleには出力される
								if (host.IsConnected)
									host.SendOwnerComment(str);
								AppendLogText(str);
							});
						}
					}

					ikanama.Open();
					AppendLogText("IkaLogへ接続中...");
				}
				catch(IkaNamaException e)
				{
					System.Windows.MessageBox.Show(e.Message);
					ikanama = null;
					return;
				}
				finally
				{
					UpdateData();
				}
			}
			else
			{
				ikanama.Dispose();
				ikanama = null;
				UpdateData();
			}
		}
	}
}
