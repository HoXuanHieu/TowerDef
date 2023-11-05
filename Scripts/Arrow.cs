using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Rigidbody")]
    public Rigidbody2D rb;

    [Header("Attributes")]
    public float moveSpeed = 2f;
    public int damage = 1;
    public float lifeTime = 5f;
    private Transform target;
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = target.position - transform.position;
        rb.velocity = direction.normalized * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<HealEnemy1>().takeDamge(damage);
        Destroy(gameObject);
    }
    // make a arrow rotate with same with direction
    private void Update() {
        if (!target) return;
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90f;   
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation , rotation, 300 * Time.deltaTime);
    }

}
