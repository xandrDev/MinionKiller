using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerator : MonoBehaviour
{
    [SerializeField]
    private MeshCollider _plane = null;
    
    private Vector3 _spawnPosition;
    private float _randomX;
    private float _randomZ;
    private Collider[] _colliders;
    private Plane[] _cameraPlanes;
    private Bounds _newSpawnerBounds;
    private Camera _mainCamera;

    private void Awake()
    {
        if (_plane == null)
            throw new System.Exception("Plane is not defined");
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private Vector3 GenerateRandomPositionOnPlane(float y)
    {
        _randomX = Random.Range(_plane.transform.position.x - Random.Range(0, _plane.bounds.extents.x), _plane.transform.position.x + Random.Range(0, _plane.bounds.extents.x));
        _randomZ = Random.Range(_plane.transform.position.z - Random.Range(0, _plane.bounds.extents.z), _plane.transform.position.z + Random.Range(0, _plane.bounds.extents.z));
        return new Vector3(_randomX, y, _randomZ);
    }

    private bool DetectOtherColliders(Vector3 spawnPosition, Vector3 spawnColliderSize)
    {
        _colliders = Physics.OverlapBox(spawnPosition, spawnColliderSize);
        return _colliders.Length > 0;
    }

    private bool IsInPlayerCameraArea(Vector3 spawnPosition, Vector3 spawnColliderSize)
    {
        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);
        _newSpawnerBounds = new Bounds(spawnPosition, spawnColliderSize);
        return GeometryUtility.TestPlanesAABB(_cameraPlanes, _newSpawnerBounds);
    }

    public Vector3 GetRandomPositionOnPlane(float y, Vector3 spawnColliderSize)
    {
        _spawnPosition = GenerateRandomPositionOnPlane(y);

        if (!DetectOtherColliders(_spawnPosition, spawnColliderSize))
        {
            if (!IsInPlayerCameraArea(_spawnPosition, spawnColliderSize))
                return _spawnPosition;
            else
                return GetRandomPositionOnPlane(y, spawnColliderSize);
        }
        else
            return GetRandomPositionOnPlane(y, spawnColliderSize);
    }
}
