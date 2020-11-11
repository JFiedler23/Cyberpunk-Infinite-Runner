using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : MonoBehaviour
{
    //movement variables
    public float moveSpeed;
    public float moveDistance;
    public float startPosition;

    //animation variables
    private Animator animator;
    public AnimationClip explosionAnimation;

    //collider
    Collider2D myCollider;

    void Start()
    {
        startPosition = transform.position.y;
        animator = GetComponent<Animator>();
        myCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        //moving drone up and down
        transform.position = new Vector3(transform.position.x, startPosition + Mathf.Sin(Time.time * moveSpeed) * moveDistance, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Bullet(Clone)")
        {
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
