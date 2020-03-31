using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static int scorePlayer;
    
    [SerializeField]
    private Text score;
    // Start is called before the first frame update
    private void Awake() {
        // score = GetComponent<Text>();
        scorePlayer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scorePlayer;
    }
}
