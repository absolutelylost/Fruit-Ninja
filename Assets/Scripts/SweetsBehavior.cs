using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class SweetsBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("They objects collided!!!");
            Destroy(gameObject);
        }
    }
}
