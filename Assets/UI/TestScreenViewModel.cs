using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreenViewModel : UIViewModel
{
    public UnityEngine.UI.Text ScoreTrackerText;
    void Start()
    {
        base.StoreGlobalState();
        Debug.Assert(ViewModelScreenType == ScreenType.Test, "TestScreenVideModel must always have screen type set to Test");
        ScoreTrackerText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTrackerText.text = State.PlayerScore.ToString();
    }
}
