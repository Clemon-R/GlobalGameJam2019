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
    bool refresh = false;

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
        targetedSize = Mathf.Round(camera.orthographicSize);
    }

    void LateUpdate()
    {
        targetedSize = camera.orthographicSize;
        Rect boundingBox = CalculateTargetsBoundingBox();
        Vector3 v3Pos = camera.ViewportToWorldPoint(Vector3.zero);
        Rect boundary = Rect.MinMaxRect(v3Pos.x, v3Pos.y, v3Pos.x + camera.orthographicSize * camera.aspect * 2f, v3Pos.y + camera.orthographicSize * 2f);
        World.Instance.Boundary = boundary;
        float change = camera.orthographicSize;
        if (boundary.min.y - boundingBox.min.y < -boundingBoxPadding && boundary.max.y - boundingBox.max.y > boundingBoxPadding && boundary.min.x - boundingBox.min.x < -boundingBoxPadding && boundary.max.x - boundingBox.max.x > boundingBoxPadding)
        {
            targetedSize -= 1;
        }else if (boundary.max.x - boundingBox.max.x < 2 || boundary.min.x - boundingBox.min.x > -2 || boundary.max.y - boundingBox.max.y < 2 || boundary.min.y - boundingBox.min.y > -2)
        {
            targetedSize += 1;
        }
        if (targetedSize <= maximumOrhographicSize)
            camera.orthographicSize = Mathf.Clamp(Mathf.Lerp(camera.orthographicSize, targetedSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, maximumOrhographicSize);
        else
            targetedSize = camera.orthographicSize;
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
