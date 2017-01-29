using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //public float speedHoriz;
    public float speedVert;
    public bool isAi;
    public float followDistRatio;
    public Bounds movementBounds;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isAi) {
            HumanControl();
        } else {
            AiControl();
        }
    }

    private void AiControl() {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject closestBall = null;
        float dist = float.PositiveInfinity;

        foreach(GameObject ball in balls) {
            float currDist = Vector2.Distance(ball.transform.position, this.transform.position);
            if (dist > currDist) {
                closestBall = ball;
                dist = currDist;
            }
        }

        float distDiff = Math.Abs(this.transform.position.y - closestBall.transform.position.y);
        Vector2 start = new Vector2(0, this.transform.position.y);
        Vector2 end = new Vector2(0, closestBall.transform.position.y);
        //print("DistDiff: " + distDiff.ToString());
        Vector2 diff = Vector2.MoveTowards(start, end, Math.Min(distDiff * followDistRatio, speedVert));

        //transform.Translate(x, 0, 0);
        if (movementBounds.Contains(this.transform.position)) {
            this.transform.position = new Vector3(this.transform.position.x, diff.y);
        } else if (this.transform.position.y < 0 && diff.y > this.transform.position.y) {
            this.transform.position = new Vector3(this.transform.position.x, diff.y);
        } else if (this.transform.position.y > 0 && diff.y < this.transform.position.y) {
            this.transform.position = new Vector3(this.transform.position.x, diff.y);
        }

        
    }

    private void HumanControl() {
        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * speedHoriz;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * speedVert;

        //transform.Translate(x, 0, 0);
        if (movementBounds.Contains(this.transform.position)) {
            transform.Translate(0, y, 0);
        } else if (this.transform.position.y < 0 && y > 0) {
            transform.Translate(0, y, 0);
        } else if (this.transform.position.y > 0 && y < 0) {
            transform.Translate(0, y, 0);
        }
    }
}
