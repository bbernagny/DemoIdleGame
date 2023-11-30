using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayerMask;
    
    private Vector3 lastPosition;

    public event Action OnClicked, OnExit; 


    public void Update() 
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    public bool IsPointerOverUI() 
        => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPos()
    {
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
       
        RaycastHit2D raycastHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, placementLayerMask);

        if (raycastHit.collider != null)
        {
            lastPosition = raycastHit.point;
        }
        
        return lastPosition;
    }
}
