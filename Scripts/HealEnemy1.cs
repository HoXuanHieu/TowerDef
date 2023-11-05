using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEnemy1 : MonoBehaviour
{
    [Header("Attributes")]
    public int heal = 2;
    public int dropMoney = 10;
    private bool isDead = false;
    public void takeDamge(int damage){
        heal -= damage;
        if (heal <= 0){
            EnermySpawn.onEnemyDestroy.Invoke(); 
            LevelManager.main.IncreaseMoney(dropMoney);
            isDead = true;
            Destroy(gameObject);
        }
    }
}
