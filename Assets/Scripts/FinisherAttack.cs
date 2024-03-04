using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows.Speech;

public class FinisherAttack : MonoBehaviour
{
    private float shrinkSpeed = 3f;
    private Color targetColor = Color.gray;
    private float colorChangeSpeed = 0.5f;

	private string phrase = "Fruit gods";
	private DictationRecognizer recognizer;

	private bool finisherInProgress = false;
    [SerializeField] private GameObject enemyFruit;
    private List<GameObject> fruits;
    // Start is called before the first frame update
    void Start()
    {
		// Initialize the DictationRecognizer
		recognizer = new DictationRecognizer();

		// Register the dictation recognizer
		recognizer.DictationResult += Recognizer_DictationResult;
		recognizer.DictationError += Recognizer_DictationError;

		// Start dictation recognition
		recognizer.Start();

		fruits = new List<GameObject>();
        for (int i = 0; i < enemyFruit.transform.childCount; i++)
        {
            //Debug.Log(enemyFruit.transform.GetChild(0).name);
            fruits.Add(enemyFruit.transform.GetChild(i).gameObject);
        }

    }

	void OnDestroy()
	{
		// Cleanup
		if (recognizer != null)
		{
			recognizer.Stop();
			recognizer.Dispose();
		}
	}

	// Update is called once per frame
	void Update()
    {
        //if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //{
        //    finisherInProgress = true;
        //}
        //if (finisherInProgress)
        //{
        //    FinisherMoveAction();
        //}
    }

    public void FinisherMoveAction()
    {
        Vector3 newScale = new Vector3();
        foreach (var fruit in fruits)
        {
            // Calculate the new scale
            newScale = fruit.transform.localScale - Vector3.one * shrinkSpeed * Time.deltaTime;

            // Ensure the scale doesn't go below a certain threshold
            newScale = Vector3.Max(newScale, Vector3.zero);

            // Apply the new scale to the object
            fruit.transform.localScale = newScale;

            // Pass the color to the shader if it has a property for it
            //objectMaterial.SetColor("_CustomColor", objectMaterial.color);
            // stop finisher once they are at one
            if (newScale.x <= 20.0f && finisherInProgress)
            {
                finisherInProgress = false;
                foreach (var curfruit in fruits)
                {
                    Destroy(curfruit);

                }
            }
        }



    }

	void Recognizer_DictationResult(string text, ConfidenceLevel confidence)
	{
		Debug.Log("checking recognition");
		// Check if recognized text contains the desired phrase
		if (text.ToLower().Contains(phrase.ToLower()))
		{
			// Trigger an event or execute desired action
			Debug.Log("Phrase recognized: " + text);
            if (confidence > 0) 
            {
				FinisherMoveAction();

			}
			// Place your event triggering code here
		}
	}

	void Recognizer_DictationError(string error, int hresult)
	{
		Debug.LogError("Dictation error: " + error);
	}

}
