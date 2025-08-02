using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    private List<Rigidbody2D> parts = new List<Rigidbody2D>();

    private void Start()
    {
        foreach (Rigidbody2D rb in GetComponentsInChildren<Rigidbody2D>())
        {
            rb.bodyType = RigidbodyType2D.Static;
            parts.Add(rb);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Moveable"))
            return;

        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].gameObject.layer = 11; //Uncollideable layer
            parts[i].transform.SetParent(null);
            parts[i].bodyType = RigidbodyType2D.Dynamic;
        }
        Destroy(gameObject);

    }
}