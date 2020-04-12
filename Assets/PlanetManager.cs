using UnityEngine;

public class PlanetManager : MonoBehaviour {
  public static PlanetManager instance;

  public delegate void PlanetTick();
  public static event PlanetTick planetTick;

  const float tickTime = 1f;
  
  void OnEnable() {
    if (instance != null) {
        Destroy(gameObject);
        return;
    }
    instance = this;

    InvokeRepeating(nameof(Tick), tickTime, tickTime);
  }

  void Tick() => planetTick?.Invoke();
}

