using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationTrigger : MonoBehaviour
{
    public GameObject[] objectsToActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(true);
        }
        Destroy(gameObject);
    }
}
