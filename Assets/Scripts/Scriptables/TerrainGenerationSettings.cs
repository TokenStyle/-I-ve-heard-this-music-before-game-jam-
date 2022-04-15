using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainGenerationSettings", menuName = "ScriptableObjects/TerrainGenerationSettings", order = 1)]
public class TerrainGenerationSettings : ScriptableObject
{
    [SerializeField] private int worldSizeWidth = 150;
    public int dirtLayerSize = 15; // This is dirt layer of the landscape and for underground too
    public int landscapeHeight = 150; // Landscape filled with plains, forests, mountains (without undergrounds and the moment) and so on
    public int undergroundHeight = 20; // Location with stones, caves, a lot of minerals and so on. Overall deep levels

    [Header("Terrain Padding (don't touch it)")]

    public float paddingX = 0.5f;
    public float paddingY = 0.5f;
    [SerializeField] private float tileZIndex = 0;

    public int WorldSizeWidth
    {
        get
        {
            return worldSizeWidth;
        }
    }

    // It's getter to get World Size Height
    public int WorldSizeHeight
    {
        get
        {
            return landscapeHeight + undergroundHeight;
        }
    }

    public float TileZIndex
    {
        get
        {
            return tileZIndex;
        }
    }

    public Rect GetTerrainInGamePosition()
    {
        return new Rect(paddingX, paddingY, WorldSizeWidth, WorldSizeHeight);
    }
}
