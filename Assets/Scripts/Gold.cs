using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gold : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storefrontPriceText;
    [SerializeField] private TextMeshProUGUI warehousePriceText;
    [SerializeField] private TextMeshProUGUI storeLevelText;
    [SerializeField] private TextMeshProUGUI warehouseLevelText;
    [SerializeField] private TextMeshProUGUI storeDemandLevelText;
    [SerializeField] private TextMeshProUGUI warehouseStorageText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private TextMeshProUGUI unitTotalText;
    [SerializeField] private TextMeshProUGUI storeTotalText;
    [SerializeField] private TextMeshProUGUI storeUnitTotalText;
    [SerializeField] private GameObject insufficientFundsObject;
    [SerializeField] private GameObject itemsObject;
    [SerializeField] private GameObject storefrontObject;
    [SerializeField] private GameObject calendarObject;
    private readonly static int priceLevel2 = 1000;
    private readonly static int priceLevel3 = 10000;
    public int demandLevel { get; private set; }
    public int storageLevel { get; private set; }
    private int storeLevel;
    private int warehouseLevel;
    [SerializeField] private int gold;
    IEnumerator RemoveInsufficientFundsText()
    {
        yield return new WaitForSeconds(3);
        insufficientFundsObject.SetActive(false);
    }
    public void UpdateLevelText()
    {
        storeLevelText.text = "Store Lv:" + (storeLevel + 1);
        warehouseLevelText.text = "Warehouse LV:" + (warehouseLevel + 1);
        UpdateDemandAndStorage();
        UpdateUpgradeCosts();
    }
    private void UpdateDemandAndStorage()
    {
        demandLevel = storeLevel + 1;
        storeDemandLevelText.text = "Demand: " + demandLevel + "x";
        warehouseStorageText.text = storageLevel.ToString() + "/" + ((warehouseLevel + 1) * 100);
    }
    private void UpdateUpgradeCosts()
    {
        if (storeLevel == 0)
        {
            storefrontPriceText.text = "Cost: " + priceLevel2;
        }
        else if (storeLevel == 1)
        {
            storefrontPriceText.text = "Cost: " + priceLevel3;
        }
        else
        {
            storefrontPriceText.text = "Max";
        }
        if (warehouseLevel == 0)
        {
            warehousePriceText.text = "Cost: " + priceLevel2;
        }
        else if (warehouseLevel == 1)
        {
            warehousePriceText.text = "Cost: " + priceLevel3;
        }
        else
        {
            warehousePriceText.text = "Max";
        }
    }
    public void UpgradeStoreLevel()
    {
        if (storeLevel == 0 && gold >= priceLevel2)
        {
            storeLevel += 1;
            gold -= priceLevel2;
            UpdateLevelText();
            UpdateGoldText();
        }
        else if (storeLevel == 1 && gold >= priceLevel3)
        {
            storeLevel += 1;
            gold -= priceLevel3;
            UpdateLevelText();
            UpdateGoldText();
        }
        else
        {
            insufficientFundsObject.SetActive(true);
            StartCoroutine(RemoveInsufficientFundsText());
        }
    }
    public void UpgradeWarehouseLevel()
    {
        if (warehouseLevel == 0 && gold >= priceLevel2)
        {
            warehouseLevel += 1;
            gold -= priceLevel2;
            UpdateLevelText();
            UpdateGoldText();
        }
        else if (warehouseLevel == 1 && gold >= priceLevel3)
        {
            warehouseLevel += 1;
            gold -= priceLevel3;
            UpdateLevelText();
            UpdateGoldText();
        }
        else
        {
            insufficientFundsObject.SetActive(true);
            StartCoroutine(RemoveInsufficientFundsText());
        }
    }
    private void UpdateGoldText()
    {
        goldText.text = gold.ToString();
    }
    public void BuyItems()
    {
        if (itemsObject.GetComponent<Firewood>().total + itemsObject.GetComponent<Furniture>().total + itemsObject.GetComponent<Jewelry>().total + itemsObject.GetComponent<Grain>().total + itemsObject.GetComponent<Flowers>().total <= gold 
            && (itemsObject.GetComponent<Firewood>().unit * itemsObject.GetComponent<Firewood>().size) + (itemsObject.GetComponent<Furniture>().unit * itemsObject.GetComponent<Furniture>().size) +
            (itemsObject.GetComponent<Jewelry>().unit * itemsObject.GetComponent<Jewelry>().size) + (itemsObject.GetComponent<Grain>().unit * itemsObject.GetComponent<Grain>().size) +
            (itemsObject.GetComponent<Flowers>().unit * itemsObject.GetComponent<Flowers>().size) <= (warehouseLevel + 1) * 100 - storageLevel)
        {
            gold -= (itemsObject.GetComponent<Firewood>().total + itemsObject.GetComponent<Furniture>().total + itemsObject.GetComponent<Jewelry>().total + itemsObject.GetComponent<Grain>().total + itemsObject.GetComponent<Flowers>().total);
            storefrontObject.GetComponent<StoreFirewood>().unit += itemsObject.GetComponent<Firewood>().unit;
            while (itemsObject.GetComponent<Firewood>().unit != 0)
            {
                itemsObject.GetComponent<Firewood>().UnitChangeDown();
            }
            storefrontObject.GetComponent<StoreFurniture>().unit += itemsObject.GetComponent<Furniture>().unit;
            while (itemsObject.GetComponent<Furniture>().unit != 0)
            {
                itemsObject.GetComponent<Furniture>().UnitChangeDown();
            }
            storefrontObject.GetComponent<StoreJewelry>().unit += itemsObject.GetComponent<Jewelry>().unit;
            while (itemsObject.GetComponent<Jewelry>().unit != 0)
            {
                itemsObject.GetComponent<Jewelry>().UnitChangeDown();
            }
            storefrontObject.GetComponent<StoreGrain>().AddExpirationDate();
            storefrontObject.GetComponent<StoreGrain>().unit += itemsObject.GetComponent<Grain>().unit;
            while (itemsObject.GetComponent<Grain>().unit != 0)
            {
                itemsObject.GetComponent<Grain>().UnitChangeDown();
            }
            storefrontObject.GetComponent<StoreFlowers>().AddExpirationDate();
            storefrontObject.GetComponent<StoreFlowers>().unit += itemsObject.GetComponent<Flowers>().unit;
            while (itemsObject.GetComponent<Flowers>().unit != 0)
            {
                itemsObject.GetComponent<Flowers>().UnitChangeDown();
            }
            storefrontObject.GetComponent<StoreFirewood>().UpdateUnitText();
            storefrontObject.GetComponent<StoreFurniture>().UpdateUnitText();
            storefrontObject.GetComponent<StoreJewelry>().UpdateUnitText();
            storefrontObject.GetComponent<StoreGrain>().UpdateUnitText();
            storefrontObject.GetComponent<StoreFlowers>().UpdateUnitText();
            UpdateStorageAmount();
            UpdateDemandAndStorage();
            UpdateGoldText();
        }
        else
        {
            insufficientFundsObject.SetActive(true);
            StartCoroutine(RemoveInsufficientFundsText());
        }
    }
    private void UpdateStorageAmount()
    {
        storageLevel = (storefrontObject.GetComponent<StoreFirewood>().unit * itemsObject.GetComponent<Firewood>().size) + (storefrontObject.GetComponent<StoreFurniture>().unit * itemsObject.GetComponent<Furniture>().size) + (storefrontObject.GetComponent<StoreJewelry>().unit * 
            itemsObject.GetComponent<Jewelry>().size) + (storefrontObject.GetComponent<StoreGrain>().unit * itemsObject.GetComponent<Grain>().size) + (storefrontObject.GetComponent<StoreFlowers>().unit * itemsObject.GetComponent<Flowers>().size);
    }
    public int DemandCheck(int d, int u)
    {
        if (demandLevel * d > u)
        {
            return u;
        }
        else
        {
            return demandLevel * d;
        }
    }
    public void ExpectedTotalSales()
    {
        storefrontObject.GetComponent<StoreFirewood>().expectedTotalSale = DemandCheck(itemsObject.GetComponent<Firewood>().demand, storefrontObject.GetComponent<StoreFirewood>().unit) * itemsObject.GetComponent<Firewood>().price * storefrontObject.GetComponent<StoreFirewood>().sale;
        storefrontObject.GetComponent<StoreFurniture>().expectedTotalSale = DemandCheck(itemsObject.GetComponent<Furniture>().demand, storefrontObject.GetComponent<StoreFurniture>().unit) * itemsObject.GetComponent<Furniture>().price * storefrontObject.GetComponent<StoreFurniture>().sale;
        storefrontObject.GetComponent<StoreJewelry>().expectedTotalSale = DemandCheck(itemsObject.GetComponent<Jewelry>().demand, storefrontObject.GetComponent<StoreJewelry>().unit) * itemsObject.GetComponent<Jewelry>().price * storefrontObject.GetComponent<StoreJewelry>().sale;
        storefrontObject.GetComponent<StoreGrain>().expectedTotalSale = DemandCheck(itemsObject.GetComponent<Grain>().demand, storefrontObject.GetComponent<StoreGrain>().unit) * itemsObject.GetComponent<Grain>().price * storefrontObject.GetComponent<StoreGrain>().sale;
        storefrontObject.GetComponent<StoreFlowers>().expectedTotalSale = DemandCheck(itemsObject.GetComponent<Flowers>().demand, storefrontObject.GetComponent<StoreFlowers>().unit) * itemsObject.GetComponent<Flowers>().price * storefrontObject.GetComponent<StoreFlowers>().sale;
        storefrontObject.GetComponent<StoreFirewood>().UpdateTotalText();
        storefrontObject.GetComponent<StoreFurniture>().UpdateTotalText();
        storefrontObject.GetComponent<StoreJewelry>().UpdateTotalText();
        storefrontObject.GetComponent<StoreGrain>().UpdateTotalText();
        storefrontObject.GetComponent<StoreFlowers>().UpdateTotalText();
        IncomeSummary();
    }
    public void NextDaySales()
    {
        ExpectedTotalSales();
        gold += storefrontObject.GetComponent<StoreFirewood>().expectedTotalSale + storefrontObject.GetComponent<StoreFurniture>().expectedTotalSale + storefrontObject.GetComponent<StoreJewelry>().expectedTotalSale + 
            storefrontObject.GetComponent<StoreGrain>().expectedTotalSale + storefrontObject.GetComponent<StoreFlowers>().expectedTotalSale;
        storefrontObject.GetComponent<StoreFirewood>().unit -= DemandCheck(itemsObject.GetComponent<Firewood>().demand, storefrontObject.GetComponent<StoreFirewood>().unit) * storefrontObject.GetComponent<StoreFirewood>().sale;
        storefrontObject.GetComponent<StoreFurniture>().unit -= DemandCheck(itemsObject.GetComponent<Furniture>().demand, storefrontObject.GetComponent<StoreFurniture>().unit) * storefrontObject.GetComponent<StoreFurniture>().sale;
        storefrontObject.GetComponent<StoreJewelry>().unit -= DemandCheck(itemsObject.GetComponent<Jewelry>().demand, storefrontObject.GetComponent<StoreJewelry>().unit) * storefrontObject.GetComponent<StoreJewelry>().sale;
        storefrontObject.GetComponent<StoreGrain>().RemoveUnitsFromExpirationDate();
        storefrontObject.GetComponent<StoreGrain>().unit -= DemandCheck(itemsObject.GetComponent<Grain>().demand, storefrontObject.GetComponent<StoreGrain>().unit) * storefrontObject.GetComponent<StoreGrain>().sale;
        storefrontObject.GetComponent<StoreFlowers>().RemoveUnitsFromExpirationDate();
        storefrontObject.GetComponent<StoreFlowers>().unit -= DemandCheck(itemsObject.GetComponent<Flowers>().demand, storefrontObject.GetComponent<StoreFlowers>().unit) * storefrontObject.GetComponent<StoreFlowers>().sale;
        itemsObject.GetComponent<Firewood>().NextDayPrices();
        itemsObject.GetComponent<Furniture>().NextDayPrices();
        itemsObject.GetComponent<Jewelry>().NextDayPrices();
        itemsObject.GetComponent<Grain>().NextDayPrices();
        itemsObject.GetComponent<Flowers>().NextDayPrices();
        storefrontObject.GetComponent<StoreFirewood>().DailyUpdate();
        storefrontObject.GetComponent<StoreFurniture>().DailyUpdate();
        storefrontObject.GetComponent<StoreJewelry>().DailyUpdate();
        storefrontObject.GetComponent<StoreGrain>().DailyUpdate();
        storefrontObject.GetComponent<StoreFlowers>().DailyUpdate();
        ExpectedTotalSales();
        UpdateStorageAmount();
        UpdateDemandAndStorage();
        UpdateGoldText();
        SaveAllInfo();
    }
    private void SaveAllInfo()
    {
        DataManagment.Instance.firewoodUnit = storefrontObject.GetComponent<StoreFirewood>().unit;
        DataManagment.Instance.furnitureUnit = storefrontObject.GetComponent<StoreFurniture>().unit;
        DataManagment.Instance.jewelryUnit = storefrontObject.GetComponent<StoreJewelry>().unit;
        DataManagment.Instance.grainUnit = storefrontObject.GetComponent<StoreGrain>().unit;
        DataManagment.Instance.flowersUnit = storefrontObject.GetComponent<StoreFlowers>().unit;
        DataManagment.Instance.flowersExperationDates = storefrontObject.GetComponent<StoreFlowers>().expirationDate;
        DataManagment.Instance.grainExperationDates = storefrontObject.GetComponent<StoreGrain>().expirationDate;
        DataManagment.Instance.gold = gold;
        DataManagment.Instance.storeLevel = storeLevel;
        DataManagment.Instance.warehouseLevel = warehouseLevel;
        DataManagment.Instance.date = calendarObject.GetComponent<Calendar>().date;
        DataManagment.Instance.season = calendarObject.GetComponent<Calendar>().season;
        DataManagment.Instance.forestFireDate = calendarObject.GetComponent<Calendar>().MatchInt(calendarObject.GetComponent<Calendar>().forestFireEvent, 2);
        DataManagment.Instance.SaveFile();
    }
    public void IncomeSummary()
    {
        int i = (storefrontObject.GetComponent<StoreFirewood>().expectedTotalSale + storefrontObject.GetComponent<StoreFurniture>().expectedTotalSale + storefrontObject.GetComponent<StoreJewelry>().expectedTotalSale +
            storefrontObject.GetComponent<StoreGrain>().expectedTotalSale + storefrontObject.GetComponent<StoreFlowers>().expectedTotalSale);
        totalText.text = i.ToString();
        storeTotalText.text = i.ToString();
        int u = (DemandCheck(itemsObject.GetComponent<Firewood>().demand, storefrontObject.GetComponent<StoreFirewood>().unit) * storefrontObject.GetComponent<StoreFirewood>().sale) +
            (DemandCheck(itemsObject.GetComponent<Furniture>().demand, storefrontObject.GetComponent<StoreFurniture>().unit) * storefrontObject.GetComponent<StoreFurniture>().sale) +
            (DemandCheck(itemsObject.GetComponent<Jewelry>().demand, storefrontObject.GetComponent<StoreJewelry>().unit) * storefrontObject.GetComponent<StoreJewelry>().sale) +
            (DemandCheck(itemsObject.GetComponent<Grain>().demand, storefrontObject.GetComponent<StoreGrain>().unit) * storefrontObject.GetComponent<StoreGrain>().sale) +
            (DemandCheck(itemsObject.GetComponent<Flowers>().demand, storefrontObject.GetComponent<StoreFlowers>().unit) * storefrontObject.GetComponent<StoreFlowers>().sale);
        unitTotalText.text = u.ToString();
        storeUnitTotalText.text = u.ToString();
    }
    public void UpdatePriceText()
    {
        storefrontObject.GetComponent<StoreFirewood>().priceText.text = itemsObject.GetComponent<Firewood>().price.ToString();
        storefrontObject.GetComponent<StoreFurniture>().priceText.text = itemsObject.GetComponent<Furniture>().price.ToString();
        storefrontObject.GetComponent<StoreJewelry>().priceText.text = itemsObject.GetComponent<Jewelry>().price.ToString();
        storefrontObject.GetComponent<StoreGrain>().priceText.text = itemsObject.GetComponent<Grain>().price.ToString();
        storefrontObject.GetComponent<StoreFlowers>().priceText.text = itemsObject.GetComponent<Flowers>().price.ToString();
    }
    private void Awake()
    {
        gold = DataManagment.Instance.gold;
        storeLevel = DataManagment.Instance.storeLevel;
        warehouseLevel = DataManagment.Instance.warehouseLevel;
        UpdateGoldText();
    }
}
