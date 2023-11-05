using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer sprite;
    public Color hoverColor;
    public GameObject towerPrefab;
    private Color startcolor;
    public Archer archer;
    private void Start()
    {
        startcolor = sprite.color;
    }
    private void OnMouseEnter()
    {
        sprite.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sprite.color = startcolor;
    }

    private void OnMouseDown()
    {
        if(UIManager.main.IsHovering())
        {
            return;
        }
        if (towerPrefab != null)
        {
            archer.OpenUpgradeUI();
            return;
        }
        Tower towerToBuild = BuildeManager.main.GetTowerPrefab();

        if(towerToBuild.price > LevelManager.main.money)
        {
            return;
        }
        LevelManager.main.SpenMoney(towerToBuild.price);
        towerPrefab = (GameObject)Instantiate(towerToBuild.prefab, new Vector2(transform.position.x, transform.position.y + 0.18f), Quaternion.identity);
        archer = towerPrefab.GetComponent<Archer>();
    }
}
