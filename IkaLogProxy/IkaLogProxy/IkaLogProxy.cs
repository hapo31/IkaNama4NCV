using System;
using WebSocket4Net;
using Codeplex.Data;

using System.Collections.Generic;
using Hapo31.IkaLogProxy.Util;


namespace Hapo31.IkaLogProxy.Core
{
	public class Connecter : IDisposable
	{
		private WebSocket ws;
		private byte[] buffer = new byte[128];

		private SplatoonMatchResult result;

		public SplatoonMatchResult LastButtleResult
		{
			get { return result; }
		}

		//IkaLogイベントハンドラのリスト
		public Dictionary<string, EventHandler> Events { get; set; } = new Dictionary<string, EventHandler>();

		public WebSocket WebSocket { get { return ws; } }
		public bool Opened { get; private set; } = false;

		public bool Disposed { get; set; } = true;

		public bool Waiting
		{
			get
			{
				return !Disposed && !Opened;
			}
		}

		/// <summary>
		/// ホストとポート番号を指定して初期化
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		public Connecter(string host, int port)
		{
			Connect(host, port);
		}

		public Connecter() { }

		public void Dispose()
		{
			if (!Disposed)
			{
				if (ws != null)
				{
					Opened = false;
					ws.Close();
					ws = null;
				}
				Disposed = true;
			}
		}

		public void Connect(string host = "127.0.0.1", int port = 9090)
		{
			ws = new WebSocket(String.Format("ws://{0}:{1}/ws", host, port), origin: "ikanama.ballade.jp");

			ws.Opened += (s, m) => { Opened = true; };
			ws.Closed += (s, m) => { Opened = false; };
			ws.Error += (s, m) => { Opened = false; };
			Disposed = false;
		}

		/// <summary>
		/// WebSocketsを有効化してIkaLogに接続する
		/// </summary>
		public void Open()
		{
			//メッセージ受信時の処理
			ws.MessageReceived += (s, m) =>
			{
				var json = DynamicJson.Parse(m.Message);
				string Event;

				try
				{
					Event = json.@event;
				}
				catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException )
				{
					//eventの通知ではないとき
					return;
				}
				if (result == null)
					result = new SplatoonMatchResult();

				//イベントの処理をする
				EventDispatch(json);

			};
			ws.Open();
		}

		/// <summary>
		/// イベントハンドラを起動する
		/// </summary>
		/// <param name="json">IkaLogから送信されたjson</param>
		private void EventDispatch(dynamic json)
		{
			string Event = json.@event;
			switch (Event)
			{
				//ゲーム開始
				case "on_game_start":
					result = new SplatoonMatchResult();
					result.Stage = Utils.SplatoonTranslates.ToJapanese(json.stage);
					result.Rule = Utils.SplatoonTranslates.ToJapanese(json.rule);
					break;
				//イカを倒した
				case "on_game_killed":
					result.Kills += 1;
					break;
				//倒された
				case "on_game_dead":
					result.Deaths += 1;
					break;
				//死んだ理由
				case "on_death_reason_identified":
					result.LastDeadReason = Utils.SplatoonTranslates.ToJapanese(json.reason);
					break;
				//試合結果
				case "on_result_detail":
					result.Weapon = Utils.SplatoonTranslates.ToJapanese(json.weapon);
					result.Kills = (int)json.kills;
					result.Deaths = (int)json.deaths;
					result.Won = json.won ? "勝ち" : "負け";
					result.Score = (int?)json.score ?? 0;
					break;
			}
			if (Events.ContainsKey(Event))
			{
				Events[Event](result, new EventArgs());
			}
		}

	}
}
