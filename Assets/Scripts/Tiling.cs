using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    public int offsetX = 2;     // the offset so we dont get any wierd errors

    // These are used for checking if we need to instantiate stuff.
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    public bool reverseScale = false;       //used if the object is not tilable

    private float spriteWidth = 0f;         //the width of our element
    private Camera cam;
    private Transform myTransform;

    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start () {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
        //does it still need buddies? if not do nothing
		if (hasALeftBuddy == false || hasARightBuddy == false)
        {
            //Calculate the cameras extend (half the width) of what the cammera can see in world coordinates
            float camHoizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            //calculate the x position where the camera can see the edge of the sprite (element)
            float edgeVisablePositionRight = (myTransform.position.x + spriteWidth / 2) - camHoizontalExtend;
            float edgeVisablePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHoizontalExtend;

            // checking if we can see the edge of the element and then calling makeNewBuddy if we can
            if (cam.transform.position.x >= edgeVisablePositionRight - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisablePositionLeft + offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
	}

    // A function that creates a buddy on the side required
    void MakeNewBuddy (int rightOrLeft)
    {
        //calculating the new position for our new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z );
        //Instantiating our new buddy and storing him in a new variable
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        //if not tilable let's reverse the size of our object to get rid of ugly seans
        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0 )
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
