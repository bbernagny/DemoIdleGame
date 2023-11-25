using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectDatabase objectDatabase;


    public void Start()
    {
        objectDatabase.InitializeDatabase();
    }

    public void Update()
    {
        Debug.Log(objectDatabase.GetData(1).Name);
        //if ()
        //{

        //}
    }
}
