using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DepartureData
{
    [SerializeField] private int _track, _cars;
    [SerializeField] private Station _station;
    [SerializeField] private TrainType _tarinType;

    public int Track
    {
        get
        {
            return _track;
        }
    }

    public int Cars
    {
        get
        {
            return _cars;
        }
    }

    public Station Station
    {
        get
        {
            return _station;
        }
    }

    public TrainType TrainType
    {
        get
        {
            return _tarinType;
        }
    }
}

[CreateAssetMenu(menuName = "KamataYosai/DepartureDataManager")]
public class DepartureDataManager : ScriptableObject
{
    [SerializeField] DepartureData[] _track1Yokohama, _track1HanedaAirport, _track2, _track3, _track4Shinagawa, _track4HanedaAirport, _track5, _track6;

    public DepartureData GetTrack(int track)
    {
        switch (track)
        {
            case 1:
                return GetRandom(_track1Yokohama, _track1HanedaAirport);
            case 2:
                return GetRandom(_track2);
            case 3:
                return GetRandom(_track3);
            case 4:
                return GetRandom(_track4Shinagawa, _track4HanedaAirport);
            case 5:
                return GetRandom(_track5);
            case 6:
                return GetRandom(_track6);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public DepartureData GetFloor(Floor floor)
    {
        switch (floor)
        {
            case Floor.Floor2nd:
                return GetRandom(_track4Shinagawa, _track4HanedaAirport, _track5, _track6);
            case Floor.Floor3rd:
                return GetRandom(_track1Yokohama, _track1HanedaAirport, _track2, _track3);
            default:
                throw new ArgumentException();
        }
    }

    public DepartureData GetDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Shinagawa:
                return GetRandom(_track4Shinagawa, _track5, _track6);
            case Direction.Uraga:
                return GetRandom(_track1Yokohama, _track2, _track3);
            case Direction.HanedaAirport:
                return GetRandom(_track1HanedaAirport, _track4HanedaAirport);
            default:
                throw new ArgumentException();
        }
    }

    public DepartureData GetRandomAll()
    {
        return GetRandom(_track1Yokohama, _track1HanedaAirport, _track2, _track3, _track4Shinagawa, _track4HanedaAirport, _track5, _track6);
    }

    DepartureData GetRandom(DepartureData[] datas)
    {
        return CollectionGOIS.GetRandomElement(datas);
    }

    DepartureData GetRandom(params IEnumerable<DepartureData>[] collections)
    {
        return CollectionGOIS.GetRandomElement(collections);
    }
}
