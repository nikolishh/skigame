using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyFlag : MonoBehaviour
{
    public enum Side { Left, Right }
    public Side correctSide = Side.Left;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 toPlayer = player.position - transform.position;
            float dot = Vector3.Dot(transform.right, toPlayer.normalized);

            bool passedCorrectSide = (correctSide == Side.Left && dot < 0) ||
                                     (correctSide == Side.Right && dot > 0);

            if (!passedCorrectSide)
            {
                Debug.Log("wrong side passed");
                GameManager.CallRacePenalty();
            }
            else
            {
                Debug.Log("right side passed");
            }

            GetComponent<Collider>().enabled = false;
        }
    }
}
