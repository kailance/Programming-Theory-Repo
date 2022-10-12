using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storefront : MonoBehaviour
{
    public int unit;
    public int expectedTotalSale;
    public int sale { get; private set; }
    [SerializeField] public TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI unitText;
    [SerializeField] private TextMeshProUGUI totalText;   
    [SerializeField] private GameObject itemsObject;
    [SerializeField] private GameObject goldObject;
    void Start()
    {
        StartCoroutine(WaitToUpdate());
    }
    public void UpdateUnitText()
    {
        unitText.text = unit.ToString();
    }
    public void UpdateTotalText()
    {
        totalText.text = expectedTotalSale.ToString();
    }
    public void Sell()
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
    IEnumerator WaitToUpdate()
    {
        yield return new WaitForSeconds(.01f);
        goldObject.GetComponent<Gold>().UpdatePriceText();
        UpdateUnitText();
        goldObject.GetComponent<Gold>().ExpectedTotalSales();
        goldObject.GetComponent<Gold>().UpdateLevelText();
    }
}
