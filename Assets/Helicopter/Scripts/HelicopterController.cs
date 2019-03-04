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
    
    public float TurnForce = 5f;    // Turn torque due to tilt-x (hMove.x)   Original: 3
    public float ForwardForce = 10f;   // Forward force due to tilt-y (hMove.y)
    public float ForwardTiltForce = 20f;
    public float TurnTiltForce = 30f;
    public float EffectiveHeight =  100f;

    public float turnTiltForcePercent = 1.5f;
    public float turnForcePercent = 3f;   // Original: 1.3

    private float _engineForce;
    public float maxEngineForce = 50;
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

    private Vector2 hMove = Vector2.zero;
    private Vector2 hTilt = Vector2.zero;
    private float hTurn = 0f;
    public bool IsOnGround = true;

    // Use this for initialization
	void Start ()
	{
        ControlPanel.KeyPressed += OnKeyPressed;
	}

	void Update () {
	}
  
    void FixedUpdate()
    {
        LiftProcess();
        MoveProcess();
        TiltProcess();
        if (Input.GetKeyDown("u")) {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void MoveProcess()   // Turn due to tilt-x and forward due to tilt-y. RIGHT
    {
        var turn = TurnForce * hMove.x * (turnTiltForcePercent - Mathf.Abs(hMove.y)) * HelicopterModel.mass * EngineForce / maxEngineForce;
        var forw_force = hMove.y * ForwardForce * HelicopterModel.mass * EngineForce / maxEngineForce;
        HelicopterModel.AddRelativeTorque(0f, turn, 0f);
        HelicopterModel.AddRelativeForce(Vector3.forward * forw_force);

        /*
        var turn = TurnForce * Mathf.Lerp(hMove.x, hMove.x * (turnTiltForcePercent - Mathf.Abs(hMove.y)), Mathf.Max(0f, hMove.y))
        hTurn = Mathf.Lerp(hTurn, turn, Time.fixedDeltaTime * TurnForce);
        HelicopterModel.AddRelativeTorque(0f, hTurn * HelicopterModel.mass, 0f);
        HelicopterModel.AddRelativeForce(Vector3.forward * Mathf.Max(0f, hMove.y * ForwardForce * HelicopterModel.mass));
        */
    }

    private void LiftProcess()    // Upwards force depending on EffectiveHeight. RIGHT
    {
        var upForce = 1 - Mathf.Clamp(HelicopterModel.transform.position.y / EffectiveHeight, 0, 1);
        upForce = Mathf.Lerp(0f, EngineForce, upForce) * HelicopterModel.mass;
        HelicopterModel.AddRelativeForce(Vector3.up * upForce);
    }

    private void TiltProcess()
    {
        float coef_userTilt = 150.0f;
        HelicopterModel.AddRelativeTorque(coef_userTilt * new Vector3(hMove.y, 0.0f, -hMove.x) * EngineForce / maxEngineForce);
        // Debug.Log(coef_userTilt * new Vector3(hMove.y, 0.0f, -hMove.x));

        /*
        hTilt.x = Mathf.Lerp(hTilt.x, hMove.x * TurnTiltForce, Time.deltaTime);
        hTilt.y = Mathf.Lerp(hTilt.y, hMove.y * ForwardTiltForce, Time.deltaTime);
        HelicopterModel.transform.localRotation = Quaternion.Euler(hTilt.y, HelicopterModel.transform.localEulerAngles.y, -hTilt.x);
        */
    }

    private void OnKeyPressed(PressedKeyCode[] obj)
    {
        float tempY = 0;
        float tempX = 0;
        float stabilityTorqueX = 5.0f;
        float stabilityTorqueZ = 10.0f;
        float xAng, zAng;

        // Stability Torque:
        xAng = HelicopterModel.transform.localEulerAngles.x;
        if (xAng > 180) xAng -= 360;
        xAng *= -1;
        zAng = HelicopterModel.transform.localEulerAngles.z;
        if (zAng > 180) zAng -= 360;
        zAng *= -1;
        Vector3 stabilityVector = new Vector3(xAng * stabilityTorqueX, 0.0f, zAng * stabilityTorqueZ);
        // Debug.Log("x = " + xAng + "; z = " + zAng + ";");

        HelicopterModel.AddRelativeTorque(stabilityVector * EngineForce / maxEngineForce);
        
        // stable forward
        if (hMove.y > 0)
            tempY = - Time.fixedDeltaTime;
        else
            if (hMove.y < 0)
                tempY = Time.fixedDeltaTime;

        // stable lurn
        if (hMove.x > 0)
            tempX = -Time.fixedDeltaTime;
        else
            if (hMove.x < 0)
                tempX = Time.fixedDeltaTime;
        


        foreach (var pressedKeyCode in obj)
        {
            switch (pressedKeyCode)
            {
                case PressedKeyCode.SpeedUpPressed:

                    EngineForce += 0.1f;
                    if (EngineForce > maxEngineForce) EngineForce = maxEngineForce;
                    // Debug.Log(EngineForce);
                    break;
                case PressedKeyCode.SpeedDownPressed:

                    EngineForce -= 0.12f;
                    if (EngineForce < 0) EngineForce = 0;
                    // Debug.Log(EngineForce);
                    break;

                    case PressedKeyCode.ForwardPressed:

                    if (IsOnGround) break;
                    tempY = Time.fixedDeltaTime;
                    break;
                    case PressedKeyCode.BackPressed:

                    if (IsOnGround) break;
                    tempY = -Time.fixedDeltaTime;
                    break;
                    case PressedKeyCode.LeftPressed:

                    if (IsOnGround) break;
                    tempX = -Time.fixedDeltaTime;
                    break;
                    case PressedKeyCode.RightPressed:

                    if (IsOnGround) break;
                    tempX = Time.fixedDeltaTime;
                    break;
                    case PressedKeyCode.TurnRightPressed:
                    {
                        if (IsOnGround) break;
                        var force = (turnForcePercent - Mathf.Abs(hMove.y))*HelicopterModel.mass * EngineForce / maxEngineForce;
                        HelicopterModel.AddRelativeTorque(0f, force, 0);
                    }
                    break;
                    case PressedKeyCode.TurnLeftPressed:
                    {
                        if (IsOnGround) break;
                        
                        var force = -(turnForcePercent - Mathf.Abs(hMove.y))*HelicopterModel.mass * EngineForce / maxEngineForce;
                        HelicopterModel.AddRelativeTorque(0f, force, 0);
                    }
                    break;

                    case PressedKeyCode.InteractPressed:
                    {
                        if (victimBoundary.playerNearby) {
                            Debug.Log("Rescued!!!");
                            Destroy(victim);
                        }
                    }
                    break;

            }
        }

        hMove.x += tempX;
        hMove.x = Mathf.Clamp(hMove.x, -1, 1);

        hMove.y += tempY;
        hMove.y = Mathf.Clamp(hMove.y, -1, 1);

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