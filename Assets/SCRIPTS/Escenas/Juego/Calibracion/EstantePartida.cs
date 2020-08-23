using UnityEngine;
using System.Collections;

public class EstantePartida : ManejoPallets
{
	//public Cinta CintaReceptora;//cinta que debe recibir la bolsa
	public GameObject ManoReceptora;
	//public Pallet.Valores Valor;
	
	void Update () 
	{
		//prueba
		if(Input.GetKey(KeyCode.A))
			Dar(ManoReceptora.GetComponent<ManejoPallets>());
	}
	
	void OnTriggerEnter(Collider other)
	{
		ManejoPallets recept = other.GetComponent<ManejoPallets>();
		if(recept != null)
		{
			Dar(recept);
		}
	}
	
	//------------------------------------------------------------//
	
	public virtual void Dar(ManejoPallets receptor)
	{
		switch (receptor.tag)
		{
		case "Mano":
			if(Tenencia())
			{
				if(receptor.gameObject == ManoReceptora)
				{
					if(receptor.Recibir(Pallets[0]))
					{
						//enciende la cinta y el indicador
						//cambia la textura de cuantos pallet le queda
						//CintaReceptora.Encender();
						//Controlador.SalidaPallet(Pallets[0]);
						Pallets.RemoveAt(0);
						//Debug.Log("pallet entregado a Mano de Estanteria");
					}
				}
				
			}
			break;
			
		case "Cinta":
			break;
			
		case "Estante":
			break;
		}
	}
	
	public override bool Recibir (Pallet pallet)
	{
		//pallet.CintaReceptora = CintaReceptora.gameObject;
		pallet.Portador = gameObject;
		return base.Recibir (pallet);
	}
}
