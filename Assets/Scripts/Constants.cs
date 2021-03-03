using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static Vector3 launchWeaponOffset = new Vector3(0, 0.5f, 0);
    public static float raycastEpsilon = 0.025f;

    public static Vector2Int worldSize = new Vector2Int(100, 100);
}
