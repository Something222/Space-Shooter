using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Config Stats")]
    [SerializeField] private float health = 100;
    [SerializeField] private float shotCounter;
    [SerializeField] private float minTimeBetweenShots = 1f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private float projectileSpeed = -2f;

    [Header("Enemy Config Art")]
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float explosionTimer = 1f;

    [Header("Sound")]
    [SerializeField] private AudioClip laserSFX;
    [SerializeField] [Range(0, 1)] private float laserVolume = .2f;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField][Range(0,1)] private float deathSoundVolume = .7f;

    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject laser = (Instantiate(projectile, transform.position, Quaternion.identity)) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position,laserVolume);
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
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
        var explosion = Instantiate(explosionParticle, transform.position, transform.rotation) as GameObject;
        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(explosion,explosionTimer);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
    }
}
