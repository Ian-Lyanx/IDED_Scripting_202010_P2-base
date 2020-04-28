using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaCommand : Command
{

    private Player playersito;

    public void Execute()
    {
        playersito.TakeDamage(1);
    }    

    public VidaCommand(Player playerP)
    {
        playersito = playerP;
    }
  
}
