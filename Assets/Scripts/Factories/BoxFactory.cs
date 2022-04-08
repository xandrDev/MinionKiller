using UnityEngine;

public class BoxFactory : GenericFactory<Box>
{
    [SerializeField]
    private ColorChanger _colorChanger = null;

    void Awake()
    {
        if (_colorChanger == null)
            throw new System.Exception("ColorChanger is not defined");
    }

    public void CreateBox(Vector3 spawnPosition)
    {
        var newBox = GetInstance(spawnPosition);
        Color color = _colorChanger.ChangeBoxMaterialColor(newBox.gameObject);
        newBox.Color = color;
    }
}
