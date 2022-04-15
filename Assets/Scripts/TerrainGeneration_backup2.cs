using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration_backup2 : MonoBehaviour
{
    [Header("Tile Sprites")]

    public BlocksContainer blocksContainer;

    [Header("Generation Settings")]

    [SerializeField] private int worldSizeWidth = 150;
    public int dirtLayerSize = 15; // This is dirt layer of the landscape and for underground too
    public int landscapeHeight = 150; // Landscape filled with plains, forests, mountains (without undergrounds and the moment) and so on
    public int undergroundHeight = 20; // Location with stones, caves, a lot of minerals and so on. Overall deep levels

    [Header("Terrain Padding")]

    public float paddingX = 0.5f;
    public float paddingY = 0.5f;

    [Header("Noise Settings")]

    public float noiseLandscapeFreq; // noise Landscape Frequency
    public float noiseUndergroundFreq; // noise Underground Frequency
    [Range(0, 1)] public float createTileThreshold = 0.5f; // Create tile on this threshold; if higher, more chances tile will be created. Value is float number from [0-1]
    public Texture2D cavesNoiseTexture;

    [Header("Ores Settings")]

    public OreSettings coalOreSettings;
    public OreSettings ironOreSettings;
    public OreSettings silverOreSettings;

    public Texture2D oresTexture;

    [Header("Trees")]

    [Range(0, 1)] public float treeSpawnChance;

    [Header("Debugging")]
    [Space]

    public float seed;
    [SerializeField] private Sprite defaultSpriteForDebugging;

    private float tileZIndex = 0; // Tile Z Index
    private NoiseTextureLib noiseTextureLib;

    
    
    public int WorldSizeWidth { 
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
    

    // Start is called before the first frame update
    private void Start()
    {
        noiseTextureLib = new NoiseTextureLib();

        seed = Random.Range(-10000, 10000);

        // Generate Noise Texture for caves
        cavesNoiseTexture = new Texture2D(WorldSizeWidth, undergroundHeight + landscapeHeight);
        noiseTextureLib.GenerateNoiseTexture(seed, noiseUndergroundFreq, cavesNoiseTexture);

        // Generate Ores Texture for ores
        oresTexture = new Texture2D(WorldSizeWidth, undergroundHeight + landscapeHeight);

        for (int x = 0; x < oresTexture.width; x++)
        {
            for (int y = 0; y < oresTexture.height; y++)
            {
                float coalOreChance = Random.Range(0f, coalOreSettings.spawnChance);

                if (coalOreChance > Random.Range(0f, 1f))
                {
                    oresTexture.SetPixel(x, y - 1, coalOreSettings.color);
                    oresTexture.SetPixel(x + 1, y, coalOreSettings.color);
                    oresTexture.SetPixel(x, y, coalOreSettings.color);
                    oresTexture.SetPixel(x, y + 1, coalOreSettings.color);
                    oresTexture.SetPixel(x - 1, y, coalOreSettings.color);
                }
            }
        }

        oresTexture.Apply();


        GenerateTerrain();
    }

    // DEBUG : Just for testing reasons, no other reasons tbh
    /* private void OnValidate()
    {
        GenerateNoiseTexture();
    } */

    public void GenerateTerrain()
    {
        for (int x = 0; x < WorldSizeWidth; x++)
        {
            float height;

            for (int y = 0; y < WorldSizeHeight; y++)
            {
                Sprite tileSprite = defaultSpriteForDebugging;

                // It's multiply landscape and adds underground height that's has specific number
                height = Mathf.PerlinNoise((x + seed) * noiseLandscapeFreq, (seed) * noiseLandscapeFreq)
                * landscapeHeight + undergroundHeight;

                if (y < height)
                {
                    // Calculates what tile will be placed
                    if (y >= height - 1)
                    {
                        // Top layer of the terrain (landscape btw)
                        // Set a dirt grass on top of the map
                        tileSprite = blocksContainer.dirtGrassSprite;

                        float t = Random.Range(0f, treeSpawnChance);

                        if (t > Random.Range(0f, 1f))
                        {
                            GenerateAndPlaceTree(blocksContainer.logSprite, blocksContainer.leafSprite, x, y);
                        }
                    }
                    else if (y >= height - dirtLayerSize)
                    {
                        // Set just a dirt
                        tileSprite = blocksContainer.dirtSprite;
                    }
                    else if (y < height - dirtLayerSize)
                    {
                        // Everything else is a stone or ores
                        tileSprite = blocksContainer.stoneSprite;

                        // Check for ore
                        if (oresTexture.GetPixel(x, y) == coalOreSettings.color)
                        {
                            tileSprite = blocksContainer.coalSprite;
                        }
                    }

                    // Placing tiles
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

    private GameObject CreateTile(Sprite sprite, ref GameObjectPosition gameObjectPosition, Transform parent)
    {
        GameObject gameObjectTile = new GameObject();
        gameObjectTile.AddComponent<SpriteRenderer>();
        gameObjectTile.GetComponent<SpriteRenderer>().sprite = sprite;
        // gameObjectTile.transform.position = new Vector3(x + paddingX, y + paddingY, tileZIndex);
        gameObjectTile.transform.position = Vector3.zero;

        gameObjectTile.AddComponent<BoxCollider2D>();

        gameObjectTile.layer = 3;

        //transform.SetParent(gameObjectTile.transform);
        gameObjectTile.transform.SetParent(parent);

        gameObjectPosition.SetGameObject(gameObjectTile);
        gameObjectPosition.SetPosition(Vector2Int.zero);

        blocksContainer.worldBlocks.Add(gameObjectPosition);

        return gameObjectTile;
    }

    private void CreateAndPlaceTile(Sprite sprite, int x, int y)
    {
        Vector2Int placePosition = new Vector2Int(x, y);

        if (blocksContainer.CheckIsThereBlock(placePosition.x, placePosition.y) == false)
        {

            GameObjectPosition gameObjectPosition = new GameObjectPosition();
            GameObject gameObjectTile = CreateTile(sprite, ref gameObjectPosition, transform);

            gameObjectPosition.SetPosition(placePosition);
            gameObjectTile.transform.position = new Vector3(placePosition.x + paddingX, placePosition.y + paddingY, tileZIndex);
        }
    }

    private void GenerateAndPlaceTree(Sprite logSprite, Sprite leafSprite, int x, int y)
    {
        CreateAndPlaceTile(logSprite, x, y + 1);
        CreateAndPlaceTile(logSprite, x, y + 2);
        CreateAndPlaceTile(logSprite, x, y + 3);

        CreateAndPlaceTile(leafSprite, x, y + 4);
        CreateAndPlaceTile(leafSprite, x, y + 5);


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
