using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject bulletPrefab;
    protected AudioSource shootSource;

    public FP_Input playerInput;

    public Color BullenColor { get; set; }
}
