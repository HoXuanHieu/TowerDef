using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class Wizard : MonoBehaviour
{
    [Header("References")]
    public LayerMask enemyMask;
    public GameObject BulletPrefab;
    public Transform firingPoint;
    public Transform Weapon;

    [Header("Attributes")]
    public float targetRange = 5f;
    public float bulletSpeed = 1f;
    public float rotationSpeed = 5f;

    private Transform target;
    private float timeshoot;

    private void Update() {
        if (target == null){
            FindTarget();
            return;
        } 
        RotateToTarget();
        if(!CheckRange())
        {
            target = null;
        }else{
            timeshoot += Time.deltaTime;
            if (timeshoot >= 1f / bulletSpeed){
                timeshoot = 0f;
                Shoot();
            }
        }
    }
    private void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0){
            target = hits[0].transform;
        }
    }
    private void Shoot(){
        GameObject bullet = Instantiate(BulletPrefab, firingPoint.position, Quaternion.identity);
        WizardBullet bulletScript = bullet.GetComponent<WizardBullet>();
        bulletScript.SetTarget(target);
    }
    private void RotateToTarget(){
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180f;   
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        Weapon.rotation = Quaternion.RotateTowards(Weapon.rotation, rotation, rotationSpeed * Time.deltaTime); 
    }
    private bool CheckRange(){
        return Vector2.Distance(target.position, transform.position) <= targetRange;
    }
    private void OnDrawGizmosSelected() {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, targetRange);
    }
}
