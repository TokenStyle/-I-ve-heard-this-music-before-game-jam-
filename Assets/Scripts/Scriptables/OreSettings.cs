using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OreSettings", menuName = "ScriptableObjects/OreSettings", order = 1)]
public class OreSettings : ScriptableObject
{
    [Range(0, 1)] public float spawnChance;
    [Range(0, 999)] public float depositSize;
    public Color color;
}
