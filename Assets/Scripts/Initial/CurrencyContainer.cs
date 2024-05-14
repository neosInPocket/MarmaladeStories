using TMPro;
using UnityEngine;

public class CurrencyContainer : MonoBehaviour
{
	[SerializeField] private TMP_Text currencyHolder;
	[SerializeField] private TMP_Text sphereHolder;
	[SerializeField] private TMP_Text hammerHolder;
	[SerializeField] private TMP_Text playLevel;

	private void Start()
	{
		currencyHolder.text = SafeKeeper.Entry.currency.ToString();
		sphereHolder.text = SafeKeeper.Entry.initial.ToString() + "/5";
		hammerHolder.text = SafeKeeper.Entry.secondary.ToString() + "/5";
		playLevel.text = $"play level {SafeKeeper.Entry.layer}";
	}

	public void ContainCurrency()
	{
		currencyHolder.text = SafeKeeper.Entry.currency.ToString();
		sphereHolder.text = SafeKeeper.Entry.initial.ToString() + "/5";
		hammerHolder.text = SafeKeeper.Entry.secondary.ToString() + "/5";
	}
}
