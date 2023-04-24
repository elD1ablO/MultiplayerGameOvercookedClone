using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData : IEquatable<PlayerData>
{
    public ulong clientID;

    public bool Equals(PlayerData other)
    {
        return clientID == other.clientID;
    }
}
