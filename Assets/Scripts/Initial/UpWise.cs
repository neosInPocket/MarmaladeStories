using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpWise : MonoBehaviour
{
	[SerializeField] private Button upgradeBuyer;
	[SerializeField] private Image buyerController;
	[SerializeField] private TMP_Text buyerControllerText;
	[SerializeField] private TMP_Text buyTextCost;
	[SerializeField] private TMP_Text upgradeControllerTextButton;
	[SerializeField] private int upWiseCost;
	[SerializeField] private bool isInitialState;
	[SerializeField] private Color allBoughtColor;
	[SerializeField] private Color noBoughtColor;
	public Action UpgradeUpWised { get; set; }

	private void Start()
	{
		buyTextCost.text = upWiseCost.ToString();
	}

	public void UpWiseRefreshState()
	{
		int updateState = isInitialState ? SafeKeeper.Entry.initial : SafeKeeper.Entry.secondary;
		buyerController.fillAmount = (float)updateState / 5f;
		buyerControllerText.text = updateState.ToString() + "/5";

		if (SafeKeeper.Entry.currency <= upWiseCost)
		{
			if (updateState >= 5)
			{
				upgradeControllerTextButton.text = "UPGRADED TO MAX";
				upgradeControllerTextButton.color = allBoughtColor;
				upgradeBuyer.interactable = false;
			}
			else
			{
				upgradeControllerTextButton.text = "NOT ENOUGH COINS";
				upgradeControllerTextButton.color = noBoughtColor;
				upgradeBuyer.interactable = false;
			}
		}
		else
		{
			if (updateState >= 5)
			{
				upgradeControllerTextButton.text = "UPGRADED TO MAX";
				upgradeControllerTextButton.color = allBoughtColor;
				upgradeBuyer.interactable = false;
			}
			else
			{
				upgradeControllerTextButton.text = "PURCHASE";
				upgradeControllerTextButton.color = Color.white;
				upgradeBuyer.interactable = true;
			}
		}
	}

	public void UpWiseUpgrade()
	{
		SafeKeeper.Entry.currency -= upWiseCost;
		if (isInitialState)
		{
			SafeKeeper.Entry.initial++;
		}
		else
		{
			SafeKeeper.Entry.secondary++;
		}

		SafeKeeper.Entry.Safer();

		UpgradeUpWised?.Invoke();
	}
}
