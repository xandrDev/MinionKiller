using UnityEngine;

public class Box : MonoBehaviour
{
    public Color Color { get; set; }

    private Renderer _renderer;
    private AudioSource _pickUpSource;

    private void Start()
    {
        _pickUpSource = GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();
    }

    private void PlayPickUpSound()
    {
        if (_pickUpSource != null)
            _pickUpSource.Play();
    }

    public void PickUp()
    {
        PlayPickUpSound();
        _renderer.enabled = false;
        Destroy(gameObject, _pickUpSource.clip.length);
    }
}
