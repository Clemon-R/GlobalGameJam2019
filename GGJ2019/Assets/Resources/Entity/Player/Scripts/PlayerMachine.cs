using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMachine : StateMachine
{
    public enum PlayerStates { Idle, Move, Die, Respawn}

    private PlayerInputController _inputController;

    [SerializeField]
    private float _deathDuration = 2;
    private float _deathTime;

    [SerializeField]
    private Transform _muzzle;
    [SerializeField]
    private MuzzleFlash _muzzleFlash;
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

    private Rigidbody2D rigidBody;

    [SerializeField]
    private Transform _gun;
    [SerializeField]
    private Vector3 _muzzleBasePosition;
    [SerializeField]
    private Vector3 _muzzleFlippedPosition;

    private GameObject _deathEffectPrefab;

	void Start ()
    {
        _deathEffectPrefab = (GameObject)Resources.Load("VFX/Prefabs/DeathEffects/DeathEffect_Prefab");
        rigidBody = GetComponent<Rigidbody2D>();
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
        GetComponent<Animator>().SetBool("IsIdle", true);
    }

    void Idle_Update()
    {
        Rotate();
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
        GetComponent<Animator>().SetBool("IsIdle", false);
    }

    void Move_EnterState()
    {
        GetComponent<Animator>().SetBool("IsWalk", true);
    }

    void Move_Update()
    {
        Rotate();
        if (_inputController.GetInputs().Shoot)
            Shoot();
        if (transform.position.x + _movementInput.x < World.Instance.Boundary.min.x || transform.position.x + _movementInput.x > World.Instance.Boundary.max.x)
            _movementInput.x = 0;
        if (transform.position.y + _movementInput.y < World.Instance.Boundary.min.y || transform.position.y + _movementInput.y > World.Instance.Boundary.max.y)
            _movementInput.y = 0;
        if (_movementInput == Vector3.zero)
        {
            currentState = PlayerStates.Idle;
            return;
        }
        rigidBody.MovePosition(transform.position + _movementInput * _moveSpeed * Time.deltaTime);
    }

    void Move_ExitState()
    {
        GetComponent<Animator>().SetBool("IsWalk", false);
    }

    void Die_EnterState()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GameObject go = Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
        if (go != null)
        {
            go.GetComponent<Animator>().SetBool(GetComponent<Player>().GetColor().ToString(), true);
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        GetComponent<Player>().Dead = true;
        _deathTime = Time.time;
    }

    void Die_Update()
    {
       if (_deathTime + _deathDuration < Time.time)
        {
            currentState = PlayerStates.Respawn;
            return;
        }
    }

    void Rotate()
    {
        if (_rotationInput != Vector3.zero)
        {
            Quaternion gunRotation = Quaternion.RotateTowards(_gun.rotation, Quaternion.LookRotation(Vector3.forward, _rotationInput), _rotateSpeed * Time.deltaTime);
            _gun.rotation = Quaternion.LookRotation(Vector3.forward, _rotationInput) * Quaternion.Euler(0, 0, 90);
            //Vector3 eulerRotation;
        }
        Vector3 rotation = _gun.rotation.eulerAngles;

        if (rotation.z > 90 && rotation.z < 270)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            _gun.GetComponent<SpriteRenderer>().flipY = true;
            _muzzle.transform.localPosition = _muzzleFlippedPosition;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            _gun.GetComponent<SpriteRenderer>().flipY = false;
            _muzzle.transform.localPosition = _muzzleBasePosition;
        }

        if (rotation.z > 180)
        {
            _gun.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        else
        {
            _gun.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
    }

    void Die_ExitState()
    {

    }

    void Respawn_EnterState()
    {
        GetComponent<Player>().Dead = false;
        transform.position = World.Instance.Fire.transform.position;
    }

    void Respawn_Update()
    {
        currentState = PlayerStates.Idle;
    }

    void Respawn_ExitState()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    void Shoot()
    {
        _muzzleFlash.Trigger();
        if (_lastShot + _shootRate < Time.time)
        {
            //Debug.Log("Shoot");
            GameObject obj = Instantiate(projectilePrefab, _muzzle.position, _gun.rotation * Quaternion.Euler(0, 0, -90));
            PlayerProjectile projectile = obj.GetComponent<PlayerProjectile>();
            Player player = GetComponent<Player>();
            if (projectile == null || player == null)
                return;
            projectile.CasterGameObject = gameObject;
            projectile.SetColor(player.GetColor());
            _lastShot = Time.time;
        }
    }
}
