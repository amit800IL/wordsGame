using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("ControllerAttributes")]
    [SerializeField] private float _railSizeMultiplyer;
    [Header("Components")]
    [SerializeField] private GameObject _rail;
    [Header("Inputs")]
    [SerializeField] private KeyCode _input_right;
    [SerializeField] private KeyCode _input_left;
    [SerializeField] private KeyCode _input_shoot;
    // Start is called before the first frame update
    private void Awake()
    {
   
        if (_rail == null) return;
        _rail.transform.localScale = new Vector3(transform.localScale.x * (_railSizeMultiplyer*_railSizeMultiplyer) , 1, 1);
        transform.localPosition = Vector3.zero + new Vector3(0,0,1);
   
        
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }
    private void Move(float direction)
    {
        float nextPosition = transform.localPosition.x + (transform.localScale.x * direction);     
        float movementRange = (1 - (1/_railSizeMultiplyer))/2 + 0.01f;
        if (nextPosition  > movementRange || nextPosition < -movementRange) return;
        transform.localPosition += new Vector3(transform.localScale.x * direction, 0, 0);
    }

    private void InputHandler()
    {     
        if (Input.GetKeyDown(_input_right)) Move(1);
        if (Input.GetKeyDown(_input_left)) Move(-1);
        if (Input.GetKeyDown(_input_shoot)) Shoot();
    }
    private void Shoot()
    {

    }
}


