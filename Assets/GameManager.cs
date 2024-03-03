using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[HideInInspector] public int Score = 0;
	[SerializeField] private TextMeshProUGUI Scoring;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//uopdate score
		Scoring.text = Score.ToString();

	}
}
