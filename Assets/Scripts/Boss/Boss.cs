using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.ComponentModel;

public abstract class Boss : MonoBehaviour, IDamageable
{
	#region PublicVariables
	public string bossName;
	#endregion

	#region PrivateVariables
	protected Animator anim;
	private GameObject rend;
	private Sequence hitSeq;

	[SerializeField] private Vector2 respawnPoint;
	[SerializeField] protected int hpCurrent;
	[SerializeField] protected int hpMax;
	[SerializeField] protected BossPattern startPattern;
	[SerializeField] protected List<BossPattern> patternList = new List<BossPattern>();

	[SerializeField] protected int patternIndex;
	[SerializeField] protected BossPattern currentPattern;
	#endregion

	#region PublicMethod
	public Animator GetAnimator() => anim;
	public int GetMaxHp() => hpMax;
	public virtual void Initialize()
	{
		hpCurrent = hpMax;
		patternIndex = -1;
	}
	public virtual void Hit(int _damage, GameObject _source)
	{
		hitSeq.Restart();
		hpCurrent = Mathf.Clamp(hpCurrent - _damage, 0, hpMax);
	}
	public virtual void OnDamage(int damage = 1)
	{
		hitSeq.Restart();
		hpCurrent = Mathf.Clamp(hpCurrent - damage, 0, hpMax);
    }
	public virtual void BossKilled()
	{
		anim.Play("Die");
		ShutdownAction();
	}
	public void PatternStart()
	{
		currentPattern = startPattern;
		currentPattern.StartAct();
	}
	public void PatternNext()
	{


		patternIndex = GetNextPatternIndex(patternIndex);
		currentPattern = patternList[patternIndex];
		currentPattern.StartAct();
	}
	public void ShutdownAction()
	{
		currentPattern.ShutdownAction();
	}
	#endregion

	#region PrivateMethod
	protected virtual void Awake()
	{
		//rend = transform.Find("renderer").gameObject;
		//transform.Find("renderer").TryGetComponent(out anim);
	}
	protected virtual void OnEnable()
	{
		transform.position = respawnPoint;
	}
	protected virtual void Start()
	{
		/*hitSeq = DOTween.Sequence()
			.SetAutoKill(false)
			.Append(rend.transform.DOShakePosition(0.1f, 0.2f))
			.Pause();*/
		Initialize();
	}
	private int GetNextPatternIndex(int _currentIndex)
	{
		int result = _currentIndex;
		if (result >= patternList.Count - 1)
		{
			result = 0;
		}
		else
		{
			++result;
		}
		return result;
	}
	void OnGUI()
	{
		var style = new GUIStyle();
		style.fontSize = 100;
		style.normal.textColor = Color.white;
		GUI.Label(new Rect(Screen.width / 2, Screen.height / 10, Screen.width, 2 * Screen.height / 10), $"Boss : {hpCurrent} / {hpMax}", style);
	}


	#endregion
}
