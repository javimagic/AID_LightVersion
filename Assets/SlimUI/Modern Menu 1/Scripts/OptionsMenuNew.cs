using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenuNew : MonoBehaviour {

	// toggle buttons
    public GameObject shadowofftext;
	public GameObject shadowofftextLINE;
	public GameObject shadowlowtext;
	public GameObject shadowlowtextLINE;
	public GameObject shadowhightext;
	public GameObject shadowhightextLINE;
    public GameObject ShowMiniMapToggle;
    public Toggle windToggle;
    public GameObject tooltipstext;
    public int dificultad;
    public GameObject difficultyfaciltext;
    public GameObject difficultyfaciltextLINE;
    public GameObject difficultynormaltext;
	public GameObject difficultynormaltextLINE;
	public GameObject difficultydificiltext;
	public GameObject difficultydificiltextLINE;
    public RawImage solWeathertextLINE;
    public GameObject solWeathertext;
    public RawImage nubeWeathertextLINE;
    public GameObject nubeWeathertext;
    public GameObject SunImage;
    public GameObject MoonImage;
    public GameObject cameraeffectstext;
	public GameObject invertmousetext;
	public GameObject vsynctext;
	public GameObject motionblurtext;
	public GameObject ambientocclusiontext;
	public GameObject texturelowtext;
	public GameObject texturelowtextLINE;
	public GameObject texturemedtext;
	public GameObject texturemedtextLINE;
	public GameObject texturehightext;
	public GameObject texturehightextLINE;
    public GameObject playBtn;


	// sliders
	public GameObject windSlider;
	public GameObject sensHeliSlider;
	public GameObject sensDronLSlider;
	public GameObject sensDronRSlider;

	private float sliderValue = 0.0f;
	private float sliderValueXSensitivity = 0.0f;
	private float sliderValueYSensitivity = 0.0f;
	private float sliderValueSmoothing = 0.0f;

	public void  Start (){
        // check difficulty
        dificultad = PlayerPrefs.GetInt("Difficulty");
        PlayerPrefs.SetFloat("SensHeli", 1);
        PlayerPrefs.SetFloat("SensDronL", 1);
        PlayerPrefs.SetFloat("SensDronR", 1);


        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            difficultynormaltextLINE.gameObject.SetActive(false);
            difficultydificiltextLINE.gameObject.SetActive(false);
            difficultyfaciltextLINE.gameObject.SetActive(true);

        }
            if (PlayerPrefs.GetInt("Difficulty") == 2){
			difficultynormaltextLINE.gameObject.SetActive(true);
			difficultydificiltextLINE.gameObject.SetActive(false);
            difficultyfaciltextLINE.gameObject.SetActive(false);
        }
		else
		{
			difficultydificiltextLINE.gameObject.SetActive(true);
            difficultyfaciltextLINE.gameObject.SetActive(false);
            difficultynormaltextLINE.gameObject.SetActive(false);
		}
        if (PlayerPrefs.GetInt("Weather") == 1)
        {
            SunImage.SetActive(true);
            MoonImage.SetActive(false);
            // solWeathertextLINE.enabled = true;
            // nubeWeathertextLINE.enabled = false;

        }
        else
        {
            SunImage.SetActive(false);
            MoonImage.SetActive(true);
            // solWeathertextLINE.enabled = false;
            // nubeWeathertextLINE.enabled = true;
        }

        // check sliders values
        PlayerPrefs.SetFloat("WindForce", windSlider.GetComponent<Slider>().value);
		sensHeliSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SensHeli");
        sensDronLSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SensDronL");
        sensDronRSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SensDronR");

        PlayerPrefs.SetInt("WindEnabled", (windToggle.isOn)? 1 : 0);
        // Debug.Log("Wind is on: " + PlayerPrefs.GetInt("WindEnabled"));

        /*
        // check full screen
        if(Screen.fullScreen == true)
        {
            fullscreentext.GetComponent<Text>().text = "on";
		}
		else if(Screen.fullScreen == false){
            fullscreentext.GetComponent<Text>().text = "off";
		}
        */


        // check tool tip value
       // if (PlayerPrefs.GetInt("ToolTips") == 0)
       // {
         //   tooltipstext.GetComponent<Text>().text = "off";
       // }
        //else
        //{
         //   tooltipstext.GetComponent<Text>().text = "on";
        //}


       
        /*
        // check shadow distance/enabled
        if (PlayerPrefs.GetInt("Shadows") == 0)
        {
            QualitySettings.shadowCascades = 0;
            QualitySettings.shadowDistance = 0;
            shadowofftext.GetComponent<Text>().text = "OFF";
            shadowlowtext.GetComponent<Text>().text = "low";
            shadowhightext.GetComponent<Text>().text = "high";
            shadowofftextLINE.gameObject.SetActive(true);
            shadowlowtextLINE.gameObject.SetActive(false);
            shadowhightextLINE.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Shadows") == 1)
        {
            QualitySettings.shadowCascades = 2;
            QualitySettings.shadowDistance = 75;
            // shadowofftext.GetComponent<Text>().text = "off";
            shadowlowtext.GetComponent<Text>().text = "LOW";
            shadowhightext.GetComponent<Text>().text = "high";
            shadowofftextLINE.gameObject.SetActive(false);
            shadowlowtextLINE.gameObject.SetActive(true);
            shadowhightextLINE.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Shadows") == 2)
        {
            QualitySettings.shadowCascades = 4;
            QualitySettings.shadowDistance = 500;
            shadowofftext.GetComponent<Text>().text = "off";
            shadowlowtext.GetComponent<Text>().text = "low";
            shadowhightext.GetComponent<Text>().text = "HIGH";
            shadowofftextLINE.gameObject.SetActive(false);
            shadowlowtextLINE.gameObject.SetActive(false);
            shadowhightextLINE.gameObject.SetActive(true);
        }
        */

        /*
        // check vsync
        if (QualitySettings.vSyncCount == 0)
        {
            vsynctext.GetComponent<Text>().text = "off";
        }
        else if (QualitySettings.vSyncCount == 1)
        {
            vsynctext.GetComponent<Text>().text = "on";
        }
        */

        /*
        // check mouse inverse
        if (PlayerPrefs.GetInt("Inverted") == 0)
        {
            invertmousetext.GetComponent<Text>().text = "off";
        }
        else if (PlayerPrefs.GetInt("Inverted") == 1)
        {
            invertmousetext.GetComponent<Text>().text = "on";
        }
        */

        /*
        // check motion blur
        if (PlayerPrefs.GetInt("MotionBlur") == 0)
        {
            motionblurtext.GetComponent<Text>().text = "off";
        }
        else if (PlayerPrefs.GetInt("MotionBlur") == 1)
        {
            motionblurtext.GetComponent<Text>().text = "on";
        }
        */

        /*
        // check ambient occlusion
        if (PlayerPrefs.GetInt("AmbientOcclusion") == 0)
        {
            ambientocclusiontext.GetComponent<Text>().text = "off";
        }
        else if (PlayerPrefs.GetInt("AmbientOcclusion") == 1)
        {
            ambientocclusiontext.GetComponent<Text>().text = "on";
        }
        */

        /*
        // check texture quality
        if (PlayerPrefs.GetInt("Textures") == 0)
        {
            QualitySettings.masterTextureLimit = 2;
            texturelowtext.GetComponent<Text>().text = "LOW";
            texturemedtext.GetComponent<Text>().text = "med";
            texturehightext.GetComponent<Text>().text = "high";
            texturelowtextLINE.gameObject.SetActive(true);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Textures") == 1)
        {
            QualitySettings.masterTextureLimit = 1;
            texturelowtext.GetComponent<Text>().text = "low";
            texturemedtext.GetComponent<Text>().text = "MED";
            texturehightext.GetComponent<Text>().text = "high";
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(true);
            texturehightextLINE.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Textures") == 2)
        {
            QualitySettings.masterTextureLimit = 0;
            texturelowtext.GetComponent<Text>().text = "low";
            texturemedtext.GetComponent<Text>().text = "med";
            texturehightext.GetComponent<Text>().text = "HIGH";
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(true);
        }
        */

    }

		
	

	public void  Update (){
        // Debug.Log(PlayerPrefs.GetInt("AAA"));
        /*
        Debug.Log(
            "H_Sens = " + PlayerPrefs.GetFloat("SensHeli") +
            ". DronL_Sens = " + PlayerPrefs.GetFloat("SensDronL") +
            ". DronR_Sens = " + PlayerPrefs.GetFloat("SensDronR")
            );
        */
        // sliderValue = windSlider.GetComponent<Slider>().value;
		// sliderValueXSensitivity = sensitivityXSlider.GetComponent<Slider>().value;
		// sliderValueYSensitivity = sensitivityYSlider.GetComponent<Slider>().value;
		// sliderValueSmoothing = mouseSmoothSlider.GetComponent<Slider>().value;
    }
   

	public void windSliderChange (float value){
        PlayerPrefs.SetFloat("WindForce", windSlider.GetComponent<Slider>().value);
        Debug.Log("Wind force now set to " + PlayerPrefs.GetFloat("WindForce"));
	}

	public void  sensHeliSliderChange (){
		PlayerPrefs.SetFloat("SensHeli", sensHeliSlider.GetComponent<Slider>().value);
        // Debug.Log("SensHeli now set to " + PlayerPrefs.GetFloat("SensHeli"));
    }

    public void sensDronLSliderChange() {
        PlayerPrefs.SetFloat("SensDronL", sensDronLSlider.GetComponent<Slider>().value);
        // Debug.Log("SensDronL now set to " + PlayerPrefs.GetFloat("SensDronL"));
    }

    public void sensDronRSliderChange() {
        PlayerPrefs.SetFloat("SensDronR", sensDronRSlider.GetComponent<Slider>().value);
        // Debug.Log("SensDronR now set to " + PlayerPrefs.GetFloat("SensDronR"));
    }


    public void  SensitivitySmoothing (){
		PlayerPrefs.SetFloat("MouseSmoothing", sliderValueSmoothing);
		Debug.Log(PlayerPrefs.GetFloat("MouseSmoothing"));
	}

	// the playerprefs variable that is checked to enable hud while in game
	public void  showMiniMap (bool value){
		if ( value ){
			PlayerPrefs.SetInt("ShowMiniMap", 1);
		}
		else {
			PlayerPrefs.SetInt("ShowMiniMap", 0);
      
		}
	}

    public void enableWind (bool value)
    {
        Debug.Log(PlayerPrefs.GetFloat("WindForce"));
        if (value)
        {
            PlayerPrefs.SetInt("WindEnabled", 1);
        }
        else
        {
            PlayerPrefs.SetInt("WindEnabled", 0);

        }
    }

    // show tool tips like: 'How to Play' control pop ups
    public void  ToolTips (){
		if(PlayerPrefs.GetInt("ToolTips")==0){
			PlayerPrefs.SetInt("ToolTips",1);
		//tooltipstext.GetComponent<Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("ToolTips")==1){
			PlayerPrefs.SetInt("ToolTips",0);
			//tooltipstext.GetComponent<Text>().text = "off";
		}
	}

    public void facilDifficulty()
    {
     
        difficultydificiltextLINE.gameObject.SetActive(false);
        difficultynormaltextLINE.gameObject.SetActive(false);
        difficultyfaciltextLINE.gameObject.SetActive(true);
        PlayerPrefs.SetInt("Difficulty", 1);
        dificultad = 1;
       
    }
    public void  normalDifficulty (){

		difficultydificiltextLINE.gameObject.SetActive(false);
        difficultyfaciltextLINE.gameObject.SetActive(false);
        difficultynormaltextLINE.gameObject.SetActive(true);
        PlayerPrefs.SetInt("Difficulty", 2);
        dificultad = 2;
    }

	public void  dificilDifficulty (){
	
		difficultydificiltextLINE.gameObject.SetActive(true);
		difficultynormaltextLINE.gameObject.SetActive(false);
        difficultyfaciltextLINE.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Difficulty", 3);
        dificultad = 3;
    }
    public void solWeather()
    {

        // solWeathertextLINE.enabled = true;
        // nubeWeathertextLINE.enabled = false;
        SunImage.SetActive(true);
        MoonImage.SetActive(false);
        PlayerPrefs.SetInt("Weather", 1);
    }
    public void nubeWeather()
    {

        // solWeathertextLINE.gameObject.SetActive(false);
        // solWeathertextLINE.enabled = false;
        // nubeWeathertextLINE.enabled = true;
        SunImage.SetActive(false);
        MoonImage.SetActive(true);
        PlayerPrefs.SetInt("Weather", 2);
    }
   


    public void  ShadowsOff (){
		PlayerPrefs.SetInt("Shadows",0);
		QualitySettings.shadowCascades = 0;
		QualitySettings.shadowDistance = 0;
		shadowofftext.GetComponent<Text>().text = "OFF";
		shadowlowtext.GetComponent<Text>().text = "low";
		shadowhightext.GetComponent<Text>().text = "high";
		shadowofftextLINE.gameObject.SetActive(true);
		shadowlowtextLINE.gameObject.SetActive(false);
		shadowhightextLINE.gameObject.SetActive(false);
	}

	public void  ShadowsLow (){
		PlayerPrefs.SetInt("Shadows",1);
		QualitySettings.shadowCascades = 2;
		QualitySettings.shadowDistance = 75;
		shadowofftext.GetComponent<Text>().text = "off";
		shadowlowtext.GetComponent<Text>().text = "LOW";
		shadowhightext.GetComponent<Text>().text = "high";
		shadowofftextLINE.gameObject.SetActive(false);
		shadowlowtextLINE.gameObject.SetActive(true);
		shadowhightextLINE.gameObject.SetActive(false);
	}

	public void  ShadowsHigh (){
		PlayerPrefs.SetInt("Shadows",2);
		QualitySettings.shadowCascades = 4;
		QualitySettings.shadowDistance = 500;
		shadowofftext.GetComponent<Text>().text = "off";
		shadowlowtext.GetComponent<Text>().text = "low";
		shadowhightext.GetComponent<Text>().text = "HIGH";
		shadowofftextLINE.gameObject.SetActive(false);
		shadowlowtextLINE.gameObject.SetActive(false);
		shadowhightextLINE.gameObject.SetActive(true);
	}

	public void  vsync (){
		if(QualitySettings.vSyncCount == 0){
			QualitySettings.vSyncCount = 1;
			vsynctext.GetComponent<Text>().text = "on";
		}
		else if(QualitySettings.vSyncCount == 1){
			QualitySettings.vSyncCount = 0;
			vsynctext.GetComponent<Text>().text = "off";
		}
	}

	public void  InvertMouse (){
		if(PlayerPrefs.GetInt("Inverted")==0){
			PlayerPrefs.SetInt("Inverted",1);
			invertmousetext.GetComponent<Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("Inverted")==1){
			PlayerPrefs.SetInt("Inverted",0);
			invertmousetext.GetComponent<Text>().text = "off";
		}
	}

	public void  MotionBlur (){
		if(PlayerPrefs.GetInt("MotionBlur")==0){
			PlayerPrefs.SetInt("MotionBlur",1);
			motionblurtext.GetComponent<Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("MotionBlur")==1){
			PlayerPrefs.SetInt("MotionBlur",0);
			motionblurtext.GetComponent<Text>().text = "off";
		}
	}

	public void  AmbientOcclusion (){
		if(PlayerPrefs.GetInt("AmbientOcclusion")==0){
			PlayerPrefs.SetInt("AmbientOcclusion",1);
			ambientocclusiontext.GetComponent<Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("AmbientOcclusion")==1){
			PlayerPrefs.SetInt("AmbientOcclusion",0);
			ambientocclusiontext.GetComponent<Text>().text = "off";
		}
	}

	public void  CameraEffects (){
		if(PlayerPrefs.GetInt("CameraEffects")==0){
			PlayerPrefs.SetInt("CameraEffects",1);
			cameraeffectstext.GetComponent<Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("CameraEffects")==1){
			PlayerPrefs.SetInt("CameraEffects",0);
			cameraeffectstext.GetComponent<Text>().text = "off";
		}
	}

	public void  TexturesLow (){
		PlayerPrefs.SetInt("Textures",0);
		QualitySettings.masterTextureLimit = 2;
		texturelowtext.GetComponent<Text>().text = "LOW";
		texturemedtext.GetComponent<Text>().text = "med";
		texturehightext.GetComponent<Text>().text = "high";
		texturelowtextLINE.gameObject.SetActive(true);
		texturemedtextLINE.gameObject.SetActive(false);
		texturehightextLINE.gameObject.SetActive(false);
	}

	public void  TexturesMed (){
		PlayerPrefs.SetInt("Textures",1);
		QualitySettings.masterTextureLimit = 1;
		texturelowtext.GetComponent<Text>().text = "low";
		texturemedtext.GetComponent<Text>().text = "MED";
		texturehightext.GetComponent<Text>().text = "high";
		texturelowtextLINE.gameObject.SetActive(false);
		texturemedtextLINE.gameObject.SetActive(true);
		texturehightextLINE.gameObject.SetActive(false);
	}

	public void  TexturesHigh (){
		PlayerPrefs.SetInt("Textures",2);
		QualitySettings.masterTextureLimit = 0;
		texturelowtext.GetComponent<Text>().text = "low";
		texturemedtext.GetComponent<Text>().text = "med";
		texturehightext.GetComponent<Text>().text = "HIGH";
		texturelowtextLINE.gameObject.SetActive(false);
		texturemedtextLINE.gameObject.SetActive(false);
		texturehightextLINE.gameObject.SetActive(true);
	}
}