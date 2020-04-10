using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetarySporeManager : MonoBehaviour { 
    [SerializeField] PlanetAllegiance myAllegiance;
    [SerializeField] PlanetConfiguration planetConfiguration;

    List<Transform> mySpores;
    
    void OnEnable() {
        PlanetManager.planetTick += Tick;
    }

    void OnDisable() {
        PlanetManager.planetTick -= Tick;
    }

    void Tick() {
        if (mySpores.Count < planetConfiguration.MaxSpores) 
            mySpores.Add(SporePool.instance.GetSporeFromPool());
    }
    
    void Update() {
        
    }
}
