using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{

	//Speed
	Rigidbody _Rb;
	public float _basicPower = 10f;

	private int _bounce = 0;

	public GameObject explosionPrefab;

	// Start is called before the first frame update
	void Start()
    {
		_Rb = GetComponent<Rigidbody>();	
		_Rb.AddForce(transform.forward * _basicPower, ForceMode.VelocityChange);
		StartCoroutine(Explosion());
	}

    // Update is called once per frame
    void Update()
    {
		
	}

	//Explosion after some time
	public IEnumerator Explosion()
	{
		yield return new WaitForSeconds(3);
		Explode();
	}

	//Check bounce and enemy
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			//Explode
			Explode();
		}

		if (collision.gameObject.tag == "Floor")
		{
			//Check for bounce
			Bounce();
		}		
	}

	//Explode granade
	public void Explode()
	{
		//instanciar una explosion
		print("Explosion");
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	//Check the bounces and explode
	public void Bounce()
	{
		if (_bounce != 2)
		{
			_bounce ++;
		}
		else if (_bounce == 2)
		{
			Explode();
		}
	}
}
