using UnityEngine;

public class CopyMove : MonoBehaviour
{
	public Transform Target;
	
	void LateUpdate () 
	{
		transform.position = Target.position;
	}
}
