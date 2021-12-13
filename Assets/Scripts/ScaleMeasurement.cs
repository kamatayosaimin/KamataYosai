using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMeasurement : MonoBehaviour
{
    private Quaternion _hitRotate, _addRotation;
    private Vector3? _hitPoint;
    [SerializeField] private Transform _originPoint, _rayPoint;

    void Awake()
    {
        if (!_rayPoint)
            _rayPoint = transform;
    }

    // Use this for initialization
    void Start()
    {
        if (!_originPoint)
            return;

        Ray ray = new Ray(_rayPoint.position, _originPoint.position - _rayPoint.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _hitRotate = Quaternion.identity;
            _addRotation = Random.rotation;
            _hitPoint = hit.point;

            Debug.Log("HorieYoshiko : " + Vector3.Distance(_originPoint.position, hit.point));
        }
        else
            Debug.Log("KumaStar");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hitPoint.HasValue)
            return;

        System.Action<Vector3, Color> lineArg = (d, c) =>
        {
            d = _hitRotate * d;

            Debug.DrawLine(_hitPoint.Value - d, _hitPoint.Value + d, c);
        };

        lineArg(Vector3.right, Color.red);
        lineArg(Vector3.up, Color.green);
        lineArg(Vector3.forward, Color.blue);

        _hitRotate = _addRotation * _hitRotate;
    }
}
