using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenExplorer : MonoBehaviour
{
	public TMP_Text levelExplorer;
	public TMP_Text rewExplorer;
	public TMP_Text scoreExplorer;
	public Image fullImage;
	public int maxProgress;

	public void RestartScoreExplorer(int maximumScore, int rewAmount, int currentLayer)
	{
		maxProgress = maximumScore;
		rewExplorer.text = rewAmount.ToString();
		levelExplorer.text = "LEVEL " + currentLayer;
		ExploreLevelInfo(0);
	}

	public void ExploreLevelInfo(int currentProgress)
	{
		fullImage.fillAmount = (float)currentProgress / (float)maxProgress;
		scoreExplorer.text = $"{currentProgress}/{maxProgress}";
	}
}
