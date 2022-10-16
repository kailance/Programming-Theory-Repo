using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFirewood : Storefront
{
    public override void UpdateUnitText()
    {
        unitText.text = unit.ToString();
        summaryUnitText.text = (goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Firewood>().demand, unit) * sale).ToString();
    }
    public override void Sell()
    {
        IntSwitch();
        expectedTotalSale = goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Firewood>().demand, unit) * itemsObject.GetComponent<Firewood>().price * sale;
        UpdateTotalText();
        UpdateUnitText();
    }
    protected override void UpdatePriceText()
    {
        priceText.text = itemsObject.GetComponent<Firewood>().price.ToString();
    }
    private void Awake()
    {
        unit = DataManagment.Instance.firewoodUnit;
    }
}
