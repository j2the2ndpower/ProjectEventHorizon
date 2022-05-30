using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {
  public AudioClip SFX;

  void PlayClip() {
    if (!SFX) return;
    SoundSystem._instance.PlaySFX(SFX);
  }
}
