using UnityEngine;

public class Spin : MonoBehaviour {
    void Update() => transform.Rotate(Vector3.left * GlobalSettings.PlanetRotationSpeed * Time.deltaTime);
}
