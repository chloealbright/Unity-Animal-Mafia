using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;
    public Vector2 defaultValue;

    public void OnAfterDeserialize()
    {
        Debug.Log("RESET");
        initialValue = defaultValue;
    }

    public void OnBeforeSerialize()
    {
    }
}
