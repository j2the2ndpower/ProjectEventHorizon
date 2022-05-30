using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  private Animator anim;
  private CharacterController controller;
  private HUD hud;

  public float speed = 600.0f;
  public float turnSpeed = 400.0f;
  public float gravity = 20.0f;
  public float stretchRate = 0.01f;
  public Vector3 ClampOffset = Vector3.zero;
  public float rotationSpeed = 5.0f;
  public GameObject DeathPrefab;

  private Vector3 moveDirection = Vector3.zero;
  private float StartingZ;
  private int stable = 100;
  private Obstacle lastHit;

  void Start () {
    controller = GetComponent <CharacterController>();
    anim = gameObject.GetComponentInChildren<Animator>();
    hud = FindObjectOfType<HUD>();
    StartingZ = transform.position.z;
  }

  void Update () {
    // Move player
    moveDirection = -Vector3.down * Input.GetAxis("Vertical") * speed;
    moveDirection += -Vector3.left * Input.GetAxis("Horizontal") * speed;
    controller.Move(moveDirection * Time.deltaTime);
    
    // Clamp to edge of screen
    Vector3 clampedPosition = transform.position;
    float max_x = Mathf.Cos(Camera.main.fieldOfView) * transform.position.z;
    float max_y = max_x / Camera.main.aspect;
    clampedPosition.x = Mathf.Clamp(clampedPosition.x, max_x + ClampOffset.x, -max_x + ClampOffset.x);
    clampedPosition.y = Mathf.Clamp(clampedPosition.y, max_y + ClampOffset.y, -max_y + ClampOffset.y);
    clampedPosition.z = StartingZ;
    transform.position = clampedPosition;

    // Rotate Player if damaged
    int damage = 100 - stable;
    Vector3 rotation = new Vector3(0, 0, damage * rotationSpeed * Time.deltaTime);

    this.transform.Rotate(rotation);
  }

  public void OnHit(Obstacle ob){
    if (ob == null || ob == lastHit) return;
    Rigidbody rb = ob.GetComponent<Rigidbody>();

    //When making contact with an obstacle
    stable = hud.Destabilize(ob.damage);

    Vector3 difference = ob.transform.position - transform.position;
    Vector3 knockback = new Vector3(difference.normalized.x, difference.normalized.y, 0)*20f;
    rb.AddForce(knockback, ForceMode.Impulse);//Destroy(other);
    ob.SetLaunched(false);
    //knockback? spin?

    // Remember last hit object
    lastHit = ob;

    if (hud.isDead()) {
      Instantiate(DeathPrefab, transform.position, transform.rotation);
      Destroy(gameObject);
    }
  }

  public void OnPickup(Collectable collect) {
    if (!collect) return;

    // Stabilize
    stable = hud.Destabilize(Mathf.FloorToInt(-collect.HealAmount));
    
    // Unstretch everything by a percentage
    float stretchHeal = collect.HealAmount / 100;
    Unstretch("StretchArmLeft", stretchHeal);
    Unstretch("StretchArmRight", stretchHeal);
    Unstretch("StretchLegLeft", stretchHeal);
    Unstretch("StretchLegRight", stretchHeal);

    // Destroy pickup
    Destroy(collect.gameObject);
  }

  private void Unstretch(string StretchParam, float stretchHeal) {
    if (!anim) return;
    anim.SetFloat(StretchParam, Mathf.Clamp(anim.GetFloat(StretchParam) - stretchHeal, 0, 1));
  }

  void OnControllerColliderHit(ControllerColliderHit hit) {
    Obstacle ob = hit.gameObject.GetComponent<Obstacle>();
    Collectable c = hit.gameObject.GetComponent<Collectable>();
    if (ob) OnHit(ob);
    if (c) OnPickup(c);
  }

}
