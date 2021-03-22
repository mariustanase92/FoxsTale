using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlamTarget : MonoBehaviour
{
    public EventHandler OnPlayerInRange;
    public EventHandler OnPlayerOutOfRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
            OnPlayerInRange?.Invoke(this, EventArgs.Empty);
    }
}
