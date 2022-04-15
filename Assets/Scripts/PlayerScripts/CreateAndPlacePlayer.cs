using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAndPlacePlayer : MonoBehaviour
{
    public GameObject playerPref;
    public TerrainGenerationSettings generationSettings;

    public PlayerStats playerStats;

    private GameObject player;
    private TerrainGenerationSettings realGenerationSettings;

    // Start is called before the first frame update
    void Awake()
    {
        player = Instantiate(playerPref);

        realGenerationSettings = Instantiate(generationSettings);

        float x = generationSettings.WorldSizeWidth / 2 + generationSettings.paddingX;
        float y = generationSettings.landscapeHeight + generationSettings.undergroundHeight + generationSettings.paddingY + 10;

        player.transform.position = new Vector3(x, y, player.transform.position.z);

        player.transform.SetParent(transform);

        // playerStats.playerCurrent = player;
    }
}
