using System;
using UnityEngine;

public class CountAttenuator : MonoBehaviour
{
	public Action Attenuated { get; set; }

	public void StartAttenuator()
	{
		gameObject.SetActive(true);
	}

	public void CloseAttenuator()
	{
		gameObject.SetActive(false);
		Attenuated?.Invoke();
		Attenuated = null;
	}
}
