using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ScreenType
{
    Create,
    Place,
    Test
}

public class UIViewModel : MonoBehaviour
{
    public ScreenType ViewModelScreenType;

    protected GlobalSettings SettingsInstance;
    protected GameState State;
    // Start is called before the first frame update
    protected void StoreGlobalState()
    {
        SettingsInstance = gameObject.scene.GetRootGameObjects()
            .Select(gameObject => gameObject.GetComponent<GlobalSettings>())
            .First(settings => settings != null);

        Debug.Assert(SettingsInstance != null, "Can't use Clickables without a Settings object");
        State = SettingsInstance.gameObject.GetComponent<GameState>();
    }
}
