using UnityEngine;

public class KeepInitializator : MonoBehaviour
{
	[SerializeField] private ScaleBall ball1;
	[SerializeField] private ScaleBall ball2;

	private void Start()
	{
		ball1.ActivateBall();
		ball2.ActivateBall();
	}
}
