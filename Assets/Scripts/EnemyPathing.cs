﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] WaveConfig waveConfig;
    List<Transform> waypoints;
    float moveSpeed = 2f;
    int waypointIndex = 0;

    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.MoveSpeed;
       
       
        transform.position = waypoints[0].position;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
      
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (transform.position == targetPosition)
                waypointIndex++;
        }
        else
            Destroy(gameObject);
    }

}
