using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Card", menuName ="Building Card")]

public class BuildingCardType : ScriptableObject
{
    public BuildingType buildingType;
    public string _name = "type";
    public int goldCost = 1;
    public int gemCost = 1;

    public Sprite buildingsArt;
   // public GameObject buildingPrefab;

}

public enum BuildingType
{
   BuildingA,
   BuildingB,
   BuildingC,
   BuildingD,
   BuildingE,
   BuildingF
}
