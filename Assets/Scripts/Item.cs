using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    protected int basePrice { get; set; }
    public int price;
    protected int unit;
    protected int total;
    protected float priceModifier;
    protected float springMin;
    protected float springMax;
    protected float summerMin;
    protected float summerMax;
    protected float fallMin;
    protected float fallMax;
    protected float winterMin;
    protected float winterMax;
    protected int experationDate;
    protected int size;
    public int demand { get; protected set; }
    protected string itemName;
    [SerializeField] private TextMeshProUGUI baseString;
    [SerializeField] private TextMeshProUGUI nameString;
    [SerializeField] private TextMeshProUGUI priceString;
    protected void DisplayItemInfo()
    {
        baseString.text = basePrice.ToString();
        nameString.text = itemName;
        priceString.text = price.ToString();
    }
    protected float RandomModifier()
    {
        if (GameObject.FindGameObjectWithTag("Calander").GetComponent<Calander>().season == 0)
        {
            float i =
            Random.Range(springMin, springMax);
            return i;
        }
        else if (GameObject.FindGameObjectWithTag("Calander").GetComponent<Calander>().season == 1)
        {
            float i =
            Random.Range(summerMin, summerMax);
            return i;
        }
        else if (GameObject.FindGameObjectWithTag("Calander").GetComponent<Calander>().season == 2)
        {
            float i =
            Random.Range(fallMin, fallMax);
            return i;
        }
        else
        {
            float i =
            Random.Range(winterMin, winterMax);
            return i;
        }
    }
}