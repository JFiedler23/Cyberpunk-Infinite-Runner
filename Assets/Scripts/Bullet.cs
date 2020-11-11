using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    public AnimationClip bulletHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(bulletSpeed, 0f);
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player" && collision.name != "Bullet(Clone)")
        {
            animator.SetTrigger("hit");
            rb.velocity = new Vector2(0f, 0f);
            StartCoroutine("DestroyTimer");
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(bulletHit.length);
        Destroy(gameObject);
    }
}
