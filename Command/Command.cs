using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public float cTime;

    public virtual void Do(Avatar avatar)
    {
    }

    public virtual void UnDo(Avatar avatar)
    {
    }
}