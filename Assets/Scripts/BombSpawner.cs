using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTimer;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public float spawnBombEvery;
    public float spawnBombEveryMin = 0.5f;
    public float bombSpawnSpeedRaiseCount = 0.5f;
    public float bombSpawnSpeedRaiseEvery = 5f;
    public float spawnBombFlyUpAdd = 10;
    public int bombDamageCount = 1;
    public float bombBlowUpTimer = 3f;
    public float bombBlowUpRadius = 12f;
    public TerrainGenerationSettings generationSettings;
    public BlocksContainer blocksContainer;
    public PlayerStats playerStats;

    private Timer _timer;
    private Timer _timerSpeedRaise;
    private GameObject player;

    private void Start()
    {
        StartBombSpawner();

        StartBombSpawnSpeedRaise();
    }

    private void SpawnBomb()
    {
        Rect terrainPosition = generationSettings.GetTerrainInGamePosition();
        Vector3 bombPosition = new Vector3((terrainPosition.xMax - terrainPosition.xMin), terrainPosition.yMax + spawnBombFlyUpAdd, generationSettings.TileZIndex);

        bombPosition.x = terrainPosition.xMin + Random.Range(0f, bombPosition.x);

        GameObject bomb = Instantiate(bombPrefab, bombPosition, Quaternion.identity, transform);
        BombScript bombScript = bomb.GetComponent<BombScript>();

        bombScript.damageCount = bombDamageCount;
        bombScript.blowUpAfter = bombBlowUpTimer;
        bombScript.blowRadius = bombBlowUpRadius;
        bombScript.blocksContainer = blocksContainer;
        bombScript.damageCount = bombDamageCount;
        bombScript.player = player;
    }

    private void StartBombSpawner()
    {
        _timer = Timer.Register(spawnBombEvery, SpawnBomb, isLooped: true);
    }

    private void StartBombSpawnSpeedRaise()
    {
        _timerSpeedRaise = Timer.Register(bombSpawnSpeedRaiseEvery, IncreaseBombSpawnSpeed, isLooped: true);
    }

    private void IncreaseBombSpawnSpeed()
    {
        if (_timer != null)
        {
            spawnBombEvery = Mathf.Max(spawnBombEvery - bombSpawnSpeedRaiseCount, spawnBombEveryMin);

            Timer.Cancel(_timer);

            StartBombSpawner();
        }
    }

    // Debugging - restart timer on changing any value of this script
    private void OnValidate()
    {
        if (_timer != null && Application.isPlaying)
        {
            Timer.Cancel(_timer);

            StartBombSpawner();
        }
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}