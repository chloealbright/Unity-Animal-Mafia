using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dungeon_VectorValue : ScriptableObject, ISerializationCallbackReceiver
{   
    public Vector2 initialValue;
    
    public Vector2 targetValue;

    public void OnAfterDeserialize()
    {
        initialValue=targetValue;
    }

    public void OnBeforeSerialize()
    {
    }
}


