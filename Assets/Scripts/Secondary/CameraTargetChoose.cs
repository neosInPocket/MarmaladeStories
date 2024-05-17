using System;
using UnityEngine;

public class CameraTargetChoose : MonoBehaviour
{
	[SerializeField] public Transform firstTarget;
	[SerializeField] public Transform secondTarget;
	[SerializeField] public float topOffsetValue;
	[Range(0, 1f)]
	[SerializeField]
	private float cameraTopBottomOffsets;
	private Transform currentTarget;
	private Vector3 camPosition;
	[HideInInspector] public Vector2 cameraSize;
	private float offset;
	private float topOffset;
	public Action TargetOverflow { get; set; }
	private bool disabled;

	private void Start()
	{
		cameraSize = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
		offset = 2 * cameraSize.y * cameraTopBottomOffsets;

		camPosition.x = 0;
		camPosition.z = -10;
		camPosition.y = firstTarget.position.y + cameraSize.y - offset;
		transform.position = camPosition;
		topOffset = 2 * cameraSize.y * topOffsetValue;
	}

	private void Update()
	{
		bool firstTargetBottom = secondTarget.transform.position.y - firstTarget.transform.position.y > 0;

		if (firstTargetBottom)
		{
			currentTarget = firstTarget;
		}
		else
		{
			currentTarget = secondTarget;
		}

		camPosition.y = currentTarget.position.y + cameraSize.y - offset;
		transform.position = camPosition;

		CheckTargetOverflow();
	}

	public void CheckTargetOverflow()
	{
		if (disabled) return;
		Transform otherTarget = currentTarget == firstTarget ? secondTarget : firstTarget;

		if (Mathf.Abs(firstTarget.transform.position.y - secondTarget.transform.position.y) > 2 * cameraSize.y - offset + currentTarget.localScale.x / 2 - topOffset)
		{
			disabled = true;
			TargetOverflow?.Invoke();
		}
	}
}
