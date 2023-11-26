using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    private static PlacementManager instance;
    public static PlacementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlacementManager>();
                if (instance == null)
                {
                    instance = new GameObject("Created_PlacementManager").AddComponent<PlacementManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private GameObject mouseIndicator, cellIndicator;

    [SerializeField] private InputManager inputManager;

    [SerializeField] private Grid grid;

    [SerializeField] private ObjectDatabase objDatabase;
    private int selectedObjectIndex = -1;

    [SerializeField] private GameObject gridVisualization;

    [SerializeField] private GameObject _confirmationMenu;
    [SerializeField] private Button _confirmationButton;
    [SerializeField] private Button _rejectButton;

    private GameObject placedObject; // Instantiate edilen objeyi tutmak için bir referans

    public bool IsSelected;
    public bool IsPurchased = true;

    [SerializeField] private int _isSelectedID;

    [SerializeField] private List<ButtonEventArg> _cardButtonList = new List<ButtonEventArg>();

    private void Start()
    {
        StopPlacement();

        foreach (var button in _cardButtonList)
        {
            button.OnButtonClickEvent += ShowPurchaseConfirmation;
        }
        _rejectButton.onClick.AddListener(RejectButtonEvent);
    }

    public void StartPlacement(int Id)
    {
        StopPlacement();
        selectedObjectIndex = objDatabase.objectData.FindIndex(data => data.Id == Id);
        if (selectedObjectIndex < 0)
        {
            Debug.Log($"NoId found {Id}");
            return;
        }
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector2 mousePosition = inputManager.GetSelectedMapPos();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if (IsSelected)
        {
            placedObject = Instantiate(objDatabase.objectData[selectedObjectIndex].Prefab);
            placedObject.transform.position = grid.CellToWorld(gridPosition);
            _confirmationMenu.SetActive(true);
        }
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        SetIndicators();
    }

    private void SetIndicators()
    {
        if (selectedObjectIndex < 0)
            return;
        Vector2 mousePosition = inputManager.GetSelectedMapPos();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

    public void ShowPurchaseConfirmation(int id)
    {
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
        int gemPrice = objDatabase.GetData(id).gemCost;
        int goldPrice = objDatabase.GetData(id).goldCost;

        if (GameManager.Instance.GetGoldCoin >= goldPrice && GameManager.Instance.GetGemCoin >= gemPrice)
        {
            GameManager.Instance.GetGoldCoin -= goldPrice;
            GameManager.Instance.GetGemCoin -= gemPrice;
            _isSelectedID = 0;
            IsSelected = false;
            _confirmationMenu.SetActive(false);
        }
    }

    public void RejectButtonEvent()
    {
        IsSelected = false;
        _isSelectedID = 0;
        IsPurchased = false;
        _confirmationMenu.SetActive(false);

        // Eğer placedObject null değilse destroy et
        if (placedObject != null)
        {
            Destroy(placedObject);
        }
    }
}
