using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager
{
    // 2
    protected string _state;
    public abstract string state { get; set; }
    // 3
    public abstract void Initialize();
}
