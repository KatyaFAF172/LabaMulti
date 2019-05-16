using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public Text loseMessage = null;
    

    [SyncVar]
    private bool _isDead = false;
    public bool IsDead { get => _isDead; set => _isDead = value; }


    [SerializeField]
    private int maxHealth = 100;


    [SyncVar]
    private int curHealth;
    //server needs to know health of each player at the specific moment of time and here's required total sync of this variable
    //with server.

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    #region Setup for player

    private bool firstSetup = true;

    
    public void Setup()
    {
        CmdBroadcastNewSetup();
    }

    [Command]
    private void CmdBroadcastNewSetup()
    {
        RpcSetupOnClients();
    }

    [ClientRpc]
    private void RpcSetupOnClients()
    {
        if(firstSetup)
        {
            wasEnabled = new bool[disableOnDeath.Length];

            for (int i = 0; i < wasEnabled.Length; i++)
            {
                wasEnabled[i] = disableOnDeath[i].enabled;
            }

            firstSetup = false;
        }

        SetDefaults();

    }
    #endregion

    public int GetcurHealth()
    {
        return this.curHealth;
    }
    

    [ClientRpc]
    public void RpcTakeDamage(int damage)  //player gets damage locally and updates only value of curHealth with server
    {
        if (IsDead)
            return;

        curHealth -= damage;

     

        Debug.Log(transform.name + " now has " + curHealth + " hp");

        if(curHealth <= 0)
        {
            Die();
            //loseMessage.text = "You Loser!";
            //Debug.Log("You Loser!");
            //SceneManager.LoadScene("Lose");
        }
    }

    private void Die()
    {
        IsDead = true;
        loseMessage.text = "You Loser!";
        Debug.Log("You Loser!");
        //Disable components for dead player for evading errors
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider col = GetComponent<Collider>();
        if (col)
            col.enabled = false;

        Debug.Log(transform.name + " is dead");

        //call respawn for dead player
        //StartCoroutine(Respawn());
        

    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);


        if (isServer)
        {
            transform.position = new Vector3(MapGeneration.mapSize.x - 15, 50, MapGeneration.mapSize.z - 15);
        }
        else if (isClient)
        {
            transform.position = new Vector3(15, 50, 15);
        }
        yield return new WaitForSeconds(0.1f);

        Setup();

        Debug.Log(transform.name + " respawned");
    }

    public void SetDefaults()
    {
        IsDead = false;

        curHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider col = GetComponent<Collider>();
        if (col)
            col.enabled = true;
        
    }

    //void Update()
    //{
    //    if (!isLocalPlayer)
    //        return;

    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        RpcTakeDamage(999);
    //    }
    //}
    
}