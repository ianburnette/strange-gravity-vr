using System.Collections.Generic;
using UnityEngine;
using Zinnia.Pointer;

public class PlayerSelection : MonoBehaviour {
    readonly List<PlanetSelection> selectedPlanets = new List<PlanetSelection>();
    
    public void Select(ObjectPointer.EventData transformData) {
        if (transformData == null)
            return;
        var planetSelection = transformData.CollisionData.transform?.GetComponent<PlanetSelection>();
        Debug.Log($"hit {transformData.Transform} with {transformData.CollisionData.transform}");
        if (planetSelection == null) return;
        if (planetSelection.Allegiance.myAllegiance != PlanetSettings.Allegiance.player) return;
        
        planetSelection.Select();
        if (!selectedPlanets.Contains(planetSelection)) {
            selectedPlanets.Add(planetSelection);
        }
    }

    public void Transfer(ObjectPointer.EventData transformData) {
        if (transformData == null)
            return;
        var planetSelection = transformData.CollisionData.transform?.GetComponent<PlanetSelection>();
        if (planetSelection == null) return;
        foreach (var selectedPlanet in selectedPlanets) {
            selectedPlanet.TransferSpores(planetSelection);
            selectedPlanet.Deselect();
        }
        selectedPlanets.Clear();
    }

    public void Deselect() {
        foreach (var selectedPlanet in selectedPlanets) {
            selectedPlanet.Deselect();
        }
        selectedPlanets.Clear();
    }
}
