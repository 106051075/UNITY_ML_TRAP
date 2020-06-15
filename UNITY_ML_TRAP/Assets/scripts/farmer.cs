using UnityEngine;
public class farmer : MonoBehaviour
{
    public static bool complete;
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.name == "兔子")
        {
            complete = true;
        }
    }
}
