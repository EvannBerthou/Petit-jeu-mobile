using System.Collections.Generic;
using UnityEngine;
using TrelloAPI;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI objects")]
    public TMP_InputField inputTitle;
    public TMP_InputField inputDescription;
    public Dropdown reportOptionsDropDown;

    void Start()
    {
        if (BugReport.instance != null)
            reportOptionsDropDown.AddOptions(BugReport.instance.reportType);

        inputTitle.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = Langue.textes.Title;
        inputDescription.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = Langue.textes.Description;
    }

    public void ReportIssue()
    {
        if (BugReport.instance != null)
        {
            BugReport.instance.SendReport(inputTitle.text, inputDescription.text, reportOptionsDropDown.captionText.text);
            
            // After reporting We clear the input fields so they are ready to be used again
            inputTitle.text = "";
            inputDescription.text = "";
        }
    }
}