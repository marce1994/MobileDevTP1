using UnityEngine;

public class BolsaAnim : MonoBehaviour 
{
	public float GiroVel = 1;
	
	public Vector3 Amlitud = Vector3.zero;
	public float VelMov = 1;
	
	Vector3 PosIni;
	Vector3 vAuxPos = Vector3.zero;
	bool Subiendo = true;
	Vector3 vAuxGir = Vector3.zero;
	
	public bool Giro = true;
	public bool MovVert = true;
	
	float TiempInicio;
	bool Iniciado = false;
	
	void Start ()
	{
		PosIni = transform.position;
		
		TiempInicio = Random.Range(0, 2);
	}
	
	void Update ()
	{
		if(Iniciado)
		{
			if(Giro)
			{
				vAuxGir = Vector3.zero;
				vAuxGir.y = T.GetDT() * GiroVel;
				transform.localEulerAngles += vAuxGir;
			}
			
			if(MovVert)
			{
				if(Subiendo)
				{
					transform.localPosition += Amlitud.normalized * Time.deltaTime * VelMov;
					
					if((transform.position - PosIni).magnitude > Amlitud.magnitude / 2)
					{
						Subiendo = false;
						transform.localPosition -= Amlitud.normalized * Time.deltaTime * VelMov;
					}
						
				}
				else
				{
					transform.localPosition -= Amlitud.normalized * Time.deltaTime * VelMov;
					if((transform.position - PosIni).magnitude > Amlitud.magnitude / 2)
					{
						Subiendo = true;
						transform.localPosition += Amlitud.normalized * Time.deltaTime * VelMov;
					}
						
				}
			}
		}
		else
		{
			TiempInicio -= Time.deltaTime;
			if(TiempInicio <= 0)
				Iniciado = true;
		}
	}
}
