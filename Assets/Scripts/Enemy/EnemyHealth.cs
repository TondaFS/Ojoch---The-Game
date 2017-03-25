using UnityEngine;
using System.Collections;


/// <summary>
/// Skript má na starost záležitosti týkající se životů nepřátel.
/// </summary>
public class EnemyHealth : MonoBehaviour {   
    /// <summary>
    /// Životy
    /// </summary>
    public int hp = 1;
    /// <summary>
    /// Kolik hráč dostane skóre za zabití
    /// </summary>
    public int score = 50;
       
    /// <summary>
    /// Dá nepříteli zranění. Pokud následkem toho přijde o svůj zbývající život,
    /// spustí animaci a zvuk smrti, přidá Ojochovi skóre a zajistí, aby nepřítel zemřel.
    /// V opačném případě zajistí, aby nepřítel červeně blikl.
    /// </summary>
    /// <param name="damage">Jaké zranění něpřítel dostane</param>
    public void EnemyDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            EnemyDeathSound(GetComponent<CommonAI>().enemyType);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetTrigger("bDeath");

            if(GetComponent<CommonAI>().enemyType == EnemyType.rat && GetComponent<RatAI>().exploded)
            {
                SessionController.instance.GetComponent<ScoreScript>().UpdateScoreStuff(score, 0, 1, true);
                SessionController.instance.GetComponent<EndGameScript>().enemyInSession += 1;
            }
        }
        else
        {
            GetComponent<EnemyHit>().SetRedColor();
            GetComponent<EnemyHit>().isHit = true;
        }
    }
    
    /// <summary>
    /// Přehraje odpovídající zvuk smrti daného nepřítele
    /// </summary>
    /// <param name="enemy">Typ nepřítele</param>
    public void EnemyDeathSound(EnemyType enemy)
    {
        switch (enemy)
        {
            case EnemyType.bird:
                GameManager.instance.GetComponent<SoundManager>().PlaySoundPitchShift(GameManager.instance.GetComponent<SoundManager>().birdDeath);
                break;
            case EnemyType.squirrel:
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().squirrelDeath);
                break;
            case EnemyType.sputnik:
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().pokoutnikDeath);
                break;
            case EnemyType.rat:
                GameManager.instance.GetComponent<SoundManager>().PlaySoundPitchShift(GameManager.instance.GetComponent<SoundManager>().ratDeath);
                break;
        }
    }
}
