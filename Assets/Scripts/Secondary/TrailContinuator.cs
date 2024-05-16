using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailContinuator : MonoBehaviour
{
	public Transform followTarget;

	private void Update()
	{
		transform.position = followTarget.position;
	}
}
