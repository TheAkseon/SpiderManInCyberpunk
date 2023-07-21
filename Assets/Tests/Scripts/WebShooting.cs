using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WebShooting : MonoBehaviour
{
    [SerializeField] private GameObject Web;
    [SerializeField] private GameObject Player;
    [SerializeField] private float TimeBetweenShots;
    [SerializeField] private bool _isMultipleshots = false;

    public PlayerMove PlayerMoveScript;
    
    private float startTimeBetweenShots;

    public static WebShooting instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        startTimeBetweenShots = TimeBetweenShots;
        Player.GetComponent<Transform>();
    }

    public void ChangeShootMode()
    {
        if (_isMultipleshots == true)
        {
            _isMultipleshots = false;
        }
        else
        {
            _isMultipleshots = true;
        }
    }

    void FixedUpdate()
    {
        if (PlayerMoveScript.CanMove())
        {
            if (TimeBetweenShots <= 0)
            {
               /* Quaternion rotation = Player.transform.rotation;
                float changeAngle = 2f;
                for (int i = 0; i < _numOfShots; i++)
                {
                    Instantiate(Web, Player.transform.position + new Vector3(0f, 1f, 0.5f), rotation);
                    rotation = Quaternion.Euler(0f, rotation.y + changeAngle, 0f) * rotation;
                    changeAngle *= (-1);
                }*/
                if (_isMultipleshots)
                {
                    Quaternion rotation = Player.transform.rotation;
                    Quaternion leftRotation = Quaternion.Euler(0f, -3f, 0f) * rotation;
                    Quaternion rightRotation = Quaternion.Euler(0f, 3f, 0f) * rotation;

                    Instantiate(Web, Player.transform.position + new Vector3(0f, 1f, 0.5f), rotation);
                    Instantiate(Web, Player.transform.position + new Vector3(0f, 1f, 0.5f), leftRotation);
                    Instantiate(Web, Player.transform.position + new Vector3(0f, 1f, 0.5f), rightRotation);
                }
                else
                {
                    Quaternion rotation = Player.transform.rotation;
                    Instantiate(Web, Player.transform.position + new Vector3(0f, 1f, 0.5f), rotation);
                }
                TimeBetweenShots = startTimeBetweenShots;
                //Instantiate(Web, Player.transform.position + new Vector3(0f, 1f, 0.5f), Player.transform.rotation);
            }
            else
            {
                TimeBetweenShots -= Time.deltaTime;
            }
        }
    }
}