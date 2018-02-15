using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseSelector : MonoBehaviour {

    public GameObject particle;
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo)) {
                GameObject objet = hitInfo.collider.gameObject;
                if (objet.name == "ButtonValidate") {
                    // TODO
                    SceneManager.LoadScene("nom de la scene");
                }

                Debug.Log(objet);
                //Instantiate(particle, transform.position, transform.rotation);
            }
        }
    }
}
