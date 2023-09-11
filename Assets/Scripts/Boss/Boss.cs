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
	protected bool isDeal;
	private bool isWeak;
	public Collider2D bossCollider;
	protected SpriteRenderer spriteRenderer;

	[SerializeField] private Vector2 respawnPoint;
	[SerializeField] protected int hpCurrent;
	[SerializeField] protected int hpMax;
	[SerializeField] protected BossPattern startPattern;
	[SerializeField] protected List<BossPattern> patternList = new List<BossPattern>();
	[SerializeField] protected List<BossPattern> dealTimePatternList = new List<BossPattern>();

	[SerializeField] protected int patternIndex;
	[SerializeField] protected BossPattern currentPattern;

	[SerializeField] private string angryAnimationName;
	[SerializeField] private float angryDelay;
	[SerializeField] private float comeOutDelay;
	#endregion

	#region PublicMethod
	public Animator GetAnimator() => anim;
	public int GetMaxHp() => hpMax;
	public virtual void Initialize()
	{
		patternIndex = -1;
		PatternStart();
	}
	public virtual void Hit(int _damage, GameObject _source)
	{
		hitSeq.Restart();
		hpCurrent = Mathf.Clamp(hpCurrent - _damage, 0, hpMax);
	}

	public virtual void OnDamage(int damage = 1)
	{
        
	}

	public void StartOnWeak()
    {
		StartCoroutine(nameof(OnWeak));
    }
	public IEnumerator OnWeak()
    {
		bossCollider.enabled = false;
		ShutdownAction();
		anim.Play(angryAnimationName);

		spriteRenderer.DOColor(Color.red, angryDelay / 4);
		yield return new WaitForSeconds(angryDelay / 4);
		spriteRenderer.DOColor(Color.white, angryDelay / 4);
		yield return new WaitForSeconds(angryDelay / 4);
		spriteRenderer.DOColor(Color.red, angryDelay / 4);
		yield return new WaitForSeconds(angryDelay / 4);
		spriteRenderer.DOColor(Color.white, angryDelay / 4);
		yield return new WaitForSeconds(angryDelay / 4);
		anim.Play("Boss_come_out");
		yield return new WaitForSeconds(comeOutDelay);
		
		isDeal = true;
		spriteRenderer.color = new Color(1f, 0.5f, 0.5f);
		patternIndex = 0;
		

		PatternNext();
    }

	public virtual void BossKilled()
	{
		ShutdownAction();
	}

	
	public void PatternStart()
	{
		currentPattern = startPattern;
		currentPattern.StartAct();
	}
	public void PatternNext()
	{
		var targetList = isDeal ? dealTimePatternList : patternList;
		patternIndex = isDeal? GetNextDealPatternIndex(patternIndex) : GetNextPatternIndex(patternIndex);
		currentPattern = targetList[patternIndex];
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
		anim =GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
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
		bossCollider = GetComponent<Collider2D>();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDeal && collision.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
			playerHealth.OnDamage();
        }
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

	protected virtual int GetNextDealPatternIndex(int _currentIndex)
	{
		int result = _currentIndex;
		if (result >= dealTimePatternList.Count - 1)
		{
			isDeal = false;
			patternIndex = 0;
			ShutdownAction();
			PatternNext();
		}
		else
		{
			++result;
		}
		return result;
	}

    #endregion
}
