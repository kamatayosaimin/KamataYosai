using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KamataYosai/DisplayPanelHeader")]
public class DisplayPanelHeaderData : ScriptableObject
{
    [SerializeField] private Name _secondFloor, _thirdFloor, _forShinagawa, _forYokohama, _forHanedaAirport;

    public Name SecondFloor
    {
        get
        {
            return _secondFloor;
        }
    }

    public Name ThirdFloor
    {
        get
        {
            return _thirdFloor;
        }
    }

    public Name ForShinagawa
    {
        get
        {
            return _forShinagawa;
        }
    }

    public Name ForYokohama
    {
        get
        {
            return _forYokohama;
        }
    }

    public Name ForHanedaAirport
    {
        get
        {
            return _forHanedaAirport;
        }
    }
}
