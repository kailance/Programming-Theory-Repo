using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowers : Item
{ 
    void Start()
    {
        basePrice = 5;
        itemName = "Flowers";
        Total = 0;
        Unit = 0;
        price = basePrice;
        DisplayItemInfo();
    }
}
