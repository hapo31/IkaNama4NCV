using System.Collections.Generic;

namespace Hapo31.IkaLogProxy.Utils
{
    static class SplatoonTranslates
    {
        static private readonly Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            {"shionome", "シオノメ油田"},
            {"mongara", "モンガラキャンプ場"},
            {"hakofugu", "ハコフグ倉庫"},
            {"dekaline", "デカライン高架下"},
            {"kinmedai", "キンメダイ美術館"},
            {"tachiuo", "タチウオパーキング"},
            {"arowana", "アロワナモール"},
            {"hirame", "ヒラメが丘団地"},
            {"masaba", "マサバ海峡大橋"},
            {"negitoro", "ネギトロ炭鉱"},
            {"hokke", "ホッケふ頭"},
            {"mozuku", "モズク農園"},
            {"bbass", "Bバスパーク"},
            {"mahimahi", "マヒマヒリゾート&スパ"},
            {"anchovy", "アンチョビットゲームズ"},
            {"shottsuru", "ショッツル鉱山" },

            {"nawabari", "ナワバリバトル"},
            {"yagura", "ガチヤグラ"},
            {"hoko", "ガチホコバトル"},
            {"area", "ガチエリア"},


            {"sshooter", "スプラシューター"},
            {"heroshooter_replica", "ヒーローシューターレプリカ"},
            {"sshooter_collabo", "スプラシューターコラボ"},
            {"octoshooter_replica", "オクタシューターレプリカ"},

            {"splatcharger", "スプラチャージャー"},
            {"herocharger_replica", "ヒーローチャージャーレプリカ"},
            {"splatscope", "スプラスコープ"},
            {"splatcharger_wakame", "スプラチャージャーワカメ"},
            {"splatscope_wakame", "スプラスコープワカメ"},

            {"liter3k", "リッター3K"},
            {"liter3k_scope", "3Kスコープ"},
            {"liter3k_custom", "リッター3Kカスタム"},
            {"liter3k_scope_custom", "3Kスコープカスタム"},

            {"hydra", "ハイドラント"},
            {"hydra_custom", "ハイドラントカスタム"},

            {"promodeler_mg", "プロモデラーMG"},
            {"promodeler_rg", "プロモデラーRG"},

            {"hokusai", "ホクサイ"},
            {"hokusai_hue", "ホクサイ"},

            {"bold", "ボールドマーカー"},
            {"bold_neo", "ボールドマーカーネオ"},
            
            {"rapid", "ラピッドブラスター"},
            {"rapid_deco", "ラピッドブラスターデコ"},

            {"barrelspinner", "バレルスピナー"},
            {"barrelspinner_deco", "バレルスピナーデコ"},

            {"dualsweeper", "デュアルスイーパー"},
            {"dualsweeper_custom", "デュアルスイーパーカスタム"},

            {"splatroller", "スプラローラー"},
            {"splatroller_collabo", "スプラローラーコラボ"},
            {"heroroller_replica", "ヒーローローラーレプリカ"},

            {"hotblaster", "ホットブラスター"},
            {"hotblaster_custom", "ホットブラスターカスタム"},

            {"52gal", "52ガロン"},
            {"52gal_deco", "52ガロンデコ"},

            {"sharp", "シャープマーカー"},
            {"sharp_neo", "シャープマーカーネオ"},

            {"bamboo14mk1", "14式竹筒銃・甲"},
            {"bamboo14mk2", "14式竹筒銃・乙"},

            {"splatspinner", "スプラスピナー"},
            {"splatspinner_collabo", "スプラスピナーコラボ"},

            {"squiclean_a", "スクイックリンα"},
            {"squiclean_b", "スクイックリンβ"},

            {"jetsweeper", "ジェットスイーパー"},
            {"jetsweeper_custom", "ジェットスイーパーカスタム"},

            {"96gal", "96ガロン"},
            {"96gal_deco", "96ガロンデコ"},

            {"rapid_elite", "Rブラスターエリート"},
            {"rapid_elite_deco", "Rブラスターエリートデコ"},

            {"bucketslosher", "バケットスロッシャー"},
            {"bucketslosher_deco", "バケットスロッシャーデコ"},

            {"wakaba", "わかばシューター"},
            {"momiji", "もみじシューター"},

            {"h3reelgun", "H3リールガン"},
            {"h3reelgun_d", "H3リールガンD"},

            {"l3reelgun", "L3リールガン"},
            {"l3reelgun_d", "L3リールガンD"},

            {"dynamo", "ダイナモローラー"},
            {"dynamo_tesla", "ダイナモローラーテスラ"},

            {"longblaster", "ロングブラスター"},
            {"longblaster_custom", "ロングブラスターカスタム"},

            {"hissen", "ヒッセン"},
            {"hissen_hue", "ヒッセン・ヒュー"},

            {"nova", "ノヴァブラスター"},
            {"nova_neo", "ノヴァブラスターネオ"},

            {"pablo", "パブロ"},
            {"pablo_hue", "パブロ・ヒュー"},

            {"prime", "プライムシューター"},
            {"prime_collabo", "プライムシューターコラボ"},

            {"carbon", "カーボンローラー"},
            {"carbon_deco", "カーボンローラーデコ"},

            {"screwslosher", "スクリュースロッシャー"},
            {"screwslosher_neo", "スクリュースロッシャーネオ"},

            {"nzap85", "N-ZAP85"},
            {"nzap87", "N-ZAP87"},

            {"splashbomb", "スプラッシュボム"},
            {"sprinkler", "スプリンクラー"},
            {"jumpbeacon", "ジャンプビーコン"},
            {"quickbomb", "クイックボム"},
            {"splashshield", "スプラッシュシールド"},
            {"kyubanbomb", "キューバンボム"},
            {"pointsensor", "ポイントセンサー"},
            {"trap", "トラップ"},
            {"poison", "ポイズンボール"},
            {"chasebomb", "チェイスボム"},

            {"supershot", "スーパーショット"},
            {"tornado", "トルネード"},
            {"daioika", "ダイオウイカ"},
            {"megaphone", "メガホンレーザー"},
            {"bombrush", "ボムラッシュ"},
            {"supersensor", "スーパーセンサー"},
            {"barrier", "バリア"},
            {"hoko_shot", "ガチホコショット"},
            {"hoko_barrier", "ガチホコバリア"},
            {"hoko_inksplode", "ガチホコの爆発"},
			
			//ブキチセレクションvol1
			{"dynamo_burned", "ダイナモローラーバーンド"},
            {"bamboo14mk3", "14式竹筒銃・丙" },
            {"squiclean_g", "スクイックリンγ" },
            {"splatspinner_repair", "スプラスピナーリペア" },
            {"pablo_permanent", "パーマネントパブロ" },
            {"sshooter_wasabi", "スプラシューターワサビ" },
            {"prime_berry", "プライムシューターベリー" },
            {"bucketslosher_soda", "バケットスロッシャーソーダ" },

            //ブキチセレクションvol2
            { "splatroller_corocoro", "スプラローラーコロコロ" },
            { "splatcharger_bento","スプラチャージャーベントー"},
            { "splatscope_bento","スプラスコープベントー"},
            { "barrelspinner_remix","バレルスピナーリミックス"},
            { "nzap89","N-ZAP89"},
            { "longblaster_necro","ロングブラスターネクロ"},
            { "bold_7","ボールドマーカー7" },
            { "promodeler_pg", "プロモデラーPG"},
        };

        static public string ToJapanese(string key)
        {
            string r;
            if (dic.TryGetValue(key, out r))
                return r;
            else
                return key;
        }
    }
}
