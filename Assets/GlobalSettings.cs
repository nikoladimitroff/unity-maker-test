using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class stores all sorts of global settings 
public class GlobalSettings : MonoBehaviour
{
    [Header("Explosion settings")]
    public AudioClip ExplosionSFX;

    [Header("Score settings")]
    public AudioClip ScoreSFX;
    // The amount by which score should be incremented after a scorable is clicked
    public int ScoringStep;
}
