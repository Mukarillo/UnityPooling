using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pooling;
using System;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class PoolingExample : MonoBehaviour {

	public GameObject referenceObject;
	public Text uiText;

	private Pooling<ObjectExample> mPooling = new Pooling<ObjectExample>();
	private int mObjectAmount = 0;
	private int mObjectActivated = 0;
	private int mObjectDeactivated = 0;
 
	void Start () {
		mPooling.OnObjectCreationCallBack += OnCreateObject;
		mPooling.Initialize(10, referenceObject, transform);      
	}

	private void OnCreateObject(ObjectExample obj)
	{
		obj.OnCollectCallback += OnCollectCallback;
		obj.OnReleaseCallback += OnReleaseCallback;
		mObjectAmount++;
	}

	private void OnReleaseCallback()
	{
		if(mObjectActivated > 0)
		    mObjectActivated--;
		mObjectDeactivated++;
		RefreshUi();
	}

	private void OnCollectCallback()
	{
		mObjectActivated++;
		if(mObjectDeactivated > 0)
		    mObjectDeactivated--;
		RefreshUi();
	}

	private void RefreshUi()
	{
		uiText.text = string.Format("Number of objects created: <b>{0}</b>\nNumber of objects activated: <b>{1}</b>\nNumber of objects deactivated: <b>{2}</b>",
									mObjectAmount,
									mObjectActivated,
									mObjectDeactivated
								   );
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			var o = mPooling.Collect();
			o.transform.position = new Vector3(Random.Range(-13f, 13f), Random.Range(-6.5f, 6.5f), 2.66f);
		}
	}
}
