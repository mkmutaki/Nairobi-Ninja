using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugScript : MonoBehaviour {

	public float moveSpeed = 4f;
	private Rigidbody2D myBody;
	private Animator anim;

	public LayerMask playerLayer;

	private bool moveLeft;

	private bool canMove;
	private bool Killed;

	public Transform left_Collision, right_Collision, top_Collision, down_Collision;
	private Vector3 left_Collision_Pos, right_Collision_Pos;

	void Awake() {
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		left_Collision_Pos = left_Collision.position;
		right_Collision_Pos = right_Collision.position;
	}

	void Start () {
		moveLeft = true;
		canMove = true;
		
	}
	
	void Update () {
		if (canMove) {
			if (moveLeft) {
			myBody.velocity = new Vector2 (-moveSpeed, myBody.velocity.y);
		} else {
			myBody.velocity = new Vector2 (moveSpeed, myBody.velocity.y);
		}

	}
		
		CheckCollision();

	}

	void CheckCollision() {

		RaycastHit2D leftHit = Physics2D.Raycast (left_Collision.position, Vector2.left, 0.5f, playerLayer);
		RaycastHit2D rightHit = Physics2D.Raycast (right_Collision.position, Vector2.right, 0.5f, playerLayer);

		Collider2D topHit = Physics2D.OverlapCircle (top_Collision.position, 1f, playerLayer);

		if(topHit != null) {
			if (topHit.gameObject.tag == "Player") {
				if (!Killed) {
					topHit.gameObject.GetComponent<Rigidbody2D>().velocity = 
					new Vector2 (topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 18f);

					 canMove = false;
					 myBody.velocity = new Vector2 (0, 0);

					 anim.Play ("Killed");
					 Killed = true;

					 //OTHER ENEMY CODE HERE
					 if(tag == MyTags.BEETLE_TAG) {
						 anim.Play ("Killed");
						 StartCoroutine (Dead(0.5f));
					 }

				}
			}
		}

		if (leftHit) {
			if(leftHit.collider.gameObject.tag == "Player") {
				if (!Killed) {
					//APPLY DAMAGE TO PLAYER
					leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
				} else {
					if (tag != MyTags.BEETLE_TAG) {
						myBody.velocity = new Vector2 (100f, myBody.velocity.y);
						StartCoroutine (Dead(1f));
					}
				}
			}
		}

		if (rightHit) {
			if(rightHit.collider.gameObject.tag == "Player") {
				if (!Killed) {
					//APPLY DAMAGE TO PLAYER
					rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
				} else {
					if (tag != MyTags.BEETLE_TAG) {
						myBody.velocity = new Vector2 ( -100f, myBody.velocity.y);
						StartCoroutine (Dead(1f));
					}
				}
			}
		}

		if(tag == MyTags.BUG_TAG) {
			if (Physics2D.Raycast (left_Collision.position, Vector2.left, 0.1f)) {
			ChangeDirection();
		}
		}

		if (!Physics2D.Raycast (down_Collision.position, Vector2.down, 0.1f)) {

			ChangeDirection();
		}
		
	}

	void ChangeDirection() {

		moveLeft = !moveLeft;

		Vector3 tempScale = transform.localScale;

		if (moveLeft) {
			tempScale.x = Mathf.Abs (tempScale.x);

			left_Collision.position = left_Collision_Pos;
			right_Collision.position = right_Collision_Pos;

		} else {
			tempScale.x = -Mathf.Abs (tempScale.x);

			left_Collision.position = right_Collision_Pos;
			right_Collision.position = left_Collision_Pos;
		}

		transform.localScale = tempScale;

	}

	IEnumerator Dead(float timer) {
		yield return new WaitForSeconds (timer);
		gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == MyTags.BULLET_TAG) {

			if (tag == MyTags.BEETLE_TAG) {
				anim.Play ("Killed");

				canMove = false;
				myBody.velocity = new Vector2 (0, 0);

				StartCoroutine (Dead (0.4f));
			}

			if (tag == MyTags.BUG_TAG) {
				if (!Killed) {

					anim.Play ("Killed");
					Killed = true;
					canMove = false;
					myBody.velocity = new Vector2 (0, 0);

					StartCoroutine (Dead (0.2f));
				}

			 else {
					gameObject.SetActive (false);
				}
			}
		}
 
	}

} //class




















