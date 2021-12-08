using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnableObjectType
{
    Circle,
    Square,
}
public class CustomSpawnableObject
{
    public SpawnableObjectType Type;
    public bool IsExploding;
    public bool IsScorable;
    public Color ColorToUse;
}

public class GameState : MonoBehaviour
{
    public List<CustomSpawnableObject> SpawnableObjects = new List<CustomSpawnableObject>();
    public int PlayerScore = 0;
}
