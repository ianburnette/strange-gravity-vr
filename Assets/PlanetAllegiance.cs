using System;
using System.Collections;
using UnityEngine;

public class PlanetAllegiance : MonoBehaviour {
    public delegate void AllegianceChange(PlanetSettings.Allegiance newAllegiance, PlanetSettings.Allegiance previousAllegiance);
    public static event AllegianceChange OnAllegianceChange;
    
    public delegate void AllegianceSet(PlanetSettings.Allegiance newAllegiance);
    public static event AllegianceSet OnAllegianceSet;
    
    public PlanetSettings.Allegiance MyAllegiance {
        get => myAllegiance;
        set {
            var previousAllegiance = myAllegiance;
            myAllegiance = value;
            OnAllegianceChange?.Invoke(myAllegiance, previousAllegiance);
            StartCoroutine(TransitionColor());
        }
    }

    // set initial value so that RoundManager picks up initial planet ownership
    void OnEnable() => OnAllegianceSet?.Invoke(myAllegiance);

    IEnumerator TransitionColor() {
        var targetMat = PlanetSettings.instance.materialByAllegiance[myAllegiance];
        while (myRenderer.sharedMaterial != targetMat) {
            myRenderer.material.Lerp(myRenderer.sharedMaterial, targetMat,
                    PlanetSettings.instance.materialTransitionTime * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public PlanetSettings.Allegiance myAllegiance;
    Renderer myRenderer;
    [SerializeField] PlanetSelection planetSelection;

    public void ChangeAllegiance(PlanetSettings.Allegiance newAllegiance) {
        MyAllegiance = newAllegiance;
        if (planetSelection.IsSelected) {
            planetSelection.Deselect();
        }
    }

    void Start() => myRenderer = GetComponent<Renderer>();
}
