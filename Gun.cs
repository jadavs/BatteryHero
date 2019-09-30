using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 50f;
    // For camera and animation
    public Camera fps_cam;
    public Animator gun_anim;
    public GameObject mflashObj;
    // Timer for muzzle_flash
    public float mTimer = 0.1f;
    private float mTimerStart = 0.1f;
    public bool mFlashEnabled = false;
    // loot logic
    public float pickup_range = 5f;
    public float loot_points = 10;
    private int num_loot = 0;
    public GameObject manager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
           mFlashEnabled = true;
           shoot();
        }

        if (mFlashEnabled) {
            mflashObj.SetActive(true);
            mTimer -= Time.deltaTime;
        }

        if (mTimer <= 0) {
            mflashObj.SetActive(false);
            mFlashEnabled = false;
            mTimer = mTimerStart;
        }
        getLoot();
        if (Input.GetKeyDown(KeyCode.E)) {
            transferLoot();
        }
    }

    public void shoot() {
        RaycastHit hit;
        if (Physics.Raycast(fps_cam.transform.position, (fps_cam.transform.forward), out hit, range)) {
            enemy mimic = hit.transform.GetComponent<enemy>();
            if (mimic != null) {
                mimic.TakeDamage(damage);
            }
        }
    }

    public void getLoot() {
        RaycastHit hit;
        if (Physics.Raycast(fps_cam.transform.position, (fps_cam.transform.forward), out hit, pickup_range)) {
            if (hit.collider.tag == "Loot") {
                Debug.Log("Found Loot, updating num_loot");
                num_loot++;
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private void transferLoot() {
       RaycastHit hit;
       if (Physics.Raycast(fps_cam.transform.position, (fps_cam.transform.forward), out hit, pickup_range)) {
          if (hit.collider.tag == "Generator") {
              Debug.Log("Hit generator");
              GameController controller = manager.GetComponent<GameController>();
              if (controller != null) {
                for (int i = 0; i < num_loot; i++) {
                     controller.UpdateElectricity(loot_points);
                }
                num_loot = 0; // resets num_loot
              }
          }
       }
    }

}
