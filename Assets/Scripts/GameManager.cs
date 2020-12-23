using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance = null;
    public static int currlevel = 1;
    void Awake()
    {
      
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
