using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]    //make private variables to be shown in the inspector
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

    [SerializeField]
    private float thrusterFuelBurnSpeed = 1f;
    [SerializeField]
    private float thrusterFuelRegenSpeed = 0.3f;
    private float thrusterFuelAmount = 1f;
    public float jumpForce = 0.1f;
    Camera cam;
    [SerializeField]
    public int maxHealth = 100;
    private int curHealth;
    
    public float GetThrusterFuelAmount()
    {
        return thrusterFuelAmount;
    }

   

    private PlayerMotor motor;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        #region movement
        //calculate movement velocity as 3d vector
        float xMov = Input.GetAxisRaw("Horizontal");    //allows to set programmers function of working with movement to make smooth movement in network
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov; //transform.right shows direction of movement on xAxis (1, 0, 0).
        Vector3 movVertical = transform.forward * zMov;
        
        //calculation of movement
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;
        //use calculated movement
        motor.Move(velocity);
        #endregion

        #region CameraTurning
        //---------------------------------------------------------------------------------------
        //Calculate rotation as 3d vector (turn around) for X
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        //apply rotation
        motor.Rotate(rotation);

        //---------------------------------------------------------------------------------------
        //Calculate rotation as 3d vector (turn around) for Y
        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRot * lookSensitivity;

        //apply rotation
        motor.RotateCamera(cameraRotationX);
        //----------------------------------------------------------------------------------------
        #endregion

        #region JetPack jump
        //calculate thruster force based on the user input
        Vector3 thrusterForceLoc = Vector3.zero;

        if (Input.GetButton("Jump") && thrusterFuelAmount > 0f)
        {
            thrusterFuelAmount -= thrusterFuelBurnSpeed * Time.deltaTime;

            if(thrusterFuelAmount >= 0.01f) //fix of infinite jump up by pressing space
            {
                thrusterForceLoc = Vector3.up * thrusterForce;
            }
        }
        else
        {
            thrusterFuelAmount += thrusterFuelRegenSpeed * Time.deltaTime;
        }

        thrusterFuelAmount = Mathf.Clamp(thrusterFuelAmount, 0f, 1f);    //needs to keep amount of fuel in interval from 0 to 1

        //apply thruster force
        motor.ApplyThrusterF(thrusterForceLoc);
        #endregion

    }


}
