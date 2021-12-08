using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public enum ClickableBehaviourType
    {
        Explodable,
        Scorable,
    }

    // This should be a bitmask but Unity makes it hard to create something as simple as a bitmask editor field so use the poor man's bitmask
    public List<ClickableBehaviourType> ClickableFlags;
    private GlobalSettings SettingsInstance;
    private GameState State;
    private AppModeManager UIManager;
    // Start is called before the first frame update
    void Start()
    {
        SettingsInstance = gameObject.scene.GetRootGameObjects()
            .Select(gameObject => gameObject.GetComponent<GlobalSettings>())
            .First(settings => settings != null);

        Debug.Assert(SettingsInstance != null, "Can't use Clickables without a Settings object");
        State = SettingsInstance.gameObject.GetComponent<GameState>();
        UIManager = SettingsInstance.gameObject.GetComponent<AppModeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0) || UIManager.ActiveScreen != ScreenType.Test)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit) || hit.collider.gameObject != this.gameObject)
        {
            return;
        }

        // If we had more flags this might be turned into something a bit more sophisticated like
        // a Dict<Flag, Action> with dynamic subscription.
        if (ClickableFlags.Contains(ClickableBehaviourType.Explodable))
        {
            Explode();
        }
        if (ClickableFlags.Contains(ClickableBehaviourType.Scorable))
        {
            Score();
        }
    }

    private void Explode()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && SettingsInstance.ExplosionSFX != null)
        {
            audioSource.enabled = true;
            audioSource.PlayOneShot(SettingsInstance.ExplosionSFX);
        }
        Instantiate(SettingsInstance.ExplosionVFX, this.transform);
        Destroy(this.gameObject, 0.75f); // todo: get rid of the magic number
    }
    private void Score()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && SettingsInstance.ScoreSFX != null)
        {
            audioSource.enabled = true;
            audioSource.PlayOneShot(SettingsInstance.ScoreSFX);
        }
        State.PlayerScore += SettingsInstance.ScoringStep;
    }
}
