using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DisplayPanelElement : DisplayPanelElementBase
{
    private int _track, _cars;
    [SerializeField] private Image _trainBG;
    [SerializeField] private Text _trackText, _trainMainText, _trainSubText, _destinationMainText, _destinationSubText, _departureText, _carsText;
    private Station _station;
    private TrainType _trainType;

    protected abstract void SetFloor(FloorData floor);

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void SetElement(int track, TrainTypeData trainType, DestinationData destination)
    {
        _trackText.text = track.ToString();

        _trainBG.color = trainType.Color;

        _trainMainText.text = trainType.Name.MainName;
        _trainMainText.color = trainType.MainNameColor;

        _trainSubText.text = trainType.Name.SubName;
        _trainSubText.color = trainType.SubNameColor;

        _destinationMainText.text = destination.Name.MainName;

        _destinationSubText.text = destination.Name.SubName;
    }

    public void SetElement(int track, TrainTypeData trainType, DestinationData destination, FloorData floor)
    {
        SetElement(track, trainType, destination);
        SetFloor(floor);
    }

    public void SetDepartureCars()
    {
        SetDepartureCars(null, null);
    }

    public void SetDepartureCars(int hours, int minutes, int cars)
    {
        SetDepartureCars(hours.ToString("00") + ":" + minutes.ToString("00"), cars + "両");
    }

    void SetDepartureCars(string departure, string cars)
    {
        _departureText.text = departure;

        _carsText.text = cars;
    }
}
