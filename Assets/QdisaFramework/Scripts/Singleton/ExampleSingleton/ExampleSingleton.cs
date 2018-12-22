using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qdisa.Singleton; //继承这个空间

public class ExampleSingleton : MonoBehaviour, ISingleton{ //接口 
    public ExampleSingleton()
    {

    }
    public static ExampleSingleton Instance
    {
        get
        {
            return MonoSingletonProperty<ExampleSingleton>.Instance;
        }
    }
    public  void Dispose()
    {
        MonoSingletonProperty<ExampleSingleton>.Dispose();
    }
}

