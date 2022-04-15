using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebugging : MonoBehaviour
{
    private BlocksContainer blocksContainer;

    private void Start()
    {
        blocksContainer = GetComponent<PlayerBlockInteraction>().blocksContainer;
    }

    private void Update()
    {
        // Everything have to be in isEditor, just in case
        if (Application.isEditor)
        {
            /*
            if (Input.GetButton("Fire1"))
            {
                Camera camera = Camera.main;

                Vector3Int mouseWorldPosition = Vector3Int.CeilToInt(camera.ScreenToWorldPoint(Input.mousePosition));
                mouseWorldPosition.z = 0;

                int x1 = mouseWorldPosition.x - 1;
                int y1 = mouseWorldPosition.y - 1;

                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        GameObjectPosition gameObjectPosition = blocksContainer.GetBlockAtPosition(x1 + x, y1 + y);

                        if (gameObjectPosition != null)
                        {
                            Destroy(gameObjectPosition.gameObject);
                        }

                    }
                }


                /*
                for (int x = -3; x < 7; x++)
                {
                    for (int y = -3; y < 7; y++)
                    {
                        GameObjectPosition gameObjectPosition = blocksContainer.GetBlockAtPosition(
                            (int)transform.position.x + x, (int)transform.position.y + y);

                        if (gameObjectPosition != null)
                        {
                            Destroy(gameObjectPosition.gameObject);
                        }
                    }
                }
            
            }*/
        }
    }
}
