using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPosition
{
    public GameObject gameObject;
    public Vector2Int position;

    public GameObjectPosition() { }

    public GameObjectPosition(GameObject newGameObject, Vector2Int newPosition)
    {
        SetGameObject(newGameObject);
        SetPosition(newPosition);
    }

    public void SetGameObject(GameObject value)
    {
        gameObject = value;
    }

    public void SetPosition(Vector2Int value)
    {
        position = value;
    }

    public Vector2Int GetPosition()
    {
        return position;
    }
}
