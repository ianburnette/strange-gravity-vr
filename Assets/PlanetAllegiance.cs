using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlanetAllegiance : MonoBehaviour
{
    public enum Allegiance {
        player,
        enemy1
    }

    public Allegiance myAllegiance;
    Renderer myRenderer;

    [SerializeField] List<Material> planetMaterials;
    
    [SerializeField] Dictionary<Allegiance, Material> materialByAllegiance;

    void Start() {
        myRenderer = GetComponent<Renderer>();
        InitializeMaterialsDictionary();
    }

    void InitializeMaterialsDictionary() {
        var index = 0;
        materialByAllegiance = new Dictionary<Allegiance, Material> {
            {Allegiance.player, planetMaterials[index++]},
            {Allegiance.enemy1, planetMaterials[index++]},
        };
    }

    public Allegiance MyAllegiance {
        get => myAllegiance;
        set {
            myAllegiance = value;
            myRenderer.sharedMaterial = materialByAllegiance[myAllegiance];
        }
    }
}
