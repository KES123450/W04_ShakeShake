using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossPhase0 : MonoBehaviour,IDamageable
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

	[SerializeField] private Vector2 respawnPoint;
	[SerializeField] protected int hpCurrent;
	[SerializeField] protected int hpMax;
	[SerializeField] protected BossPattern startPattern;
	[SerializeField] protected List<BossPattern> patternList = new List<BossPattern>();
	[SerializeField] protected List<BossPattern> dealTimePatternList = new List<BossPattern>();

	[SerializeField] protected int patternIndex;
	[SerializeField] protected BossPattern currentPattern;

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
	public void OnDamage(int damage = 1)
	{
        if (!isDeal)
        {
			StartCoroutine(nameof(StartOnWeak));
        }
        else
        {
			StartCoroutine(nameof(StartNextPhase));
		}
		
	}


	public void StartOnWeak()
	{
		StartCoroutine(nameof(OnWeak));
	}
	public IEnumerator OnWeak()
	{
		ShutdownAction();
		anim.Play("Boss_magic_half_angry");
		yield return new WaitForSeconds(angryDelay);
		anim.Play("Boss_come_out");
		yield return new WaitForSeconds(comeOutDelay);

		isDeal = true;
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
		patternIndex = isDeal ? GetNextDealPatternIndex(patternIndex) : GetNextPatternIndex(patternIndex);
		currentPattern = targetList[patternIndex];
		currentPattern.StartAct();
	}

	public void ShutdownAction()
	{
		currentPattern.ShutdownAction();
	}
	#endregion

	protected virtual void Awake()
	{
		anim = GetComponent<Animator>();
	}
	protected virtual void OnEnable()
	{
		transform.position = respawnPoint;
	}
	protected virtual void Start()
	{
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

	[SerializeField] private Boss nextBossPhase;
    [SerializeField] private int nowBossHP;
    [SerializeField] private float moveToCenterSpeed;
    [SerializeField] private string animationName;
    [SerializeField] private float delay;
    
    private IEnumerator StartNextPhase()
    {
        if (isDeal)
        {
            UIManager.Instance.SetBossHP(nowBossHP);
            GameManager.instance.SetBoss(nextBossPhase.gameObject);
            ShutdownAction();
            anim.Play(animationName);
            yield return new WaitForSeconds(delay);
            Vector3 center = Vector3.zero;
            transform.DOMove(center, moveToCenterSpeed)
                .OnComplete(() =>
                {

                    nextBossPhase.transform.position = transform.position;
                    nextBossPhase.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    nextBossPhase.Initialize();
                });
        }
    }


}
