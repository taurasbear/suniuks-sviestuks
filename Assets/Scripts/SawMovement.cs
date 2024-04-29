using UnityEngine;

public class SawMovement : MonoBehaviour
{
    //////////////////////////////////////////////////// SAW-ENEMY BEHAVIOUR /////////////////////////////////////////////////////////////
    public float rotationSpeed = 100f;

	void Update()
	{
		// Rotate the saw blade around its z-axis
		transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
	}
}