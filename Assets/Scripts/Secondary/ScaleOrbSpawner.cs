using UnityEngine;

public class ScaleOrbSpawner : MonoBehaviour
{
	[SerializeField] private ScaleOrb scaleOrbPref;
	[SerializeField] private Vector2 appearDistances;
	[SerializeField] private CameraTargetChoose cam;
	[SerializeField] private Transform leftTarget;
	[SerializeField] private Transform rightTarget;
	[SerializeField] private ScaleOrb firstLeft;
	[SerializeField] private ScaleOrb firstRight;
	private ScaleOrb currentLeft;
	private ScaleOrb currentRight;

	public void Initialize()
	{
		currentLeft = firstLeft;
		currentRight = firstRight;

		currentLeft.InitializeScaleOrb(-cam.cameraSize.x / 2);
		currentRight.InitializeScaleOrb(cam.cameraSize.x / 2);
	}

	private void Update()
	{
		CheckLeft();
		CheckRight();
	}

	public void CheckLeft()
	{
		if (leftTarget.transform.position.y + 2 * cam.cameraSize.y > currentLeft.transform.position.y)
		{
			Vector2 orbPosition;
			orbPosition.x = currentLeft.transform.position.x;
			var difference = Random.Range(appearDistances.x, appearDistances.y);
			orbPosition.y = currentLeft.transform.position.y + difference;
			var left = Instantiate(scaleOrbPref, orbPosition, Quaternion.identity, transform);

			left.InitializeScaleOrb(orbPosition.x);
			currentLeft = left;
		}
	}

	public void CheckRight()
	{
		if (rightTarget.transform.position.y + 2 * cam.cameraSize.y > currentRight.transform.position.y)
		{
			Vector2 orbPosition;
			orbPosition.x = currentRight.transform.position.x;
			var difference = Random.Range(appearDistances.x, appearDistances.y);
			orbPosition.y = currentRight.transform.position.y + difference;
			var right = Instantiate(scaleOrbPref, orbPosition, Quaternion.identity, transform);

			right.InitializeScaleOrb(orbPosition.x);
			currentRight = right;
		}
	}
}
