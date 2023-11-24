using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public List<ButtonEventArg> buttonEventArgs = new List<ButtonEventArg>();
    public Image image;

    public bool onSelected;


    private void Start()
    {
        AssignButtonClickActions();
        image.gameObject.SetActive(false);
    }

    private void Update()
    {
        image.gameObject.SetActive(onSelected);
        if (onSelected)
        {
            image.transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonDown(1))
        {
            onSelected = false;
        }
    }

    private void AssignButtonClickActions()
    {
        foreach (var buttonEventArg in buttonEventArgs)
        {
            buttonEventArg.buttonAction = OnButtonClicked;
        }
    }

    private void OnButtonClicked(BuildingType buttonType, BuildingCardType buildingType)
    {
        Debug.Log("Tıklanan buton tipi: " + buttonType);
        Debug.Log("Tıklanan buton tipi: " + buildingType);

        switch (buttonType)
        {
            case BuildingType.BuildingA:
                SelectBuilding(buildingType);  
                break;
            case BuildingType.BuildingB:
                SelectBuilding(buildingType);
                break;
            case BuildingType.BuildingC:
                SelectBuilding(buildingType);
                break;
            case BuildingType.BuildingD:
                SelectBuilding(buildingType);
                break;
            case BuildingType.BuildingE:
                SelectBuilding(buildingType);
                break;
            case BuildingType.BuildingF:
                SelectBuilding(buildingType);
                break;
            default:
                break;
        }
    }

    public void SelectBuilding(BuildingCardType buildingCardType)
    {
        if (!onSelected)
        {
            image.sprite = buildingCardType.buildingsArt;
            onSelected = true;
        }
      
    }
    
}
