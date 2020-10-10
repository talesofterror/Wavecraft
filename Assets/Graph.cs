﻿using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;
    Transform[] points;

    [Range (10, 100)] public int resolution = 10;

    private void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.z = 0f;
        position.y = 0f;

        points = new Transform[resolution];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
             // position.y = position.x * position.x * position.x; // FUNCTION
             // replacing this with a float deifnition at the beginning of this method
             // for purposes of animation. 
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            // position.y = Mathf.Sin(Mathf.PI * (position.x * Time.time));
            position.y = position.x * Mathf.Sin((Time.time * 10) / position.x);
            point.localPosition = position; 
        }
    }
}
