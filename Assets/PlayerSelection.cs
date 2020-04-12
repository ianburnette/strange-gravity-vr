using System.Collections.Generic;
using UnityEngine;
using Zinnia.Pointer;

public class PlayerSelection : MonoBehaviour {
    readonly List<PlanetSelection> selectedPlanets = new List<PlanetSelection>();
    
    public void Select(ObjectPointer.EventData transformData) {
        var planetSelection = transformData.CollisionData.transform.GetComponent<PlanetSelection>();
        if (planetSelection == null) return;
        planetSelection.Select();
        selectedPlanets.Add(planetSelection);
    }

    public void Transfer(ObjectPointer.EventData transformData) {
        var planetSelection = transformData.CollisionData.transform.GetComponent<PlanetSelection>();
        if (planetSelection == null) return;
        foreach (var selectedPlanet in selectedPlanets) {
            selectedPlanet.TransferSpores(transformData);
        }
    }

    public void Deselect() {
        foreach (var selectedPlanet in selectedPlanets) {
            selectedPlanet.Deselect();
        }
        selectedPlanets.Clear();
    }
}
