/****************************************************************************
 *
 * Copyright (c) 2019 Rain
 *
 ****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qdisa;

[QMonoSingletonPath("PoolManager")]
public class PoolControl : MonoBehaviour,ISingleton {
    [SerializeField]
    private string poolName="Pool1";
    public Transform ElephantTrans;
    public Transform StageTrans;
    public Transform StartPosTrans;

    public PoolControl()
    {

    }
    public static PoolControl Instance
    {
        get
        {
            return MonoSingletonProperty<PoolControl>.Instance;
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnElephant();
        }
	}
    /// <summary>
    /// 生产大象
    /// </summary>
    public Transform SpawnElephant()
    {
        Transform transform= PoolManager.Pools[poolName].Spawn
            (
            ElephantTrans,
            StartPosTrans.position,
            ElephantTrans.rotation,
            StageTrans   
            );
        return transform;
    }
    /// <summary>
    /// 消除掉生成的大象
    /// </summary>
    /// <param name="transform"></param>
    public void  DespawnElephant(Transform transform)
    {
        PoolManager.Pools[poolName].Despawn(transform);
    }
    public void Dispose()
    {
        MonoSingletonProperty<PoolControl>.Dispose();
    }

}


