using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public abstract class BossPattern : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField]protected Boss main;
	protected Animator anim;
	[SerializeField] protected string animationStateName;
	[SerializeField] protected int preDelaySeconds;
	[SerializeField] protected int postDelaySeconds;
	private WaitForSeconds waitpreDelay;
	private WaitForSeconds waitpostDelay;
	protected CancellationTokenSource preDelaySource = new CancellationTokenSource();
	protected CancellationTokenSource postDelaySource = new CancellationTokenSource();
	#endregion

	#region PublicMethod

	public void StartAct()
    {
		StartCoroutine(nameof(Act));
    }
	public IEnumerator Act()
	{
		PreProcessing();
		PlayAnimation();
		yield return waitpreDelay;
		ActionContext();
		yield return waitpostDelay;
		PostProcessing();

		CallNextAction();
	}
	public void CallNextAction()
	{
		main.PatternNext();
	}
	public void ShutdownAction()
	{
		preDelaySource.Cancel();
		postDelaySource.Cancel();
	}
	#endregion

	#region PrivateMethod
	protected virtual void Awake()
	{
		transform.parent.TryGetComponent(out main);
		anim = main.GetAnimator();
		waitpreDelay = new WaitForSeconds(preDelaySeconds);
		waitpostDelay = new WaitForSeconds(postDelaySeconds);

	}
	private void Start()
	{
		
	}
	protected virtual void OnEnable()
	{
		if (preDelaySource != null)
			preDelaySource.Dispose();
		preDelaySource = new CancellationTokenSource();
		if(postDelaySource != null)
			postDelaySource.Dispose();
		postDelaySource = new CancellationTokenSource();
	}
	protected virtual void OnDisable()
	{
		ShutdownAction();
	}
	private void PlayAnimation()
	{
		if (animationStateName != "")
		{
			anim = main.GetComponent<Animator>();
			Debug.Log(animationStateName);
			Debug.Log(anim);
			
			anim.Play(animationStateName);
		}
	}
	protected virtual void PreProcessing()
	{

	}
	protected virtual void PostProcessing()
	{

	}
	protected abstract void ActionContext();
	#endregion
}
