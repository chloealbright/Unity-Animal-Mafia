using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit",menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject {
    public Type Type;
    public BaseUnit UnitPrefab;
}

public enum Type {
    Character = 0,
    Enemy = 1
}