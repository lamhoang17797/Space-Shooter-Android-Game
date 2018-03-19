using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	//tg doi de tao ra cac thien thach
	public float startWait;
	// short pause after the game begin cho player chuan bi
	public float waveWait;
// tg doi giua cac waves
	//public void AddScore(int newscoreValue);
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool restart;
	private bool gameOver;

	private int score;
	private int highscore;

	void Start ()
	{
		restart = false;
		gameOver = false;
		restartText.text = null;
		gameOverText.text = null;
		score = 0;
		highscore = PlayerPrefs.GetInt ("hs", 0);
		UpdateScore ();
		StartCoroutine (spawnWaves ()); //su dung coroutine!!??
	}

	void Update ()
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}

	IEnumerator spawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z); //set pos bat dau cac thien thach
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			if (gameOver) {
				restartText.text = "Tap to restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue; //scorevalue la so diem mong muon 
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score:" + score;
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!" + '\n' + "Score : " + score + '\n' + "Highscore : " + highscore;
		if (score > highscore) {
			highscore = score;
			gameOverText.text += '\n' + "New Highscore!";
			PlayerPrefs.SetInt ("hs", highscore);
		}
		gameOver = true;
	}
}