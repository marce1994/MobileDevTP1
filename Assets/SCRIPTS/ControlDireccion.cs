using UnityEngine;

public class ControlDireccion : MonoBehaviour 
{
	public string player = "";

	public Transform ManoDer;
	public Transform ManoIzq;
	
	public float MaxAng = 90;
	public float DesSencibilidad = 90;
	
	public bool Habilitado = true;

	void Update () 
	{
		float axis = InputManager.Instance.GetAxis("Horizontal" + player);
		gameObject.SendMessage("SetGiro", axis);
	}
}
