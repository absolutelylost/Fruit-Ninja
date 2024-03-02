using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechRecognition : MonoBehaviour
{
    private string phrase = "Fruit gods";
    private DictationRecognizer recognizer;

    void Start()
    {
        // Initialize the DictationRecognizer
        recognizer = new DictationRecognizer();

        // Register the dictation recognizer
        recognizer.DictationResult += Recognizer_DictationResult;
        recognizer.DictationError += Recognizer_DictationError;

        // Start dictation recognition
        recognizer.Start();
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

    void Recognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        // Check if recognized text contains the desired phrase
        if (text.ToLower().Contains(phrase.ToLower()))
        {
            // Trigger an event or execute desired action
            Debug.Log("Phrase recognized: " + text);
            // Place your event triggering code here
        }
    }

    void Recognizer_DictationError(string error, int hresult)
    {
        Debug.LogError("Dictation error: " + error);
    }
}
