using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotateSpeed = 1.0f;

    public void Move(Vector2 move)
    {
        Debug.Log("["+name+"] - Move X: "+move.x+", Move Y: "+move.y);
        transform.position += new Vector3(move.x, 0, move.y) * speed * Time.deltaTime;
    }

    public void Rotate(Vector2 move)
    {
        Debug.Log("["+name+"] - Rotate X: "+move.x+", Rotate Y: "+move.y);
        transform.Rotate(new Vector3(move.x, 0, move.y) * rotateSpeed  * Time.deltaTime);
    }
}
