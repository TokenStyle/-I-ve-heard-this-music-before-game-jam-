using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlocksContainer", menuName = "ScriptableObjects/BlocksContainer", order = 1)]
public class BlocksContainer : ScriptableObject, ISerializationCallbackReceiver
{
    public Sprite dirtSprite;
    public Sprite dirtGrassSprite;
    public Sprite stoneSprite;
    public Sprite logSprite;
    public Sprite leafSprite;
    public Sprite debuggingBlock;

    [Header("Ores")]
    public Sprite coalSprite;
    public Sprite ironSprite;
    public Sprite silverSprite;

    public List<GameObjectPosition> worldBlocks = new List<GameObjectPosition>();

    public bool CheckIsThereBlock(int x, int y)
    {
        foreach (GameObjectPosition item in worldBlocks)
        {
            Vector2Int pos = item.GetPosition();

            if (x == pos.x && y == pos.y)
            {
                return true;
            }
        }

        return false;
    }

    public GameObjectPosition GetBlockAtPosition(int x, int y)
    {
        foreach (GameObjectPosition item in worldBlocks)
        {
            Vector2Int pos = item.GetPosition();

            if (x == pos.x && y == pos.y)
            {
                return item;
            }
        }

        return null;
    }

    public void AddBlockToWorldBlocks(GameObjectPosition gameObjectPosition)
    {
        worldBlocks.Add(gameObjectPosition);
    }

    public void DestroyBlockAtPosition(int x, int y)
    {
        GameObjectPosition gameObjectPosition = GetBlockAtPosition(x, y);

        if (gameObjectPosition != null)
        {
            Destroy(gameObjectPosition.gameObject);
            worldBlocks.Remove(gameObjectPosition);
        }
    }

    // TODO: to change color from bombs ( BombScript ). Delete it later maybe or rewrite it
    public void BlockSpriteRendererTint(int x, int y, Color color)
    {
        GameObjectPosition gameObjectPosition = GetBlockAtPosition(x, y);

        if (gameObjectPosition != null)
        {
            SpriteRenderer spriteRenderer = gameObjectPosition.gameObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer.color.r > color.r)
            {
                spriteRenderer.color = color;
            }
        }
    }

    public void OnAfterDeserialize()
    {
        // Reset world blocks
        worldBlocks = new List<GameObjectPosition>();
    }

    public void OnBeforeSerialize() { }
}
