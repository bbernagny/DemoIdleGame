using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database", menuName ="Database")]

public class ObjectDatabase : ScriptableObject
{
    public List<ObjectData> objectData = new List<ObjectData>();

    private Dictionary<int, ObjectData> _objData
        = new Dictionary<int, ObjectData>();


    public void InitializeDatabase()
    {
        foreach (ObjectData objData in objectData)
        {
            if (!_objData.ContainsKey(objData.Id))
            {
                _objData.Add(objData.Id, objData);
            }
        }
        
    }

    public ObjectData GetData(int id)
    {
        if (_objData.TryGetValue(id, out ObjectData data))
        {
            return data;
        }
        return null;
    }

}

