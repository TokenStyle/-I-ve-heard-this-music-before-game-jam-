using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatistics", menuName = "ScriptableObjects/PlayerStatistics", order = 1)]
public class PlayerStats : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Initial Player Statistics (when the game starts)")]
    [SerializeField] private int initialHealth;
    [SerializeField] private int initialStamina;
    [SerializeField] private int initialSpeed;
    [SerializeField] private int initialSpeedBoost;
    [SerializeField] private int initialJumpForcePower;

    // [HideInInspector] public GameObject playerCurrent;

    private int health;
    private int stamina;
    private int speed;
    private int speedBoost;
    private int jumpForcePower;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public int Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            stamina = value;
        }
    }

    public int Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    public int SpeedBoost
    {
        get
        {
            return speedBoost;
        }
        set
        {
            speedBoost = value;
        }
    }

    public int JumpForcePower
    {
        get
        {
            return jumpForcePower;
        }
        set
        {
            jumpForcePower = value;
        }
    }


    public void DealDamage(int damageCount)
    {
        Health -= damageCount;
    }

    /* private void OnEnable()
    {
        Debug.Log("Stats enabled");
        health = initialHealth;
        stamina = initialStamina;
        speed = initialSpeed;
        speedBoost = initialSpeedBoost;
        jumpForcePower = initialJumpForcePower;

        // playerCurrent = null;
    } */

    public void OnBeforeSerialize()
    {
        // On serialization changes not through getter/setter, we change the variable "as is"
        // Dunno why, just in case, could be change in future, though
        health = initialHealth;
        stamina = initialStamina;
        speed = initialSpeed;
        speedBoost = initialSpeedBoost;
        jumpForcePower = initialJumpForcePower;

        // playerCurrent = null;

        Debug.Log("Stats serialized");
    }

    public void OnAfterDeserialize()
    {

    }
}
