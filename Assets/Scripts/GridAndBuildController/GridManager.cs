using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Transform _cam;
    //public Transform parentTransform;
    private int _id;



    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                GameObject spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                //spawnedTile.transform.SetParent(parentTransform, false);
               

                //TileID tileID = spawnedTile.GetComponent<TileID>();
                //tileID.Id = ++_id;
            }
        }

        _cam.transform.position = new Vector3((float)_width/2- 4f, (float)_height / 2 - 0.5f, -10);
    }
}
