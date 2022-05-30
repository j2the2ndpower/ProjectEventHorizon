using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public int damage; //The amount of damage the object does to your Stability on contact.
    public Vector3 spawnVelocity;
    public Rigidbody rb;
    bool launched = true;

    void Start() {
      rb = GetComponent<Rigidbody>();
    }

    void Update () {
      if (launched) {
        //Vector3 rbVelocity = rb.velocity;
        //rb.velocity = rbVelocity.normalized * spawnVelocity.magnitude;
      }
    }

    public void SetLaunched(bool l) {
      launched = l;
    }
}
