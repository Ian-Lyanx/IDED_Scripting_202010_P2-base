using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCommand : Command
{
    private Target targetsito;

    public void Execute()
    {
        targetsito.TargetDaño(1);
    }

    public ScoreCommand(Target Target)
    {
        targetsito = Target;

    }
}
