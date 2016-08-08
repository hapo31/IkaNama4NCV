

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Hapo31.IkaNama.Model.Config
{
	/// <summary>
	/// 設定ファイルを読みこんだり書き出したりする
	/// </summary>
	public class ConfigFileReaderWriter : Utils.XmlReaderWriter<ConfigData>
	{
		public ConfigFileReaderWriter(string filename) : base(filename) { }
	}

	/// <summary>
	/// 設定Xmlファイル定義
	/// </summary>
	[DataContract(Namespace = "", Name = "IkaNamaConfig")]
	public class ConfigData
	{
		/// <summary>
		/// Key シーン名, Value そのシーンに対する設定
		/// </summary>
		[DataMember]
		public Dictionary<string, SceneData> SceneCommentSources { get; set; }

		public ExtensionDataObject ExtensionData { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="d">元となる辞書データ</param>
		public ConfigData(Dictionary<string, SceneData> d)
		{
			SceneCommentSources = d;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ConfigData()
		{
			SceneCommentSources = new Dictionary<string, SceneData>();
		}

		/// <summary>
		/// アクセサ
		/// </summary>
		/// <param name="SceneName"></param>
		/// <returns></returns>
		public SceneData this[string SceneName]
		{
			get
			{
				return SceneCommentSources[SceneName];
			}
			set
			{
				Set(SceneName, value);
			}
		}

		/// <summary>
		/// データの変更、追加
		/// </summary>
		/// <param name="key">シーン名 キーが存在しない場合はデータに追加する</param>
		/// <param name="value">Scene classのインスタンス</param>
		/// <returns></returns>
		public bool Set(string key, SceneData value)
		{
			if (SceneCommentSources.ContainsKey(key))
			{
				return false;
			}
			else
			{
				SceneCommentSources.Add(key, value);
				return true;
			}
		}

		/// <summary>
		/// デフォルトデータを書き出す
		/// </summary>
		/// <returns></returns>
		public static ConfigData CreateDefault()
		{
			return new ConfigData(new Dictionary<string, SceneData>()
				{
					{"on_game_start", new SceneData( false, 3, "スタート！","画面に'Go!'と出た瞬間") },
					{"on_game_killed", new SceneData( true, 3, "イカを倒した！現在{kills}キル", "イカを倒した瞬間") },
					{"on_game_dead", new SceneData( false, 3, "やられた！", "死んだ瞬間(死んだ理由は取得できません)") },
					{"on_death_reason_identified", new SceneData( true, 0, "{reason}でやられた！現在{deaths}デス", "死んだ理由(on_game_deadが発生した3秒前後)") },
					{"on_game_finish", new SceneData( false, 3, "試合終了！", "画面に'Finish!'と出た瞬間")},
					{"on_result_detail", new SceneData( true, 0, "{rule}の{stage}で{weapon}を使って{kills}キル{deaths}デスで{won}ました！", "プレイヤー全員の戦績が表示されている画面") },

				});
		}
	}

	/// <summary>
	/// そのシーンの発生をトリガーとして投稿するコメントの指定
	/// </summary>
	[DataContract]
	public class SceneData
	{
		public SceneData()
		{
			IsEnabled = false;
			PostDelay = 0;
			PostSource = "";
		}
		public SceneData(bool is_enabled, int post_delay, string post_source, string discription = "")
		{
			IsEnabled = is_enabled;
			PostDelay = post_delay;
			PostSource = post_source;
			Discription = discription;
		}
		/// <summary>
		/// 有効、無効　trueの場合、そのシーンでコメントが投稿される
		/// </summary>
		[DataMember]
		public bool IsEnabled { get; set; }

		/// <summary>
		/// そのシーン発生の何秒後にコメントを投稿するか
		/// </summary>
		[DataMember]
		public int PostDelay { get; set; }

		/// <summary>
		/// 投稿するコメントの内容
		/// </summary>
		[DataMember]
		public string PostSource { get; set; }

		/// <summary>
		/// シーンの説明
		/// </summary>
		[DataMember]
		public string Discription { get; set; }
	}

}
