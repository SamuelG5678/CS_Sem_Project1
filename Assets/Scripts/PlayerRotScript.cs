using UnityEngine;
using UnityEngine.Timeline;

public class PlayerRotScript : MonoBehaviour
{
    public bool isInMenu = true;
    public static PlayerRotScript instance;

    private void Start()
    {
        instance = this;
    }

    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage

    void Update()
    {
        if(isInMenu == false)
        {
            rotation.x += Input.GetAxis(xAxis) * sensitivity;
            rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
            var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);

            transform.localRotation = xQuat; 
            //Quaternions seem to rotate more consistently than EulerAngles.
            //Sensitivity seemed to change slightly at certain degrees using Euler.
            //transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
        }
    }
}