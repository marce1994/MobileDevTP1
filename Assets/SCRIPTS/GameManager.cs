using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public static GameManager Instancia;
	
	public float TiempoDeJuego = 60;
	
	public enum EstadoJuego{Calibrando, Jugando, Finalizado}
	public EstadoJuego EstAct = EstadoJuego.Calibrando;
	
	public PlayerInfo PlayerInfo1 = null;
	public PlayerInfo PlayerInfo2 = null;
	
	public Player Player1;
	public Player Player2;
	
	public Transform Esqueleto1;
	public Transform Esqueleto2;
	public Vector3[] PosEsqsCarrera;
	
	bool PosSeteada = false;
	
	bool ConteoRedresivo = true;
	public Rect ConteoPosEsc;
	public float ConteoParaInicion = 3;
	public GUISkin GS_ConteoInicio;
	
	public Rect TiempoGUI = new Rect();
	public GUISkin GS_TiempoGUI;
	Rect R = new Rect();
	
	public float TiempEspMuestraPts = 3;
	
	public Vector3[]PosCamionesCarrera = new Vector3[2];
	public Vector3 PosCamion1Tuto = Vector3.zero;
	public Vector3 PosCamion2Tuto = Vector3.zero;
	
	public GameObject[] ObjsCalibracion1;
	public GameObject[] ObjsCalibracion2;

	public GameObject[] ObjsTuto1;
	public GameObject[] ObjsTuto2;
	//la pista de carreras
	public GameObject[] ObjsCarrera;
	IList<int> users;
	
	void Awake()
	{
		GameManager.Instancia = this;
	}
	
	void Start()
	{
		IniciarCalibracion();
	}
	
	void Update()
	{
		//REINICIAR
		if(Input.GetKey(KeyCode.Mouse1) &&
		   Input.GetKey(KeyCode.Keypad0))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		
		//CIERRA LA APLICACION
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		
		
		switch (EstAct)
		{
		case EstadoJuego.Calibrando:
			
			if(Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Keypad0))
			{
				if(PlayerInfo1 != null && PlayerInfo2 != null)
				{
					FinCalibracion(0);
					FinCalibracion(1);
					
					FinTutorial(0);
					FinTutorial(1);
				}
			}

                if (PlayerInfo1.PJ == null && InputManager.Instance.GetAxis("Vertical1") > 0.5f) {
                    PlayerInfo1 = new PlayerInfo(0, Player1);
                    PlayerInfo1.LadoAct = Visualizacion.Lado.Izq;
                    SetPosicion(PlayerInfo1);
                }

                if (PlayerInfo2.PJ == null && InputManager.Instance.GetAxis("Vertical2") > 0.5f) {
                    PlayerInfo2 = new PlayerInfo(1, Player2);
                    PlayerInfo2.LadoAct = Visualizacion.Lado.Der;
                    SetPosicion(PlayerInfo2);
                }
			
			if(PlayerInfo1.PJ != null && PlayerInfo2.PJ != null)
			{
				if(PlayerInfo1.FinTuto2 && PlayerInfo2.FinTuto2)
				{
					EmpezarCarrera();
				}
			}
			
			break;
			
			
		case EstadoJuego.Jugando:
			
			//SKIP LA CARRERA
			if(Input.GetKey(KeyCode.Mouse1) && 
			   Input.GetKey(KeyCode.Keypad0))
			{
				TiempoDeJuego = 0;
			}
			
			if(TiempoDeJuego <= 0)
			{
				FinalizarCarrera();
			}
			if(ConteoRedresivo)
			{
				ConteoParaInicion -= T.GetDT();
				if(ConteoParaInicion < 0)
				{
					EmpezarCarrera();
					ConteoRedresivo = false;
				}
			}
			else
			{
				//baja el tiempo del juego
				TiempoDeJuego -= T.GetDT();
			}
			
			break;
			
			
		case EstadoJuego.Finalizado:
			TiempEspMuestraPts -= Time.deltaTime;
			if(TiempEspMuestraPts <= 0)
				Application.LoadLevel(Application.loadedLevel +1);				
			
			break;		
		}
	}

    void OnGUI()
    {
        switch (EstAct)
        {
            case EstadoJuego.Jugando:
                if (ConteoRedresivo)
                {
                    GUI.skin = GS_ConteoInicio;

                    R.x = ConteoPosEsc.x * Screen.width / 100;
                    R.y = ConteoPosEsc.y * Screen.height / 100;
                    R.width = ConteoPosEsc.width * Screen.width / 100;
                    R.height = ConteoPosEsc.height * Screen.height / 100;

                    if (ConteoParaInicion > 1)
                    {
                        GUI.Box(R, ConteoParaInicion.ToString("0"));
                    }
                    else
                    {
                        GUI.Box(R, "GO");
                    }
                }

                GUI.skin = GS_TiempoGUI;
                R.x = TiempoGUI.x * Screen.width / 100;
                R.y = TiempoGUI.y * Screen.height / 100;
                R.width = TiempoGUI.width * Screen.width / 100;
                R.height = TiempoGUI.height * Screen.height / 100;
                GUI.Box(R, TiempoDeJuego.ToString("00"));
                break;
        }

        GUI.skin = null;
    }

    public void IniciarCalibracion()
	{
		for(int i = 0; i < ObjsCalibracion1.Length; i++)
		{
			ObjsCalibracion1[i].SetActiveRecursively(true);
			ObjsCalibracion2[i].SetActiveRecursively(true);
		}
		
		for(int i = 0; i < ObjsTuto2.Length; i++)
		{
			ObjsTuto2[i].SetActiveRecursively(false);
			ObjsTuto1[i].SetActiveRecursively(false);
		}
		
		for(int i = 0; i < ObjsCarrera.Length; i++)
		{
			ObjsCarrera[i].SetActiveRecursively(false);
		}
		
		
		Player1.CambiarACalibracion();
		Player2.CambiarACalibracion();
	}

	void CambiarATutorial()
	{
		PlayerInfo1.FinCalibrado = true;
			
		for(int i = 0; i < ObjsTuto1.Length; i++)
		{
			ObjsTuto1[i].SetActiveRecursively(true);
		}
		
		for(int i = 0; i < ObjsCalibracion1.Length; i++)
		{
			ObjsCalibracion1[i].SetActiveRecursively(false);
		}
		Player1.GetComponent<Frenado>().Frenar();
		Player1.CambiarATutorial();
		Player1.gameObject.transform.position = PosCamion1Tuto;//posiciona el camion
		Player1.transform.forward = Vector3.forward;
			
			
		PlayerInfo2.FinCalibrado = true;
			
		for(int i = 0; i < ObjsCalibracion2.Length; i++)
		{
			ObjsCalibracion2[i].SetActiveRecursively(false);
		}
		
		for(int i = 0; i < ObjsTuto2.Length; i++)
		{
			ObjsTuto2[i].SetActiveRecursively(true);
		}
		Player2.GetComponent<Frenado>().Frenar();
		Player2.gameObject.transform.position = PosCamion2Tuto;
		Player2.CambiarATutorial();
		Player2.transform.forward = Vector3 .forward;
	}
	
	void EmpezarCarrera()
	{
		Player1.GetComponent<Frenado>().RestaurarVel();
		Player1.GetComponent<ControlDireccion>().Habilitado = true;
			
		Player2.GetComponent<Frenado>().RestaurarVel();
		Player2.GetComponent<ControlDireccion>().Habilitado = true;
	}
	
	void FinalizarCarrera()
	{		
		EstAct = GameManager.EstadoJuego.Finalizado;
		
		TiempoDeJuego = 0;
		
		if(Player1.Dinero > Player2.Dinero)
		{
			//lado que gano
			if(PlayerInfo1.LadoAct == Visualizacion.Lado.Der)
				DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
			else
				DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
			
			//puntajes
			DatosPartida.PtsGanador = Player1.Dinero;
			DatosPartida.PtsPerdedor = Player2.Dinero;
		}
		else
		{
			//lado que gano
			if(PlayerInfo2.LadoAct == Visualizacion.Lado.Der)
				DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
			else
				DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
			
			//puntajes
			DatosPartida.PtsGanador = Player2.Dinero;
			DatosPartida.PtsPerdedor = Player1.Dinero;
		}
		
		Player1.GetComponent<Frenado>().Frenar();
		Player2.GetComponent<Frenado>().Frenar();
		
		Player1.ContrDesc.FinDelJuego();
		Player2.ContrDesc.FinDelJuego();
	}
	
	void SetPosicion(PlayerInfo pjInf)
	{	
		pjInf.PJ.GetComponent<Visualizacion>().SetLado(pjInf.LadoAct);
		pjInf.PJ.ContrCalib.IniciarTesteo();
		PosSeteada = true;
		
		
		if(pjInf.PJ == Player1)
		{
			if(pjInf.LadoAct == Visualizacion.Lado.Izq)
				Player2.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Der);
			else
				Player2.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Izq);
		}
		else
		{
			if(pjInf.LadoAct == Visualizacion.Lado.Izq)
				Player1.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Der);
			else
				Player1.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Izq);
		}
		
	}
	
	void CambiarACarrera()
	{
		Esqueleto1.transform.position = PosEsqsCarrera[0];
		Esqueleto2.transform.position = PosEsqsCarrera[1];
		
		for(int i = 0; i < ObjsCarrera.Length; i++)
		{
			ObjsCarrera[i].SetActiveRecursively(true);
		}

		//desactivacion de la calibracion
		PlayerInfo1.FinCalibrado = true;
			
		for(int i = 0; i < ObjsTuto1.Length; i++)
		{
			ObjsTuto1[i].SetActiveRecursively(true);
		}
		
		for(int i = 0; i < ObjsCalibracion1.Length; i++)
		{
			ObjsCalibracion1[i].SetActiveRecursively(false);
		}
		
		PlayerInfo2.FinCalibrado = true;
			
		for(int i = 0; i < ObjsCalibracion2.Length; i++)
		{
			ObjsCalibracion2[i].SetActiveRecursively(false);
		}
		
		for(int i = 0; i < ObjsTuto2.Length; i++)
		{
			ObjsTuto2[i].SetActiveRecursively(true);
		}
		
		
		
		
		//posiciona los camiones dependiendo de que lado de la pantalla esten
		if(PlayerInfo1.LadoAct == Visualizacion.Lado.Izq)
		{
			Player1.gameObject.transform.position = PosCamionesCarrera[0];
			Player2.gameObject.transform.position = PosCamionesCarrera[1];
		}
		else
		{
			Player1.gameObject.transform.position = PosCamionesCarrera[1];
			Player2.gameObject.transform.position = PosCamionesCarrera[0];
		}
		
		Player1.transform.forward = Vector3 .forward;
		Player1.GetComponent<Frenado>().Frenar();
		Player1.CambiarAConduccion();
			
		Player2.transform.forward = Vector3 .forward;
		Player2.GetComponent<Frenado>().Frenar();
		Player2.CambiarAConduccion();
		
		//los deja andando
		Player1.GetComponent<Frenado>().RestaurarVel();
		Player2.GetComponent<Frenado>().RestaurarVel();
		//cancela la direccion
		Player1.GetComponent<ControlDireccion>().Habilitado = false;
		Player2.GetComponent<ControlDireccion>().Habilitado = false;
		//les de direccion
		Player1.transform.forward = Vector3.forward;
		Player2.transform.forward = Vector3.forward;
		
		EstAct = GameManager.EstadoJuego.Jugando;
	}
	
	public void FinTutorial(int playerID)
	{
		if(playerID == 0)
		{
			PlayerInfo1.FinTuto2 = true;
			
		}else if(playerID == 1)
		{
			PlayerInfo2.FinTuto2 = true;
		}
		
		if(PlayerInfo1.FinTuto2 && PlayerInfo2.FinTuto2)
		{
			CambiarACarrera();
		}
	}
	
	public void FinCalibracion(int playerID)
	{
		if(playerID == 0)
		{
			PlayerInfo1.FinTuto1 = true;
			
		}else if(playerID == 1)
		{
			PlayerInfo2.FinTuto1 = true;
		}
		
		if(PlayerInfo1.PJ != null && PlayerInfo2.PJ != null)
			if(PlayerInfo1.FinTuto1 && PlayerInfo2.FinTuto1)
				CambiarACarrera();//CambiarATutorial();
		
	}
	
	
	
	
	[System.Serializable]
	public class PlayerInfo
	{
		public PlayerInfo(int tipoDeInput, Player pj)
		{
            TipoDeInput = tipoDeInput;
			PJ = pj;
		}
		
		public bool FinCalibrado = false;
		public bool FinTuto1 = false;
		public bool FinTuto2 = false;
		
		public Visualizacion.Lado LadoAct;

        public int TipoDeInput = -1;
		
		public Player PJ;
	}
	
}
