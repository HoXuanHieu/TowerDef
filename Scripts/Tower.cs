using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Tower 
{
    public string name;
    public int price;
    public GameObject prefab;

    public Tower(string _name, int _price, GameObject _prefab)
    {
        name = _name;
        price = _price;
        prefab = _prefab;
    }
}
