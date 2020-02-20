using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxing : MonoBehaviour {

    public Transform[] Backgrounds;              //Array of all the backgrounds to be paralaxed
    private float[] ParallaxScales;              //The proportion of the cameras movement to move the backgrounds by.
    public float smoothing = 1f;                 //How smooth the parralax is going to be. (set above '0')

    private Transform cam;                       //Reference to the mains cammera transform
    private Vector3 PreviousCamPos;              //The position of the camera in the previous frame

    //Is called before Start(). Great for References
    void Awake(){
        //set us the Camera Reference
        cam = Camera.main.transform;
    }


    // Use this for initialization
    void Start () {
        //The previuse frame had the current camera position
        PreviousCamPos = cam.position;

        // Assigning Corisponding Paralexscales.
        ParallaxScales = new float[Backgrounds.Length];
        for (int i = 0; i <Backgrounds.Length; i++) {
            ParallaxScales[i] = Backgrounds[i].position.z * -1;
        }

    }
	
	// Update is called once per frame
	void Update () {

		//for each background
        for (int i = 0; i < Backgrounds.Length; i++){
            // the paralax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (PreviousCamPos.x - cam.position.x) * ParallaxScales[i];

            //set a target x possition witch is the current possition plus the parallax.
            float BackgroundTargetPosX = Backgrounds[i].position.x + parallax;

            //create a target position which is the backgrounds current position with it's target x position
            Vector3 BackgroundTargetPos = new Vector3(BackgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);

            //fade between curent position and the target possition using lerp
            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, BackgroundTargetPos, smoothing * Time.deltaTime);
        }

        //set the previuseCamPos to the camera's possition at the end of the frame
        PreviousCamPos = cam.position;
	}
}
