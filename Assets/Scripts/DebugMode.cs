using Jumpy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMode : MonoBehaviour
{
    public List<string> InputBuffer;
    private List<GameObject> textInputs = new List<GameObject>();

    public GameObject TextPrefab;

    public Transform KeysPanel;
    public CanvasGroup canvas;
    public Toggle BunnyHop;
    public Slider MouseControl;

    public GameObject player;
    private int _maxLimit = 3;
    private JumpyController jumpyController;
    private PowerJump jumpyCannonBall;

    public Slider accel;
    public Slider deaccel;
    public Text currentAccel;
    public Text currentDeAccel;

    private void Start()
    {
        if (!player)
            player = GameObject.Find("Player");

        if (player != null)
        {
            jumpyController = player.GetComponent<JumpyController>();
            jumpyCannonBall = player.GetComponent<PowerJump>();
        }

        if (jumpyController != null)
        {
            //jumpyController.EnableBunnyHop = BunnyHop.isOn;

            //float parseAccel = jumpyController.HorizontalAcceleration / 100;
            //accel.maxValue = 1.5f;
            //accel.value = parseAccel;

            //float parseDeAccel = jumpyController.HorizontalDecceleration / 100;
            //deaccel.maxValue = 1.5f;
            //deaccel.value = parseDeAccel;
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            string key = e.keyCode.ToString();

            if (key != "None")
            {
                InputBuffer.Add(key);
                AddKeyText(key);

                if (InputBuffer.Count > _maxLimit)
                {
                    InputBuffer.RemoveAt(0);
                    Destroy(textInputs[0]);
                    textInputs.RemoveAt(0);
                }
            }
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            canvas.alpha = canvas.alpha != 0 ? 0 : 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (jumpyController != null)
        {
            //jumpyController.EnableBunnyHop = BunnyHop.isOn;

            //float accelValue = (accel.value * 100);
            //float deAccelValue = (deaccel.value * 100);

            //currentAccel.text = accelValue.ToString();
            //currentDeAccel.text = deAccelValue.ToString();

            //jumpyController.HorizontalAcceleration = accelValue;
            //jumpyController.HorizontalDecceleration = deAccelValue;
        }

        if (jumpyCannonBall != null)
            jumpyCannonBall.MouseControl = MouseControl.value == 0;


    }

    public void AddKeyText(string keyString)
    {
        GameObject go = Instantiate(TextPrefab, KeysPanel.position, Quaternion.identity, KeysPanel);
        go.GetComponent<Text>().text = keyString + " Pressed ";
        textInputs.Add(go);
    }

 

}
