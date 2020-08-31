using UnityEngine;
using UnityEngine.UI;

public class FadeInicioFinal : MonoBehaviour 
{
	public float Duracion = 2;
	public float Vel = 2;
	float TiempInicial;
	
	MngPts Mng;
	
	Color aux;
	
	bool MngAvisado = false;

	// Use this for initialization
	void Start ()
	{
		//renderer.material = IniFin;
		Mng = (MngPts)GameObject.FindObjectOfType(typeof (MngPts));
		TiempInicial = Mng.TiempEspReiniciar;
		
		aux = GetComponent<RawImage>().color;
		aux.a = 0;
		GetComponent<RawImage>().color = aux;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(Mng.TiempEspReiniciar > TiempInicial - Duracion)//aparicion
		{
			aux = GetComponent<RawImage>().color;
			aux.a += Time.deltaTime / Duracion;
			GetComponent<RawImage>().color = aux;			
		}
		else if(Mng.TiempEspReiniciar < Duracion)//desaparicion
		{
			aux = GetComponent<RawImage>().color;
			aux.a -= Time.deltaTime / Duracion;
			GetComponent<RawImage>().color = aux;
			
			if(!MngAvisado)
			{
				MngAvisado = true;
				Mng.DesaparecerGUI();
			}
		}
				
	}
}
