using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMachine : StateMachine
{
    public enum PlayerStates { Idle, Move}

    private PlayerInputController _inputController;

    [SerializeField]
    private float _rotateSpeed = 1500;
    [SerializeField]
    private float _moveSpeed = 10;
    [SerializeField]
    private float _shootRate = 0.1f;
    private float _lastShot;

    private Vector3 _movementInput;
    private Vector3 _rotationInput;
	void Start ()
    {
        _inputController = transform.GetComponent<PlayerInputController>();
        currentState = PlayerStates.Idle;
	}
	
    protected override void EarlyGlobalSuperUpdate()
    {
        _movementInput = _inputController.GetInputs().Movement;
        _rotationInput = _inputController.GetInputs().Rotation;
    }

    protected override void LateGlobalSuperUpdate()
    {
        if (_rotationInput != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, _rotationInput), _rotateSpeed * Time.deltaTime);
        if (_inputController.GetInputs().Shoot)
            Shoot();
    }

    void Idle_EnterState()
    {

    }

    void Idle_Update()
    {
        if (_movementInput != Vector3.zero)
        {
            currentState = PlayerStates.Move;
            return;
        }
    }

    void Idle_ExitState()
    {

    }

    void Move_EnterState()
    {

    }

    void Move_Update()
    {
        if (_movementInput == Vector3.zero)
        {
            currentState = PlayerStates.Idle;
            return;
        }
        transform.position += _movementInput * _moveSpeed * Time.deltaTime;
    }

    void Move_ExitState()
    {

    }

    void Shoot()
    {
        if (_lastShot + _shootRate < Time.time)
        {
            //Debug.Log("Shoot");
            // Spawn Projectile
            _lastShot = Time.time;
        }
    }
}
