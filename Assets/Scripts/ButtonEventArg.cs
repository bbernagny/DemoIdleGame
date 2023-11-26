using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonEventArg : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI gemCostText;
    [SerializeField] private TextMeshProUGUI goldCostText;
    [SerializeField] private Image _cardImage;
    [SerializeField] private ObjectData _data;


    private Button _button;
    private bool _purchasable;
    public event Action<int> OnButtonClickEvent;
    public bool GetPurchasable { get { return _purchasable; } }
    public int GetID { get { return _data.Id; } }

    private void Start()
    {
        
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() => OnButtonClickEvent.Invoke(_data.Id));
        _name.SetText(_data.Name);
        gemCostText.SetText(_data.gemCost.ToString());
        goldCostText.SetText(_data.goldCost.ToString());
        _cardImage.sprite = _data.buildingArts;
    }

    private void Update()
    {
        _purchasable = GameManager.Instance.GetGemCoin >= _data.gemCost |
            GameManager.Instance.GetGoldCoin >= _data.goldCost;

        _button.interactable = _purchasable && !PlacementManager.Instance.IsSelected;

        ChangeTextMeshColor();
    }

    private void ChangeTextMeshColor()
    {
        if (GameManager.Instance.GetGemCoin >= _data.gemCost)
        {
            gemCostText.color = Color.green;
        }
        else
        {
            gemCostText.color = Color.red;
        }
        if (GameManager.Instance.GetGoldCoin >= _data.goldCost)
        {
            goldCostText.color = Color.green;
        }
        else
        {
            goldCostText.color = Color.red;
        }
    }
}
