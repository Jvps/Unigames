using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyable : MonoBehaviour {

    public float health;
    public List<GameObject> Parts = new List<GameObject>();
    private BoxCollider bc;
    private Rigidbody rbb;
    private int cooldown = 100;
    private bool isCooldown = false;

    void Start() {
        bc = GetComponent<BoxCollider>();
        rbb = GetComponent<Rigidbody>();
    }
    
    public void takeDamage(float i) {
        health -= i;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        for (int i = 0; i < Parts.Count; i++) {
            Parts[i].AddComponent<Rigidbody>();
            Rigidbody rb = Parts[i].GetComponent<Rigidbody>();
            float x = Random.Range(-100, 100);
            float y = Random.Range(0, 100);
            float z = Random.Range(-100, 100);
            rb.AddForce(new Vector3(x, y, z));
        }
        bc.isTrigger = true;
        rbb.isKinematic = true;
        isCooldown = true;
    }

    private void Update() {
        if (isCooldown == true) {
            if (cooldown > 0) {
                cooldown -= 1;
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}
