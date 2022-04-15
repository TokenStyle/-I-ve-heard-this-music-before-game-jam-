using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public PlayerStats playerStats;
    public Cinemachine.CinemachineVirtualCamera cameraCinemachine;

    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (cameraCinemachine.Follow == null && player != null)
        {
            cameraCinemachine.Follow = player.transform;
        }
    }
}
