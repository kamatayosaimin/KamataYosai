using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPanelHanedaAirportElement : DisplayPanelElement
{
    [SerializeField] private Image _floorBG;
    [SerializeField] private Text _floorMainText, _floorSubText;

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

    protected override void SetFloor(FloorData floor)
    {
        _floorBG.color = floor.Color;

        _floorMainText.text = floor.Name.MainName;

        _floorSubText.text = floor.Name.SubName;
    }
}
