using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Item", order = 1)]
public class Item : ScriptableObject {

    public new string name;

    //key card, weapon, ammo, etc
    public string type;

    public Sprite image;

    public int stackNum = 1;
    public int quantity;

    public Item(string name, Sprite image, int quantity)
    {
        this.name = name;
        this.image = image;
        this.quantity = quantity;
    }

    public override string ToString()
    {
        return "Name: " + name + " Number: " + quantity + "/" + stackNum;
    }
}
