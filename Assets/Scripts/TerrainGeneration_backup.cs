using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration_backup : MonoBehaviour
{
    public Sprite dirtSprite;
    public Sprite dirtGrassSprite;
    public Sprite stoneSprite;
    public Sprite logSprite;
    public Sprite leafSprite;
    public int worldSizeWidth = 150;
    public int dirtLayerSize = 15; // This is dirt layer of the landscape and for underground too
    public int landscapeHeight = 150; // Landscape filled with plains, forests, mountains (without undergrounds and the moment) and so on
    public int undergroundHeight = 20; // Location with stones, caves, a lot of minerals and so on. Overall deep levels
    public float paddingX = 0.5f;
    public float paddingY = 0.5f;
    public float noiseLandscapeFreq; // noise Landscape Frequency
    public float noiseUndergroundFreq; // noise Underground Frequency
    public float seed;
    public float createTileThreshold = 0.5f;
    public Texture2D cavesNoiseTexture;


    [Range(0, 1)] public float treeSpawnChance;

    // public Texture2D oresNoiseTexture;

    private float tileZIndex = 0; // Tile Z Index
    private NoiseTextureLib noiseTextureLib;

    [Header("Debugging")]
    [Space]

    [SerializeField] private Sprite defaultSpriteForDebugging;

    // Start is called before the first frame update
    private void Start()
    {
        noiseTextureLib = new NoiseTextureLib();

        seed = Random.Range(-10000, 10000);

        // Generate Noise Texture for caves
        cavesNoiseTexture = new Texture2D(worldSizeWidth, undergroundHeight + landscapeHeight);
        noiseTextureLib.GenerateNoiseTexture(seed, noiseUndergroundFreq, cavesNoiseTexture);

        GenerateTerrain();
    }

    // DEBUG : Just for testing reasons, no other reasons tbh
    /* private void OnValidate()
    {
        GenerateNoiseTexture();
    } */

    public void GenerateTerrain()
    {
        for (int x = 0; x < worldSizeWidth; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * noiseLandscapeFreq, (seed) * noiseLandscapeFreq)
                * landscapeHeight + undergroundHeight;

            for (int y = 0; y < height; y++)
            {
                Sprite tileSprite = defaultSpriteForDebugging;

                if (y >= height - 1)
                {
                    // Top layer of the terrain (landscape btw)
                    // Set a dirt grass on top of the map
                    tileSprite = dirtGrassSprite;

                    float t = Random.Range(0f, treeSpawnChance);

                    if (t > Random.Range(0f, 1f))
                    {
                        GenerateAndPlaceTree(logSprite, leafSprite, x, y);
                    }
                }
                else if (y >= height - dirtLayerSize)
                {
                    // Set just a dirt
                    tileSprite = dirtSprite;
                }
                else if (y < height - dirtLayerSize)
                {
                    // Everything else is a stone
                    tileSprite = stoneSprite;
                }

                /*
                if (y < height - dirtLayerSize)
                {
                    tileSprite = stoneSprite;
                }
                else if (y >= height - 1)
                {
                    // Set a dirt grass on top of the map
                    tileSprite = dirtGrassSprite;
                }
                else if (y > height - dirtLayerSize)
                {
                    tileSprite = dirtSprite;
                }
                */

                if (y < undergroundHeight)
                {
                    if (cavesNoiseTexture.GetPixel(x, y).r < createTileThreshold)
                    {
                        CreateAndPlaceTile(tileSprite, x, y);
                    }
                }
                else
                {
                    CreateAndPlaceTile(tileSprite, x, y);
                }
            }
        }
    }

    /* Copy just for case
    public void GenerateTerrain()
    {
        for (int x = 0; x < worldSizeWidth; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * noiseLandscapeFreq, (seed) * noiseLandscapeFreq) * landscapeHeight + undergroundHeight;
            
            for (int y = 0; y < height; y++)
            {
                if (y < undergroundHeight)
                {
                    if (noiseTexture.GetPixel(x, y).r < createTileThreshold)
                    {
                        CreateTile(x, y);
                    }
                } else
                {
                    CreateTile(x, y);
                }
            }
        }
    } */

    private GameObject CreateTile(Sprite sprite, Transform parent)
    {
        GameObject gameObjectTile = new GameObject();
        gameObjectTile.AddComponent<SpriteRenderer>();
        gameObjectTile.GetComponent<SpriteRenderer>().sprite = sprite;
        // gameObjectTile.transform.position = new Vector3(x + paddingX, y + paddingY, tileZIndex);

        gameObjectTile.AddComponent<BoxCollider2D>();

        gameObjectTile.layer = 3;

        //transform.SetParent(gameObjectTile.transform);
        gameObjectTile.transform.SetParent(parent);

        return gameObjectTile;
    }

    private void CreateAndPlaceTile(Sprite sprite, float x, float y)
    {
        GameObject gameObjectTile = CreateTile(sprite, transform);
        gameObjectTile.transform.position = new Vector3(x + paddingX, y + paddingY, tileZIndex);
    }

    private void GenerateAndPlaceTree(Sprite logSprite, Sprite leafSprite, float x, float y)
    {
        GameObject gameObjectTile = CreateTile(logSprite, transform);
        gameObjectTile.transform.position = new Vector3(x + paddingX, y + 1 + paddingY, tileZIndex);

        gameObjectTile = CreateTile(logSprite, transform);
        gameObjectTile.transform.position = new Vector3(x + paddingX, y + 2 + paddingY, tileZIndex);

        gameObjectTile = CreateTile(logSprite, transform);
        gameObjectTile.transform.position = new Vector3(x + paddingX, y + 3 + paddingY, tileZIndex);

        gameObjectTile = CreateTile(leafSprite, transform);
        gameObjectTile.transform.position = new Vector3(x + paddingX, y + 4 + paddingY, tileZIndex);

        gameObjectTile = CreateTile(leafSprite, transform);
        gameObjectTile.transform.position = new Vector3(x + paddingX, y + 5 + paddingY, tileZIndex);


    }

    /*public void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldSizeWidth, undergroundHeight);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * noiseUndergroundFreq, (y + seed) * noiseUndergroundFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }

        noiseTexture.Apply();
    }*/
}
