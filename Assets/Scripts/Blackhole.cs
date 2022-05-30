using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class Blackhole : MonoBehaviour {
  public RenderPipelineAsset m_Urp;
  public BlackHoleURP m_Feature;
  public Material m_Mat = null;
  public float currentSize = 0f;

  float _minSize = 0.06f;
  float _minDistortion = -2.061f;
  float _minWarp = 5.9f;
  float _minColliderRadius = 0.39f;

  float _maxSize = 0.01f;
  float _maxDistortion = -1f;
  float _maxWarp = 40f;
  float _maxColliderRadius = 1.77f;

  private float m_DarkRange = 0.1f;
  private float m_Distortion = -2f;
  private float m_Warp = 30f;

  private HUD hud;
  private CapsuleCollider capsuleCollider;

  void Start() {
    hud = FindObjectOfType<HUD>();
    capsuleCollider = GetComponent<CapsuleCollider>();
    GraphicsSettings.renderPipelineAsset = m_Urp;
    m_Feature.m_Mat = m_Mat;
  }

  void Update() {
    currentSize = hud.GetSecondsAlive() / hud.GetLastLevelStartTime();
    m_DarkRange = Mathf.Lerp(_minSize, _maxSize, currentSize);
    m_Distortion = Mathf.Lerp(_minDistortion, _maxDistortion, currentSize);
    m_Warp = Mathf.Lerp(_minWarp, _maxWarp, currentSize);
    capsuleCollider.radius = Mathf.Lerp(_minColliderRadius, _maxColliderRadius, currentSize);

    Vector3 ScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
    m_Mat.SetVector("_Center", new Vector4(ScreenPoint.x/Screen.width, ScreenPoint.y/Screen.height, 0f, 0f));
    m_Mat.SetFloat("_DarkRange", m_DarkRange);
    m_Mat.SetFloat("_Distortion", m_Distortion);
    m_Mat.SetFloat("_Warp", m_Warp);
  }

  void OnTriggerEnter(Collider other) {
    // Suck in objects
    Rigidbody body = other.GetComponent<Rigidbody>();
    if (body) {
      body.constraints = 0;
      body.velocity = new Vector3(0, 0, 15);
    }
  }
}
