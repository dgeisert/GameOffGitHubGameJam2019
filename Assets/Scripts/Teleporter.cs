using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform target;
    public bool victoryPortal = false;
    void OnTriggerEnter(Collider col)
    {
        if (victoryPortal && col.tag == "Player")
        {
            Game.Instance.EndGame(true);
        }
        if (col.tag == "Player" && target != null)
        {
            GameObject.FindObjectOfType<JumpCharacter>().transform.position = target.position;
        }
    }
}