using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DayType
{
    Weekday,
    Saturday,
    Holiday,
    SaturdayHoliday
}

public enum Direction
{
    Shinagawa,
    Uraga,
    HanedaAirport
}

public enum Floor
{
    Floor2nd = 2,
    Floor3rd
}

public enum NumberOfCompartments
{
    None,
    Car4,
    Car6,
    Car8,
    Car12
}

public enum RailwayLine
{
    None,
    KeikyuMainLine,
    KeikyuAirportLine,
    KeikyuDaishiLine,
    KeikyuZushiLine,
    KeikyuKurihamaLine,
    ToeiAsakusaLine = 10,
    KeiseiMainLine = 20,
    KeiseiNaritaSKYACCESSLine,
    KeiseiHigashiNaritaLine,
    KeiseiOshiageLine,
    HokusoLine = 30,
    ShibayamaLine = 40
}

public enum Station
{
    KK01Shinagawa = 1,
    KK02Kitashinagawa,
    KK03Shimbamba,
    KK04AomonoYokocho,
    KK05Samezu,
    KK06Tachiaigawa,
    KK07Omorikaigan,
    KK08Heiwajima,
    KK09Omorimachi,
    KK10Umeyashiki,
    KK11KeikyuKamata,
    KK12Kojiya,
    KK13Otorii,
    KK14AnamoriInari,
    KK15Tenkubashi,
    KK16HanedaAirportInternationalTerminal,
    KK17HanedaAirportDomesticTerminal,
    KK18Zoshiki,
    KK19Rokugodote,
    KK20KeikyuKawasaki,
    KK21Minatocho,
    KK22Suzukicho,
    KK23Kawasakidaishi,
    KK24Higashimonzen,
    KK25Sangyodoro,
    KK26Kojimashinden,
    KK27HatchoNawate,
    KK28TsurumiIchiba,
    KK29KeikyuTsurumi,
    KK30KagetsuenMae,
    KK31Namamugi,
    KK32KeikyuShinkoyasu,
    KK33Koyasu,
    KK34KanagawaShimmachi,
    KK35Nakakido,
    KK36Kanagawa,
    KK37Yokohama,
    KK38Tobe,
    KK39Hinodecho,
    KK40Koganecho,
    KK41Minamiota,
    KK42Idogaya,
    KK43Gumyoji,
    KK44Kamiooka,
    KK45Byobugaura,
    KK46Sugita,
    KK47KeikyuTomioka,
    KK48Nokendai,
    KK49KanazawaBunko,
    KK50KanazawaHakkei,
    KK51Mutsuura,
    KK52Jimmuji,
    KK53Shinzushi,
    KK54Oppama,
    KK55KeikyuTaura,
    KK56Anjinzuka,
    KK57Hemi,
    KK58Shioiri,
    KK59YokosukaChuo,
    KK60Kenritsudaigaku,
    KK61Horinouchi,
    KK62KeikyuOtsu,
    KK63Maborikaigan,
    KK64Uraga,
    KK65ShinOtsu,
    KK66Kitakurihama,
    KK67KeikyuKurihama,
    KK68YRPNobi,
    KK69KeikyuNagasawa,
    KK70Tsukuihama,
    KK71Miurakaigan,
    KK72Misakiguchi,
    A01NishiMagome = 101,
    A02Magome,
    A03Nakanobu,
    A04Togoshi,
    A05Gotanda,
    A06Takanawadai,
    A07Sengakuji,
    A08Mita,
    A09Daimon,
    A10Shimbashi,
    A11HigashiGinza,
    A12Takaracho,
    A13Nihombashi,
    A14Ningyocho,
    A15HigashiNihombashi,
    A16Asakusabashi,
    A17Kuramae,
    A18Asakusa,
    A19HonjoAzumabashi,
    A20KS45Oshiage,
    KS09Aoto = 209,
    KS10Takasago,
    KS35Sakura = 235,
    KS38Sogosando = 238,
    KS40Narita = 240,
    KS42NaritaAirportTerminal1 = 242,
    KS42NaritaAirportTerminal1ViaNaritaSKYACCESSLine = 1242,
    HS13Inzaimakinohara = 313,
    HS14ImbaNihonIdai,
    SR01ShibayamaChiyoda = 401
}

public enum Track
{
    None,
    Track1,
    Track2,
    Track3,
    Track4,
    Track5,
    Track6,
    Track7
}

public enum TrainType
{
    None,
    Local_FUTSU,
    AirportExpress_AirportKYUKO,
    LimitedExpress_TOKKYU,
    LimitedExpress_KAITOKU,
    AirportLimitedExpress_AirportKAITOKU,
    Wing,
    MorningWing,
    Rapid = 10,
    Express,
    CommuterExpress,
    AccessExpress,
    Passage = 100,
    OutOfService
}

public static class CollectionGOIS
{

    public static int GetRandomIndex<T>(T[] values)
    {
        return GetRandomIndex(values.Length);
    }

    public static int GetRandomIndex<T>(List<T> list)
    {
        return GetRandomIndex(list.Count);
    }

    public static int GetRandomIndex<T>(IEnumerable<T> values)
    {
        return GetRandomIndex(values.Count());
    }

    static int GetRandomIndex(int length)
    {
        return UnityEngine.Random.Range(0, length);
    }

    public static T GetRandomElement<T>(T[] values)
    {
        return values[GetRandomIndex(values)];
    }

    public static T GetRandomElement<T>(List<T> list)
    {
        return list[GetRandomIndex(list)];
    }

    public static T GetRandomElement<T>(IEnumerable<T> values)
    {
        return values.ElementAt(GetRandomIndex(values));
    }

    public static T GetRandomElement<T>(params IEnumerable<T>[] collections)
    {
        return GetRandomElement(collections.SelectMany(c => c));
    }
}

public static class EnumGOIS
{

    public static int GetCount<T>()
    {
        return GetEnumerable<T>().Count();
    }

    public static IEnumerable<T> GetEnumerable<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static T GetRandom<T>()
    {
        return CollectionGOIS.GetRandomElement(GetEnumerable<T>());
    }

    public static T[] GetValues<T>()
    {
        return GetEnumerable<T>().ToArray();
    }
}

[Serializable]
public class Name
{
    [SerializeField] private string _mainName, _subName;

    public string MainName
    {
        get
        {
            return _mainName;
        }
    }

    public string SubName
    {
        get
        {
            return _subName;
        }
    }
}

public static class GOIS
{
}
