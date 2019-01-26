using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMachine : StateMachine
{
    public enum PlayerStates { Idle, Move, Die, Respawn}

    private PlayerInputController _inputController;

    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private GameObject projectilePrefab;
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

    }

    void Idle_EnterState()
    {

    }

    void Idle_Update()
    {
        if (_rotationInput != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, _rotationInput), _rotateSpeed * Time.deltaTime);
        if (_inputController.GetInputs().Shoot)
            Shoot();
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
        if (_rotationInput != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, _rotationInput), _rotateSpeed * Time.deltaTime);
        if (_inputController.GetInputs().Shoot)
            Shoot();
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

    void Die_EnterState()
    {

    }

    void Die_Update()
    {

    }

    void Die_ExitState()
    {

    }

    void Respawn_EnterState()
    {

    }

    void Respawn_Update()
    {

    }

    void Respawn_ExitState()
    {

    }

    void Shoot()
    {
        if (_lastShot + _shootRate < Time.time)
        {
            //Debug.Log("Shoot");
            Instantiate(projectilePrefab, muzzle.position, transform.rotation);
            IEntity entity = GetComponent<IEntity>();
            if (entity != null)
                Color.ChangeGameObjectColor(this.gameObject, entity.GetColor());
            // Spawn Projectile
            _lastShot = Time.time;
        }
    }
}
