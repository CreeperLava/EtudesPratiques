using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectRig : MonoBehaviour {
    protected Dictionary<string, int> dictionary = new Dictionary<string,int>
    {
        {"Hips", 0},
        {"Spine", 1},
        {"Neck", 2},
        {"Head", 3},
        {"LeftShoulder", 4},
        {"LeftArm", 5},
        {"LeftForeArm", 6},
        {"LeftHand", 7},
        {"RightShoulder", 8},
        {"RightArm", 9},
        {"RightForeArm", 10},
        {"RightHand", 11},
        {"LeftUpLeg", 12},
        {"LeftLeg", 13},
        {"LeftFoot", 14},
        {"RightUpLeg", 15},
        {"RightLeg", 16},
        {"RightFoot", 17},
        {"Reference", 18}
    };


	// Use this for initialization
	void Start () {
        Transform[] skeleton = gameObject.GetComponentsInChildren<Transform>();
        string name;

        foreach(Transform t in skeleton)
        {
            foreach(KeyValuePair<string,int> pair in dictionary)
            {
                if (t.name.Contains(pair.Key))
                {
                    gameObject.GetComponent<AvatarControllerClassic>().setBones(t, dictionary[t.name]);
                }

            }

            //Ancien code qui ne marchait pour tous types de personnages ( par exemple Ethan)
            //name = t.name;
            //if (name.Contains("_"))
            //{
            //    name = name.Split('_')[1];
            //    if (dictionary.ContainsKey(name))
            //    {
            //        gameObject.GetComponent<AvatarControllerClassic>().setBones(t, dictionary[name]);
            //    }
            //}

            
        }
        //public SkeletonBone[] skeleton; 
        //EndsWith(String)
        //Dictionary
        //objectWithOtherScript.GetComponent.< script1 > ().someVariable = someNumber;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
