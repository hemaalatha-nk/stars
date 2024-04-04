using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class constellation1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yourButton;
    PointerEventData pointerData;

    void Start()
    {
        pointerData = new PointerEventData(EventSystem.current);
        Button btn = yourButton.GetComponent<Button>();
       
        ploting_stars sn = gameObject.GetComponent<ploting_stars>();
        btn.onClick.AddListener(sn.TaskOnClick);

    }
     void Update()
    {
        
    }
   
}
