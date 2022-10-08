using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Calander : MonoBehaviour
{
    public int date;
    public int season;
    public TextMeshProUGUI dateAndSeason;

    public void NextDay()
    {
        UpdateDate();
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
            }
        }
    }
    private string Season()
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
    }
    void Start()
    {
        date = 1;
        UpdateString();
    }
}
