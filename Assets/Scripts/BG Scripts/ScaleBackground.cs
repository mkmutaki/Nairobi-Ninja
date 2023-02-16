using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackground : MonoBehaviour {

	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();

		transform.localScale = new Vector3 (4, 4, 1);

		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;

		float worldHeight = Camera.main.orthographicSize * 2f;
		float worldWidth = worldHeight / Screen.height * Screen.width;

		Vector3 tempScale = transform.localScale;
		tempScale.x = worldWidth / width + 0.9f;
		tempScale.y = worldHeight / height + 0.9f;

		transform.localScale = tempScale;


		
}

} //class


















