using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    public Button button;
    public Text information;
    private string definition;

    // Update is called once per frame

    void Start()
    {
        button.onClick.AddListener(() => getButtonName(button));
    }

    void getButtonName(Button button)
    {
        Debug.Log("You have clicked the button!");
        name = button.name;
        switch (this.name)
        {
            case "Temporal":
                definition = "Temporal:\nThe parietal bone, cranial bone forming part of the side and top of the head.";
                displayText();
                break;

            case "Parietal":

                break;

            default:

                break;
        }
    }

    public void displayText()
    {
        information.text = definition;
    }

}
