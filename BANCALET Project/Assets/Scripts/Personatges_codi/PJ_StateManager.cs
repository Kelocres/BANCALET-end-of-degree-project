using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using UnityEngine.AI;

//public class DetectiuStateManager : MonoBehaviour
public class PJ_StateManager : MonoBehaviour
{
    //Per al State Machine
    private IStateJugador stateActual;

    public ExplorarState explorarState  = new ExplorarState();
    public XarrarState xarrarState      = new XarrarState();
    public CinematicaState cineState    = new CinematicaState();

    public Vector3 inputMoure;
    public float velocitat = 5f;
    public Animator animacion; //És public per a ser accecible pels States
    public NavMeshAgent agentNavegador;
    private ColisionadorCamera_Script colcam;

    //Per a les accions que es realitzaran durant ExplorarState
    public Explorar_AccioContextual[] explorar_Accions;
    // Cada Explorar_AccioContextual senyalarà amb un delegate que es activable
    // Es guardarà en un Stack l'ordre en que cada acció contextual pot activar-se
    //private Stack<int> eac_Activables;
    private StackInt eac_Activables;

    //La càmara a la qual s'encaren no serà sempre la Camera.main
    private Camera camaraActual;

    //public InventorySystem inventari;
    public SO_InventoryObject inventari;
    public ItemsBarScript itemsBar;

    //Els sistemes de resistencia i alimentació
    public StaminaSystem stamina;
    public FeedingSystem feed;

    public bool accioEnMarxa = false;
    private float tempsAccio = 0f;
    private float TOP_TEMPS_ACCIO = 0.3f;
    public string[] accioAnims;
    
    void Start()
    {
        Debug.Log("PJ_StateManager Start()");
        //Debug.Log("PJ_StateManager Start()");
        //VA I AGARRA EL ANIMATOR PER A LES ANIMACIONS TIMELINE
        //animacion = GetComponentInChildren<Animator>();
        if (animacion == null)
            animacion = transform.GetChild(0).GetComponent<Animator>();
        agentNavegador = GetComponent<NavMeshAgent>();
        agentNavegador.speed = velocitat * 3f;
        //stateActual = explorarState;
        //Debug.Log("PJ_StateManager Start() 2");
        Debug.Log("PJ_StateManager Start() 1");
        colcam = GetComponentInChildren<ColisionadorCamera_Script>();
        //Debug.Log("PJ_StateManager Start()3");

        // Obtenció de tots els AccioContextual
        Explorar_AccioContextual [] explorarAccioInici = GetComponentsInChildren<Explorar_AccioContextual>();
        Debug.Log("PJ_StateManager Start() 2");
        // Formació de l 'array explorar_Accions
        explorar_Accions = new Explorar_AccioContextual[explorarAccioInici.Length + 1];
        for(int i = 1; i < explorar_Accions.Length; i++)
        {
            explorar_Accions[i] = explorarAccioInici[i - 1];
            explorar_Accions[i].posicioLista = i;
            explorar_Accions[i].senyalaActivable += SenlayarActivable;
        }
        Debug.Log("PJ_StateManager Start() 3");
        //Inicialització del stack per a la senyalizació d'accions contextuals
        eac_Activables = new StackInt(explorar_Accions.Length);

        //inventari = GetComponent<InventorySystem>();
        if(inventari==null)
        {
            Debug.Log("PJ_StateManager Start() El jugador no té inventari!!");
        }
        Debug.Log("PJ_StateManager Start() 4");
        //Debug.Log("PJ_StateManager Start() Explorar_Accions detectats: " + explorar_Accions.Length);

        
        Debug.Log("PJ_StateManager Start() 5");
        if (itemsBar == null)
            itemsBar = FindObjectOfType<ItemsBarScript>();
        Debug.Log("PJ_StateManager Start() 6");
        if (feed != null && stamina != null)
            feed.stamina = stamina;

        explorarState.controlador = GetComponent<CharacterController>();

        //ES NECESSARI QUE ESTA SIGA L'ÚLTIMA QUE S'EXECUTE DEL START DE PJ_StateManager
        if (stateActual == null) CanviarState_Explorar();

        Debug.Log("PJ_StateManager Start() Acabat!!");
    }


