using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavVelocity : MonoBehaviour {
    public Toggle startButton;
    public Toggle stopButton;
    public Toggle resumeButton;

    //public CAVE2WandNavigator navController;

    // Use this for initialization
    void Start()
    {
        //navController = GetComponentInParent<CAVE2WandNavigator>();

        //if (navController.navMode == CAVE2WandNavigator.NavigationMode.Walk)
        //{
        //    startButton.isOn = true;
        //}
        //else if (navController.navMode == CAVE2WandNavigator.NavigationMode.Drive)
        //{
        //    stopButton.isOn = true;
        //}
        //else if (navController.navMode == CAVE2WandNavigator.NavigationMode.Freefly)
        //{
        //    resumeButton.isOn = true;
        //}
    }

    void Update()
    {
        //UpdateButtons();
    }

    public void UpdateNavButtons()
    {
        //startButton.SetIsOnWithoutNotify(false);
        //stopButton.SetIsOnWithoutNotify(false);
        //resumeButton.SetIsOnWithoutNotify(false);

        //switch (navController.navMode)
        //{
        //    case (CAVE2WandNavigator.NavigationMode.Walk):
        //        startButton.SetIsOnWithoutNotify(true);
        //        break;
        //    case (CAVE2WandNavigator.NavigationMode.Drive):
        //        stopButton.SetIsOnWithoutNotify(true);
        //        break;
        //    case (CAVE2WandNavigator.NavigationMode.Freefly):
        //        resumeButton.SetIsOnWithoutNotify(true);
        //        break;
        //}
    }

}
