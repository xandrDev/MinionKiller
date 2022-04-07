using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    protected ColorChanger colorChanger;
    protected AudioSource shootSource;

    public FP_Input playerInput;
}
