using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverColor : MonoBehaviour {
  public VertexGradient TextNormalColor;
  public VertexGradient TextHoverColor;
  private TextMeshProUGUI tmp;

  void Start() {
    tmp = GetComponentInChildren<TextMeshProUGUI>();
  }

  public void OnPointerEnter() {
    tmp.colorGradient = TextHoverColor;
  }

  public void OnPointerExit() {
    tmp.colorGradient = TextNormalColor;
  }
}
