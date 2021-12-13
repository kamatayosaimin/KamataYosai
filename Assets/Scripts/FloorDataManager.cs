using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorData : TypeData<Floor>
{
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private Name _name;

    public Color Color
    {
        get
        {
            return _color;
        }
    }

    public static FloorData[] GetDefaultDatas
    {
        get
        {
            return GetDefaultDatas(f => new FloorData(f));
        }
    }

    public Name Name
    {
        get
        {
            return _name;
        }
    }

    public FloorData(Floor floor)
        : base(floor)
    {
    }
}

[CreateAssetMenu(menuName = "KamataYosai/FloorDataManager")]
public class FloorDataManager : TypeDataManager<FloorData, Floor>
{

    protected override FloorData[] DefaultDatas
    {
        get
        {
            return FloorData.GetDefaultDatas;
        }
    }

    protected override bool TypeEquals(Floor dataType, Floor otherType)
    {
        return dataType == otherType;
    }
}
