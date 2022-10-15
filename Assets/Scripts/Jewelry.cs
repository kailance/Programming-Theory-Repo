using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewelry : Item
{
    void Start()
    {
        basePrice = 500;
        itemName = "Jewelry";
        total = 0;
        unit = 0;
        size = 1;
        springMin = .9f;
        springMax = 1.05f;
        summerMin = 1f;
        summerMax = 1.1f;
        fallMin = 1.05f;
        fallMax = 1.2f;
        winterMin = 1f;
        winterMax = 1.1f;
        priceModifier = RandomModifier();
        demand = Mathf.RoundToInt(2 * priceModifier);
        price = Mathf.RoundToInt(basePrice * priceModifier);
        DisplayItemInfo();
    }
}
