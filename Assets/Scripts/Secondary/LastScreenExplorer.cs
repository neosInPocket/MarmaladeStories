using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastScreenExplorer : MonoBehaviour
{
	[SerializeField] private TMP_Text explorerReviewResult;
	[SerializeField] private TMP_Text coinsRewExplorer;
	[SerializeField] private TMP_Text explorerButtonText;

	public void ExploreLastScreen(int layer, int coinsGrinded)
	{
		gameObject.SetActive(true);

		if (coinsGrinded <= 0)
		{
			explorerReviewResult.text = "YOU LOSE";
			explorerButtonText.text = "TRY AGAIN";
			coinsRewExplorer.text = $"{coinsGrinded}";
		}
		else
		{
			explorerReviewResult.text = $"LEVEL {layer} COMPLETED!";
			explorerButtonText.text = "NEXT LEVEL";
			coinsRewExplorer.text = coinsGrinded.ToString();
		}
	}

	public void ExploreNext()
	{
		SceneManager.LoadScene("Secondary");
	}

	public void ExploreMainMenu()
	{
		SceneManager.LoadScene("Initial");
	}
}
