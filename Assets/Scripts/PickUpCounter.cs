using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCounter : MonoBehaviour
{
    public int points = 0;

    private void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Coins : " + points);
        
    }
}
