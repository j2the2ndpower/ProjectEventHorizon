using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour {
  public Vector3 TargetLocation;
  public Vector3 TargetRotation;
  public float DriftTime;

  private Vector3 startLocation;
  private Vector3 startRotation;
  private float TimeSinceStart = 0f;

  void Start() {
    startLocation = transform.position;
    startRotation = transform.rotation.eulerAngles;
  }

  void Update() {
    TimeSinceStart += Time.deltaTime;

    Vector3 position = Vector3.Lerp(startLocation, TargetLocation, TimeSinceStart/DriftTime);
    Vector3 rotation = Vector3.Lerp(startRotation, TargetRotation, TimeSinceStart/DriftTime);
    this.transform.position = position;
    this.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
  }
}
