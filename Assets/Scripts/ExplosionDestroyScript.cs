using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyScript : MonoBehaviour
{
    [SerializeField] private float timer = 1f;
    void Start()
    {
        Destroy(gameObject, timer);
    }
    
   
   
    // Update is called once per frame
 
}
