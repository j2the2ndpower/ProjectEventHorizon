using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipIntro : MonoBehaviour {
  LevelLoad _lvl;

  void Start() {
    _lvl = GetComponent<LevelLoad>();
  }

  void Update() {
    if (Input.GetKey(KeyCode.Escape)) {
      _lvl.LoadLevel("Game");
    }
  }
}
