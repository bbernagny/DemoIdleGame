using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("Created_GameManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    [SerializeField] private TextMeshProUGUI _goldCoinText;
    [SerializeField] private TextMeshProUGUI _gemText;
    [SerializeField] private GameObject _confirmationMenu; // Oluştur
    [SerializeField] private Button _confirmationButton;
    [SerializeField] private Button _rejectButton;
    [SerializeField] private ObjectDatabase objectDatabase;

    [SerializeField] private int _startingGoldCoin;
    [SerializeField] private int _startingGemCoin;

    [SerializeField] private int _goldCoin;
    [SerializeField] private int _gemCoin;

    public int GetGemCoin { get { return _gemCoin; } }
    public int GetGoldCoin { get { return _goldCoin; } }

    [SerializeField] private List<ButtonEventArg> _cardButtonList = new List<ButtonEventArg>();
    [SerializeField] private GameObject _spawnObject;

    public bool IsSelected;

    [SerializeField] private int _isSelectedID;

    public void Start()
    {
        _goldCoin = _startingGoldCoin;
        _gemCoin = _startingGemCoin;
        objectDatabase.InitializeDatabase();

        foreach (var button in _cardButtonList)
        {
            button.OnButtonClickEvent += ShowPurchaseConfirmation;
        }
        _rejectButton.onClick.AddListener(() => { IsSelected = false; _isSelectedID = 0; });
    }

    public void Update()
    {
        Debug.Log(objectDatabase.GetData(1).Name);
        _goldCoinText.SetText(_goldCoin.ToString());
        _gemText.SetText(_gemCoin.ToString());

        _confirmationMenu.SetActive(IsSelected);
        
    }

    public void ShowPurchaseConfirmation(int id)
    {
        Debug.Log(id);
        if (!IsSelected)
        {
            IsSelected = true;
            _isSelectedID = id;
        }
        _confirmationButton.onClick.RemoveAllListeners();
        _confirmationButton.onClick.AddListener(() => PurchaseBuilding(id));
    }

    public void PurchaseBuilding(int id)
    {
        int gemPrice = objectDatabase.GetData(id).gemCost;
        int goldPrice = objectDatabase.GetData(id).goldCost;
        Debug.Log(gemPrice);

        if(_goldCoin >= goldPrice && _gemCoin >= gemPrice)
        {
            _goldCoin -= goldPrice;
            _gemCoin -= gemPrice;
            _isSelectedID = 0;
            IsSelected = false;
            return;
        }
        
    }


}
