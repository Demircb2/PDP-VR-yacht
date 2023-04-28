using System;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

[RequireComponent(typeof(Rigidbody))]
public class YachtMovement : MonoBehaviour
{
    [SerializeField] private XRKnob knob;
    [SerializeField] [Range(0.0f, 3.0f)] private float forceMultiplier = 1.0f;
    [SerializeField] XRSlider slider;
    [SerializeField] [Range(0.0f, 10f)] private float speed = 5;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (knob != null)
        {
            knob.onValueChange.AddListener(OnKnobValueChanged);
        }
        else
        {
            Debug.LogError("XRKnob reference is not assigned.");
        }

        if (slider != null)
        {
            slider.onValueChange.AddListener(OnSliderValueChanged);
        }
        else
        {
            Debug.LogError("Slider reference is not assigned.");
        }
    }

    void Update()
    {
        HandleKeyboardInput();
    }

    private void OnSliderValueChanged(float sliderValue)
    {
        Debug.Log("Slider value changed" + sliderValue);
        ApplyEngineForce(sliderValue);
    }

    void OnDestroy()
    {
        if (knob != null)
        {
            knob.onValueChange.RemoveListener(OnKnobValueChanged);
        }
        if (slider != null)
        {
            slider.onValueChange.RemoveListener(OnSliderValueChanged);
        }
    }

    void OnKnobValueChanged(float value)
    {
        Debug.Log("Knob value changed: " + value);
        ApplyRotationForce(value);
    }

    void ApplyRotationForce(float value)
    {
        float force = value * forceMultiplier;
        rb.AddTorque(transform.up * force, ForceMode.Force);
    }

    void ApplyEngineForce(float value)
    {
        float forceEngine = value * speed;
        rb.AddRelativeForce(transform.right * forceEngine, ForceMode.Force);
    }

    void HandleKeyboardInput()
    {
        float knobValueChange = 0f;
        float sliderValueChange = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            knobValueChange -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            knobValueChange += Time.deltaTime;
        }

        if (Mathf.Abs(knobValueChange) > 0f)
        {
            knob.SetValue(knob.value + knobValueChange);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            sliderValueChange += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            sliderValueChange -= Time.deltaTime;
        }

        if (Mathf.Abs(sliderValueChange) > 0f)
        {
            slider.SetValue(slider.value + sliderValueChange);
        }
    }
}
