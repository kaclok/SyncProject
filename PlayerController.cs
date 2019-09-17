using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {

    GameObject textObj;
    Text text;
    private void Start() {
        textObj = GameObject.Find("Canvas/Text");
        text = textObj.GetComponent<Text> ();
    }
    // Update is called once per frame
    void Update () {

        if (!isLocalPlayer) return;

        var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

        if (x != 0 || z != 0) {
            string str = text.text;
            if (str.Length >= 500) {
                str = "";
            }
            text.text = str + "\n" + "x = " + x + " ||  z = " + z;
        }

        transform.Rotate (0, x, 0);
        transform.Translate (0, 0, z);
    }
}