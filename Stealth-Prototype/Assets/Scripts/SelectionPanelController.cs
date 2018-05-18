using UnityEngine;
using UnityEngine.UI;

public class SelectionPanelController : MonoBehaviour {

    private Image selectionPanel;
    private Text selectionText;

	// Use this for initialization
	void Start () {
        selectionPanel = GetComponent<Image>();
        selectionText = GetComponentInChildren<Text>();

        selectionPanel.enabled = false;
        selectionText.enabled = false;
	}

    public void Display(string text, Color color) {

        //enable/show 

        selectionPanel.enabled = true;
        selectionText.enabled = true;
        

        //display text
        selectionText.text = text;
        selectionText.color = color;
        //Debug.Log("displaying " + text + " in color " + color);
    }

    public void Close()
    {
        selectionPanel.enabled = false;
        selectionText.enabled = false;
    }
    
}
