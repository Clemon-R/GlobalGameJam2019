using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMachine))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private ColorUtil.Colors _baseColor;
    private ColorUtil.Colors _currentColor;

    [SerializeField]
    private int _playerNumber = 1;
   
    public int PlayerNumber
    {
        get { return _playerNumber; }
    }

    private bool _out;

    void Start ()
    {
        _currentColor = _baseColor;
    }

    public bool IsOutside()
    {
        return _out;
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
