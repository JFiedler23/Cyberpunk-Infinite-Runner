using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //animation variables
    private Animator animator;
    public AnimationClip explosionAnimation;

    //bullet vars
    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;
    public float shootSpeed;

    //collider
    Collider2D myCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();

        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootSpeed);
            Instantiate(bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Bullet(Clone)")
        {
            StopCoroutine("Shoot");
            myCollider.enabled = false;
            animator.SetTrigger("destroy");
            StartCoroutine("DestroyTimer");
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(explosionAnimation.length);
        Destroy(gameObject);
    }
}
