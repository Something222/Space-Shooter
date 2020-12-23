using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    //Config Boundries
    [Header("Player")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float wallPadding = 1f;
    [SerializeField] private int health = 100;

    [Header("Projectiles")]
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float timeBetweenShots=.1f;

    [Header("Sound")]
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] private float deathSoundVolume = .7f;
    [SerializeField] private AudioClip laserSFX;
    [SerializeField] [Range(0, 1)] private float laserVolume = .2f;
    //Coroutines
    Coroutine shootingCoroutine;

    //worldspace boundries 
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    //to prevent clipping into the wall
   

    //Object References
    [SerializeField] private GameObject laserPrefab;
    void Start()
    {
        SetUpWorldSpace();
    }

   

    void Update()
    {
        Move();
        Shoot();
    }

   

    private void Move()
    {

        var deltaX = Input.GetAxisRaw("Horizontal")*Time.deltaTime*speed;
        var deltaY = Input.GetAxisRaw("Vertical")*Time.deltaTime*speed;
        var newXPos=transform.position.x+deltaX;
        var newYPos = transform.position.y+deltaY;
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Shoot"))
        {
           shootingCoroutine=StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Shoot"))
        {
            StopCoroutine(shootingCoroutine);
        }
    }
    private IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserVolume);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
     private void SetUpWorldSpace()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + wallPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - wallPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + wallPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - wallPadding;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DamageDealer>() != null)
        {
            DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
            ProccessHit(damageDealer);
        }
    }

    private void ProccessHit(DamageDealer damageDealer)
    {
        
        health -= damageDealer.GetDamage;
        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
       
    }
    private void OnDestroy()
    {
        FindObjectOfType<LevelLoader>().LoadGameOver();
    }
}