    void Update()
    {
        //Activitat de l'State
        if(stateActual!=null)   
            stateActual.StateUpdate(this);

    }

    //LES FUNCIONS DE FORZAR FLOAT PARAMETER NO ES DEUEN CRIDAR, EXCEPTE SI ES TOTALMENT NECESSARI I NO HI HA ALTRA SOLUCIÓ
    public void ForzarFloatParameter(bool validar_bloqueo, float valor)
    {
        if (colcam != null) colcam.ForzarFloatParameter(validar_bloqueo, valor);
    }

    public void ForzarFloatParameter_True(float valor)
    {
        if (colcam != null) colcam.ForzarFloatParameter(true, valor);
    }

    public void ForzarFloatParameter_False()
    {
        if (colcam != null) colcam.ForzarFloatParameter(false, 0f);
    }

    //EN CAS DE QUE EL NAV MESH AGENT NO PERMETA ANAR A UN LLOC PERQUE NO HI HA NAV MESH SURFACE
    //I BLOQUEJE LES FUNCIONABILITATS DEL COL·LISIONADOR CAMERA O DE LES CAMERES:
    public void DeshabilitarNMA()
    {
        agentNavegador.enabled = false;
    }

    public void HabilitarNMA()
    {
        agentNavegador.enabled = true;
    }

    public void CanviarCamara(Camera novaCamara)
    {
        camaraActual = novaCamara;
    }
    

    //Se accede a este código desde ExplorarState
    public void SnapAlignCharacterWithCamera()
    {
        if(inputMoure.z != 0 || inputMoure.x != 0)
        {
            transform.rotation =    Quaternion.Euler(transform.eulerAngles.x,
                                    camaraActual.transform.eulerAngles.y,
                                    transform.eulerAngles.z);

            float rot = 0;
            //z = delante, x= a los lados

            // si se va en la dirección opuesta de la actual, 
            //rotar inmediatamente; si no, rotación gradual    
            
            //si se va hacia atrás, rotar 180
            if (inputMoure.z < 0) rot = 180;

            //si se va hacia los lados, rotar 90 según el vector horizontal
            if(inputMoure.z == 0) 
                rot += (inputMoure.x / Mathf.Abs (inputMoure.x)) * 90f;
            //en caso contrario, se asumirán horizontal y vertical
            else    
                rot += (Mathf.Atan (inputMoure.x/inputMoure.z)) * 45f;
            
            transform.rotation = Quaternion.Euler(  transform.eulerAngles.x,
                                                    transform.eulerAngles.y + rot,
                                                    transform.eulerAngles.z);

        }
    }
   

    //Funció que executarà la mateixa funció en CinematicaState
    public void CaminarCapDesti_DespresExplorar(Transform desti)
    {
        Debug.Log("Funció CaminarCapDestí de DetectiuStateManager executada");
        if(stateActual != cineState)    CanviarState_Cinematica(true);

        cineState.CaminarCapDesti(this, desti, true);
    }

    public void CaminarCapDesti_NoExplorar(Transform desti)
    {
        Debug.Log("Funció CaminarCapDestí de DetectiuStateManager executada");
        if (stateActual != cineState) CanviarState_Cinematica(true);

        cineState.CaminarCapDesti(this, desti, false);
    }

