using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayerMask;
    

    private Vector3 lastPosition;

    public Vector3 GetSelectedMapPos()
    {
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
       
        RaycastHit2D raycastHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, placementLayerMask);

        if (raycastHit.collider != null)
        {
            Debug.Log(raycastHit.collider.name);
            lastPosition = raycastHit.point;

            //TileID tileID = raycastHit.collider.GetComponent<TileID>();
            //Debug.Log(tileID.Id);
        }
        

        return lastPosition;
    }
}
