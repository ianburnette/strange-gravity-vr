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

    public void Select() {
        if(planetAllegiance.myAllegiance == PlanetSettings.Allegiance.player)
            IsSelected = true;
    }

    public void Deselect() => IsSelected = false;

    public void TransferSpores(TransformData transformData) => sporeManager.TransferSpores(transformData.TryGetGameObject()?.GetComponent<PlanetarySporeManager>());
}
