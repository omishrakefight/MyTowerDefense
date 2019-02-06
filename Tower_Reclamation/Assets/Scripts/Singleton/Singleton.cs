using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Singleton : MonoBehaviour {

    public static Singleton Instance { get; private set; }

    [SerializeField] public int scenesChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        scenesChanged = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }



    //private static Singleton instance = null;
    //private static readonly object padlock = new object();

    //Singleton()
    //{
    //    int scenesChanged = 0;

    //}

    //public static Singleton Instance
    //{
    //    get
    //    {
    //        lock (padlock)
    //        {
    //            if (instance == null)
    //            {
    //                instance = new Singleton();
    //            }
    //            return instance;
    //        }
    //    }
    //}
}
