using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dyelaga;

namespace Dyelaga.TheShip {

    public class TakeDamage : MonoBehaviour
    {
        public GameFinish gameFinish;
        public void TakeDamageAmount(int amount) {
            // We don't actually care about the amount
            if (gameFinish is null)
                Console.Error.WriteLine("Ship needs a game finish object");
            else 
                gameFinish.EndGameDestroyed();
        }
    }

}