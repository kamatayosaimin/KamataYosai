using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TypeData<T>
{
    [SerializeField] private T _type;

    public T Type
    {
        get
        {
            return _type;
        }
    }

    protected static TData[] GetDefaultDatas<TData>(System.Func<T, TData> selector) where TData : TypeData<T>
    {
        return EnumGOIS.GetEnumerable<T>().Select(selector).ToArray();
    }

    public TypeData(T type)
    {
        _type = type;
    }
}

public abstract class TypeDataManager<TData, TType> : ScriptableObject where TData : TypeData<TType>
{
    [SerializeField] private TData[] _datas;

    protected abstract TData[] DefaultDatas
    {
        get;
    }

    protected abstract bool TypeEquals(TType dataType, TType otherType);

    protected TypeDataManager()
    {
        _datas = DefaultDatas;
    }

    public TData GetData(TType type)
    {
        return _datas.FirstOrDefault(d => TypeEquals(d.Type, type));
    }
}
