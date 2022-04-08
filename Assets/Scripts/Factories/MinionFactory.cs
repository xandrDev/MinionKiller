using UnityEngine;

public class MinionFactory : GenericFactory<Minion>
{
    [SerializeField]
    private Player _minionTarget = null;
    [SerializeField]
    private BoxFactory _boxFactory = null;
    [SerializeField]
    private PositionGenerator _positionGenerator = null;
    [SerializeField]
    private float _spawnPositionY = 1.15f;

    private Vector3 _sizeSpawnCollider = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 _spawnPosition;
    private float _spawnStopTimerValue = 3.0f;
    private float _spawnTimerValue = 0;
    private Minion _newMinion;


    private void Awake()
    {
        if (_boxFactory == null)
            throw new System.Exception("Box Factory is not defined");

        if (_positionGenerator == null)
            throw new System.Exception("Position Generator is not defined");
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

            _spawnPosition = _positionGenerator.GetRandomPositionOnPlane(_spawnPositionY, _sizeSpawnCollider);
            _newMinion = GetInstance(_spawnPosition);
            _newMinion.SetTarget(_minionTarget);
            _newMinion.SetParent(this);
        }
    }

    public void MinionKilled(Vector3 deathPosition)
    {
        _boxFactory.CreateBox(new Vector3(deathPosition.x, deathPosition.y + 0.3f, deathPosition.z));
    }
}
