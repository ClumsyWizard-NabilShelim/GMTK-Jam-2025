using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObjectUI : MonoBehaviour
{
    [SerializeField] private GameObject marker;

    private void Start()
    {
        marker.SetActive(false);
    }

    public void ToggleMarker(bool toggle)
    {
        marker.SetActive(toggle);
    }
}