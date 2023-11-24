using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventArg : MonoBehaviour
{
    public BuildingType buildingType;
    public Action<BuildingType, BuildingCardType> buttonAction;
    public BuildingCardType buildingCardType;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickEvent);
    }

    public void OnClickEvent()
    {
        if(buttonAction != null)
        {
            buttonAction.Invoke(buildingType, buildingCardType);
        }
    }
}