    public void CanviarState_Explorar()  
    {
        Debug.Log("Canviar a ExplorarState");
        stateActual = explorarState;
        //Configurar càmara
        FindObjectOfType<ManejaCamares>().CanviarCamara("CameraJugador");
        Controla_Camara cc = FindObjectOfType<Controla_Camara>();
        SeguixJugador sj = FindObjectOfType<SeguixJugador>();

        //cc.NouEnfoque(this.gameObject.transform, sj.posCamara);
        sj.ActivarCameraExplorar();
    }
    public void CanviarState_Xarrar()   
    {
        Debug.Log("Canviar a XarrarState");
        if (stateActual == cineState && cineState.isActivatNMA())
            cineState.FiDelCami(this);
        stateActual = xarrarState;
        //La configuració de càmara ho realitzarà el sistema determinat, entre els possibles: 
        //      MainTriggersDialog, per exemple

        //El personatge te que parar de caminar
        //Debug.Log("El personatge deixa de caminar");
        if (animacion == null)
            animacion = transform.GetChild(0).GetComponent<Animator>();
        animacion.SetBool("isCaminant",false);

    }

    public void CanviarState_Cinematica(bool canviarAnim)
    {
        Debug.Log("Canviar a CinematicaState");
        stateActual = cineState;

        //El personatge te que parar de caminar
        //Debug.Log("El personatge deixa de caminar");
        if (animacion == null)
            animacion = transform.GetChild(0).GetComponent<Animator>();

        // Si es mana false, no es canvien els anims
        if (canviarAnim)
            animacion.SetBool("isCaminant", false);
    }

    //FUNCIONS PER A CANVIAR PARÀMETRES AMB EL SIGNAL RECEIVER
    //NOMÉS SI ESTÀ EN CINEMATICA STATE !!
    public void SetIsCaminant(bool intro)
    {
        //Debug.Log("SetIsCaminant()");
        if (stateActual == cineState)
        {
            Debug.Log("SetIsCaminant() Se cambia el parámetro");
            if (animacion == null)
                animacion = transform.GetChild(0).GetComponent<Animator>();
            animacion.SetBool("isCaminant", intro);
        }
    }

    public void SetIsXarrant(bool intro)
    {
        Debug.Log("SetIsXarrant()");
        if (stateActual == cineState)
        {
            Debug.Log("SetIsXarrant() Se cambia el parámatro");
            if (animacion == null)
                animacion = transform.GetChild(0).GetComponent<Animator>();
            animacion.SetBool("isXarrant", intro);
        }
    }

    // Codi dedicat als Explorar_AccioContextual
    private void SenlayarActivable(int posicio)
    {
        //Debug.Log("PJ_StateManager SenyalarActivable()");
        //Afegir senyalacio al Stack
        if (posicio > 0)
        {
            eac_Activables.Push(posicio);
            //Debug.Log("PJ_StateManager SenyalarActivable() Push(" + posicio + ")");

        }
        //Eliminar senyalacio del Stack
        else
        {
            eac_Activables.Delete(-posicio);
            //Debug.Log("PJ_StateManager SenyalarActivable() Delete(" + -posicio + ")");
        }

        //Mostrar senyal de l'acció activable
        
        /*int[] showStack = eac_Activables.ShowStack();
        for (int i = 0; i < showStack.Length; i++)
            Debug.Log("PJ_StateManager SenyalarActivable() showStack[" + i + "] = " + showStack[i]);
        */
    }

    public void GestionarAccioContextual()
    {
        if(explorar_Accions.Length!= 0)
        {
            int accio = eac_Activables.ShowPeek();
            //Debug.Log("PJ_StateManager GestionarAccioContextual() ShowPeek() = "+accio + ", ShowSize() = "+ eac_Activables.ShowSize());
            if (accio > 0)
            {
                //RotateWhenAction(obj);
                explorar_Accions[accio].Activar(this);
            }
            else
                Debug.Log("PJ_StateManager GestionarAccioContextual() Ninguna acció activable");
        }
    }

    public void GestionarAccioContextual(GameObject obj)
    {
        if (explorar_Accions.Length != 0)
        {
            //foreach(Explorar_AccioContextual accio in explorar_Accions)
            for(int i=1; i<explorar_Accions.Length; i++)
            {
                Explorar_AccioContextual accio = explorar_Accions[i];
                if(accio.ComprovarAccio(obj))
                {
                    //RotateWhenAction(obj);
                    accio.Activar(this, obj);
                    return;
                }
            }
        }
    }

