using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

//public class Data : ScriptableObject
//{
//    public List<Param> param = new List<Param>();

//    [System.SerializableAttribute]
//    public class Param
//    {
//        public int intValue;
//        public float floatValue;
//        public string stringValue;
//    }
//}

public class CSVToDiagram : AssetPostprocessor
{

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        string targetFile = "Assets/CSV/Data.csv",
            exportFile = "Assets/CSV/Data.asset";

        foreach (string asset in importedAssets)
        {
            // 合致しないものはスルー
            if (!targetFile.Equals(asset))
                continue;

            //// 既存のマスタを取得
            //Data data = AssetDatabase.LoadAssetAtPath<Data>(exportFile);

            //// 見つからなければ作成する
            //if (data == null)
            //{
            //    data = ScriptableObject.CreateInstance<Data>();
            //    AssetDatabase.CreateAsset((ScriptableObject)data, exportFile);
            //}
            //// 中身を削除
            //data.param.Clear();

            Diagram diagram = AssetDatabase.LoadAssetAtPath<Diagram>(exportFile);

            if (!diagram)
            {
                diagram = ScriptableObject.CreateInstance<Diagram>();

                AssetDatabase.CreateAsset(diagram, exportFile);
            }

            List<DiagramStation> stationList = new List<DiagramStation>();

            //// CSVファイルをオブジェクトへ保存
            //using (StreamReader sr = new StreamReader(targetFile))
            //{

            //    // ヘッダをやり過ごす
            //    sr.ReadLine();

            //    // ファイルの終端まで繰り返す
            //    while (!sr.EndOfStream)
            //    {
            //        string line = sr.ReadLine();
            //        string[] dataStrs = line.Split(',');

            //        // 追加するパラメータを生成
            //        Data.Param p = new Data.Param();
            //        // 値を設定する
            //        p.intValue = int.Parse(dataStrs[0]);
            //        p.floatValue = float.Parse(dataStrs[1]);
            //        p.stringValue = dataStrs[2];
            //        // 追加
            //        data.param.Add(p);
            //    }
            //}

            using (StreamReader reader = new StreamReader(targetFile, System.Text.Encoding.GetEncoding("shift_jis")))
            {
                int count = 0;
                Dictionary<string, Station> stationDictionary = new Dictionary<string, Station>() { { "品川", Station.KK01Shinagawa }, { "北品川", Station.KK02Kitashinagawa }, { "新馬場", Station.KK03Shimbamba }, { "青物横丁", Station.KK04AomonoYokocho }, { "鮫洲", Station.KK05Samezu }, { "立会川", Station.KK06Tachiaigawa }, { "大森海岸", Station.KK07Omorikaigan }, { "平和島", Station.KK08Heiwajima }, { "大森町", Station.KK09Omorimachi }, { "梅屋敷", Station.KK10Umeyashiki }, { "京急蒲田", Station.KK11KeikyuKamata }, { "糀谷", Station.KK12Kojiya }, { "大鳥居", Station.KK13Otorii }, { "穴守稲荷", Station.KK14AnamoriInari }, { "天空橋", Station.KK15Tenkubashi }, { "羽田空港国際線ターミナル", Station.KK16HanedaAirportInternationalTerminal }, { "羽田空港国内線ターミナル", Station.KK17HanedaAirportDomesticTerminal }, { "雑色", Station.KK18Zoshiki }, { "六郷土手", Station.KK19Rokugodote }, { "京急川崎", Station.KK20KeikyuKawasaki }, { "港町", Station.KK21Minatocho }, { "鈴木町", Station.KK22Suzukicho }, { "川崎大師", Station.KK23Kawasakidaishi }, { "東門前", Station.KK24Higashimonzen }, { "産業道路", Station.KK25Sangyodoro }, { "小島新田", Station.KK26Kojimashinden }, { "八丁畷", Station.KK27HatchoNawate }, { "鶴見市場", Station.KK28TsurumiIchiba }, { "京急鶴見", Station.KK29KeikyuTsurumi }, { "花月園前", Station.KK30KagetsuenMae }, { "生麦", Station.KK31Namamugi }, { "京急新子安", Station.KK32KeikyuShinkoyasu }, { "子安", Station.KK33Koyasu }, { "神奈川新町", Station.KK34KanagawaShimmachi }, { "仲木戸", Station.KK35Nakakido }, { "神奈川", Station.KK36Kanagawa }, { "横浜", Station.KK37Yokohama }, { "戸部", Station.KK38Tobe }, { "日ノ出町", Station.KK39Hinodecho }, { "黄金町", Station.KK40Koganecho }, { "南太田", Station.KK41Minamiota }, { "井土ヶ谷", Station.KK42Idogaya }, { "弘明寺", Station.KK43Gumyoji }, { "上大岡", Station.KK44Kamiooka }, { "屏風浦", Station.KK45Byobugaura }, { "杉田", Station.KK46Sugita }, { "京急富岡", Station.KK47KeikyuTomioka }, { "能見台", Station.KK48Nokendai }, { "金沢文庫", Station.KK49KanazawaBunko }, { "金沢八景", Station.KK50KanazawaHakkei }, { "六浦", Station.KK51Mutsuura }, { "神武寺", Station.KK52Jimmuji }, { "新逗子", Station.KK53Shinzushi }, { "追浜", Station.KK54Oppama }, { "京急田浦", Station.KK55KeikyuTaura }, { "安針塚", Station.KK56Anjinzuka }, { "逸見", Station.KK57Hemi }, { "汐入", Station.KK58Shioiri }, { "横須賀中央", Station.KK59YokosukaChuo }, { "県立大学", Station.KK60Kenritsudaigaku }, { "堀ノ内", Station.KK61Horinouchi }, { "京急大津", Station.KK62KeikyuOtsu }, { "馬堀海岸", Station.KK63Maborikaigan }, { "浦賀", Station.KK64Uraga }, { "新大津", Station.KK65ShinOtsu }, { "北久里浜", Station.KK66Kitakurihama }, { "京急久里浜", Station.KK67KeikyuKurihama }, { "ＹＲＰ野比", Station.KK68YRPNobi }, { "京急長沢", Station.KK69KeikyuNagasawa }, { "津久井浜", Station.KK70Tsukuihama }, { "三浦海岸", Station.KK71Miurakaigan }, { "三崎口", Station.KK72Misakiguchi }, { "西馬込", Station.A01NishiMagome }, { "馬込", Station.A02Magome }, { "中延", Station.A03Nakanobu }, { "戸越", Station.A04Togoshi }, { "五反田", Station.A05Gotanda }, { "高輪台", Station.A06Takanawadai }, { "泉岳寺", Station.A07Sengakuji }, { "三田", Station.A08Mita }, { "大門", Station.A09Daimon }, { "新橋", Station.A10Shimbashi }, { "東銀座", Station.A11HigashiGinza }, { "宝町", Station.A12Takaracho }, { "日本橋", Station.A13Nihombashi }, { "人形町", Station.A14Ningyocho }, { "東日本橋", Station.A15HigashiNihombashi }, { "浅草橋", Station.A16Asakusabashi }, { "蔵前", Station.A17Kuramae }, { "浅草", Station.A18Asakusa }, { "本所吾妻橋", Station.A19HonjoAzumabashi }, { "押上", Station.A20KS45Oshiage } };

                while (!reader.EndOfStream)
                {
                    string firstLine = reader.ReadLine(),
                        secondLine = reader.ReadLine();
                    string[] firstSources = firstLine.Split(','),
                        secondSources = string.IsNullOrEmpty(secondLine) ? new string[4] : secondLine.Split(',');

                    string key = firstSources[0];

                    Debug.Log("Read : " + count + " / " + key);

                    count++;

                    if (!stationDictionary.ContainsKey(key))
                        continue;

                    Station station = stationDictionary[key];

                    Debug.Log("Station : " + station);

                    if (firstSources[3] == "レ")
                    {
                        stationList.Add(new DiagramStation(station));

                        continue;
                    }

                    stationList.Add(new DiagramStation(station, GetTime(firstSources[3]), GetTime(secondSources[3])));
                }
            }

            Debug.Log("Count : " + stationList.Count);

            diagram.SetStations(stationList.ToArray());

            // 保存
            AssetDatabase.SaveAssets();

            Debug.Log("Data updated.");
        }
    }

    static DiagramTime GetTime(string source)
    {
        if (string.IsNullOrEmpty(source))
            return new DiagramTime();

        string[] splited = source.Split(':', '着', '発');

        return new DiagramTime(int.Parse(splited[0]), int.Parse(splited[1]));
    }
}
