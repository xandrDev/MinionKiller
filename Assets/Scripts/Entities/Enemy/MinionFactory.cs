using UnityEngine;

public class MinionFactory : GenericFactory<Minion>
{
    [SerializeField]
    private MeshCollider _plane;
    [SerializeField]
    private float _spawnPositionY = 1.15f;
    [SerializeField]
    private Player _minionTarget;
    [SerializeField]
    private BoxFactory _boxFactory;

    private Vector3 _sizeSpawnCollider = new Vector3(1.0f, 1.0f, 1.0f);

    private float _spawnStopTimerValue = 3.0f;
    private float _spawnTimerValue = 0;

    private Vector3 _spawnPosition;
    private float _randomX;
    private float _randomZ;
    private Collider[] _colliders;
    private Plane[] _planes;
    private Camera _mainCamera;
    private Minion _newMinion;

    private void Awake()
    {
        if (_plane == null)
            throw new System.Exception("Plane is not defined");
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_spawnTimerValue < _spawnStopTimerValue)
        {
            _spawnTimerValue += Time.deltaTime;
        }
        else
        {
            _spawnTimerValue = 0.0f;

            GetSpawnPosition();
            _newMinion = GetInstance(_spawnPosition);
            _newMinion.SetTarget(_minionTarget);
            _newMinion.SetParent(this);
        }
    }

    public void MinionKilled(Vector3 deathPosition)
    {
        _boxFactory.CreateBox(new Vector3(deathPosition.x, deathPosition.y + 0.3f, deathPosition.z));
    }

    private void GetSpawnPosition()
    {
        _randomX = Random.Range(_plane.transform.position.x - Random.Range(0, _plane.bounds.extents.x), _plane.transform.position.x + Random.Range(0, _plane.bounds.extents.x));
        _randomZ = Random.Range(_plane.transform.position.z - Random.Range(0, _plane.bounds.extents.z), _plane.transform.position.z + Random.Range(0, _plane.bounds.extents.z));
        _spawnPosition = new Vector3(_randomX, _spawnPositionY, _randomZ);

        if (!CheckSpawnPosition())
            GetSpawnPosition();
    }

    bool CheckSpawnPosition()
    {
        _colliders = Physics.OverlapBox(_spawnPosition, _sizeSpawnCollider);
        if (_colliders.Length <= 0)
        {
            _planes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);

            if (!GeometryUtility.TestPlanesAABB(_planes, new Bounds(_spawnPosition, _sizeSpawnCollider)))
                return true;
        }

        return false;
    }
}
