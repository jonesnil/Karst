using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaddieEventArgs : EventArgs
{
    public Baddie baddiePayload;
}


public static class GameEvents
{

    public static event EventHandler<BaddieEventArgs> PlayerHit;
    public static event EventHandler GameOver;

    public static void InvokePlayerHit(Baddie baddie)
    {
        PlayerHit(null, new BaddieEventArgs { baddiePayload = baddie });
    }

    public static void InvokeGameOver()
    {
        GameOver(null, EventArgs.Empty);
    }

}