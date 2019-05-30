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
    public ControlChanger changer;
    public GameObject victim1;
    public GameObject victim2;
    public GameObject victim3;
    public bool controllingThisHelicopter = true;
    public bool helicopterIsAlive = true;
    public float autoStability = 0.4f;
    public float PID_P = 1f;
    public float PID_I = 1f;
    public float PID_D = 1f;
    public float airResistanceCoef =  0.03f;
    private float error = 0f, lastError = 0f, sumErrors = 0f;
    private bool wasControllingHeli = true;
    public PauseMenu pauseMenu;

    public float TurnForce = 5f;    // Turn torque due to tilt-x (hMove.x)   Original: 3
    public float ForwardForce = 10f;   // Forward force due to tilt-y (hMove.y)
    public float upwardsPowerForce = 2.0f;
    // public float ForwardTiltForce = 20f;
    public float TurnTiltForce = 30f;
    public float coef_userTiltX = 150.0f;
    public float coef_userTiltZ = 150.0f;
    public float EffectiveHeight =  100f;

    public float turnTiltForcePercent = 1.5f;
    public float turnForcePercent = 3f;   // Original: 1.3

    private float _engineForce;
    public float maxEngineForce = 30;
    public float EngineForce = 0f;

    public float stabilityForce = 150f;
    public float stabilityTorqueX = 5.0f;
    public float stabilityTorqueZ = 10.0f;

    private Vector2 hMove = Vector2.zero;
    private float stdVolume;

    private CountDown timer1;
    private CountDown timer2;
    private CountDown timer3;
    private VictimInteractionBoundary victim1Boundary;
    private VictimInteractionBoundary victim2Boundary;
    private VictimInteractionBoundary victim3Boundary;
    private float sensitivity;

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
    

    
    private Vector2 hTilt = Vector2.zero;
    private float hTurn = 0f;

    */

    public bool IsOnGround = true;




    // Use this for initialization
	void Start ()
	{
        timer1 = victim1.GetComponent<CountDown>();
        timer2 = victim2.GetComponent<CountDown>();
        timer3 = victim3.GetComponent<CountDown>();
        victim1Boundary = victim1.transform.Find("Zone2Boundary").GetComponent<VictimInteractionBoundary>();
        victim2Boundary = victim2.transform.Find("Zone2Boundary").GetComponent<VictimInteractionBoundary>();
        victim3Boundary = victim3.transform.Find("Zone2Boundary").GetComponent<VictimInteractionBoundary>();
        EngineForce = 0f;
        controllingThisHelicopter = true;
        stdVolume = HelicopterSound.volume;
        sensitivity = PlayerPrefs.GetFloat("SensHeli");
    }

    void useControls ()
    {
        float moveHorizontal = Mathf.Clamp(Input.GetAxis("Horizontal") * sensitivity, -1, 1);
        float moveVertical = Mathf.Clamp(Input.GetAxis("Vertical") * sensitivity, -1, 1);
        float throttle = Input.GetAxis("PS4_R2");
        // float throttle = 0.1f; // provisional, porque no va hoy el mando
        bool turnright = Input.GetButton("PS4_R1");
        bool turnleft = Input.GetButton("PS4_L1");
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
            if (victim1Boundary.playerNearby && timer1.victimIsAlive && !timer1.victimAboard)
            {
                // Debug.Log("Rescued!!!");
                timer1.victimGoesIn();
                Destroy(victim1.transform.Find("Person").gameObject);
            }
            if (victim2Boundary.playerNearby && timer2.victimIsAlive && !timer2.victimAboard)
            {
                // Debug.Log("Rescued!!!");
                timer2.victimGoesIn();
                Destroy(victim2.transform.Find("Person").gameObject);
            }
            if (victim3Boundary.playerNearby && timer3.victimIsAlive && !timer3.victimAboard)
            {
                // Debug.Log("Rescued!!!");
                timer3.victimGoesIn();
                Destroy(victim3.transform.Find("Person").gameObject);
            }
        }
    }

    private void Update()
    {
        HelicopterSound.volume = (!pauseMenu.gamePaused && helicopterIsAlive) ? stdVolume : 0f;
    }

    void FixedUpdate () {
        float xAng, zAng;

        if (!helicopterIsAlive) return;

        if (wasControllingHeli && !controllingThisHelicopter) {
            error = 0f;
            lastError = 0f;
            sumErrors = 0f;
        }
        wasControllingHeli = controllingThisHelicopter;

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

            // EngineForce = Mathf.Lerp(EngineForce, EngineForce - GetComponent<Rigidbody>().velocity.y, Time.fixedDeltaTime * autoStability);
            EngineForce -= PID(changer.lastHeight, transform.position.y);
            EngineForce = Mathf.Clamp(EngineForce, 0, maxEngineForce);
        }
        
        LiftProcess();
        MoveProcess();
        TiltProcess();
        airResistance();
        if (Input.GetButton("PS4_PSN"))
        {
            SceneManager.LoadScene("AIDS_Menu");
        }

    }
  
    private void airResistance()
    {
        float speedY = gameObject.GetComponent<Rigidbody>().velocity.y;
        float extraEngineForce;
        if (speedY < 0)
        {
            extraEngineForce = Mathf.Abs(Mathf.Pow(speedY * airResistanceCoef, 2));
            EngineForce += extraEngineForce;
            EngineForce = Mathf.Clamp(EngineForce, 0f, maxEngineForce);
        }
    }

    
    private float PID (float target, float current) {
        float correction;
        lastError = error;
        error = target - current;
        sumErrors += error;
        
        correction = PID_P * (current - target);
        correction -= PID_I * sumErrors;
        correction -= PID_D * (error - lastError);
        // Debug.Log("P = " + PID_P * (actual - target) + "; I = " + -PID_I * sumErrors + "; D = " + -PID_D * (error - lastError));
        return correction;
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
        
        HelicopterModel.AddRelativeTorque(new Vector3(coef_userTiltX * hMove.y, 0.0f, coef_userTiltZ * -hMove.x) * EngineForce / maxEngineForce);
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