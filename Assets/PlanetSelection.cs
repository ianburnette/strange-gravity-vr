using UnityEngine;
using UnityEngine.UI;
using Zinnia.Data.Type;
using Zinnia.Extension;

public class PlanetSelection : MonoBehaviour {
    bool isSelected;
    [SerializeField] Image selectionRing;
    [SerializeField] PlanetAllegiance planetAllegiance;
    [SerializeField] PlanetarySporeManager sporeManager;
    
    public bool IsSelected {
        get => isSelected;
        set {
            isSelected = value;
            selectionRing.enabled = isSelected;
        }
    }

    public PlanetAllegiance Allegiance => planetAllegiance;

    public void Select() {
        if(Allegiance.myAllegiance == PlanetSettings.Allegiance.player)
            IsSelected = true;
    }

    public void Deselect() => IsSelected = false;

    public void TransferSpores(PlanetSelection destination) => sporeManager.TransferSpores(destination.sporeManager);
}
