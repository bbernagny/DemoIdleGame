using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] private ObjectDatabase objectDatabase;

    [SerializeField] private int _startingGoldCoin;
    [SerializeField] private int _startingGemCoin;

    [SerializeField] private int _goldCoin;
    [SerializeField] private int _gemCoin;

    public int GetGemCoin { get { return _gemCoin; } set { _gemCoin = value; } }
    public int GetGoldCoin { get { return _goldCoin; } set { _goldCoin = value; } }



    public void Start()
    {
        _goldCoin = _startingGoldCoin;
        _gemCoin = _startingGemCoin;
        objectDatabase.InitializeDatabase();
    }

    public void Update()
    {
        Debug.Log(objectDatabase.GetData(1).Name);
        _goldCoinText.SetText(_goldCoin.ToString());
        _gemText.SetText(_gemCoin.ToString());
        
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




}
