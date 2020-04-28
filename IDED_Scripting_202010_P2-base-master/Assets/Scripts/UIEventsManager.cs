using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventsManager : MonoBehaviour
{

    public UIEventsManager instance
    {
        get { return instance; }

        set { }

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if ( instance != this)
        {
            Destroy(gameObject);
        }

        Player playerRef = FindObjectOfType<Player>();
        UIController uiRef = FindObjectOfType<UIController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
