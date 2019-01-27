using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMachine))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private int _playerNumber = 1;
    public int PlayerNumber
    {
        get { return _playerNumber; }
    }

    [SerializeField]
    private ColorUtil.Colors _baseColor;
    [SerializeField]
    private ColorUtil.Colors _currentColor;

    private bool _temporarilyColorChanged;
    private float _colorChangeTime;
    private float _colorChangeDuration;

    private ColorChanger _colorChanger;
    private PlayerMachine _playerMachine;

    private bool _out;

    void Start ()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _playerMachine = GetComponent<PlayerMachine>();
        _currentColor = _baseColor;
        ChangeColor(_baseColor);
    }

    public bool IsOutside()
    {
        return _out;
    }

    private void Update()
    {
        if (_temporarilyColorChanged && _colorChangeDuration + _colorChangeTime < Time.time)
        {
            _temporarilyColorChanged = false;
            ChangeColor(_baseColor);
        }
    }

    public void TakeHit()
    {
        _playerMachine.currentState = PlayerMachine.PlayerStates.Die;
    }

    public void SetIsOutside(bool value)
    {
        if (value == _out)
            return;
        _out = value;
        if (value)
            ChangeColor(ColorUtil.Colors.GREY);
        else
            ChangeColor(_baseColor);
    }

    public void ChangeColor(ColorUtil.Colors newColor)
    {
        _currentColor = newColor;
        _colorChanger.ChangeColor(_currentColor);
        Animator animator = GetComponent<Animator>();

        animator.SetBool("IsRed", false);
        animator.SetBool("IsBlue", false);
        animator.SetBool("IsPurple", false);

        string booleanColor = newColor == ColorUtil.Colors.BLUE ? "IsBlue" : newColor == ColorUtil.Colors.RED ? "IsRed" : "IsPurple";
        animator.SetBool(booleanColor, true);
    }

    public void TemporaryChangeColor(float duration, ColorUtil.Colors newColor)
    {
        _temporarilyColorChanged = true;
        _colorChangeDuration = duration;
        _colorChangeTime = Time.time;
        ChangeColor(newColor);
    }

    public ColorUtil.Colors GetColor()
    {
        return _currentColor;
    }

    public ColorUtil.Colors GetBaseColor()
    {
        return _baseColor;
    }
}
