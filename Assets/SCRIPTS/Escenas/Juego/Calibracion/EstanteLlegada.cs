using UnityEngine;
using System.Collections;

public class EstanteLlegada : ManejoPallets
{

	public GameObject Mano;
	public ContrCalibracion ContrCalib;
	
	//-----------------------------------------------//

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	//--------------------------------------------------//
	
	public override bool Recibir(Pallet p)
	{
		if(p.Portador == Mano)
		{
			if(p.CintaReceptora == gameObject)
			{
				//apaga el indicador de donde meter la bolsa
				//Controlador.LlegadaPallet(p);
				p.Portador = this.gameObject;
				base.Recibir(p);
				//p.transform.position = transform.position;
				//p.GetComponent<Pallet>().enabled = false;
				ContrCalib.FinTutorial();
				
				return true;
			}
		}
		return false;
	}
}
