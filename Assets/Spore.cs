using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spore : MonoBehaviour {
    public TrailRenderer trailRenderer;
    
    float currentSize;
    Vector3 planetaryOffset;
    Transform parentPlanet;
    float parentScale;
    Transform myTransform;
    Rigidbody myRigidbody;

    public Vector3 myRandomAxis;
    float myOrbitalOffset;

    Spore opponentSpore;
   
    void OnEnable() {
        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody>();
        myRandomAxis = Vector3.zero.Random();
    }

    public void BeginGrowing(Transform parentPlanet, float planetScale){
        transform.localScale = Vector3.zero;
        planetaryOffset = Random.onUnitSphere * (planetScale / 2);
        this.parentPlanet = parentPlanet;
        parentScale = planetScale;
        myOrbitalOffset = Mathf.Max(Random.value * (parentScale / 1f), parentScale / 1.7f);
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
        myRigidbody.AddForce(
            opponentSpore.transform.position - 
            transform.position * 
            SporeSettings.instance.AttackSpeedInitial
        );
    }
    
    void Update() {
        if (transform.localScale.magnitude < SporeSettings.instance.SporeMaxSize) {
            myTransform.localScale += Vector3.one * (SporeSettings.instance.GrowSpeed * SporeSettings.instance.deltaTime);
            myTransform.position = parentPlanet.position + planetaryOffset;
            //TODO: account for rotation
        } else if (parentPlanet) {
            if (!trailRenderer.enabled) trailRenderer.enabled = true;
            var parentPosition = parentPlanet.position;
            transform.RotateAround(parentPosition, myRandomAxis, SporeSettings.instance.OrbitalSpeed);
         
            var position = transform.position;
            var desiredPosition = (position - parentPosition).normalized * ((SporeSettings.instance.SporeOrbitalOffset * parentScale/2f) + myOrbitalOffset) + parentPosition;
            position = Vector3.MoveTowards(position, desiredPosition, Time.deltaTime * SporeSettings.instance.SporeGravity);
            transform.position = position;
        } else if (opponentSpore) {
            myRigidbody.AddForce(opponentSpore.transform.position - transform.position * (SporeSettings.instance.AttackSpeedPersistent * SporeSettings.instance.deltaTime));
        }
    }
}
