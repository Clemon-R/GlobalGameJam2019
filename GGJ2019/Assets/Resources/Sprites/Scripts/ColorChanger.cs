using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpriteColor
{
    public ColorUtil.Colors _color;
    public Sprite _sprite;
}

public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    private List<SpriteColor> _spriteColorsList = new List<SpriteColor>();
    private Dictionary<ColorUtil.Colors, Sprite> _sprites = new Dictionary<ColorUtil.Colors, Sprite>();

    private void Awake()
    {
        foreach (SpriteColor item in _spriteColorsList)
        {
            _sprites.Add(item._color, item._sprite);
        }
    }

    public void ChangeColor(ColorUtil.Colors color)
    {
        Sprite sprite;
        _sprites.TryGetValue(color, out sprite);
        if (sprite != null)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sprite = Instantiate(sprite);
            }
        }
        foreach (Transform child in transform)
        {
            ColorChanger colorChanger = child.gameObject.GetComponent<ColorChanger>();
            if (colorChanger != null)
            {
                Debug.Log("Change color of " + child.name + " to " + color.ToString());
                colorChanger.ChangeColor(color);
            }
        }
    }
}
