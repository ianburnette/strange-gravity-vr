using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetarySporeManager : MonoBehaviour { 
    [SerializeField] PlanetAllegiance myAllegiance;
    [SerializeField] PlanetConfiguration planetConfiguration;

    List<Spore> mySpores = new List<Spore>();
    Transform myTransform;

    [SerializeField] TMP_Text countText;
    
    void OnEnable() {
        PlanetManager.planetTick += Tick;
        myTransform = transform;
    }

    void OnDisable() {
        PlanetManager.planetTick -= Tick;
    }

    void Tick() {
        if (mySpores.Count < planetConfiguration.MaxSpores) 
            BeginGrowingSpore();
    }

    void BeginGrowingSpore() {
        var sporeFromPool = SporePool.instance.GetSporeFromPool();
        StartCoroutine(sporeFromPool.BeginGrowing(myTransform, myTransform.localScale.x, AddSpore));
    }

    void AddSpore(Spore sporeToAdd) {
        mySpores.Add(sporeToAdd);
        countText.text = mySpores.Count.ToString();
    }

    void AcceptIncomingSpore(Spore incomingSpore, PlanetSettings.Allegiance sourceAllegiance) {
        if (myAllegiance.myAllegiance == sourceAllegiance || 
            myAllegiance.myAllegiance == PlanetSettings.Allegiance.unclaimed) {
            incomingSpore.ChangeOwnership(this);
            AddSpore(incomingSpore);
            if (myAllegiance.myAllegiance == PlanetSettings.Allegiance.unclaimed)
                myAllegiance.ChangeAllegiance(sourceAllegiance);
        } else {
            if (mySpores.Count > 0) {
                var finalSporeIndex = mySpores.Count - 1;
                var defendingSpore = mySpores[finalSporeIndex];
                mySpores.RemoveAt(finalSporeIndex);
                incomingSpore.AttackOtherSpore(defendingSpore);
            } else
                myAllegiance.ChangeAllegiance(sourceAllegiance);
        } 
    }
    
    void Update() {
        for (var i = 0; i < mySpores.Count; i++) {
            Debug.DrawRay(transform.position, mySpores[i].transform.position - transform.position, Color.cyan);
        }
    }

    public void TransferSpores(PlanetarySporeManager destinationSporeManager) {
        var countToTransfer = mySpores.Count / 2;
        for (var i = mySpores.Count - 1; i >= countToTransfer; i--) {
            destinationSporeManager.AcceptIncomingSpore(mySpores[i], myAllegiance.myAllegiance);
            mySpores.RemoveAt(i);
        }
    }
}
