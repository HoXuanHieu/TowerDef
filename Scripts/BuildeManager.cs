using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildeManager : MonoBehaviour
{
    public static BuildeManager main;
    [Header("References")]
    // public GameObject[] towerPrefab;
    public Tower[] towerPrefab;
    private int selectedTower = 0;
    private void Awake() {
        main = this;
    }
    public Tower GetTowerPrefab(){
        return towerPrefab[selectedTower];
    }

    public void SelectTower(int index){
        selectedTower = index;
    }
}
