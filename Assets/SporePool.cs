using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporePool : MonoBehaviour {
    public static SporePool instance;
    
     [SerializeField] GameObject sporePrefab;
     [SerializeField] int poolSize = 500;

     List<Transform> sporePool = new List<Transform>();

     void Awake() {
         if (instance != null)
             Destroy(gameObject);
         instance = this;
         
         InstantiateSporesIntoPool();
     }

     void InstantiateSporesIntoPool() {
         for (var i = 0; i < poolSize; i++) {
             var spore = Instantiate(sporePrefab, transform);
             spore.SetActive(false);
             sporePool.Add(spore.transform);
         }
     }

     public Transform GetSporeFromPool() {
         while (true) {
             for (var i = 0; i < poolSize; i++) {
                 if (!sporePool[i].gameObject.activeSelf) {
                     sporePool[i].gameObject.SetActive(true);
                     return sporePool[i];
                 }
             }

             InstantiateSporesIntoPool();
         }
     }

     public void ReturnSporeToPool(Transform sporeToReturn) {
         sporeToReturn.parent = transform;
         sporeToReturn.gameObject.SetActive(false);
     }
}
