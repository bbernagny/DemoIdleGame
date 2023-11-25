using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DataCard", menuName = "DataCard")]
public class ObjectData : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public int Id { get; private set; }

    [field: SerializeField]
    public int goldCost { get; private set; }

    [field: SerializeField]
    public int gemCost { get; private set; }

    [field: SerializeField]
    public Sprite buildingArts { get; private set; }

    [field: SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;

    [field: SerializeField]
    public GameObject Prefab { get; private set; }

}