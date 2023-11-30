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

    private GridData gridData, itemData;
    private Renderer previewRenderer;
    private List<GameObject> placedGameObject = new();

    [SerializeField] private GameObject _confirmationMenu;
    [SerializeField] private Button _confirmationButton;
    [SerializeField] private Button _rejectButton;

    private GameObject placedObject; // Instantiate edilen objeyi tutmak için referansım

    public bool IsSelected;
    [SerializeField] private int _isSelectedID;

    [SerializeField] private List<ButtonEventArg> _cardButtonList = new List<ButtonEventArg>();

    Dictionary<GameObject, Vector3Int> spawnedObjectData = new Dictionary<GameObject, Vector3Int>();

    private void Start()
    {
        StopPlacement();

        gridData = new GridData();
        itemData = new GridData();
        previewRenderer = cellIndicator.GetComponent<Renderer>();

        foreach (var button in _cardButtonList)
        {
            button.OnButtonClickEvent += ShowPurchaseConfirmation;
        }
        _rejectButton.onClick.AddListener(RejectButtonEvent);
    }

    private void Update()
    {
        SetIndicators(); 
    }

    public void StartPlacement(int Id)
    {
        StopPlacement();
        selectedObjectIndex = objDatabase.objectData.FindIndex(data => data.Id == Id);
        if (selectedObjectIndex < 0)
        {
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

        bool placementValidity = CheckPlacement(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
            return;

        if (IsSelected)
        {
            placedObject = Instantiate(objDatabase.objectData[selectedObjectIndex].Prefab);
            placedObject.transform.position = grid.CellToWorld(gridPosition);
            
            GridData selectedData = objDatabase.objectData[selectedObjectIndex].Id == 0 ? gridData : itemData;
            selectedData.AddObject(gridPosition, objDatabase.objectData[selectedObjectIndex].Size,
                objDatabase.objectData[selectedObjectIndex].Id, placedGameObject.Count - 1);

            _confirmationMenu.SetActive(true);
            IsSelected = false;
        }
    }

    private bool CheckPlacement(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = objDatabase.objectData[selectedObjectIndex].Id == 0 ? gridData : itemData;
       
        return selectedData.CanPlaceObject(gridPosition, objDatabase.objectData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void SetIndicators()
    {
        if (selectedObjectIndex < 0)
            return;
        Vector2 mousePosition = inputManager.GetSelectedMapPos();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacement(gridPosition, selectedObjectIndex);
        previewRenderer.material.color = placementValidity ? Color.green : Color.red;

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

            placedGameObject.Add(placedObject);

            Building building = placedObject.GetComponent<Building>();
            if (building != null)
            {
                building.objectData = objDatabase.GetData(id);
            }

            _isSelectedID = 0;
            IsSelected = false;
            _confirmationMenu.SetActive(false);
        }
    }

    public void RejectButtonEvent()
    {
        IsSelected = false;
        _isSelectedID = 0;
        _confirmationMenu.SetActive(false);

        if (placedObject != null)
        {
            GridData selectedData = objDatabase.objectData[selectedObjectIndex].Id == 0 ? gridData : itemData;

            selectedData.RemoveObject(grid.WorldToCell(placedObject.transform.position), objDatabase.objectData[selectedObjectIndex].Size);

            Destroy(placedObject);
            placedGameObject.Remove(placedObject);
        }
    }
}
