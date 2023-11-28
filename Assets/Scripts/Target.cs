using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    private bool RotateEnabled = false;
    private Rigidbody rigidbody;
    private Vector3 grav;
    private Vector3 obj_grav;
    private float floatMax;
    // Start is called before the first frame update
    private void Awake() {
        GameObject floor = GameObject.FindGameObjectWithTag("Floor");
        floatMax = floor.gameObject.transform.position.y + 10;
        grav = Physics.gravity;
        obj_grav = grav;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = false;

        transform.rotation = Quaternion.Euler(Random.Range(45f, 135f), Random.Range(0f, 360f), Random.Range(45f, 135f));

        Vector3 force = transform.up;
        rigidbody.AddForce(force * 600);
    }

     // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            RotateEnabled = !RotateEnabled;
            if (RotateEnabled) {
                obj_grav = -grav;
            } else {
                obj_grav = grav;
            }
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            Renderer ren = GetComponent<Renderer>();
            ren.material.color = new Vector4(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        }


        if (RotateEnabled) {
            float obj_rotation = 50f * Time.deltaTime;
            transform.Rotate(obj_rotation, obj_rotation, obj_rotation);
            if (transform.position.y >= floatMax) {
                obj_grav = Vector3.zero;
                rigidbody.velocity = Vector3.zero;
            }

        }

        
    }

    
    private void FixedUpdate() {
        rigidbody.AddForce(obj_grav, ForceMode.Acceleration);
    }

    private void OnMouseDown() {
        player p = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        p.Points += 1;
        GameObject.Destroy(gameObject);
    }


}
