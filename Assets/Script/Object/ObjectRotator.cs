using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public static float DELAY_TIME = 0.5f;

    private GameObject hitObject;
    [SerializeField]
    private LayerMask layerMask;
    private bool isClickable = true;

    private void Update()
    {
        if(!isClickable || GameManager.instance.IsGameLose() || GameManager.instance.IsGameWin())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            hitObject = CastRay();
            if(hitObject == null)
            {
                return;
            }
            hitObject.GetComponent<Puzzle>().Rotate();

            isClickable = false;
            StartCoroutine(ClickDelay());
        }
        if(Input.GetMouseButtonUp(0))
        {
            hitObject = null;
        }
    }

    private IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(DELAY_TIME);
        isClickable = true;
    }

    private GameObject CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Puzzle"))
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}
