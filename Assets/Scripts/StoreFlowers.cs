using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFlowers : Storefront
{
    public List<int> expirationDate { get; private set; }
    public override void UpdateUnitText()
    {
        unitText.text = unit.ToString();
        summaryUnitText.text = (goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Flowers>().demand, unit) * sale).ToString();
    }
    public override void Sell()
    {
        IntSwitch();
        expectedTotalSale = goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Flowers>().demand, unit) * itemsObject.GetComponent<Flowers>().price * sale;
        UpdateTotalText();
        UpdateUnitText();
    }
    protected override void UpdatePriceText()
    {
        priceText.text = itemsObject.GetComponent<Flowers>().price.ToString();
    }
    public void RemoveUnitsFromExpirationDate()
    {
        for (int i = goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Flowers>().demand, unit) * sale; i > 0;)
        {
            if(i > expirationDate[1])
            {
                i -= expirationDate[1];
                expirationDate.RemoveRange(1, 2);
            }
            else
            {
                expirationDate[1] -= i;
                i = 0;
            }
        }
    }
    private void DecreaseDateOfExpiration()
    {
        print("Run Check to decrease experation date.");
        int p = expirationDate.Count;
        print(expirationDate.Count + " numbers in list");
        for (int i = 1; i < p;)
        {
            expirationDate[i + 1] -= 1;
            print(expirationDate[i + 1].ToString() + " Number of days left.");
            if(expirationDate[i+1] == 0)
            {
                unit -= expirationDate[i];
                expirationDate.RemoveRange(i, i + 1);
                p -= 2;
                print("Unit should have been reduced.");
            }
            else
            {
                i += 2;
            }
        }
    }
    public void AddExpirationDate()
    {
        if (itemsObject.GetComponent<Flowers>().unit != 0)
        {
            print("Added to Experation Date.");
            expirationDate.Add(itemsObject.GetComponent<Flowers>().unit);
            expirationDate.Add(10);
        }
    }
    public override void DailyUpdate()
    {
        DecreaseDateOfExpiration();
        UpdatePriceText();
        UpdateUnitText();
        UpdateTotalText();
    }
    private void Awake()
    {
        unit = DataManagment.Instance.flowersUnit;
        expirationDate = new List<int>();
        expirationDate = DataManagment.Instance.flowersExperationDates;
        if (expirationDate.Count == 0)
        {
            expirationDate.Add(0);
        }
    }
}
