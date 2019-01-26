using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    int _playerNumber = 1;
    PlayerInputs _playerInputs;

	void Update ()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Move_Horizontal_P" + _playerNumber), -Input.GetAxisRaw("Move_Vertical_P" + _playerNumber), 0);
        Vector3 rotation = new Vector3(Input.GetAxisRaw("Rotate_Horizontal_P" + _playerNumber), -Input.GetAxisRaw("Rotate_Vertical_P" + _playerNumber), 0);
        bool shoot = Input.GetAxisRaw("Shoot_P" + _playerNumber) != 0;
        //Debug.Log(Input.GetAxisRaw("Shoot_P" + _playerNumber));
        //Debug.Log("Movement " + movement + "  Rotation : " + rotation);

        _playerInputs = new PlayerInputs()
        {
            Movement = movement,
            Rotation = rotation,
            Shoot = shoot
        };
	}

    public PlayerInputs GetInputs()
    {
        return _playerInputs;
    }
}

public struct PlayerInputs
{
    public Vector3 Movement;
    public Vector3 Rotation;
    public bool Shoot;
}