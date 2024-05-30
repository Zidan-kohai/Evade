using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour, ISee
{
    
    [SerializeField] private ReachArea area;
    [SerializeField] private CinemachineVirtualCamera firstPerson;
    [SerializeField] private CinemachineVirtualCamera thirdPerson;
    [SerializeField] private int enemyCount;
    private Sequence firstSequence; 
    private Sequence thirdSequence;

    [Header("Parametrs")]
    public int Duration = 10;
    public int Strength = 1;
    public int Vibrato = 5;
    public int Randomness = 180;
    public bool Snapping = false;
    public bool FadeOut = true;
    public ShakeRandomnessMode shakeRandomlessMode;
    public void Start()
    {
        area.SetISee(this);
    }

    public void AddHumanoid(IHumanoid IHumanoid)
    {
        if(IHumanoid.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemyCount++;
            Shke();
        }
    }

    public void RemoveHumanoid(IHumanoid IHumanoid)
    {
        if (IHumanoid.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemyCount--;

            if(enemyCount <= 0)
            {
                thirdSequence.Kill();
                firstSequence.Kill();
            }
        }
    }

    [ContextMenu("Shake")]
    private void Shke()
    {
        firstSequence = DOTween.Sequence().Append(firstPerson.transform.DOShakePosition(Duration, Strength, Vibrato, Randomness, Snapping, FadeOut, shakeRandomlessMode))
            .Join(firstPerson.transform.DOShakeRotation(Duration, Strength, Vibrato, Randomness, FadeOut, shakeRandomlessMode));

        thirdSequence = DOTween.Sequence().Append(thirdPerson.transform.DOShakePosition(Duration, Strength, Vibrato, Randomness, Snapping, FadeOut, shakeRandomlessMode))
            .Join(thirdPerson.transform.DOShakeRotation(Duration, Strength, Vibrato, Randomness, FadeOut, shakeRandomlessMode));
    }
}
