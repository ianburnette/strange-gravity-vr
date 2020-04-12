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
            AddSpore();
    }

    void AddSpore() {
        var sporeFromPool = SporePool.instance.GetSporeFromPool();
        sporeFromPool.BeginGrowing(myTransform, myTransform.localScale.x);
        mySpores.Add(sporeFromPool);
        countText.text = mySpores.Count.ToString();
    }

    public void AcceptIncomingSpore(Spore incomingSpore) {
        if (mySpores.Count > 0) {
            var finalSporeIndex = mySpores.Count - 1;
            var defendingSpore = mySpores[finalSporeIndex];
            mySpores.RemoveAt(finalSporeIndex);
            incomingSpore.AttackOtherSpore(defendingSpore);
        } else {
            incomingSpore.ChangeOwnership(this);
            mySpores.Add(incomingSpore);
        }
    }
    
    void Update() {
        for (var i = 0; i < mySpores.Count; i++) {
         //   Debug.DrawRay(transform.position, mySpores[i].transform.position - transform.position, Color.cyan);
        }
    }

    public void TransferSpores(PlanetarySporeManager destinationSporeManager) {
        for (var i = mySpores.Count - 1; i >= mySpores.Count / 2; i--)
            destinationSporeManager.AcceptIncomingSpore(mySpores[i]);
    }
}
