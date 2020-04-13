using UnityEngine;

public class SporeSettings : MonoBehaviour {
    public static SporeSettings instance;

    public float SporeMaxSize = .3f;
    public float deltaTime;
    public float GrowSpeed = .1f;
    
    public float SporeGravity = 18;
    public float SporeOrbitalOffset = .5f;
    public float OrbitalSpeed = 8f;

    public float AttackSpeedInitial = 5f;
    public float AttackSpeedPersistent = 2f;
    public float CollisionDistance = 1f;

    public float SporeLifetime = 5f;//TODO: set this to trail length

    void OnEnable() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Update() => deltaTime = Time.deltaTime;
}
