using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Player playerRef;

    [SerializeField]
    private Image[] lifeImages;

    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private Button restartBtn;

    [SerializeField]
    private float tickRate = 0.2F;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        ToggleRestartButton(false);

        scoreLabel.text = "0000";

        Player.instance.OnPlayerHit += UpdateLifeImages;
        Player.instance.OnPlayerDied += Died;
        Player.instance.OnPlayerChangeScore += UpdateLabel;
    }

    public void ToggleRestartButton(bool val)
    {
        if (restartBtn != null)
        {
            restartBtn.gameObject.SetActive(val);
        }
    }

    public void UpdateLabel(int val)
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = Player.instance.Score.ToString();
        }
    }

    public void UpdateLifeImages(int lives)
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (lifeImages[i] != null && lifeImages[i].enabled)
            {
                lifeImages[i].gameObject.SetActive(Player.instance.Lives >= i + 1);
            }
        }
    }

    private void Died()
    {
        if (Player.instance.Lives <= 0)
        {
            CancelInvoke();

            if (scoreLabel != null)
            {
                scoreLabel.text = "Game Over";
            }

            ToggleRestartButton(true);
        }
    }
}