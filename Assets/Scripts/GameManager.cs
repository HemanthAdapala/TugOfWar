using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON

    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                SetUpInstance();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static void SetUpInstance()
    {
        instance = FindAnyObjectByType<GameManager>();
        if (instance == null)
        {
            var manager = new GameObject
            {
                name = "GameManager"
            };
            instance = manager.AddComponent<GameManager>();
            DontDestroyOnLoad(manager);
        }
    }

    #endregion

    public void Start()
    {
        //
        
    }
}
