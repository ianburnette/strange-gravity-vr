using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance;
    void OnEnable() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
}
