using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportCard : MonoBehaviour
{
    public Text gradeText;
    public VerticalLayoutGroup verticalBox;
    public Text rescuedPersonEntryPrefab;
    public Color easterEggColor;
    public void Initialise ()
    {
        int score = GameManager.instance.score;

        if (score >= 40)
        {
            gradeText.text = "A+";
        }
        else if (score >= 30)
        {
            gradeText.text = "A";
        }
        else if (score >= 20)
        {
            gradeText.text = "B";
        }
        else if (score >= 10)
        {
            gradeText.text = "C";
        }
        else if (score >= 0)
        {
            gradeText.text = "F";
        }

        foreach (var item in verticalBox.GetComponentsInChildren<Text>())
        {
            Destroy(item.gameObject);
        }

        foreach (string name in GameManager.instance.rescuedPersonNames)
        {
            Text entry = Instantiate(rescuedPersonEntryPrefab, verticalBox.transform);
            entry.text = "- " + name;

            if (name == "Cedar Bishop" || name == "Max Heins" ||name == "Ivan Karyakin" ||
                name == "Guido Gautsch" || name == "Jessica Fredricksen" || name == "Shaleise-rose Leishman")
            {
                entry.color = easterEggColor;
                entry.fontStyle = FontStyle.Italic;
            }
        }
    }
}
