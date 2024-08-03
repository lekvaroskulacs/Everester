using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int numOfPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numOfPlayers > 1 )
            Destroy( gameObject );
        else
            DontDestroyOnLoad( gameObject );
    }
}
