using UnityEngine;


public class ATMPool : MonoBehaviour
{

    #region FIELDS

    [SerializeField]
    private int poolCounter = 3;
    [SerializeField]
    private bool autoExpand = true;
    [SerializeField]
    private ATMBase atmPref;


    private BasePool<ATMBase> pool;

    #endregion

    #region UNITY_METHODS

    private void Start()
    {
        pool = new BasePool<ATMBase>(atmPref, gameObject.transform, poolCounter)
        {
            autoExpand = autoExpand
        };
    }

    #endregion

    #region MAIN_METHODS

    public void SpawnATM(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        var el = pool.GetFreeEl();
        el.transform.position = spawnPosition;
        el.transform.rotation = spawnRotation;
    }

    #endregion

}
