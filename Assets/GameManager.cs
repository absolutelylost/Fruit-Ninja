using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int Score;
	[SerializeField] private TextMeshProUGUI Scoring;

	// Start is called before the first frame update
	void Start()
    {
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
		//uopdate score
		Scoring.text = Score.ToString();

	}
}
