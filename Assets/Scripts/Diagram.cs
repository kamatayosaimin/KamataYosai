using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KamataYosai/Diagram")]
public class Diagram : ScriptableObject
{
    [SerializeField] private Station _departureStation, _destinationStation;
    [SerializeField] private DiagramStation[] _stations;

#if UNITY_EDITOR
    public void SetStations(DiagramStation[] stations)
    {
        _stations = stations;
    }
#endif
}

[Serializable]
public class DiagramStation
{
    [SerializeField] private bool _isStop;
    [SerializeField] private NumberOfCompartments _changeNumberOfCompartments;
    [SerializeField] private RailwayLine _changeLine;
    [SerializeField] private Station _station;
    [SerializeField] private Track _track;
    [SerializeField] private TrainType _changeType;
    [SerializeField] private DiagramTime _arrivalTime, _departureTime, _passageTime;

    public DiagramStation(Station station)
    {
        _isStop = false;
        _station = station;
    }

    public DiagramStation(Station station, DiagramTime arrivalTime, DiagramTime depatureTime)
    {
        _isStop = true;
        _station = station;
        _arrivalTime = arrivalTime;
        _departureTime = depatureTime;
    }
}

[Serializable]
public class DiagramTime
{
    [SerializeField] [DiagramTime(24)] private int _hours = -1;
    [SerializeField] [DiagramTime(60)] private int _minutes = -1, _seconds = -1;

    public DiagramTime()
    {
    }

    public DiagramTime(int hours, int minutes)
    {
        _hours = hours;
        _minutes = minutes;
    }
}

public class DiagramTimeAttribute : PropertyAttribute
{
    private int[] _values;
    private GUIContent[] _options;

    public int[] Values
    {
        get
        {
            return _values;
        }
    }

    public GUIContent[] Options
    {
        get
        {
            return _options;
        }
    }

    public DiagramTimeAttribute(int range)
    {
        range += 1;

        _values = new int[range];
        _options = new GUIContent[range];

        _values[0] = -1;
        _options[0] = new GUIContent("None");

        for (int i = 1; i < range; i++)
        {
            int value = i - 1;

            _values[i] = value;
            _options[i] = new GUIContent(value.ToString("00"));
        }
    }
}
