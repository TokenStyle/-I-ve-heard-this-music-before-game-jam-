using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockInteraction : MonoBehaviour
{
    public float destroyDistance = 10;
    public float placeDistance = 10;
    public float placeMinDistance = 2;

    public BlocksContainer blocksContainer;
    public GameObject circleDistanceGameObject;
    public TerrainGeneration terrainGeneration; // TODO: delete it and make it normal, not through this

    public Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        GameObject terrainGenerationGO = GameObject.FindGameObjectWithTag("TerrainGenerator");

        if (terrainGenerationGO != null)
        {
            terrainGeneration = terrainGenerationGO.GetComponent<TerrainGeneration>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Remake it, because this is just dublicated code
        if (Input.GetButton("Fire1"))
        {
            Vector3Int mouseWorldPosition = Vector3Int.CeilToInt(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            mouseWorldPosition.z = 0;

            int x1 = mouseWorldPosition.x - 1;
            int y1 = mouseWorldPosition.y - 1;

            for (int x2 = -1; x2 < 2; x2++)
            {
                for (int y2 = -1; y2 < 2; y2++)
                {
                    int x = x1 + x2;
                    int y = y1 + y2;

                    if (Vector2.Distance(transform.position, new Vector2(x, y)) < destroyDistance)
                    {
                        DestructBlock(x, y);
                    }
                }
            }
        }

        if (Input.GetButton("Fire2"))
        {
            Vector3Int mouseWorldPosition = Vector3Int.CeilToInt(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            mouseWorldPosition.z = 0;

            int x1 = mouseWorldPosition.x - 1;
            int y1 = mouseWorldPosition.y - 1;

            for (int x2 = -1; x2 < 2; x2++)
            {
                for (int y2 = -1; y2 < 2; y2++)
                {
                    int x = x1 + x2;
                    int y = y1 + y2;

                    if (Vector2.Distance(transform.position, new Vector2(x, y)) < placeDistance &&
                        Vector2.Distance(transform.position, new Vector2(x, y)) > placeMinDistance)
                    {
                        terrainGeneration.CreateAndPlaceTile(blocksContainer.stoneSprite, x, y);
                    }
                }
            }
        }

        if ((int)circleDistanceGameObject.transform.lossyScale.x != placeDistance)
        {
            circleDistanceGameObject.transform.localScale = Vector3.one * placeDistance * 2 * transform.localScale.x;
        }
    }

    public void DestructBlock(int x, int y)
    {
        blocksContainer.DestroyBlockAtPosition(x, y);
    }
}
