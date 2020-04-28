using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour 
{
    public Command scoreCommand;

    [SerializeField]
    private int maxHP = 1;

    private int currentHP;

    [SerializeField]
    private int scoreAdd = 10;

    private void Start()
    {
        scoreCommand = new ScoreCommand(this);
        currentHP = maxHP;        
    }

    private void OnCollisionEnter(Collision collision)
    {
        int collidedObjectLayer = collision.gameObject.layer;

        if (collidedObjectLayer.Equals(Utils.BulletLayer))
        {
            this.gameObject.SetActive(false);

            scoreCommand.Execute();

            if (currentHP <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        else if (collidedObjectLayer.Equals(Utils.PlayerLayer) || collidedObjectLayer.Equals(Utils.KillVolumeLayer))
        {            
            if (Player.instance != null)
            {
                Player.instance.vidaCommand.Execute();

                if (Player.instance.Lives <= 0 && Player.instance.OnPlayerDied != null)
                {
                    Player.instance.OnPlayerDied();
                }
            }

            gameObject.SetActive(false);
        }
    }

    public void TargetDaño(int daño)
    {
        currentHP -= daño;

        if (Player.instance != null)
        {
            Player.instance.AddScore(scoreAdd);
        }
    }
}
