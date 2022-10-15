using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storefront : MonoBehaviour
{
    public int unit;
    public int expectedTotalSale;
    public int sale { get; protected set; }
    [SerializeField] public TextMeshProUGUI priceText;
    [SerializeField] protected TextMeshProUGUI unitText;
    [SerializeField] protected TextMeshProUGUI totalText;
    [SerializeField] protected TextMeshProUGUI summaryTotalText;
    [SerializeField] protected TextMeshProUGUI summaryUnitText;
    [SerializeField] protected GameObject itemsObject;
    [SerializeField] protected GameObject goldObject;
    void Start()
    {
        StartCoroutine(WaitToUpdate());
    }
    public virtual void UpdateUnitText()
    {
        unitText.text = unit.ToString();
    }
    public void UpdateTotalText()
    {
        totalText.text = expectedTotalSale.ToString();
        summaryTotalText.text = expectedTotalSale.ToString();
    }
    public virtual void Sell()
    {
        if(sale == 1)
        {
            sale = 0;
        }
        else
        {
            sale = 1;
        }
        goldObject.GetComponent<Gold>().ExpectedTotalSales();
    }
    protected virtual void UpdatePriceText()
    {
        priceText.text = itemsObject.GetComponent<Firewood>().price.ToString();
    }
    IEnumerator WaitToUpdate()
    {
        yield return new WaitForSeconds(.01f);
        goldObject.GetComponent<Gold>().UpdatePriceText();
        UpdateUnitText();
        goldObject.GetComponent<Gold>().ExpectedTotalSales();
        goldObject.GetComponent<Gold>().UpdateLevelText();
    }
    protected void IntSwitch()
    {
        if (sale == 1)
        {
            sale = 0;
        }
        else
        {
            sale = 1;
        }
    }
    public void DailyUpdate()
    {
        UpdatePriceText();
        UpdateUnitText();
        UpdateTotalText();
    }
}
