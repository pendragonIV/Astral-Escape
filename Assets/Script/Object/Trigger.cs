using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Trigger"))
        {
            PuzzleManager.instance.AddTriggered(this.gameObject);
            PuzzleManager.instance.AddTriggered(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trigger"))
        {
            PuzzleManager.instance.RemoveTriggered(this.gameObject);
            PuzzleManager.instance.RemoveTriggered(collision.gameObject);
        }
    }
}
