﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorUtil
{
    public enum Colors { GREY, RED, BLUE, PURPLE }

    public static Colors GetRandomColor()
    {
        return (Colors)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Colors)).Length);
    }

    public static Colors Mix(Colors a, Colors b)
    {
        if ((a == Colors.RED && b == Colors.BLUE) || (a == Colors.BLUE && b == Colors.RED))
            return Colors.PURPLE;
        return b;
    }


    public static void ChangeGameObjectColor(GameObject go, ColorUtil.Colors newColor)
    {

    }
}
