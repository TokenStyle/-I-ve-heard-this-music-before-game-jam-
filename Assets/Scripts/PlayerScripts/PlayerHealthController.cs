using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public PlayerStats playerStats;

    public void DealDamage(int damageCount)
    {
        playerStats.DealDamage(damageCount);
    }
}
