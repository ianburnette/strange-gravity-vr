using System.Collections.Generic;
using UnityEngine;

public class PlanetSettings : MonoBehaviour {
    public static PlanetSettings instance;
    
    [SerializeField] List<Material> planetMaterials;
    [SerializeField] public Dictionary<Allegiance, Material> materialByAllegiance;

    public float materialTransitionTime = 1f;
    
    public enum Allegiance {
        player,
        enemy1,
        unclaimed
    }

    void OnEnable() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start() => InitializeMaterialsDictionary();

    void InitializeMaterialsDictionary() {
        var index = 0;
        materialByAllegiance = new Dictionary<Allegiance, Material> {
            {Allegiance.player, planetMaterials[index++]},
            {Allegiance.enemy1, planetMaterials[index++]},
            {Allegiance.unclaimed, planetMaterials[index++]},
        };
    }
}