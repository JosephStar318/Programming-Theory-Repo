using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;
    [SerializeField] float speed = 19;
    public GameObject origin;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip punchAudio;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        AttackEnemy();
    }

    private void AttackEnemy()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("Enemy")))
        {
            if (hit.distance < 5)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(hit.transform, transform);
                    audioSource.PlayOneShot(punchAudio);
                }
            }
        }
    }

    private void MovePlayer()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        transform.rotation = origin.transform.rotation;
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        animator.SetFloat("Speed_f", verticalInput*verticalInput + horizontalInput*horizontalInput);
    }
}
