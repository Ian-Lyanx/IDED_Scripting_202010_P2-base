using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(GameObjectPool))]
public class Player : MonoBehaviour
{
    public static Player instance;
    public const int PLAYER_LIVES = 3;

    private const float PLAYER_RADIUS = 0.4F;

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 1F;

    private float hVal;

    #region Bullet

    [Header("Bullet")]
    [SerializeField]
    private Transform bulletSpawnPoint;
    private GameObjectPool pool;

    [SerializeField]
    private float bulletSpeed = 3F;

    #endregion Bullet

    #region BoundsReferences

    private float referencePointComponent;
    private float leftCameraBound;
    private float rightCameraBound;

    #endregion BoundsReferences

    #region StatsProperties

    public int Score { get; set; }
    public int Lives { get; set; }

    #endregion StatsProperties

    #region MovementProperties

    public bool ShouldMove
    {
        get =>
            (hVal != 0F && InsideCamera) ||
            (hVal > 0F && ReachedLeftBound) ||
            (hVal < 0F && ReachedRightBound);
    }

    private bool InsideCamera
    {
        get => !ReachedRightBound && !ReachedLeftBound;
    }

    private bool ReachedRightBound { get => referencePointComponent >= rightCameraBound; }
    private bool ReachedLeftBound { get => referencePointComponent <= leftCameraBound; }

    private bool CanShoot
    {
        get => bulletSpawnPoint != null;
    }

    #endregion MovementProperties

    public Command vidaCommand;
    public Action OnPlayerDied;
    public Action<int> OnPlayerHit;
    public Action<int> OnPlayerChangeScore;


    private void Awake()
    {
        vidaCommand = new VidaCommand(this);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad((this.gameObject));
        }
        else Destroy(this);

        pool = GetComponent<GameObjectPool>();
        leftCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
                              0F, 0F, 0F)).x + PLAYER_RADIUS;

        rightCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
                               1F, 0F, 0F)).x - PLAYER_RADIUS;

        Lives = PLAYER_LIVES;
    }

    // Update is called once per frame
    private void Update()
    {
        hVal = Input.GetAxis("Horizontal");

        if (ShouldMove)
        {
            transform.Translate(Time.deltaTime * hVal * moveSpeed * transform.right);
            referencePointComponent = transform.position.x;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            && CanShoot)
        {
            pool.AllocateObject(bulletSpawnPoint.position,Quaternion.identity,0).GetComponent<Rigidbody>().AddForce(transform.up * bulletSpeed, ForceMode.Impulse);
        }
    }

    public void TakeDamage(int damager)
    {
        Lives -= damager;
        print(Lives);
        
        if (Lives >= 0)
        {
            if (OnPlayerHit != null)
            {
                OnPlayerHit(damager);
            }
        }
        else
        {
            if(OnPlayerDied != null)
            {
                OnPlayerDied();
                Destroy(instance);
            }
        }

        if(Lives <= 0)
        {
            this.enabled = false;
            gameObject.SetActive(false);
        }
    }

    public void AddScore(int cantidad)
    {
        Score += cantidad;

        if(OnPlayerChangeScore != null)
        {
            OnPlayerChangeScore(cantidad);
        }
    }
}