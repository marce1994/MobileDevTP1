using UnityEngine;

public class Respawn : MonoBehaviour 
{
	CheakPoint CheckPointAct;
	CheakPoint CechPointAnt;
	
	public float MaxAngle = 90;//angulo maximo antes del cual se reinicia el camion
	int VerifPorCuadro = 20;
	int Counter = 0;
	
	public float RangMinDer = 0;
	public float RangMaxDer = 0;
	
	bool IgnorandoColision = false;
	public float TiempDeNoColision = 2;
	float Tempo = 0;

	private new Rigidbody rigidbody;

	void Start () 
	{
		rigidbody = GetComponent<Rigidbody>();

		//restaura las colisiones
		Physics.IgnoreLayerCollision(8,9,false);
	}
	
	void Update ()
	{
		if(CheckPointAct != null)
		{
			Counter++;
			if(Counter == VerifPorCuadro)
			{
				Counter = 0;
				if(MaxAngle < Quaternion.Angle(transform.rotation, CheckPointAct.transform.rotation))
				{
					Respawnear();
				}
			}
		}
		
		if(IgnorandoColision)
		{
			Tempo += T.GetDT();
			if(Tempo > TiempDeNoColision)
			{
				IgnorarColision(false);
			}
		}
		
	}

	public void Respawnear()
	{
		rigidbody.Sleep();
		
		gameObject.SendMessage("SetGiro", 0f);
		
		if(CheckPointAct != null && CheckPointAct.Habilitado())
		{
			if(GetComponent<Visualizacion>().LadoAct == Visualizacion.Lado.Der)
				transform.position = CheckPointAct.transform.position + CheckPointAct.transform.right * Random.Range(RangMinDer, RangMaxDer);
			else 
				transform.position = CheckPointAct.transform.position + CheckPointAct.transform.right * Random.Range(RangMinDer * (-1), RangMaxDer * (-1));
			transform.forward = CheckPointAct.transform.forward;
		}
		else if(CechPointAnt != null)
		{
			if(GetComponent<Visualizacion>().LadoAct == Visualizacion.Lado.Der)
				transform.position = CechPointAnt.transform.position + CechPointAnt.transform.right * Random.Range(RangMinDer, RangMaxDer);
			else
				transform.position = CechPointAnt.transform.position + CechPointAnt.transform.right * Random.Range(RangMinDer * (-1), RangMaxDer * (-1));
			transform.forward = CechPointAnt.transform.forward;
		}
		
		IgnorarColision(true);
		
		//animacion de resp
		
	}
	
	public void Respawnear(Vector3 pos)
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		

		gameObject.SendMessage("SetGiro", 0f);
		
		transform.position = pos;
		
		IgnorarColision(true);
	}
	
	public void Respawnear(Vector3 pos, Vector3 dir)
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		
		gameObject.SendMessage("SetGiro", 0f);
		
		transform.position = pos;
		transform.forward = dir;
		
		IgnorarColision(true);
	}
	
	public void AgregarCP(CheakPoint cp)
	{
		if(cp != CheckPointAct)
		{
			CechPointAnt = CheckPointAct;
			CheckPointAct = cp;
		}
	}
	
	void IgnorarColision(bool b)
	{
		Physics.IgnoreLayerCollision(8,9,b);
		IgnorandoColision = b;	
		Tempo = 0;
	}
}
