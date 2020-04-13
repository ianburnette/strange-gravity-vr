using System.Collections.Generic;
using UnityEngine;

public class SporePool : MonoBehaviour {
    public static SporePool instance;
    
     [SerializeField] GameObject sporePrefab;
     [SerializeField] int poolSize = 500;

     List<Spore> sporePool = new List<Spore>();

     void OnEnable() {
         if (instance != null) {
             Destroy(gameObject);
             return;
         }
         instance = this;
         
         InstantiateSporesIntoPool();
     }

     void InstantiateSporesIntoPool() {
         for (var i = 0; i < poolSize; i++) {
             var spore = Instantiate(sporePrefab, transform);
             spore.SetActive(false);
             sporePool.Add(spore.GetComponent<Spore>());
         }
     }

     public Spore GetSporeFromPool() {
         while (true) {
             for (var i = 0; i < poolSize; i++) {
                 if (!sporePool[i].gameObject.activeSelf) {
                     sporePool[i].trailRenderer.enabled = false;
                     sporePool[i].gameObject.SetActive(true);
                     return sporePool[i];
                 }
             }

             InstantiateSporesIntoPool();
         }
     }

     public void ReturnSporeToPool(Spore sporeToReturn) {
         if (!sporeToReturn)
             return;
         sporeToReturn.transform.parent = transform;
         sporeToReturn.gameObject.SetActive(false);
     }
}
