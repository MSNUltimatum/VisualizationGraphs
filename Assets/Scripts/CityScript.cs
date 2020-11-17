using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CityScript : MonoBehaviour
{ 
    public GameObject gameManager;
    public GameObject endPoint;
    private GameManagerScr gmsrc;
    private void Start()
    {
        gmsrc = gameManager.GetComponent<GameManagerScr>();
    }

    private void OnMouseDown()
    {
        if (GameManagerScr.startPosition == null)
        {
            GameManagerScr.startPosition = endPoint;
        } else if (GameManagerScr.endPosition == null || GameManagerScr.endPosition != endPoint)
        {
            GameManagerScr.endPosition = endPoint;
        }
    }
}