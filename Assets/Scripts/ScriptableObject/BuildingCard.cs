using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingCard : MonoBehaviour
{
    [SerializeField] private BuildingCardType buildingCardType = null;

    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI goldCostText;
    [SerializeField] private TextMeshProUGUI gemCostText;

    [SerializeField] private Image buildingArt;
    //[SerializeField] private GameObject buildingObj;

    void Start()
    {
        cardNameText.text = buildingCardType._name;
        goldCostText.text = buildingCardType.goldCost.ToString();
        gemCostText.text = buildingCardType.gemCost.ToString();

        buildingArt.sprite = buildingCardType.buildingsArt;
       // buildingObj = buildingCardType.buildingPrefab;

    }

}
