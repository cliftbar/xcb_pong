using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScored : MonoBehaviour {
    public Text countText;
    public Vector2 ballRestartVelocity;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    //countText.text = (float.Parse(countText.text) + 1).ToString();
    private void OnCollisionEnter2D(Collision2D collision) {
        countText.text = (float.Parse(countText.text) + 1).ToString();
        collision.gameObject.transform.position = new Vector3(0, 0, 0);
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = ballRestartVelocity;
    }
}
