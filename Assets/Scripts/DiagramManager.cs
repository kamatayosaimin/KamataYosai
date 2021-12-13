using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KamataYosai/DiagramManager")]
public class DiagramManager : ScriptableObject
{
    [SerializeField] private Diagram[] _weekdayDiagrams, _holidayDiagrams;
}
