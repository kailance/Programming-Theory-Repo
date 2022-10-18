using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowers : Item
{ // INHERITANCE
    void Awake()
    {
        basePrice = 5;
        itemName = "Flowers";
        total = 0;
        unit = 0;
        size = 1;
        springMin = 1.1f;
        springMax = 1.2f;
        summerMin = 1f;
        summerMax = 1.1f;
        fallMin = .9f;
        fallMax = 1f;
        winterMin = 1.1f;
        winterMax = 1.3f;
        priceModifier = RandomModifier();
        demand = Mathf.RoundToInt(25 * priceModifier);
        price = Mathf.RoundToInt(basePrice * priceModifier);
        DisplayItemInfo();
    }
    // POLYMORPHISM
    public override void NextDayPrices()
    {
        priceModifier = RandomModifier();
        price = Mathf.RoundToInt(basePrice * priceModifier * DoubleEventEffect(eventPriceMod));
        demand = Mathf.RoundToInt(50 * priceModifier * DoubleEventEffect(eventDemandMod));
        CalculateTotal();
        DisplayItemInfo();
    }
    // ABSTRACTION
    private float DoubleEventEffect(float i)
    {
        if(i > 1)
        {
            return i * 2;
        }
        else
        {
            return 1;
        }
    }
}
