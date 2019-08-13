using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		LookCamera();
	}

	//Look at camera
	public void LookCamera()
	{
		transform.LookAt(new Vector3(transform.position.x, Camera.main.transform.position.y -40, transform.position.z));
	}
}
