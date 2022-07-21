﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets Instance
    {
        get
        {
            if(_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
            return _i;
        }
    }

    public Transform PFDamagePopUp;
}
