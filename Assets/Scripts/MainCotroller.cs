using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCotroller : MonoBehaviour
{

    enum DisplayPanel
    {
        AllTracks,
        Floor2nd,
        Floor3rd,
        ForShinagawa,
        ForYokohama,
        ForHanedaAirport
    }

    delegate void DisplayPanelSetter(DisplayPanelElement element, int track, TrainTypeData trainType, DestinationData destination);

    [SerializeField] private int _minutesSpanRange = 2;
    private DisplayPanel _displayPanel;
    [SerializeField] private UnityEngine.UI.Text _headerMainText, _headerSubText;
    [SerializeField] DepartureDataManager _depatureManager;
    [SerializeField] private DestinationDataManager _destinationManager;
    [SerializeField] private DiagramManager _diagramManager;
    private DisplayPanelHanedaAirportElement[] _hanedaAirportElements;
    private DisplayPanelHeader _header;
    [SerializeField] private DisplayPanelHeaderData _headerData;
    private DisplayPanelPlaneElement[] _planeElements;
    [SerializeField] private FloorDataManager _floorManager;
    [SerializeField] private TrainTypeDataManager _trainTypeManager;

    void Awake()
    {
        _hanedaAirportElements = GetComponentsInChildren<DisplayPanelHanedaAirportElement>(true);
        _header = GetComponentInChildren<DisplayPanelHeader>(true);
        _planeElements = GetComponentsInChildren<DisplayPanelPlaneElement>(true);
    }

    // Use this for initialization
    void Start()
    {
        SetAllTracks();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetDisplayPanel()
    {
        DisplayPanelElement[] elements = GetComponentsInChildren<DisplayPanelElement>();

        switch (_displayPanel)
        {
            case DisplayPanel.AllTracks:
                SetDisplayPanel(elements, _depatureManager.GetTrack, false);
                break;
            case DisplayPanel.Floor2nd:
                SetDisplayPanel(elements, i => _depatureManager.GetFloor(Floor.Floor2nd), true);
                break;
            case DisplayPanel.Floor3rd:
                SetDisplayPanel(elements, i => _depatureManager.GetFloor(Floor.Floor3rd), true);
                break;
            case DisplayPanel.ForShinagawa:
                SetDisplayPanel(elements, i => _depatureManager.GetDirection(Direction.Shinagawa), true);
                break;
            case DisplayPanel.ForYokohama:
                SetDisplayPanel(elements, i => _depatureManager.GetDirection(Direction.Uraga), true);
                break;
            case DisplayPanel.ForHanedaAirport:
                DisplayPanelSetter setArg = (element, track, trainType, destination) =>
                {
                    Floor floor;

                    switch (track)
                    {
                        case 1:
                            floor = Floor.Floor3rd;
                            break;
                        case 4:
                            floor = Floor.Floor2nd;
                            break;
                        default:
                            floor = default;
                            break;
                    }

                    FloorData floorData = _floorManager.GetData(floor);

                    SetDisplayPanel(element, track, trainType, destination, floorData);
                };

                SetDisplayPanel(elements, i => _depatureManager.GetDirection(Direction.HanedaAirport), true, setArg);
                break;
        }
    }

    void SetDisplayPanel(DisplayPanelElement[] elements, Func<int, DepartureData> dataSelector, bool isOrder)
    {
        SetDisplayPanel(elements, dataSelector, isOrder, SetDisplayPanel);
    }

    void SetDisplayPanel(DisplayPanelElement[] elements, Func<int, DepartureData> dataSelector, bool isOrder, DisplayPanelSetter setArg)
    {
        DateTime date = DateTime.Now;

        for (int i = 0; i < elements.Length; i++)
        {
            TrainType[] stopTypes = new[]
            {
                TrainType.Local_FUTSU,
                TrainType.AirportExpress_AirportKYUKO,
                TrainType.LimitedExpress_TOKKYU,
                TrainType.LimitedExpress_KAITOKU
            };
            DateTime settingDate = date.AddMinutes(UnityEngine.Random.Range(1, _minutesSpanRange));
            DepartureData departureData = dataSelector(i + 1);

            bool isOutOfService = departureData.Station == Station.KK11KeikyuKamata,
                isPassage = !isOutOfService && IsPassage(departureData.TrainType);
            Station station = departureData.Station;
            DestinationData destination = GetDestination(isOutOfService, isPassage, station);
            DisplayPanelElement element = elements[i];
            TrainTypeData trainType = GetTrainType(isOutOfService, isPassage, departureData.TrainType);

            setArg(element, departureData.Track, trainType, destination);

            if (isOutOfService || isPassage)
                element.SetDepartureCars();
            else
                element.SetDepartureCars(settingDate.Hour, settingDate.Minute, departureData.Cars);

            if (isOrder)
                date = settingDate;
        }
    }

    void SetDisplayPanel(DisplayPanelElement element, int track, TrainTypeData trainType, DestinationData destination)
    {
        element.SetElement(track, trainType, destination);
    }

    void SetDisplayPanel(DisplayPanelElement element, int track, TrainTypeData trainType, DestinationData destination, FloorData floor)
    {
        element.SetElement(track, trainType, destination, floor);
    }

    public void SetDisplayPanelRandom()
    {
        DisplayPanelElement[] elements = GetComponentsInChildren<DisplayPanelElement>();
        Action<bool> arg = o => SetDisplayPanel(elements, i => _depatureManager.GetRandomAll(), o);

        switch (_displayPanel)
        {
            case DisplayPanel.AllTracks:
                SetDisplayPanelRandom(elements, false);
                break;
            default:
                SetDisplayPanelRandom(elements, true);
                break;
        }
    }

    public void SetDisplayPanelRandom(DisplayPanelElement[] elements, bool isOrder)
    {
        DateTime date = DateTime.Now;

        for (int i = 0; i < elements.Length; i++)
        {
            int track = UnityEngine.Random.Range(1, 6),
                cars = CollectionGOIS.GetRandomElement(new[] { 4, 6, 8, 12 });
            DateTime settingDate = date.AddMinutes(UnityEngine.Random.Range(1, _minutesSpanRange));
            DestinationData destination = _destinationManager.GetDataRandom();
            DisplayPanelElement element = elements[i];
            FloorData floor = _floorManager.GetData(EnumGOIS.GetRandom<Floor>());
            TrainTypeData trainType = _trainTypeManager.GetData(EnumGOIS.GetRandom<TrainType>());

            SetDisplayPanel(element, track, trainType, destination, floor);

            element.SetDepartureCars(settingDate.Hour, settingDate.Minute, cars);

            if (isOrder)
                date = settingDate;
        }
    }

    public void SetAllTracks()
    {
        SetPlaneElements(DisplayPanel.AllTracks, false);
    }

    public void SetFloor2nd()
    {
        SetPlaneElements(DisplayPanel.Floor2nd, _headerData.SecondFloor);
    }

    public void SetFloor3rd()
    {
        SetPlaneElements(DisplayPanel.Floor3rd, _headerData.ThirdFloor);
    }

    public void SetForShinagawa()
    {
        SetPlaneElements(DisplayPanel.ForShinagawa, _headerData.ForShinagawa);
    }

    public void SetForYokohama()
    {
        SetPlaneElements(DisplayPanel.ForYokohama, _headerData.ForYokohama);
    }

    public void SetForHanedaAirport()
    {
        _displayPanel = DisplayPanel.ForHanedaAirport;

        foreach (var e in _hanedaAirportElements)
            e.gameObject.SetActive(true);

        _header.gameObject.SetActive(true);

        foreach (var e in _planeElements)
            e.gameObject.SetActive(false);

        SetHeader(_headerData.ForHanedaAirport);
        SetDisplayPanel();
    }

    void SetPlaneElements(DisplayPanel displayPanel, bool isHeader)
    {
        _displayPanel = displayPanel;

        int length = isHeader ? _planeElements.Length - 1 : _planeElements.Length;

        foreach (var e in _hanedaAirportElements)
            e.gameObject.SetActive(false);

        _header.gameObject.SetActive(isHeader);

        for (int i = 0; i < _planeElements.Length; i++)
            _planeElements[i].gameObject.SetActive(i < length);

        SetDisplayPanel();
    }

    void SetPlaneElements(DisplayPanel displayPanel, Name name)
    {
        SetPlaneElements(displayPanel, true);
        SetHeader(name);
    }

    void SetHeader(Name name)
    {
        _headerMainText.text = name.MainName;

        _headerSubText.text = name.SubName;
    }

    bool IsPassage(TrainType trainType)
    {
        TrainType[] stopTypes = new[]
        {
            TrainType.Local_FUTSU,
            TrainType.AirportExpress_AirportKYUKO,
            TrainType.LimitedExpress_TOKKYU,
            TrainType.LimitedExpress_KAITOKU
        };

        return !stopTypes.Any(t => t == trainType);
    }

    DestinationData GetDestination(bool isOutOfService, bool isPassage, Station station)
    {
        if (isOutOfService)
            return _destinationManager.None;

        return isPassage ? _destinationManager.Passage : _destinationManager.GetData(station);
    }

    TrainTypeData GetTrainType(bool isOutOfService, bool isPassage, TrainType trainType)
    {
        TrainType type = isOutOfService ? TrainType.OutOfService : isPassage ? TrainType.Passage : trainType;

        return _trainTypeManager.GetData(type);
    }
}
