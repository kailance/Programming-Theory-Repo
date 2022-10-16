using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Calendar : MonoBehaviour
{
    public int date;
    public int season;
    private string eventString;
    public ArrayList springFestivalEvent { get; private set; } = new ArrayList();
    public ArrayList fallFestivalEvent { get; private set; } = new ArrayList();
    public ArrayList forestFireEvent { get; private set; } = new ArrayList();
    [SerializeField] private TextMeshProUGUI dateAndSeason;
    [SerializeField] private TextMeshProUGUI eventText;
    [SerializeField] private TextMeshProUGUI[] calendarText;
    [SerializeField] private string[] calendarString;
    [SerializeField] private GameObject itemsObject;

    public void NextDay()
    {
        UpdateDate();
        eventString = "";
        ClearModificationEffects();
        RunAllEventChecks();
        UpdateString();
    }
    private void UpdateDate()
    {
        date++;
        if (date > 10)
        {
            date = 1;
            season++;
            if (season > 3)
            {
                season = 0;
                AddRandomEventInfo();
                UpdateCalanderText();
            }
        }
    }
    public string Season()
    {
        if (season == 0)
        {
            return "Spring";
        }
        else if (season == 1)
        {
            return "Summer";
        }
        else if (season == 2)
        {
            return "Fall";
        }
        else
        {
            return "Winter";
        }
    }
    private void UpdateString()
    {
        dateAndSeason.text = "Day " + date + " of " + Season();
        eventText.text = eventString;
    }
    private void AddToCalendarString(ArrayList e)
    {
        if (e[10].Equals(true))
        {
            if (MatchInt(e,1) == 0)
            {
                calendarString[MatchInt(e, 2) - 1] += e[0].ToString();
            }
            if (MatchInt(e, 1) == 1)
            {
                calendarString[MatchInt(e, 2) + 9] += e[0].ToString();
            }
            if (MatchInt(e, 1) == 2)
            {
                calendarString[MatchInt(e, 2) + 19] += e[0].ToString();
            }
            if (MatchInt(e, 1) == 3)
            {
                calendarString[MatchInt(e, 2) + 29] += e[0].ToString();
            }
        }
    }
    private void UpdateCalanderText()
    {
        AddToCalendarString(springFestivalEvent);
        AddToCalendarString(fallFestivalEvent);
        AddToCalendarString(forestFireEvent);
        for (int i = 0; i <= 38; i++)
        {
            calendarText[i].text = calendarString[i];
        }
    }
    private void AddFixedEventInfo()
    {
        //Event Info goes name, season, day, price effect, demand effect, firewood bool, furniture bool, jewelry bool, grain bool, flowers bool, and calander bool to tell if it shows up on the calander. 
        springFestivalEvent.Add("Spring Festival");
        springFestivalEvent.Add(0);
        springFestivalEvent.Add(5);
        springFestivalEvent.Add(.5f);
        springFestivalEvent.Add(2.5f);
        springFestivalEvent.Add(true);
        springFestivalEvent.Add(true);
        springFestivalEvent.Add(true);
        springFestivalEvent.Add(true);
        springFestivalEvent.Add(true);
        springFestivalEvent.Add(true);
        fallFestivalEvent.Add("Fall Festival");
        fallFestivalEvent.Add(2);
        fallFestivalEvent.Add(9);
        fallFestivalEvent.Add(.5f);
        fallFestivalEvent.Add(2.5f);
        fallFestivalEvent.Add(true);
        fallFestivalEvent.Add(true);
        fallFestivalEvent.Add(true);
        fallFestivalEvent.Add(true);
        fallFestivalEvent.Add(true);
        fallFestivalEvent.Add(true);
        forestFireEvent.Add("Forest Fire");
        forestFireEvent.Add(1);
        forestFireEvent.Add(Random.Range(1,16));
        forestFireEvent.Add(4f);
        forestFireEvent.Add(2f);
        forestFireEvent.Add(true);
        forestFireEvent.Add(true);
        forestFireEvent.Add(false);
        forestFireEvent.Add(true);
        forestFireEvent.Add(true);
        forestFireEvent.Add(false);
    }
    private void AddRandomEventInfo()
    {
        //change day of event to effect if occurs this year and when.
        forestFireEvent[2] = Random.Range(1, 16);
    }
    private float MatchFloat(ArrayList e, int i)
    {
        float value = 0;
        while (value < 11)
        {
            if (e[i].Equals(value))
            {
                return value;
            }
            else
            {
                value += .1f;
            }
        }
        return 0;
    }
    public int MatchInt(ArrayList e, int i)
    {
        int value = 0;
        while (value < 11)
        {
            if (e[i].Equals(value))
            {
                return value;
            }
            else
            {
                value += 1;
            }
        }
        return 0;
    }
    private void RunAllEventChecks()
    {
        AddEventEffect(springFestivalEvent);
        AddEventEffect(fallFestivalEvent);
        AddEventEffect(forestFireEvent);
    }
    private void AddEventEffect(ArrayList e)
    {
        //determend if event happend today and add effects if so.
        if (e[1].Equals(season) && e[2].Equals(date))
        {
            eventString += "A " + e[0] + " is occuring.\r\n";
            float priceMod = MatchFloat(e, 3);
            float demandMod = MatchFloat(e, 4);
            if (e[5].Equals(true))
            {
                itemsObject.GetComponent<Firewood>().eventPriceMod += priceMod;
                itemsObject.GetComponent<Firewood>().eventDemandMod += demandMod;
                eventString += "Event is effecting firewood prices and demand.\r\n";
            }
            if (e[6].Equals(true))
            {
                itemsObject.GetComponent<Furniture>().eventPriceMod += priceMod;
                itemsObject.GetComponent<Furniture>().eventDemandMod += demandMod;
                eventString += "Event is effecting furniture prices and demand.\r\n";
            }
            if (e[7].Equals(true))
            {
                itemsObject.GetComponent<Jewelry>().eventPriceMod += priceMod;
                itemsObject.GetComponent<Jewelry>().eventDemandMod += demandMod;
                eventString += "Event is effecting jewelry prices and demand.\r\n";
            }
            if (e[8].Equals(true))
            {
                itemsObject.GetComponent<Grain>().eventPriceMod += priceMod;
                itemsObject.GetComponent<Grain>().eventDemandMod += demandMod;
                eventString += "Event is effecting grain prices and demand.\r\n";
            }
            if (e[9].Equals(true))
            {
                itemsObject.GetComponent<Flowers>().eventPriceMod += priceMod;
                itemsObject.GetComponent<Flowers>().eventDemandMod += demandMod;
                eventString += "Event is effecting flowers prices and demand.\r\n";
            }
        }
    }
    private void ClearModificationEffects()
    {
        itemsObject.GetComponent<Firewood>().eventPriceMod = 1;
        itemsObject.GetComponent<Firewood>().eventDemandMod = 1;
        itemsObject.GetComponent<Furniture>().eventPriceMod = 1;
        itemsObject.GetComponent<Furniture>().eventDemandMod = 1;
        itemsObject.GetComponent<Jewelry>().eventPriceMod = 1;
        itemsObject.GetComponent<Jewelry>().eventDemandMod = 1;
        itemsObject.GetComponent<Grain>().eventPriceMod = 1;
        itemsObject.GetComponent<Grain>().eventDemandMod = 1;
        itemsObject.GetComponent<Flowers>().eventPriceMod = 1;
        itemsObject.GetComponent<Flowers>().eventDemandMod = 1;
    }
    void Awake()
    {
        calendarString = new string[39];
        AddFixedEventInfo();
        if (DataManagment.Instance.loadGame)
        {
            forestFireEvent[2] = DataManagment.Instance.forestFireDate;
            date = DataManagment.Instance.date;
            season = DataManagment.Instance.season;
        }
        ClearModificationEffects();
        RunAllEventChecks();
        UpdateCalanderText();
        UpdateString();
    }
}
