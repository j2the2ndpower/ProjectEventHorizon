using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {
  public void EndTalk() {
    LevelManager.instance.LoadLevel("Game");
  }
}
