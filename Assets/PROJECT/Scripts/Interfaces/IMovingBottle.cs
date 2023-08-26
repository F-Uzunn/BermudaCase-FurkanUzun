using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovingBottle
{
    float startPointX { get; set; }
    float moveSpeed { get; set; }
    void Movement();
}
