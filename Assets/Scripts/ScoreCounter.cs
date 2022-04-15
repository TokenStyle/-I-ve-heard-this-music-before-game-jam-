using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreCounterFAST", menuName = "ScriptableObjects/ScoreCounterFAST", order = 1)]
public class ScoreCounter : ScriptableObject, ISerializationCallbackReceiver
{
    public float score;
    public float scoreHighscore;

    public void OnAfterDeserialize()
    {
        // Reset world blocks
        score = 0;
    }

    public void OnBeforeSerialize() { }
}
