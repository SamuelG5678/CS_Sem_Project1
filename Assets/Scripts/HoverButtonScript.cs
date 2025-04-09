using Unity.VisualScripting;
using UnityEngine;

public class HoverButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void OnMouseOver()
    {
        AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.swapbuttons);
    }
}
