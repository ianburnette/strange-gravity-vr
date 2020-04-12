using UnityEngine;

public class FacePlayer : MonoBehaviour {
    void Update() => transform.LookAt(Player.instance.transform);
}
