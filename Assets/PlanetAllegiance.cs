using UnityEngine;

public class PlanetAllegiance : MonoBehaviour {
    public PlanetSettings.Allegiance MyAllegiance {
        get => myAllegiance;
        set {
            myAllegiance = value;
            myRenderer.sharedMaterial = PlanetSettings.instance.materialByAllegiance[myAllegiance];
        }
    }

    public PlanetSettings.Allegiance myAllegiance;
    Renderer myRenderer;
    [SerializeField] PlanetSelection planetSelection;

    public void ChangeAllegiance(PlanetSettings.Allegiance newAllegiance) {
        myAllegiance = newAllegiance;
        if (planetSelection.IsSelected) {
            planetSelection.Deselect();
        }
    }

    void Start() => myRenderer = GetComponent<Renderer>();
}
