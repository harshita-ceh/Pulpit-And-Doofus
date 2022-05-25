using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    public float playerSpeed = 8f;
    [SerializeField] private Rigidbody rb;

    private void OnEnable() {
        transform.position = new Vector3(0, 1, 0);
    }

    private void FixedUpdate() {
        Vector3 pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        rb.MovePosition(transform.position + pos * playerSpeed * Time.fixedDeltaTime);
    }
}
