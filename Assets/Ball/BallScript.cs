using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public float startSpeedHoriz;
    public float startSpeedVert;

    public float deadZoneRatio;
    public float speedIncreaseRatio;
    public float dirAngleIncrease;

    private Rigidbody2D rb2d = null;

	// Use this for initialization
	void Start () {
        print("Start Ball");
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(startSpeedHoriz, startSpeedVert);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        print(this.name + " colliding with " + collision.gameObject.name);
        if(collision.gameObject.tag == "Player") {
            PlayerHit(collision.gameObject);
        }
    }

    private void PlayerHit(GameObject hitPlayer) {
        print(rb2d.velocity);
        //PlayerHitSpeedIncrease(hitPlayer.transform.position, hitPlayer.GetComponent<Collider2D>().bounds);
        float hitOffsetX = this.transform.position.y - hitPlayer.transform.position.y;
        float hitMaxX = hitPlayer.GetComponent<Collider2D>().bounds.extents.y;
        float RatioX = Math.Abs(hitOffsetX) / hitMaxX;

        Vector2 scaledVect = rb2d.velocity;
        float absY = Math.Abs(scaledVect.y);

        print("Scaling speed");
        if (RatioX < deadZoneRatio) {
            float HorizSpeed = rb2d.velocity.x * (1 - speedIncreaseRatio);
            if(Math.Abs(HorizSpeed) < Math.Abs(startSpeedHoriz)) {
                HorizSpeed = Math.Abs(startSpeedHoriz) * Math.Sign(HorizSpeed);
            }
            float VertSpeed = rb2d.velocity.y * (1 - speedIncreaseRatio);
            if (Math.Abs(VertSpeed) < Math.Abs(startSpeedVert)) {
                VertSpeed = Math.Abs(startSpeedVert) * Math.Sign(VertSpeed);
            }
            print(rb2d.velocity);
            rb2d.velocity = new Vector2(HorizSpeed, VertSpeed);
            print(rb2d.velocity);
            return;
        }
        rb2d.velocity = new Vector2(rb2d.velocity.x * (1 + speedIncreaseRatio), rb2d.velocity.y * (1 + speedIncreaseRatio));

        int travelDirVert = Math.Sign(rb2d.velocity.y);
        if (hitOffsetX >= 0 && travelDirVert < 0) {
            //print("Ball up");
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * -1);
        } else if (hitOffsetX < 0 && travelDirVert >= 0) {
            //print("Ball down");
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * -1);
        }

        print("rotating");
        print(rb2d.velocity);
        if (hitOffsetX >= 0 && travelDirVert >= 0) {
            print("Ball up");
            rb2d.velocity = Quaternion.Euler(0, 0, dirAngleIncrease) * (new Vector2(rb2d.velocity.x, rb2d.velocity.y));
        } else if (hitOffsetX < 0 && travelDirVert < 0) {
            print("Ball down");
            rb2d.velocity = Quaternion.Euler(0, 0, -1 * dirAngleIncrease) * (new Vector2(rb2d.velocity.x, rb2d.velocity.y));
        }
        print(rb2d.velocity);
    }
}
