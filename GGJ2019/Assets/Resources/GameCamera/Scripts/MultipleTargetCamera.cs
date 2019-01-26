using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    Transform[] targets;

    [SerializeField]
    float boundingBoxPadding = 5f;

    [SerializeField]
    float minimumOrthographicSize = 6f;

    [SerializeField]
    float zoomSpeed = 2f;

    Camera camera;

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
    }

    void LateUpdate()
    {
        Rect boundingBox = CalculateTargetsBoundingBox();
        transform.position = CalculateCameraPosition(boundingBox);
        camera.orthographicSize = CalculateOrthographicSize(boundingBox);
    }

    /// <summary>
    /// Calculates a bounding box that contains all the targets.
    /// </summary>
    /// <returns>A Rect containing all the targets.</returns>
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
        minX = minX - boundingBoxPadding;
        if (Mathf.Abs(minX) > World.Instance.MaxRadius.x)
            minX = minX < 0 ? -World.Instance.MaxRadius.x : World.Instance.MaxRadius.x;
        maxX = maxX + boundingBoxPadding;
        if (Mathf.Abs(maxX) > World.Instance.MaxRadius.x)
            maxX = maxX < 0 ? -World.Instance.MaxRadius.x : World.Instance.MaxRadius.x;


        minY = minY - boundingBoxPadding;
        if (Mathf.Abs(minY) > World.Instance.MaxRadius.y)
            minY = minY < 0 ? -World.Instance.MaxRadius.y : World.Instance.MaxRadius.y;
        maxY = maxY + boundingBoxPadding;
        if (Mathf.Abs(maxY) > World.Instance.MaxRadius.y)
            maxY = maxY < 0 ? -World.Instance.MaxRadius.y : World.Instance.MaxRadius.y;
        return Rect.MinMaxRect(minX, maxY, maxX, minY);
    }

    /// <summary>
    /// Calculates a camera position given the a bounding box containing all the targets.
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all targets.</param>
    /// <returns>A Vector3 in the center of the bounding box.</returns>
    Vector3 CalculateCameraPosition(Rect boundingBox)
    {
        Vector2 boundingBoxCenter = boundingBox.center;

        return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, camera.transform.position.z);
    }

    /// <summary>
    /// Calculates a new orthographic size for the camera based on the target bounding box.
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all targets.</param>
    /// <returns>A float for the orthographic size.</returns>
    float CalculateOrthographicSize(Rect boundingBox)
    {
        float orthographicSize = camera.orthographicSize;
        Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
        Vector3 topRightAsViewport = camera.WorldToViewportPoint(topRight);

        if (topRightAsViewport.x >= topRightAsViewport.y)
            orthographicSize = Mathf.Abs(boundingBox.width) / camera.aspect / 2f;
        else
            orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

        return Mathf.Clamp(Mathf.Lerp(camera.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
    }
}
