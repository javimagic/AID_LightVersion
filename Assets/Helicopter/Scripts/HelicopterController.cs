using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelicopterController : MonoBehaviour
{
    public AudioSource HelicopterSound;
    public ControlPanel ControlPanel;
    public Rigidbody HelicopterModel;
    public HeliRotorController MainRotorController;
    public HeliRotorController SubRotorController;
    public VictimInteractionBoundary victimBoundary;
    public GameObject victim;
    public bool controllingThisHelicopter = true;
    public float autoStability = 0.4f;
    
    public float TurnForce = 5f;    // Turn torque due to tilt-x (hMove.x)   Original: 3
    public float ForwardForce = 10f;   // Forward force due to tilt-y (hMove.y)
    public float upwardsPowerForce = 2.0f;
    // public float ForwardTiltForce = 20f;
    public float TurnTiltForce = 30f;
    public float coef_userTilt = 150.0f;
    public float EffectiveHeight =  100f;

    public float turnTiltForcePercent = 1.5f;
    public float turnForcePercent = 3f;   // Original: 1.3

    private float _engineForce;
    public float maxEngineForce = 30;
    public float EngineForce = 0f;


    /*
    public float EngineForce
    {
        get { return _engineForce; }
        set
        {
            MainRotorController.RotarSpeed = value * 80;
            SubRotorController.RotarSpeed = value * 40;
            HelicopterSound.pitch = Mathf.Clamp(value / 40, 0, 1.2f);
            // if (UIGameController.runtime.EngineForceView != null)
            //     UIGameController.runtime.EngineForceView.text = string.Format("Engine value [ {0} ] ", (int)value);

            _engineForce = value;
        }
    }
    */

    private Vector2 hMove = Vector2.zero;
    private Vector2 hTilt = Vector2.zero;
    private float hTurn = 0f;
    public bool IsOnGround = true;




    // Use this for initialization
	void Start ()
	{
        EngineForce = 0f;
        controllingThisHelicopter = true;
    }

    void useControls ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float throttle = Input.GetAxis("PS4_R2");
        // float throttle = 0.1f; // provisional, porque no va hoy el mando
        bool turnright = Input.GetButton("PS4_R1");
        bool turnleft = Input.GetButton("PS4_L1");
        bool pause = Input.GetButton("PS4_option");
        bool interaction = Input.GetButton("PS4_X");

        EngineForce = Mathf.Lerp(EngineForce, maxEngineForce * (0.1f + throttle) * 5f, Time.fixedDeltaTime);
        hMove.x = moveHorizontal;
        hMove.y = moveVertical;
        if (turnright)
        {
            var force = (turnForcePercent - Mathf.Abs(hMove.y)) * HelicopterModel.mass * EngineForce / maxEngineForce;
            HelicopterModel.AddRelativeTorque(0f, force, 0);
        }
        if (turnleft)
        {
            var force = -(turnForcePercent - Mathf.Abs(hMove.y)) * HelicopterModel.mass * EngineForce / maxEngineForce;
            HelicopterModel.AddRelativeTorque(0f, force, 0);
        }
        if (interaction)
        {
            if (victimBoundary.playerNearby)
            {
                Debug.Log("Rescued!!!");
                Destroy(victim);
            }
        }
    }

	void FixedUpdate () {
        float stabilityTorqueX = 5.0f;
        float stabilityTorqueZ = 10.0f;
        float stabilityForce = 150f;
        float xAng, zAng;

        // Engine Force:
        MainRotorController.RotarSpeed = EngineForce * 80;
        SubRotorController.RotarSpeed = EngineForce * 40;
        HelicopterSound.pitch = Mathf.Clamp(EngineForce / 40, 0, 1.2f);

        // Stability Torque:
        xAng = HelicopterModel.transform.localEulerAngles.x;
        if (xAng > 180) xAng -= 360;
        xAng *= -1;
        zAng = HelicopterModel.transform.localEulerAngles.z;
        if (zAng > 180) zAng -= 360;
        zAng *= -1;
        Vector3 stabilityVector = new Vector3(xAng * stabilityTorqueX, 0.0f, zAng * stabilityTorqueZ);
        // Debug.Log(Mathf.Pow(EngineForce / maxEngineForce, 0.3f));
        HelicopterModel.AddRelativeTorque(stabilityVector * Mathf.Pow(EngineForce / maxEngineForce, 0.3f));

        // Stability Force:
        GetComponent<Rigidbody>().AddForce(Vector3.ProjectOnPlane(-GetComponent<Rigidbody>().velocity * stabilityForce, Vector3.up));

        
        if (controllingThisHelicopter)
        {
            useControls();
        } else
        {
            EngineForce = Mathf.Lerp(EngineForce, EngineForce - GetComponent<Rigidbody>().velocity.y, Time.fixedDeltaTime * autoStability);
            Mathf.Clamp(EngineForce, 0, maxEngineForce);
        }
        
        LiftProcess();
        MoveProcess();
        TiltProcess();
        if (Input.GetButton("PS4_option"))
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
  
    
    private void MoveProcess()   // Turn due to tilt-x and forward due to tilt-y. RIGHT
    {
        var turn = TurnForce * hMove.x * (turnTiltForcePercent - Mathf.Abs(hMove.y)) * HelicopterModel.mass * EngineForce / maxEngineForce;
        // var forw_force = hMove.y * ForwardForce * HelicopterModel.mass * EngineForce / maxEngineForce;
        var forw_force = hMove.y * ForwardForce * HelicopterModel.mass * (1 - Mathf.Pow(((EngineForce - maxEngineForce) / maxEngineForce), 4));
        HelicopterModel.AddRelativeTorque(0f, turn, 0f);
        HelicopterModel.AddRelativeForce(Vector3.forward * forw_force);
    }

    private void LiftProcess()    // Upwards force depending on EffectiveHeight. RIGHT
    {
        var upForce = 1 - Mathf.Clamp(HelicopterModel.transform.position.y / EffectiveHeight, 0, 1);
        upForce = Mathf.Lerp(0f, EngineForce, upForce) * upwardsPowerForce;
        HelicopterModel.AddRelativeForce(Vector3.up * upForce);
    }

    private void TiltProcess()
    {
        
        HelicopterModel.AddRelativeTorque(coef_userTilt * new Vector3(hMove.y, 0.0f, -hMove.x) * EngineForce / maxEngineForce);
    }
    
    private void OnCollisionEnter()
    {
        IsOnGround = true;
    }

    private void OnCollisionExit()
    {
        IsOnGround = false;
    }
}