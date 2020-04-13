using System;
using System.Collections;
using System.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spore : MonoBehaviour {
    public TrailRenderer trailRenderer;
    
    float currentSize;
    Vector3 planetaryOffset;
    public Transform parentPlanet;
    float parentScale;
    Transform myTransform;
    Rigidbody myRigidbody;

    public Vector3 myRandomAxis;
    float myOrbitalOffset;

    public Spore opponentSpore;

    bool beingDestroyed;
   
    void OnEnable() {
        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody>();
        myRandomAxis = Vector3.zero.Random();
    }

    public IEnumerator BeginGrowing(Transform parentPlanet, float planetScale, Action<Spore> addSporeCallback){
        transform.localScale = Vector3.zero;
        planetaryOffset = Random.onUnitSphere * (planetScale / 2);
        this.parentPlanet = parentPlanet;
        parentScale = planetScale;
        myOrbitalOffset = Mathf.Max(Random.value * (parentScale / 1f), parentScale / 1.7f);
        
        while (transform.localScale.magnitude < SporeSettings.instance.SporeMaxSize && parentPlanet) {
            myTransform.localScale += Vector3.one * (SporeSettings.instance.GrowSpeed * SporeSettings.instance.deltaTime);
            myTransform.position = parentPlanet.position + planetaryOffset;
            //TODO: account for rotation
            yield return new WaitForEndOfFrame();
        }

        addSporeCallback(this);
    }

    public void ChangeOwnership(PlanetarySporeManager newPlanet) {
        var parentPlanet = newPlanet.transform;
        this.parentPlanet = parentPlanet;
        parentScale = parentPlanet.localScale.x;
    }

    public void GetAttackedByOtherSpore(Spore attackingSpore) {
        parentPlanet = null;
        opponentSpore = attackingSpore;
    }
    
    public void AttackOtherSpore(Spore opponentSpore) {
        parentPlanet = null;
        this.opponentSpore = opponentSpore;
        opponentSpore.GetAttackedByOtherSpore(this);
       //myRigidbody.AddForce(
       //    opponentSpore.transform.position - 
       //    transform.position * 
       //    SporeSettings.instance.AttackSpeedInitial
       //);
    }
    
    void Update() {
        if (parentPlanet) {
            if (!trailRenderer.enabled) trailRenderer.enabled = true;
            var parentPosition = parentPlanet.position;
            transform.RotateAround(parentPosition, myRandomAxis, SporeSettings.instance.OrbitalSpeed);
         
            var position = transform.position;
            var desiredPosition = (position - parentPosition).normalized * ((SporeSettings.instance.SporeOrbitalOffset * parentScale/2f) + myOrbitalOffset) + parentPosition;
            position = Vector3.MoveTowards(position, desiredPosition, Time.deltaTime * SporeSettings.instance.SporeGravity);
            transform.position = position;
        } else if (opponentSpore) {
            myRigidbody.AddForce(opponentSpore.transform.position - transform.position * (SporeSettings.instance.AttackSpeedPersistent * SporeSettings.instance.deltaTime));
            if (!(Vector3.Distance(transform.position, opponentSpore.transform.position) <
                  SporeSettings.instance.CollisionDistance)) return;
            if (!beingDestroyed) 
                Task.Run(async () => await Destroy());
            if (myTransform.localScale.x > 0)
                myTransform.localScale -= Vector3.one * (SporeSettings.instance.GrowSpeed * SporeSettings.instance.deltaTime);
        }
    }

    async Task Destroy() {
        beingDestroyed = true;
        await Task.Delay(TimeSpan.FromSeconds(SporeSettings.instance.SporeLifetime));
        SporePool.instance.ReturnSporeToPool(this);
        opponentSpore.opponentSpore = null;
        SporePool.instance.ReturnSporeToPool(opponentSpore);
        opponentSpore = null;
        beingDestroyed = false;
    }
}
