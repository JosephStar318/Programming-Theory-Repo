using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Wrestler : Enemy
{
    private GameObject player;
    public static Wrestler instance;
    public AudioClip wrestlerGruntClip;
    public float health;
    public float damage;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Health = health;
        Damage = damage;
        Speed = speed;

        instance = this;
        player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTowardsPlayer(transform, player.transform);
        EnemyPlayerCQC(transform, player.transform);
        CheckEnemy();
    }
    public override void TakeDamage(Transform enemy, Transform player)
    {
        audioSource.PlayOneShot(wrestlerGruntClip);
        base.TakeDamage(enemy, player);
    }
}
