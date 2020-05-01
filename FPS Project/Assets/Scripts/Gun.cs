using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    [Header("Gun Configuration")]
    public float damage;
    public float range;
    public float firerate;
    public float waitToFirerate;
    public Camera cam;
    public ParticleSystem armoParticle;
    public GameObject impact;
    public bool hold = false;

    [Space]
    [Header("Ammo")]
    public int maxAmmoInMagazine;
    public int ammoInMagazine;
    public int ammo;
    public int timeToReload;

    [Space]
    [Header("Canvas")]
    public Text ammoTxt;
    public Slider reloadShow;
    public GameObject reloadGO;

    private bool reloadb = false;
    private int timetr;

    // Update is called once per frame
    void Update() {
        reloadShow.value = timetr;
        ammoTxt.text = ammoInMagazine + "/" + ammo;

        if (Input.GetButtonDown("Fire1"))
            hold = true;
        if (Input.GetButtonUp("Fire1"))
            hold = false;

        if (hold == true)
            waitToFirerate += 1;

        if (waitToFirerate > firerate && ammoInMagazine > 0)
            Shoot();

        if (Input.GetButtonDown("Reload") && ammoInMagazine != maxAmmoInMagazine && ammo != 0 && reloadb == false) {
            reloadGO.SetActive(true);
            reloadb = true;
        }

        if (reloadb == true) {
            if (timetr > timeToReload) {
                for (int i = 0; i < maxAmmoInMagazine; i++) {
                    if (ammoInMagazine < maxAmmoInMagazine && ammo > 0) {
                    ammo -= 1;
                    ammoInMagazine += 1;
                    } else {
                        break;
                    }
                }
                reloadb = false;
                timetr = 0;
                reloadGO.SetActive(false);
            } else {
                timetr += 1;
            }
        }
    }

    private void Start() {
        reloadShow.maxValue = timeToReload;
    }

    void Shoot() {
        waitToFirerate = 0;
        armoParticle.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)) {
            Debug.Log("Mirando em:" + hit.transform.name);

            ObjectDestroyable ob = hit.transform.GetComponent<ObjectDestroyable>();
            if (ob != null) {
                ob.takeDamage(damage);
            } 

            GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
        }

        ammoInMagazine -= 1;
    }
}
