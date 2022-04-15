using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BombScript : MonoBehaviour
{
    public GameObject explosionRadiusObject;
    public GameObject explosionRadiusGrowObject;
    public float blowCircleAnimationSpeed = 2;
    [HideInInspector] public int damageCount;
    [HideInInspector] public float blowUpAfter;
    [HideInInspector] public float blowRadius;
    [HideInInspector] public BlocksContainer blocksContainer;
    [HideInInspector] public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        UnityTimer.Timer.Register(blowUpAfter, BlowUp, autoDestroyOwner : this);

        explosionRadiusObject.transform.DOScale(blowRadius, blowUpAfter / blowCircleAnimationSpeed);
        explosionRadiusGrowObject.transform.DOScale(blowRadius, blowUpAfter);
    }

    // Update is called once per frame
    private void BlowUp()
    {
        float rSquared = blowRadius * blowRadius / 2;
        float radius = blowRadius / 2;
        float radiusDrawBlack = blowRadius / 2; // TODO: For blowing effect. Maybe delete it later
        float radiusDrawBlackSquared = Mathf.Sqrt(radiusDrawBlack * radiusDrawBlack * radiusDrawBlack ); // TODO: For blowing effect. Maybe delete it later
        float x = transform.position.x;
        float y = transform.position.y;

        for (float u = x - radius; u < x + radius + 1; u++)
            for (float v = y - radius; v < y + radius + 1; v++)
                if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                {
                    blocksContainer.DestroyBlockAtPosition((int)u, (int)v);
                }

        // TODO: Blowing effect. Maybe delete it later
        /*
        for (float u = x - radiusDrawBlackSquared; u < x + radiusDrawBlackSquared + 1; u++)
            for (float v = y - radiusDrawBlackSquared; v < y + radiusDrawBlackSquared + 1; v++)
            {
                float dis = Mathf.Sqrt((x - u) * (x - u) + (y - v) * (y - v));
                if (dis < radiusDrawBlackSquared)
                {
                    float distance = Mathf.Clamp((dis / blowRadius) / (blowRadius - radiusDrawBlack), 0.6f, 1f);

                    blocksContainer.BlockSpriteRendererTint((int)u, (int)v,
                        new Color (distance, distance, distance, 1));
                }
            }
        */


        if (Vector2.Distance(player.transform.position, transform.position) < radius)
        {
            player.GetComponent<PlayerHealthController>().DealDamage(damageCount);
        }


        Destroy(gameObject);
    }
}
