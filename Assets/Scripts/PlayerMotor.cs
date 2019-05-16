using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;


    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;  //setting current position of camera rot.
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //get movement vector from user
    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    //get rotational vector for user
    public void Rotate(Vector3 rotation)
    {
        this.rotation = rotation;
    }

    //get rotational vector for user
    public void RotateCamera(float cameraRotationX)
    {
        this.cameraRotationX = cameraRotationX;
    }

    //get a force thruster vector
    public void ApplyThrusterF(Vector3 thrusterForce)
    {
        this.thrusterForce = thrusterForce;
    }

    // Update is called once per frame
    void Update()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0) { rb.AddForce(Vector3.up * 5, ForceMode.Impulse); }
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + this.velocity * Time.fixedDeltaTime);  //this methods makes movement considering collisions to stop
        }

        if (thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0) { rb.AddForce(Vector3.up * 5, ForceMode.Impulse); }
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));  //make a quaternion for setting camera
        if (cam != null)
        {
            //manual rotation calculation
            currentCameraRotationX -= cameraRotationX;  //if will be set '+' then camera will be inverted
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);    //limits possible view referring to the X axis

            //apply rotation to the transform of camera considering current position of camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }
}
