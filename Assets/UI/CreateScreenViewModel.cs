using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScreenViewModel : UIViewModel
{
    public UnityEngine.UI.Text SavedObjectCounter;


    public UnityEngine.UI.Toggle IsSphereToggle;
    public UnityEngine.UI.Toggle IsExplodableToggle;
    public UnityEngine.UI.Toggle IsScorableToggle;

    // Start is called before the first frame update
    void Start()
    {
        base.StoreGlobalState();
        Debug.Assert(ViewModelScreenType == ScreenType.Create, "CreateScreenViewModel must always have screen type set to Create");
        Debug.Assert(IsSphereToggle != null);
        Debug.Assert(IsExplodableToggle != null);
        Debug.Assert(IsScorableToggle != null);
        SavedObjectCounter.text = "0";
    }

    public void SaveObject()
    {
        CustomSpawnableObject savedObject = new CustomSpawnableObject();
        savedObject.IsExploding = IsExplodableToggle.isOn;
        savedObject.IsScorable = IsScorableToggle.isOn;
        savedObject.Type = IsSphereToggle.isOn ? SpawnableObjectType.Circle : SpawnableObjectType.Square;
        savedObject.ColorToUse = Random.ColorHSV();
        State.SpawnableObjects.Add(savedObject);
        SavedObjectCounter.text = State.SpawnableObjects.Count.ToString();
    }
}
