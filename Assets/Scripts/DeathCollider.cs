using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        Game.Instance.EndGame();
    }
    void OnTriggerEnter(Collider col)
    {
        Game.Instance.EndGame();
    }
}