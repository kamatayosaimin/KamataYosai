using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainTypeData : TypeData<TrainType>
{
    [SerializeField] private Color _color = Color.white, _mainNameColor = Color.white, _subNameColor = Color.white;
    [SerializeField] private Name _name;

    public Color Color
    {
        get
        {
            return _color;
        }
    }

    public Color MainNameColor
    {
        get
        {
            return _mainNameColor;
        }
    }

    public Color SubNameColor
    {
        get
        {
            return _subNameColor;
        }
    }

    public Name Name
    {
        get
        {
            return _name;
        }
    }

    public static TrainTypeData[] GetDefaultDatas
    {
        get
        {
            return GetDefaultDatas(t => new TrainTypeData(t));
        }
    }

    public TrainTypeData(TrainType type)
        : base(type)
    {
    }
}

[CreateAssetMenu(menuName = "KamataYosai/TrainTypeDataManager")]
public class TrainTypeDataManager : TypeDataManager<TrainTypeData, TrainType>
{

    protected override TrainTypeData[] DefaultDatas
    {
        get
        {
            return TrainTypeData.GetDefaultDatas;
        }
    }

    protected override bool TypeEquals(TrainType dataType, TrainType otherType)
    {
        return dataType == otherType;
    }
}
