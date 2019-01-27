using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PrefabExplosion
{
    public ColorUtil.Colors _color;
    public GameObject _go;
}
public class PlayerProjectile : MonoBehaviour
{
    private ColorUtil.Colors _color;

    [SerializeField]
    private GameObject _deathFX;

    [SerializeField]
    private float _colorChangeDuration = 2.0f;

    [SerializeField]
    private int _damage = 30;

    [SerializeField]
    private float _moveSpeed = 20;

    [SerializeField]
    private List<PrefabExplosion> _prefabExplosions = new List<PrefabExplosion>();
    private Dictionary<ColorUtil.Colors, GameObject> _explosions = new Dictionary<ColorUtil.Colors, GameObject>();

    private ColorChanger _colorChanger;
    // Durée de vie ou disparition hors caméra ?
    [SerializeField]
    private float _lifeTime = 5;
    private float _spawnTime;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        foreach (PrefabExplosion item in _prefabExplosions)
        {
            _explosions.Add(item._color, item._go);
        }
    }

    private void Start()
    {
        _colorChanger = GetComponent<ColorChanger>();
        rigidBody = GetComponent<Rigidbody2D>();
        _spawnTime = Time.time;
        _colorChanger.ChangeColor(_color);
    }

    void Update ()
    {
        rigidBody.MovePosition(transform.position + transform.up * _moveSpeed * Time.deltaTime);
        if (_spawnTime + _lifeTime < Time.time)
        {
            GetDestroyed();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CasterGameObject == null || CasterGameObject == collision.gameObject)
            return;
        if (collision.transform.CompareTag("Monster"))
        {
            Monster monster = collision.transform.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeHit(_damage, _color);
            }
            GetDestroyed();
        }
        else if (collision.transform.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            player.TemporaryChangeColor(_colorChangeDuration, ColorUtil.Mix(GetColor(), player.GetColor()));
            GetDestroyed();
        }
    }

    public void GetDestroyed()
    {
        GameObject go = Instantiate(_deathFX, transform.position, Quaternion.identity);
        go.GetComponent<Animator>().SetBool(_color.ToString(), true);
        Destroy(gameObject);
    }

    public ColorUtil.Colors GetColor()
    {
        return _color;
    }

    public void SetColor(ColorUtil.Colors color)
    {
        _color = color;
    }

    public GameObject CasterGameObject { get; set; }
}
