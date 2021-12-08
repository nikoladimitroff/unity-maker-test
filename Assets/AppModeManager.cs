using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AppModeManager : MonoBehaviour
{
    // This might need to be converted to a Dictionary but given we only have 3 screens, a list is just as good
    public List<GameObject> ScreenList;
    public ScreenType ActiveScreen { get; private set; }

    private void OnValidate()
    {
        Debug.Assert(ScreenList.Count > 0, "The ModeManager requires at least 1 screen!");
        foreach (GameObject screenRoot in ScreenList)
        {
            Debug.Assert(screenRoot != null, "A screen must be non-null");
            UIViewModel viewModel = screenRoot.GetComponent<UIViewModel>();
            Debug.Assert(viewModel != null, "A screen must have a UIViewModel component!");
        }
    }
    void Start()
    {
        ActiveScreen = ScreenList[0].GetComponent<UIViewModel>().ViewModelScreenType;
        ActivateScreen(ActiveScreen);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateScreen(string nextScreenName)
    {
        ScreenType nextScreen;
        if (!System.Enum.TryParse<ScreenType>(nextScreenName, out nextScreen))
        {
            Debug.LogErrorFormat("Invalid name for a screen: {}", nextScreenName);
            return;
        }

        ActivateScreen(nextScreen);
    }

    private void ActivateScreen(ScreenType nextScreen)
    {
        foreach (GameObject screenRoot in ScreenList)
        {
            UIViewModel viewModel = screenRoot.GetComponent<UIViewModel>();
            screenRoot.SetActive(viewModel.ViewModelScreenType == nextScreen);
        }
        ActiveScreen = nextScreen;
        Debug.LogFormat("Activating screen {0}", nextScreen);
    }
}
