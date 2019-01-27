using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour {
    Transform[] targets;

    [SerializeField]
    float boundingBoxPadding = 5f;

    [SerializeField]
    float minimumOrthographicSize = 6f;
    [SerializeField]
    float maximumOrhographicSize = 12f;

    [SerializeField]
    float zoomSpeed = 5f;

    Camera camera;
    float targetedSize;

    private void Start()
    {
        targets = new Transform[World.Instance.Players.Length];
        for (int i = 0; i < World.Instance.Players.Length; i++)
            targets[i] = World.Instance.Players[i].transform;
    }

    void Awake()
    {
        camera = GetComponent<Camera>();
        camera.orthographic = true;
        targetedSize = camera.orthographicSize;
        Rect cameraBoundary = GetCameraBoundary();
        World.Instance.Boundary = cameraBoundary;
    }

    void LateUpdate()
    {
        targetedSize = camera.orthographicSize;
        Rect targetsBoundary = CalculateTargetsBoundingBox();
        Rect cameraBoundary = GetCameraBoundary();
        World.Instance.Boundary = cameraBoundary;
        if (cameraBoundary.min.y - targetsBoundary.min.y < -boundingBoxPadding && cameraBoundary.max.y - targetsBoundary.max.y > boundingBoxPadding && cameraBoundary.min.x - targetsBoundary.min.x < -boundingBoxPadding && cameraBoundary.max.x - targetsBoundary.max.x > boundingBoxPadding)
            targetedSize -= 1;
        else if (cameraBoundary.max.x - targetsBoundary.max.x < 2 || cameraBoundary.min.x - targetsBoundary.min.x > -2 || cameraBoundary.max.y - targetsBoundary.max.y < 2 || cameraBoundary.min.y - targetsBoundary.min.y > -2)
            targetedSize += 1;
        
        if (targetedSize != camera.orthographicSize && targetedSize <= maximumOrhographicSize)
            camera.orthographicSize = Mathf.Clamp(Mathf.Lerp(camera.orthographicSize, targetedSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, maximumOrhographicSize);
    }

    Rect GetCameraBoundary()
    {
        Vector3 v3Pos = camera.ViewportToWorldPoint(Vector3.zero);
        Rect boundary = Rect.MinMaxRect(v3Pos.x, v3Pos.y, v3Pos.x + camera.orthographicSize * camera.aspect * 2f, v3Pos.y + camera.orthographicSize * 2f);
        return boundary;
    }

    Rect CalculateTargetsBoundingBox()
    {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        foreach (Transform target in targets)
        {
            Vector3 position = target.position;

            minX = Mathf.Min(minX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxX = Mathf.Max(maxX, position.x);
            maxY = Mathf.Max(maxY, position.y);
        }
        return Rect.MinMaxRect(minX, minY, maxX, maxY);
    }
}
