using UnityEngine;

public class ScaleOrb : MonoBehaviour
{
	[SerializeField] private GameObject fit;
	[SerializeField] private GameObject unFit;
	[SerializeField] private GameObject neutral;
	[SerializeField] private float fitError;
	[SerializeField] private Vector2 minMaxScale;
	[SerializeField] private GameObject fitBlow;

	public void InitializeScaleOrb(float xPosition)
	{
		var scale = Random.Range(minMaxScale.x, minMaxScale.y);
		transform.localScale = new Vector3(scale, scale, scale);
		transform.position = new Vector2(xPosition, transform.position.y);
	}

	public bool CheckScaleBallFit(Transform ball)
	{
		var ballScale = ball.localScale.x;
		var selfScale = transform.localScale.x;
		bool returnValue = Mathf.Abs(ballScale - selfScale) < fitError;
		FitCheck(returnValue);
		return returnValue;
	}

	public void FitCheck(bool isFit)
	{
		if (isFit)
		{
			neutral.SetActive(false);
			fit.SetActive(true);
			fitBlow.SetActive(true);
		}
		else
		{
			neutral.SetActive(false);
			unFit.SetActive(true);
		}
	}
}
