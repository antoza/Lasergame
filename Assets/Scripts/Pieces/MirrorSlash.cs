using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSlash : Piece
{
    public override (int, int)[] ComputeNewDirections((int, int) sourceDirection)
    {
        (int, int)[] newDirections = new (int, int)[1];
        newDirections[0] = (sourceDirection.Item2, sourceDirection.Item1);
        return newDirections;
    }
}
