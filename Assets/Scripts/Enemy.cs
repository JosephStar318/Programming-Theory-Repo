using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ABSTRACTION
public abstract class Enemy : MonoBehaviour
{
    // ENCAPSULATION
    public float Health { get; set; }
    public float Damage { get; set; }
    public float Speed { get; set; }
    public bool IsHit { get; set; }
    public bool IsDead { get; set; }
    protected AudioSource audioSource;
    public void MoveTowardsPlayer(Transform from, Transform player)
    {
        if(!IsHit && !IsDead)
        {
            Vector3 lookDirection = new Vector3(player.position.x, from.position.y, player.position.z);
            from.LookAt(lookDirection);
            from.Translate(Speed * Time.deltaTime * Vector3.forward);
            from.GetComponent<Animator>().SetFloat("Speed_f", 1);
        }
    }

    public void EnemyPlayerCQC(Transform enemy, Transform player)
    {
        Vector3 dir = player.transform.position - enemy.position;
        if(dir.magnitude < 2)
        {
            DealDamage(enemy, player);
        }
    }

    public virtual void DealDamage(Transform enemy, Transform player)
    {
        IsHit = true;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = 0.3f * Damage * (enemy.forward + Vector3.up);
        StartCoroutine(IdleForSeconds(1));

    }
    public virtual void TakeDamage(Transform enemy, Transform player)
    {
        IsHit = true;
        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        rb.velocity = 0.7f * Damage * (player.forward + Vector3.up/2);
        Health -= Damage;
        if(Health < 0)
        {
            IsDead = true;
        }
        StartCoroutine(IdleForSeconds(1));
    }
    public void CheckEnemy()
    {
        if(transform.position.y < -10 || transform.position.y > 20)
        {
            Destroy(transform.gameObject);
            LevelManager.Instance.killCount++;
        }
        if(IsDead)
        {
            transform.GetComponent<Animator>().SetInteger("DeathType_int", 1);
            transform.GetComponent<Animator>().SetBool("Death_b", true);
            transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetComponent<Rigidbody>().useGravity = false;

        }
        
    }

    public IEnumerator IdleForSeconds(int t)
    {
        yield return new WaitForSeconds(t);
        IsHit = false;
    }
}
