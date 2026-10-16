using UnityEngine;

public class QuestBoard : Interface
{
    [Header("Questboard")]
    [Tooltip("This is the Questboard UI object.")]
    public GameObject questboardUi;

    protected override void Update()
    {
        base.Update(); //use all of the code you inherent.

        if (timeHeld >= timeToHold && inRange)
        {
            questboardUi.SetActive(true);//set the questboardUI to active.
            if (debug)print("Questboard UI is active."); // print if the Questboard UI is active.
            timeHeld = timeToHold; //pause the time when it hit the timeToHold.
        }
        //if you are not inside the range turn the ui off and start to lower the time.
        else if (!inRange || timeHeld <= timeToHold)
        {
            taskDone = false;
            questboardUi.SetActive(false);//set the questboardUI to inactive.
            if (debug)print("Questboard UI is inactive.");//print if the Questboard UI is inactive.
        }


    }
}
