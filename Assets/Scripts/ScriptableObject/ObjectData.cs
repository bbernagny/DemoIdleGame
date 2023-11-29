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
    public Vector2Int Size { get; private set; }

    [field: SerializeField]
    public GameObject Prefab { get; private set; }


    [field: SerializeField]
    public int generatedGold { get; private set; }

    [field: SerializeField]
    public int generatedGem { get; private set; }

    [field: SerializeField]
    public float productionTime { get; private set; }


    public void GenerateGoldAndGem(float duration, ref float timer)
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            GameManager.Instance.GetGemCoin += generatedGem;
            GameManager.Instance.GetGoldCoin += generatedGold;
            timer = 0;
        }
    }
}

