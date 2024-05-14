using System.Collections.Generic;
using UnityEngine;

public class UpWiser : MonoBehaviour
{
	[SerializeField] private List<UpWise> upWises;
	[SerializeField] private List<CurrencyContainer> currencyContainers;

	private void Start()
	{
		foreach (var upWise in upWises)
		{
			upWise.UpgradeUpWised += UpgradeUpWised;
		}

		RefershUpwisers();
		currencyContainers.ForEach(x => x.ContainCurrency());
	}

	public void RefershUpwisers()
	{
		upWises.ForEach(x => x.UpWiseRefreshState());
	}

	public void UpgradeUpWised()
	{
		RefershUpwisers();
		currencyContainers.ForEach(x => x.ContainCurrency());
	}

	private void OnDestroy()
	{
		foreach (var upWise in upWises)
		{
			upWise.UpgradeUpWised -= UpgradeUpWised;
		}
	}
}
