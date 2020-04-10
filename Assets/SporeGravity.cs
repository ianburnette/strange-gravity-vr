using UnityEngine;

public class SporeGravity : MonoBehaviour {
    public Transform target;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (!target)
            return;
        rb.AddForce((target.position - transform.position) * GlobalSettings.SporeGravitySpeed, ForceMode.Acceleration);
    }
}
