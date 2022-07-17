using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButtonOnSpace : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = GetComponent<Button>(); 
    }


    void Update()
    {
        if(Input.GetKeyUp("space"))
        {
            button.Select();
            button.onClick.Invoke();
        }   
    }
}
