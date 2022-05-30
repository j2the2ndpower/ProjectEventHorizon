using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanStretch : MonoBehaviour {
  public string stretchParam;
  public Player player;

  private Animator ani;
  private bool stretching;

  void Start() {
    if (player) {
      ani =player.GetComponent<Animator>();
    } else {
      Debug.Log("No player set for CanStretch Component on: " + gameObject.name);
    }
  }

  void Update() {
    if (stretching && ani) {
      float newStretch = Mathf.Clamp(ani.GetFloat(stretchParam) + player.stretchRate * Time.deltaTime, 0, 1);
      ani.SetFloat(stretchParam, newStretch);
    }
  }

  void OnTriggerEnter(Collider collider) {
    if (!collider.GetComponent<Blackhole>()) return;

    stretching = true;
  }

  void OnTriggerExit(Collider collider) {
    stretching = false;
  }

  void OnCollisionEnter(Collision other) {
    Obstacle ob = other.gameObject.GetComponent<Obstacle>();
    Collectable c = other.gameObject.GetComponent<Collectable>();
    if (ob) player.OnHit(ob);
    if (c) player.OnPickup(c);
  }
}
