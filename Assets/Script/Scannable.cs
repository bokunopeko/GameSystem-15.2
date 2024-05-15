using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scannable : MonoBehaviour
{
    public enum Category
    {
        Environment,
        Enemy,
        Item
    }

    [SerializeField] private Category scanCategory;
    [SerializeField] private string scanName, scanDescription;

    public void Scan(ScanPopup popup )
    {
        // findobjectoftpye is expensive for performance to run in update.
        popup.DisplayScan(scanName, scanDescription);
        
    }
}
