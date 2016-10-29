using UnityEngine;
using System.Collections;

/// <summary>
/// Typy všech nepřátel a bossů ve hře.
/// </summary>
public enum EnemyType
{
    /// <summary>
    /// Agresivní žlutý pták
    /// </summary>
    bird,
    /// <summary>
    /// Krysa v botě s dynamitem.
    /// </summary>
    rat,
    /// <summary>
    /// Veverka na sově, házející ořechy.
    /// </summary>
    squirrel,
    /// <summary>
    /// Sputnik, střílící naváděné vodky.
    /// </summary>
    sputnik,
    /// <summary>
    /// Vesmírné prase, střílící lazery.
    /// </summary>
    pig,
    /// <summary>
    /// Sojka se dvěma klobouky a zlatým procentem.
    /// </summary>
    sojka,
    /// <summary>
    /// Duch Přepepře smradinoha.
    /// </summary>
    prepepr,    
    /// <summary>
    /// Socha, pronásledující Ojocha.
    /// </summary>
    statue,
    /// <summary>
    /// Defaultně žádný, nutno změnit v inspektoru.
    /// </summary>
    none
}

/// <summary>
/// Skript má na starost záležitosti týkající se životů nepřátel.
/// </summary>
public class EnemyHealth : MonoBehaviour {
    /// <summary>
    /// Typ nepřítele
    /// </summary>
    public EnemyType enemyType = EnemyType.none;
    /// <summary>
    /// Životy
    /// </summary>
    public int hp = 1;
    /// <summary>
    /// Kolik hráč dostane skóre za zabití
    /// </summary>
    public int score = 50;

    //Nevím jestli budu vůbec potřebovat... uvidíme.
    /// <summary>
    /// Je tohle boss?
    /// </summary>
    public bool isBoss = false;
   
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
            EnemyDeathSound(enemyType);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetTrigger("bDeath");

            if(!GetComponent<EnemyAI>().exploded)
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
