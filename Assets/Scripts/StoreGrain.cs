using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGrain : Storefront
{
    public override void UpdateUnitText()
    {
        unitText.text = unit.ToString();
        summaryUnitText.text = (goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Grain>().demand, unit) * sale).ToString();
    }
    public override void Sell()
    {
        IntSwitch();
        expectedTotalSale = goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Grain>().demand, unit) * itemsObject.GetComponent<Grain>().price * sale;
        UpdateTotalText();
        UpdateUnitText();
    }
    protected override void UpdatePriceText()
    {
        priceText.text = itemsObject.GetComponent<Grain>().price.ToString();
    }
}
