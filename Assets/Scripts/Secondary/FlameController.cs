using System.Collections;
using UnityEngine;

public class FlameController : MonoBehaviour
{
	[SerializeField] private ParticleSystem mainParticle;
	[SerializeField] private BoxCollider2D colliderBox;
	[SerializeField] private float stopEmitSpeed;
	[SerializeField] private float startEmitSpeed;
	[SerializeField] private float pulse;

	private void Start()
	{
		StartCoroutine(PulseDelay());
	}

	public void DisableFlame()
	{
		colliderBox.enabled = false;
		var main = mainParticle.main;
		main.startSpeed = stopEmitSpeed;
	}

	public void EnableFlame()
	{
		colliderBox.enabled = true;
		var main = mainParticle.main;
		main.startSpeed = startEmitSpeed;
	}

	public IEnumerator PulseDelay()
	{
		DisableFlame();
		yield return new WaitForSeconds(pulse);
		EnableFlame();
		yield return new WaitForSeconds(pulse);
		StartCoroutine(PulseDelay());
	}
}
