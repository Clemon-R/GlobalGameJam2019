using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    ColorUtil.Colors _color;

    [SerializeField]
    private float _triggerRate = 0.1f;
    [SerializeField]
    private float _triggerDuration = 0.1f;

    private float _lastTriggerTime = 0;
    private float _lastTriggerEndTime = 0;
    private bool _triggered;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
        ChangeColor(_color);
    }

    void Update()
    {
		if (_triggered && _lastTriggerTime + _triggerDuration < Time.time)
        {
            _triggered = false;
            _lastTriggerEndTime = Time.time;
            Flash(false);
        }
	}

    private void Flash(bool flash)
    {
        _renderer.enabled = flash;
    }

    public void Trigger()
    {
        if (_lastTriggerEndTime + _triggerRate < Time.time)
        {
            _triggered = true;
            _lastTriggerTime = Time.time;
            Flash(true);
        }
    }

    public void ChangeColor(ColorUtil.Colors color)
    {
        if (_renderer != null)
            _renderer.color = ColorUtil.UnityColorFromColorEnum(color);
        _color = color;
    }
}
