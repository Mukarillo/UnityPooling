using System.Collections;
using System;
using UnityEngine;
using pooling;

using Random = UnityEngine.Random;

public class ObjectExample : PoolingObject {

	public Action OnCollectCallback;
	public Action OnReleaseCallback;

	private Renderer mRenderer;
	private MaterialPropertyBlock mPropBlock;
    
	void Awake()
    {
        mPropBlock = new MaterialPropertyBlock();
        mRenderer = GetComponent<Renderer>();
    }

	public override void OnCollect()
	{
		OnCollectCallback.Invoke();

		mRenderer.GetPropertyBlock(mPropBlock);
		mPropBlock.SetColor("_Color", Random.ColorHSV());
        mRenderer.SetPropertyBlock(mPropBlock);

		Invoke("OnRelease", 2f);

		base.OnCollect();
	}

	public override void OnRelease()
	{
		OnReleaseCallback.Invoke();
		base.OnRelease();
	}
}
