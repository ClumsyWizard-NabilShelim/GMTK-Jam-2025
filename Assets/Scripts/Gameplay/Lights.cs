using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField] private GameObject lightObject;
    [SerializeField] private bool isOn;

    [Header("Animation")]
    [SerializeField] private bool canFlicker;
    [SerializeField] private Vector2 flickerRange;
    private float flickerDuration;

    private void Update()
    {
        if (!canFlicker || !isOn)
            return;

        if(flickerDuration > 0.0f)
        {
            flickerDuration -= Time.deltaTime;
        }
        else
        {
            flickerDuration = Random.Range(flickerRange.x, flickerRange.y);
            lightObject.SetActive(!lightObject.activeSelf);
        }
    }

    public void Toggle(bool on)
    {
        isOn = on;
        lightObject.SetActive(isOn);
    }
}
