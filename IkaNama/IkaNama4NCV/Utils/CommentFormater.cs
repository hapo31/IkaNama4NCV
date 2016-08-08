using Hapo31.IkaLogProxy.Util;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hapo31.IkaNama.Utils
{
	/// <summary>
	/// 独自のフォーマット指定子を置換する
	/// </summary>
	class CommentFormater
	{
		public static string Format(string format, SplatoonMatchResult spresult)
		{
			//{hoge}を抽出	
			var regex = new Regex(@"{\s*[^}]\w*[^{]\s*}");
			string r = regex.Replace(format, new MatchEvaluator((Match m) =>
			{
				string s = m.Value.Replace(" ", "").Replace("{", "").Replace("}", "");

				switch(s)
				{
					case "weapon":
						return spresult.Weapon;
					case "reason":
						return spresult.LastDeadReason;
					case "kills":
						return spresult.Kills.ToString();
					case "deaths":
						return spresult.Deaths.ToString();
					case "rule":
						return spresult.Rule;
					case "stage":
						return spresult.Stage;
					case "won":
						return spresult.Won;
					default:
						return "";
				}
			}));
			return r;
		}
	}
}