    public void RotateWhenAction(GameObject obj)
    {
        transform.LookAt(obj.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    //Esta funció deuria estar en FaceCameraPersonajes
    public void ActionAnimation(string anim)
    {
        Debug.Log("PJ_StateManager ActionAnimation(" + anim + ")");
        if (!esExplorarState()) return;

        animacion.SetBool("isAccio", true);

        //Determinar valor itemNumFerramenta
        float numAnimation;
        if (accioAnims == null || accioAnims.Length == 0)
            numAnimation = 0f;
        else
        {
            float numPos = -1f;
            for(int i=0; i<accioAnims.Length; i++)
                if(accioAnims[i] == anim)
                {
                    numPos = (float)i;
                    Debug.Log("PJ_StateManager ActionAnimation() numPos = "+numPos);
                    break;
                }
            if (numPos == -1f) numPos = 0f;

            numAnimation = numPos / (accioAnims.Length-1f);
            Debug.Log("PJ_StateManager ActionAnimation() numAnimation = " + numAnimation);
        }
        
        animacion.SetFloat("itemNumFerramenta", numAnimation);
        //Començar contador
        tempsAccio = 0f;
        accioEnMarxa = true;

    }

    public void ContarTempsAccio()
    {
        tempsAccio += Time.deltaTime;
        //Debug.Log("ExplorarState ContarTempsAccio() tempsAccio = " + tempsAccio);
        if (tempsAccio >= TOP_TEMPS_ACCIO)
        {
            Debug.Log("ExplorarState ContarTempsAccio() Fi accio");
            accioEnMarxa = false;
            animacion.SetBool("isAccio", false);
        }
    }

    //Esta funció deuria estar en FaceCameraPersonajes
    public void Desmaig()
    {
        //Detindre totes les demés accions
        animacion.SetBool("isCaminant", false);
        animacion.SetBool("isAccio", false);
        animacion.SetTrigger("triggerDesmaig");
    }

    //Esta funció deuria estar en FaceCameraPersonajes
    public void Ascendir()
    {
        animacion.SetTrigger("triggerAscendir");
    }


    public bool esExplorarState()
    {
        return stateActual != null && stateActual == explorarState;
    }


}

public class StackInt
{
    //private int[] stack;
    //private int peekPosition;
    private List<int> stack;

    public StackInt(int cantidad)
    {
        //stack = new int[cantidad];
        //peekPosition = 0;
        stack = new List<int>();
    }

    public void Push(int nuevo)
    {
        //if (ComprobarEnStack(nuevo) == -1)
            //stack[++peekPosition] = nuevo;
         if(!stack.Contains(nuevo))   
            stack.Add(nuevo);
    }

    public void Delete(int borrable)
    {
        /*
        int posBorrable = ComprobarEnStack(borrable);
        if(posBorrable != -1)
        {
            
            for(int i = posBorrable +1; i <= peekPosition; i++)
            {
                if (i == stack.Length)
                    stack[i - 1] = 0;
                else
                    stack[i - 1] = stack[i];
            }

            peekPosition--;
        }  
        */
        if (stack.Contains(borrable))
            stack.Remove(borrable);

    }

    /*private int ComprobarEnStack(int objetivo)
    {
        for(int i=0; i<stack.Length; i++)
        {
            if (stack[i] == objetivo)
                return i;
        }

        return -1;
    }*/

    public int ShowPeek()
    {
        //return stack[peekPosition];
        if (stack.Count > 0)
            return stack[stack.Count - 1];
        else return 0;
    }

    public int ShowSize()
    {
        //return peekPosition; 
        return stack.Count - 1;
    }

    public List<int> ShowStack()
    {
        return stack;
    }
}
