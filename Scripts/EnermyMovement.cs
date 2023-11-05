using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyMovement : MonoBehaviour
{
    [Header("Rigidbody")]
    public Rigidbody2D rb;
    [Header("Attributes")]
    public float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start(){
        target = LevelManager.main.path[pathIndex];
    }

    private void Update(){
        if (Vector2.Distance(target.position, transform.position) < 0.1f){
            pathIndex++; 
            if (pathIndex == LevelManager.main.path.Length){
                EnermySpawn.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            } else {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }
    private void FixedUpdate() {
        Vector2 direction = target.position - transform.position;
        rb.velocity = direction.normalized * moveSpeed;
    }
}
