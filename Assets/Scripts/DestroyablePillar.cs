using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePillar : MonoBehaviour {

	public void ReactToHit() {
		StartCoroutine (Die ());
	}

	private IEnumerator Die() {
		this.transform.Translate (0, -8, 0);

		yield return new WaitForSeconds (1.5f);

		Destroy (this.gameObject);
	}
}
