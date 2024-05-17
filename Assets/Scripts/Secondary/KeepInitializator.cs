using UnityEngine;

public class KeepInitializator : MonoBehaviour
{
	[SerializeField] private ScaleBall ball1;
	[SerializeField] private ScaleBall ball2;
	[SerializeField] private ScaleOrbSpawner scaleOrbSpawner;
	[SerializeField] private CameraTargetChoose cameraTargetChoose;
	[SerializeField] private ScoreScreenExplorer scoreExplorer;
	[SerializeField] private PreviewExplorer previewExplorer;
	[SerializeField] private CountAttenuator countAttenuator;
	[SerializeField] private LastScreenExplorer lastScreenExplorer;
	public int currentLayer => SafeKeeper.Entry.layer;
	public int scorePassed { get; private set; }
	public int maximumScore => (int)(4f * Mathf.Log(currentLayer + 1) + 1.5f);
	public int maximumRew => (int)(10f * Mathf.Log(currentLayer + 2) + 10.5f);
	public bool preview
	{
		get => SafeKeeper.Entry.schooling == 1;
		set
		{
			SafeKeeper.Entry.schooling = value ? 1 : 0;
			SafeKeeper.Entry.Safer();
		}
	}

	private void Start()
	{
		ball1.Initialize();
		ball2.Initialize();
		scaleOrbSpawner.Initialize();
		scoreExplorer.RestartScoreExplorer(maximumScore, maximumRew, currentLayer);

		if (preview)
		{
			preview = false;
			previewExplorer.StartExplore();
			previewExplorer.PreviewExplored += PreviewExplored;
		}
		else
		{
			PreviewExplored();
		}
	}

	public void PreviewExplored()
	{
		previewExplorer.PreviewExplored -= PreviewExplored;
		countAttenuator.StartAttenuator();
		countAttenuator.Attenuated += CountAttenuated;
	}

	public void CountAttenuated()
	{
		ball1.ActivateBall();
		ball2.ActivateBall();
		ball1.BallFitAction += BallFitAction;
		ball2.BallFitAction += BallFitAction;
		ball1.UnFitAction += UnFitAction;
		ball2.UnFitAction += UnFitAction;
		cameraTargetChoose.TargetOverflow += UnFitAction;
	}

	public void BallFitAction()
	{
		scorePassed++;
		if (scorePassed == maximumScore)
		{
			ball1.DeactivateBall();
			ball2.DeactivateBall();
			ball1.BallFitAction -= BallFitAction;
			ball2.BallFitAction -= BallFitAction;
			ball1.UnFitAction -= UnFitAction;
			ball2.UnFitAction -= UnFitAction;
			cameraTargetChoose.TargetOverflow -= UnFitAction;
			scorePassed = maximumScore;

			lastScreenExplorer.ExploreLastScreen(currentLayer, maximumRew);
			SafeKeeper.Entry.currency += maximumRew;
			SafeKeeper.Entry.layer++;
			SafeKeeper.Entry.Safer();
		}

		scoreExplorer.ExploreLevelInfo(scorePassed);
	}

	public void UnFitAction()
	{
		ball1.DeactivateBall();
		ball2.DeactivateBall();
		ball1.BallFitAction -= BallFitAction;
		ball2.BallFitAction -= BallFitAction;
		ball1.UnFitAction -= UnFitAction;
		ball2.UnFitAction -= UnFitAction;
		cameraTargetChoose.TargetOverflow -= UnFitAction;

		ball1.BlowScaleBall();
		ball2.BlowScaleBall();

		lastScreenExplorer.ExploreLastScreen(currentLayer, 0);
	}

	private void OnDestroy()
	{
		ball1.BallFitAction -= BallFitAction;
		ball2.BallFitAction -= BallFitAction;
		ball1.UnFitAction -= UnFitAction;
		ball2.UnFitAction -= UnFitAction;
		cameraTargetChoose.TargetOverflow -= UnFitAction;
	}
}
