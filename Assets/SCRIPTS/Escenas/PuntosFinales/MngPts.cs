using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MngPts : MonoBehaviour
{
    public float TiempEmpAnims = 2.5f;
    float Tempo = 0;

    public Text player1Money;
    public Text player2Money;
    public Text winner;

    public GameObject Fondo;

    public float TiempEspReiniciar = 10;

    public float TiempParpadeo = 0.7f;
    float TempoParpadeo = 1;
    bool PrimerImaParp = true;

    public bool ActivadoAnims = false;

    Visualizacion Viz = new Visualizacion();

    // Use this for initialization
    void Start()
    {
        SetGanador();
    }

    // Update is called once per frame
    void Update()
    {
        //PARA JUGAR
        if (Input.GetKeyDown(KeyCode.KeypadEnter) ||
           Input.GetKeyDown(KeyCode.Return) ||
           Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene(0);
        }

        //REINICIAR
        if (Input.GetKeyDown(KeyCode.Mouse1) ||
           Input.GetKeyDown(KeyCode.Keypad0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //CALIBRACION DEL KINECT
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(3);
        }


        TiempEspReiniciar -= Time.deltaTime;
        if (TiempEspReiniciar <= 0)
        {
            SceneManager.LoadScene(0);
        }

        if (ActivadoAnims)
        {
            TempoParpadeo += Time.deltaTime;

            if (TempoParpadeo >= TiempParpadeo)
            {
                TempoParpadeo = 0;

                if (PrimerImaParp)
                    PrimerImaParp = false;
                else
                {
                    TempoParpadeo += 0.1f;
                    PrimerImaParp = true;
                }
            }
        }

        if (!ActivadoAnims)
        {
            Tempo += Time.deltaTime;
            if (Tempo >= TiempEmpAnims)
            {
                Tempo = 0;
                ActivadoAnims = true;
            }
        }

        if (ActivadoAnims)
        {
            SetDinero();
            SetCartelGanador();
        }
    }

    void SetGanador()
    {
        switch (DatosPartida.LadoGanadaor)
        {
            case DatosPartida.Lados.Der:
                winner.text = "PLAYER #2 IS THE WINNER";
                break;
            case DatosPartida.Lados.Izq:
                winner.text = "PLAYER #1 IS THE WINNER";
                break;
        }
    }

    void SetDinero()
    {
        if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)//izquierda
        {
            if (!PrimerImaParp)//para que parpadee
                player1Money.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador);
        }
        else
        {
            player1Money.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor);
        }

        if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Der)//derecha
        {
            if (!PrimerImaParp)//para que parpadee
                player2Money.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador);
        }
        else
        {
            player2Money.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor);
        }

    }

    void SetCartelGanador()
    {
        //winner.text = "";
    }

    public void DesaparecerGUI()
    {
        ActivadoAnims = false;
        Tempo = -100;
    }
}
