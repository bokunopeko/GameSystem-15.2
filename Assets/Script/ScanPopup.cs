using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ScanPopup : MonoBehaviour
{
    // this is the gameobject which contains all our scan popup IO
    [SerializeField] private GameObject popupBox;

    //the text components to display name and desc
    [SerializeField] private Text popupName;
    [SerializeField] private Text popupDescription;
    //this is how long in second the popup will stay for
    [SerializeField] private float displayTime;
    //the time the popup appeared
    private float displayBeginTime;
    void Start()
    {
        
    }

   
    void Update()
    {
        //if he distance between right now and our start time is larger than our display time
   //if (realtime) 8   - 5   >          3
        //setactive = false
        if(Time.time - displayBeginTime > displayTime)
        {
            popupBox.SetActive(false);
        }
    }

    public void DisplayScan(string name, string description)
    {
        //make the popup appear
      popupBox.SetActive(true);

      popupName.text = name;
      popupDescription.text = description;

        //store the time the popup
      displayBeginTime = Time.time;
    }
}
