using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {

    public new string name;

    public Sprite image;

    public int stackNum = 1;
    public int quantity;
}
