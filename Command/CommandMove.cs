using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMove : Command
{
    private Vector3 TransPos;

    public CommandMove(Vector3 transPos, float time)
    {
        TransPos = transPos;
        cTime = time;
    }

    public override void Do(Avatar avatar)
    {
        avatar.Move(TransPos);
    }

    public override void UnDo(Avatar avatar)
    {
        avatar.Move(-TransPos);
    }
}