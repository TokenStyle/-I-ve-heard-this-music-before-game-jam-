using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerStats playerStats;

    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = "Health: " + playerStats.Health;
    }
}
