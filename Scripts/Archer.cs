using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using UnityEditor;

public class Archer : MonoBehaviour
{
    [Header("References")]
    public LayerMask enemyMask;
    public GameObject arrowPrefab;
    public Transform firingPoint;
    public Transform BowRotation;
    public GameObject UpgradeUI;
    public Button upGradeButton;

    [Header("Attributes")]
    public float targetRange = 5f;
    public float arrowSpeed = 1f;
    public float rotationSpeed = 5f;
    public int baseUpgradeCost = 50;
    public Sprite level2;
    public Sprite level3;

    private float bpsBase;
    private float targetRangeBase;
    private Transform target;
    private float timeshoot;
    private int level = 1;
    private void Start() {
        bpsBase = arrowSpeed;
        targetRange = targetRangeBase;
        upGradeButton.onClick.AddListener(Upgrade);
    }
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
            if (timeshoot >= 1f / arrowSpeed){
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
        GameObject arrow = Instantiate(arrowPrefab, firingPoint.position, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetTarget(target);
    }
    private void RotateToTarget(){
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180f;   
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        BowRotation.rotation = Quaternion.RotateTowards(BowRotation.rotation, rotation, rotationSpeed * Time.deltaTime); 
    }
    public void OpenUpgradeUI(){
        UpgradeUI.SetActive(true);
        // upGradeButton.interactable = false;
    }
    public void CloseUpgradeUI(){
        UpgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    } 
    public void Upgrade(){
        if(baseUpgradeCost > LevelManager.main.money){
            return;
        }
        LevelManager.main.SpenMoney(baseUpgradeCost);
        level++;
        arrowSpeed = bpsBase * Mathf.Pow(level, 0.8f);
        targetRange = targetRangeBase * Mathf.Pow(level, 0.4f);
        CloseUpgradeUI();
        baseUpgradeCost=  CalCulateCost();
        
        // if (level == 2){
        //     this.GetComponent<SpriteRenderer>().sprite = level2;
        // }
        // if (level == 3){
        //     this.GetComponent<SpriteRenderer>().sprite = level2;

        //     upGradeButton.interactable = false;
        // }
    }
    public int CalCulateCost(){
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }
    private bool CheckRange(){
        return Vector2.Distance(target.position, transform.position) <= targetRange;
    }
    private void OnDrawGizmosSelected() {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, targetRange);
    }
}

