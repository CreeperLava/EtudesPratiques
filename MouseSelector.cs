using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Hack
{
    private static GameObject selectedChar = null;

    public static GameObject Char
    {
        get
        {
            return selectedChar;
        } set
        {
            selectedChar = value;
        }
    }
}

public class MouseSelector : MonoBehaviour {

    public GameObject particle;
    public GameObject clone;
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo)) {
                GameObject objet = hitInfo.collider.gameObject;
                print(Hack.Char);
                print(objet.name);
                if (objet.name == "ButtonValidate" && Hack.Char != null) {
                    // TODO
                    hideAll();
                    print("here");
                    
                } else
                {
                    Hack.Char = objet;
                }

                Debug.Log(objet);
                //Instantiate(particle, transform.position, transform.rotation);
            }
        }
    }

    void hideAll()
    {
        GameObject.Find(Hack.Char.name).transform.parent = GameObject.Find("Avatar").transform;
        Camera.main.transform.LookAt(GameObject.Find(Hack.Char.name).transform.position);
        //problème rotation du perso

        //clone = Object.Instantiate(GameObject.Find(Hack.Char.name), GameObject.Find("Avatar").transform);
        //Camera.main.transform.LookAt(clone.transform.position);
        //problème de mouvement du perso

        GameObject.Find("ButtonValidate").SetActive(false);
        foreach (Transform t in GameObject.Find("TurningTable").transform)
        {
            t.gameObject.SetActive(false);
        }
    }

    void showAll()
    {
        //Destroy(clone);
        GameObject.Find("ButtonValidate").SetActive(true);
        foreach (Transform t in GameObject.Find("TurningTable").transform)
        {
            t.gameObject.SetActive(true);
        }
    }
}
