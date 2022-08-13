using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    private float horizontalInput;
    [SerializeField] float sensivity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up * horizontalInput * sensivity);
    }
}
