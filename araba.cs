

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class araba : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;


    private bool isFren;
    private float currentFrenceForce;
    private float currentDonusAcisi;

    public GameObject[] kameralar;
    public int kamerasayi;
    void Start()
    {
        for (int i = 0; i < kameralar.Length; i++)
        {
            kameralar[i].SetActive(false);
        }
        kameralar[kamerasayi].SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            kameralar[kamerasayi].SetActive(false);
            kamerasayi++;
            if (kamerasayi > kameralar.Length - 1)
            {
                kamerasayi = 0;
            }
            kameralar[kamerasayi].SetActive(true);
        }
    }



    [SerializeField] private float maxDonusAcisi;
    [SerializeField] private float motorTorqueForce;
    [SerializeField] private float BreakForce;

    [SerializeField] private WheelCollider onSolTekerlerCollider;
    [SerializeField] private WheelCollider onSagTekerlerCollider;
    [SerializeField] private WheelCollider arkaSolTekerlerCollider;
    [SerializeField] private WheelCollider arkaSagTekerlerCollider;

    [SerializeField] private Transform onSolTekerlekTransform;
    [SerializeField] private Transform onSagTekerlekTransform;
    [SerializeField] private Transform arkaSolTekerlekTransform;
    [SerializeField] private Transform arkaSagTekerlekTransform;

    private void FixedUpdate()
    {
        getUserInput();
        moveTheCar();
        rotateTheCar();
        rotateTheWheels();

    }
    private void rotateTheWheels()
    {
        rotateTheWheel(onSolTekerlerCollider, onSolTekerlekTransform);
        rotateTheWheel(onSagTekerlerCollider, onSagTekerlekTransform);
        rotateTheWheel(arkaSolTekerlerCollider, arkaSolTekerlekTransform);
        rotateTheWheel(arkaSagTekerlerCollider, arkaSagTekerlekTransform);

    }
    private void rotateTheWheel(WheelCollider tekerlerCollider, Transform tekerlekTransform)
    {
        Vector3 position;
        Quaternion rotation;
        tekerlerCollider.GetWorldPose(out position, out rotation);
        tekerlekTransform.position = position;
        tekerlekTransform.rotation = rotation;
    }
    private void rotateTheCar()
    {
        currentDonusAcisi = maxDonusAcisi * horizontalInput;
        onSolTekerlerCollider.steerAngle = currentDonusAcisi;
        onSagTekerlerCollider.steerAngle = currentDonusAcisi;

    }
    private void moveTheCar()
    {
        onSolTekerlerCollider.motorTorque = verticalInput * motorTorqueForce;
        onSagTekerlerCollider.motorTorque = verticalInput * motorTorqueForce;
        currentFrenceForce = isFren ? BreakForce : 0;
        if (isFren)
        {
            onSolTekerlerCollider.brakeTorque = currentFrenceForce;
            onSagTekerlerCollider.brakeTorque = currentFrenceForce;
            arkaSolTekerlerCollider.brakeTorque = currentFrenceForce;
            arkaSagTekerlerCollider.brakeTorque = currentFrenceForce;
        }
        else
        {
            onSolTekerlerCollider.brakeTorque = 0;
            onSagTekerlerCollider.brakeTorque = 0;
            arkaSolTekerlerCollider.brakeTorque = 0;
            arkaSagTekerlerCollider.brakeTorque = 0;
        }

    }
    private void getUserInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isFren = Input.GetKey(KeyCode.Space);

        if (Input.GetKeyDown(KeyCode.R))
        {
            resetCarRotation();

        }

    }
    private void resetCarRotation()
    {
        Quaternion rotation = transform.rotation;
        rotation.z = 0f;
        rotation.x = 0f;
        transform.rotation = rotation;
    }
}





