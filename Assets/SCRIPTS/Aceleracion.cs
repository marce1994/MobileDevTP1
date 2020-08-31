using UnityEngine;

public class Aceleracion : MonoBehaviour 
{
	public Transform ManoDer;
	public Transform ManoIzq;
	
	public float AlturaMedia = 0;

	public float SensAcel = 1;
	public float SensFren = 1;
	
	public Transform Camion;
	
	//pedales
	public Transform PedalAcel;
	Vector3 PAclPosIni;
	public Transform PedalFren;
	Vector3 PFrnPosIni;
	public float SensivPed = 1;
	
	
	float DifIzq;
	float DifDer;
	
	float Frenado;
	float Acelerado;
	
	void Start () 
	{
		PAclPosIni = PedalAcel.localPosition;
		PFrnPosIni = PedalFren.localPosition;
	}
	
	void Update () 
	{
		DifDer = ManoDer.position.y - AlturaMedia;
		DifIzq = ManoIzq.position.y - AlturaMedia;
		if(DifDer > 0)
		{
			Acelerado = DifDer * SensAcel * Time.deltaTime;
			
			Camion.position += Acelerado * Camion.forward;
			
			PedalAcel.localPosition = PAclPosIni - PedalAcel.forward * SensivPed * Acelerado;
		}
		if(DifIzq > 0)
		{
			Frenado = DifIzq * SensFren * Time.deltaTime;
			
			Camion.position -= Frenado * Camion.forward;
			
			PedalFren.localPosition = PFrnPosIni - PedalFren.forward * SensivPed * Frenado;
		}
	}
}
