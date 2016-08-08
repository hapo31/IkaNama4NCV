using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hapo31.IkaLogProxy.Util
{
	public class SplatoonMatchResult
	{
		//ステージ名
		public string Stage { get; set; } = "どこか";
		//ブキ名
		public string Weapon { get; set; } = "なにか";
		//ルール
		public string Rule { get; set; } = "イカのゲーム";
		//勝ち負け
		public string Won { get; set; } = "楽しみ";
		//ナワバリバトルのスコア
		public int Score { get; set; } = 0;
		//キル数
		public int Kills { get; set; } = 0;
		//デス数
		public int Deaths { get; set; } = 0;
		//直前の死んだ理由
		public string LastDeadReason { get; set; } = "";
	}
}
