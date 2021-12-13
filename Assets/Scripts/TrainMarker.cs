using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MarkerDirection
{
    ForShinagawa,
    ForYokohama,
    Switchback
}

public class TrainMarker : MonoBehaviour
{
    [Range(0f, 11f)] [SerializeField] private int[] _frontNumbers = new int[2];
    private MeshRenderer[] _renderers;

    void Awake()
    {
        _renderers = GetComponentsInChildren<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(ForShinagawaState(4, 0.25f, 0.5f, 3));
        //StartCoroutine(ForYokohamaState(4, 0.25f, 0.5f, 3));
        //StartCoroutine(PassageForShinagawaState(4, 0.25f, 2));
        //StartCoroutine(PassageForYokohamaState(4, 0.25f, 2));
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetColor(int frontNumber, int cars)
    {
        SetColor(frontNumber, cars, true);
    }

    void SetColor(int frontNumber, int cars, bool enable)
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            bool enableColor = enable && i >= frontNumber && i < frontNumber + cars;

            _renderers[i].material.color = enableColor ? Color.red : Color.gray;
        }
    }

    int GetFrontNuberIndex(int cars)
    {
        Dictionary<int, int> dictionary = new Dictionary<int, int>
        {
            { 4, 0 },
            { 6, 1 },
            { 8, 2 },
            { 12, 3 }
        };

        return dictionary[cars];
    }

    IEnumerator ForShinagawaState(int cars, float moveSpan, float waitSpan, int waitCount)
    {
        yield return FromYokohamaState(cars, moveSpan);
        yield return WaitState(cars, waitSpan, waitCount);
        yield return ToShinagawaState(cars, moveSpan);
    }

    IEnumerator ForYokohamaState(int cars, float moveSpan, float waitSpan, int waitCount)
    {
        yield return FromShinagawaState(cars, moveSpan);
        yield return WaitState(cars, waitSpan, waitCount);
        yield return ToYokohamaState(cars, moveSpan);
    }

    IEnumerator SwitchbackState(int cars, float moveSpan, float waitSpan, int waitCount)
    {
        yield return FromYokohamaState(cars, moveSpan);
        yield return WaitState(cars, waitSpan, waitCount);
        yield return ToYokohamaState(cars, moveSpan);
    }

    IEnumerator PassageForShinagawaState(int cars, float span, int count)
    {
        for (int i = 0; i < count; i++)
            for (int j = 1 - cars; j <= _renderers.Length; j++)
            {
                SetColor(j, cars);

                yield return new WaitForSeconds(span);
            }
    }

    IEnumerator PassageForYokohamaState(int cars, float span, int count)
    {
        for (int i = 0; i < count; i++)
            for (int j = _renderers.Length - 1; j >= -cars; j--)
            {
                SetColor(j, cars);

                yield return new WaitForSeconds(span);
            }
    }

    IEnumerator FromShinagawaState(int cars, float span)
    {
        int frontNumber = _frontNumbers[GetFrontNuberIndex(cars)];

        for (int i = _renderers.Length - 1; i >= frontNumber; i--)
        {
            SetColor(i, cars);

            yield return new WaitForSeconds(span);
        }
    }

    IEnumerator FromYokohamaState(int cars, float span)
    {
        int frontNumber = _frontNumbers[GetFrontNuberIndex(cars)];

        for (int i = 1 - cars; i <= frontNumber; i++)
        {
            SetColor(i, cars);

            yield return new WaitForSeconds(span);
        }
    }

    IEnumerator ToShinagawaState(int cars, float span)
    {
        int frontNumber = _frontNumbers[GetFrontNuberIndex(cars)];

        for (int i = frontNumber; i <= _renderers.Length; i++)
        {
            SetColor(i, cars);

            yield return new WaitForSeconds(span);
        }
    }

    IEnumerator ToYokohamaState(int cars, float span)
    {
        int frontNumber = _frontNumbers[GetFrontNuberIndex(cars)];

        for (int i = frontNumber; i >= -cars; i--)
        {
            SetColor(i, cars);

            yield return new WaitForSeconds(span);
        }
    }

    IEnumerator WaitState(int cars, float span, int count)
    {
        int frontNumber = _frontNumbers[GetFrontNuberIndex(cars)];
        bool enable = true;

        for (int i = 0; i < count; i++)
            for (int j = 0; j < 2; j++)
            {
                SetColor(frontNumber, cars, enable);

                enable = !enable;

                yield return new WaitForSeconds(span);
            }
    }
}
