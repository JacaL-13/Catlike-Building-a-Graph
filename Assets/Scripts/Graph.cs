using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 500)]
    int resolution = 10;

    Transform[] points;

    // [SerializeField]
    // enum FunctionName
    // {
    //     Wave,
    //     MultiWave,
    //     Ripple
    // }

    // [SerializeField]
    // FunctionName functionName;

	[SerializeField, Range(0, 2)]
	int function;

    void Awake()
    {
        float step = 2f / resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step;

        points = new Transform[resolution];

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i] = Instantiate(pointPrefab);

            position.x = (i + 0.5f) * step - 1f;

            point.localPosition = position;
            point.localScale = scale;

            point.SetParent(transform, false);
        }
    }

    void Update()
    {
		FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
		
        float time = Time.time;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, time);
            point.localPosition = position;
        }
    }
}
