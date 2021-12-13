using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DestinationData
{
    [SerializeField] private Station _station;
    [SerializeField] private Name _name;

    public Station Station
    {
        get
        {
            return _station;
        }
    }

    public Name Name
    {
        get
        {
            return _name;
        }
    }
}

[CreateAssetMenu(menuName = "KamataYosai/DestinationDataManager")]
public class DestinationDataManager : ScriptableObject
{
    [SerializeField] private DestinationData _none, _passage;
    [SerializeField] private DestinationData[] _datas, _otherDatas;

    public DestinationData None
    {
        get
        {
            return _none;
        }
    }

    public DestinationData Passage
    {
        get
        {
            return _passage;
        }
    }

    public DestinationData GetData(Station station)
    {
        return _datas.FirstOrDefault(d => d.Station == station);
    }

    public DestinationData GetDataRandom()
    {
        DestinationData[] others = new[]
        {
            _none,
            _passage
        };

        return CollectionGOIS.GetRandomElement(_datas, _otherDatas, others);
    }
}
