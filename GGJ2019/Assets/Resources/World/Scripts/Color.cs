using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public static class Color
{
=======
public static class ColorUtil {
>>>>>>> 6c253b1cb564fbeae1ce6b6ff22e6d6284ed1392
    public static void ChangeGameObjectColor(GameObject obj, string colorCode)
    {
        if (obj == null)
            return;

        Renderer rend = obj.GetComponent<Renderer>();
        if (rend == null)
            return;

        Debug.Log("[" + obj.name + "] - Change color for: " + colorCode);
        int[] rgb = GetRGB(colorCode);
        
        rend.material.SetColor("_Color", (new Color(rgb[0], rgb[1], rgb[2])).linear);
    }

    public static int[] GetRGB(string colorCode)
    {
        int[] result = {0,0,0};
        if (colorCode != null && colorCode.Substring(0,1) == "#")
        {
            result[0] = int.Parse(colorCode.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);            
            result[1] = int.Parse(colorCode.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            result[2] = int.Parse(colorCode.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return result;
    }

    public static string GetColorCode(int[] rgb)
    {
        string result = "#000000";
        if (rgb.Length == 3)
        {
            result = "#";
            result += rgb[0].ToString("X2");
            result += rgb[1].ToString("X2");
            result += rgb[2].ToString("X2");
        }
        return result;
    }

    public static string Mix(params string[] colors)
    {
        double percent = 1.0D / (double)colors.Length;
        if (percent < 0.01D)
            percent = 0.01D;
        int[] result = { 0,0,0};
        foreach (string color in colors)
        {
            int[] data = GetRGB(color);
            result[0] += (int)(data[0] * percent);
            result[1] += (int)(data[1] * percent);
            result[2] += (int)(data[2] * percent);
        }
        return GetColorCode(result);
    }
}
