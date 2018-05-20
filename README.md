# UnityPooling
 An optimized approach object pooling.

<p align="center">
  <img src="https://github.com/Mukarillo/UnityPooling/blob/master/readmeassets/pooling_example.gif?raw=true" alt="Example"/>
</p>

## How to use
*you can find a pratical example inside this repository in PoolingScene scene*

### 1 - Create a class that extends `PoolingObject` and, if you wish, implement its virtual members (`OnRelease` and `OnCollect`)
```c#
public class ObjectExample : PoolingObject {
    private Renderer mRenderer;
    private MaterialPropertyBlock mPropBlock;

    void Awake()
    {
        mPropBlock = new MaterialPropertyBlock();
        mRenderer = GetComponent<Renderer>();
    }

    public override void OnCollect()
    {
        mRenderer.GetPropertyBlock(mPropBlock);
        mPropBlock.SetColor("_Color", Random.ColorHSV());
        mRenderer.SetPropertyBlock(mPropBlock);

        Invoke("OnRelease", 2f);

        base.OnCollect(); 
    }
}
```
### 2 - Create a class to initiate the Pooling<ObjectExample> and call `Collect` whenever you need an instance of T
```c#
public class PoolingExample : MonoBehaviour {
    public GameObject referenceObject;

    private Pooling<ObjectExample> mPooling = new Pooling<ObjectExample>();

    void Start () {
        mPooling.Initialize(10, referenceObject, transform);      
    }

    void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
          var o = mPooling.Collect();
          o.transform.position = new Vector3(Random.Range(-13f, 13f), Random.Range(-6.5f, 6.5f), 2.66f);
        }
    }
}
```

## Pooling< T > `public` overview
### Properties
|name  |type  |description  |
|--|--|--|
|`createMoreIfNeeded` |**bool** |*boolean to control if the system can create more objects if needed. Its true by default.*  |
|`OnObjectCreationCallBack` |**delegate ObjectCreationCallback** |*callback triggered whenever a new object is created, send T as parameter*  |

### Methods

</br>

> `pooling.Initialize`
- *Description*: Initiate the pooling, creating `amount` `objReference` and attaching `T` to the object.

- *Parameters*:

|name  |type  |description  |
|--|--|--|
|`amount` |**int** |*initial amount to be created.*  |
|`refObject` |**GameObject** |*a reference of the object that will be instantiated and handled by the pool.*  |
|`parent` |**Transform** |*the parent of the instantiated object. set `null` if creating in root.*  |
|`worldPos` |**Vector3** |*the initial position for instantiated object.*  |
|`startState` |**int** |*the initial state for instantiated object, if true, it will call OnCollect when instantiating each object, if false, OnRelease*  |

</br>

> `pooling.Collect`
- *Description*:
Returns an instance of `T` object that is not currently being used.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`parent` |**Transform** |*if you need to change the parent of the object, do it here.*  |
|`position` |**Vector3** |*position of the object*  |
|`localPosition` |**bool** |*if true, `position` will be int local space, else, it will be world space.*  |

</br>

> `dynamicScroll.Release`
- *Description*: Releases the object.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`obj` |**T** |*Releases the object.*  |

</br>

> `dynamicScroll.GetAllWithState`
- *Description*: Returns a list with objects either being used or not being used.

- *Parameters* :

|name  |type  |description  |
|--|--|--|
|`active` |**bool** |*if true, return a `List<T>` of actived objects, else, unused objects.*  |




