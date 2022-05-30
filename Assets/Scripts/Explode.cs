using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {
  public Vector3 ExplosionSource = new Vector3(0, 2.66f, 0);
  public float MinMagnitude = 2;
  public float MaxMagnitude = 6;

  void Start() {

    // EXPLODE!!!
    for (int i = 0; i < transform.childCount; i++) {
      Transform child = transform.GetChild(i);
      Rigidbody childRB = child.GetComponent<Rigidbody>();
      if (!childRB) continue;

      Vector3 explodeDirection = (child.position - (transform.position + ExplosionSource)).normalized;
      childRB.velocity = explodeDirection * Random.Range(MinMagnitude, MaxMagnitude);
    }  
  }
}
