using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D myBody;

	private float speed = 4f;

	private Vector3 moveDirection = Vector3.down;

	private string coroutine_Name = "ChangeMovement";

	public Transform top_Collision;
	private bool Killed;
	public LayerMask playerLayer;

	void Awake() {
		anim = GetComponent<Animator> ();
		myBody = GetComponent<Rigidbody2D> ();
	}


	void Start () {
		StartCoroutine (coroutine_Name);
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveSpider ();
		CheckCollision();
		
	}

	void MoveSpider() {
		transform.Translate (moveDirection * speed * Time.smoothDeltaTime);

	}

	IEnumerator ChangeMovement() {
		yield return new WaitForSeconds (Random.Range (2f, 4f));

		if (moveDirection == Vector3.down) {
			moveDirection = Vector3.up;
		} else {
			moveDirection = Vector3.down;
		}
 
		StartCoroutine (coroutine_Name);
	}

	IEnumerator SpiderDead() {
		yield return new WaitForSeconds (1.5f);
		gameObject.SetActive (false);
	}

	void CheckCollision() {

		Collider2D topHit = Physics2D.OverlapCircle (top_Collision.position, 1f, playerLayer);

	if(topHit != null) {
			if (topHit.gameObject.tag == "Player") {
				if (!Killed) {
					topHit.gameObject.GetComponent<Rigidbody2D>().velocity = 
					new Vector2 (topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 18f);

					GetComponent<BoxCollider2D> ().isTrigger = true;
			myBody.bodyType = RigidbodyType2D.Dynamic;

			anim.Play ("SpiderDead");
			Killed = true;

			StartCoroutine (SpiderDead ());
			StopCoroutine (coroutine_Name);
				}
			}
	}
	}


	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == MyTags.BULLET_TAG) {
			anim.Play ("SpiderDead");

			myBody.bodyType = RigidbodyType2D.Dynamic;

			StartCoroutine (SpiderDead ());
			StopCoroutine (coroutine_Name);
		}

		if (target.tag == MyTags.PLAYER_TAG) {
			target.GetComponent<PlayerDamage> ().DealDamage ();
		}
	}

} //class

























