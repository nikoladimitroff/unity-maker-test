using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SpawnablePrefabInfo
{
    public GameObject Prefab;
    public SpawnableObjectType Type;
    public Sprite Icon;
}

public class PlaceScreenViewModel : UIViewModel
{
    public GameObject SavedObjectUI;
    public GameObject SavedObjectScrollList;

    // If only Unity supported Dictionaries in the Editor...
    public List<SpawnablePrefabInfo> PrefabPairs;

    private CustomSpawnableObject NextObjectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(ViewModelScreenType == ScreenType.Place, "PlaceScreenViewModel must always have screen type set to Place");
    }

    private void OnEnable()
    {
        if (State == null)
        {
            base.StoreGlobalState();
            return;
        }
        for (int i = 0; i < State.SpawnableObjects.Count; i++)
        {
            CustomSpawnableObject savedObject = State.SpawnableObjects[i];
            GameObject spawnSelectionButton = Instantiate(SavedObjectUI, SavedObjectScrollList.transform);

            RectTransform rect = spawnSelectionButton.GetComponent<RectTransform>();
            rect.anchoredPosition = rect.anchoredPosition + new Vector2(rect.rect.width * 1.25f * (i+1), 0);

            Button button = spawnSelectionButton.GetComponent<Button>();
            button.onClick.AddListener(() => NextObjectToSpawn = savedObject);
            Image image = spawnSelectionButton.transform.GetChild(0).GetComponent<Image>();
            image.sprite = PrefabPairs.Find(pair => pair.Type == savedObject.Type).Icon;
            image.color = savedObject.ColorToUse;
            image.SetAllDirty();

            spawnSelectionButton.SetActive(true);
        }
    }
   
   private void OnDisable()
   {
       NextObjectToSpawn = null;
       foreach (Transform child in SavedObjectScrollList.transform)
       {
           Destroy(child.gameObject);
       }
   }
   
   // Update is called once per frame
   void Update()
   {
       if (NextObjectToSpawn == null || !Input.GetMouseButtonDown(0))
       {
            return;
       }
       RaycastHit hitInfo;
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hitInfo))
        {
            return;
        }
        if (!hitInfo.collider.gameObject.CompareTag("Floor"))
        {
             return;
        }
        GameObject toSpawn = PrefabPairs.Find(pair => pair.Type == NextObjectToSpawn.Type).Prefab;
        GameObject spawnedPrefab = Instantiate(toSpawn, hitInfo.point, Quaternion.identity, hitInfo.collider.gameObject.transform);
        spawnedPrefab.GetComponent<Renderer>().material.SetColor("_Color", NextObjectToSpawn.ColorToUse);
       if (NextObjectToSpawn.IsExploding)
       {
           spawnedPrefab.GetComponent<Clickable>().ClickableFlags.Add(Clickable.ClickableBehaviourType.Explodable);
       }
       if (NextObjectToSpawn.IsScorable)
       {
           spawnedPrefab.GetComponent<Clickable>().ClickableFlags.Add(Clickable.ClickableBehaviourType.Scorable);
       }
   }
}
