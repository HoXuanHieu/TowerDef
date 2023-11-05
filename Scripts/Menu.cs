using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Menu : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI moneyText;
    public Animator anim;
    
    private bool MenuOpen = true;
    public void ToggleMenu(){
        MenuOpen = !MenuOpen;
        anim.SetBool("MenuOpen", MenuOpen);
    }
    private void OnGUI() {
        moneyText.text = LevelManager.main.money.ToString();
    }
    public void SelectTower(int index){
        BuildeManager.main.SelectTower(index);
    }
}
