using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Skript řešící životy, příčetnost a zranění
/// </summary>
public class HealthScript : MonoBehaviour {
    /// <summary>
    /// Počet Ojochových životů
    /// </summary>
    public int hp = 1;              //pocet zivotu
    /// <summary>
    /// Ojochova příčetnost
    /// </summary>
    public int sanity = 3;         //Pocet pricetnosti          
    /// <summary>
    /// Reference na OjochScript
    /// </summary>
    OjochScript ojoch;   

    //Ojochovy staty
    [Space(10, order = 0)]
    [Header("OJOCHOVY REFERENCE NA UI", order = 1)]
    [Space(5, order = 3)]
    public GameObject sanityOne;
    public GameObject sanityTwo;
    public GameObject sanityThree;

    public GameObject healthOne;
    public GameObject healthTwo;
    public GameObject healthThree;
    public GameObject healthFour;
    public GameObject healthFive;
    public GameObject healthSix;
    

    void Start() {
        ojoch = OjochManager.instance.ojochScript;               
    }          

    /// <summary>
    /// Dá Ojochovi zranění a upraví ukazatel životů. Pokud ještě nezemřel, nastaví na dvě vteřiny nesmrtelnost, aktivuje smradoštít. Jinak začne courotine DieOjoch.
    /// Pokud dostane kladné zranění: Obnoví životy na 6 a upraví dle toho ukazatele.
    /// </summary>
    /// <param name="damage">Dané zranění</param>
    public void Damage(int damage)
    {
        bool smth;
        if (damage > 0)
        {
            hp -= damage;
            smth = false;
            ojoch.godMode = 2;
            ojoch.powerCombo.effects.smradostit.SetActive(true);
            OjochManager.instance.sprite.active = true;
            switch (hp)
            {
                case 6:
                    damage = 0;
                    break;
                case 5:
                    healthSix.SetActive(smth);
                    break;
                case 4:
                    healthFive.SetActive(smth);
                    break;
                case 3:
                    healthFour.SetActive(smth);
                    break;
                case 2:
                    healthThree.SetActive(smth);
                    break;
                case 1:
                    healthTwo.SetActive(smth);
                    break;
                case 0:
                    healthOne.SetActive(smth);
                    GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipOjochDeath);
                    ojoch.animator.SetTrigger("dead");
                    //StartCoroutine(DieOjoch());
                    break;
            }
        }
        else
        {
            smth = true;
            hp = 6;
            healthTwo.SetActive(smth);
            healthThree.SetActive(smth);
            healthFour.SetActive(smth);
            healthFive.SetActive(smth);
            healthSix.SetActive(smth);
        }
    }

    //Funkce, ktera ojochovi ubira pricetnost a nasledne upravuje pocet mozku ve hre
    public void LooseSanity(int damage) {
        if (sanity > 0)
        {
            sanity -= damage;
            switch (sanity)
            {
                case 3:
                    ojoch.sanityBar.SetActive(true);                    
                    break;
                case 2:
                    sanityThree.SetActive(false);
                    break;
                case 1:
                    sanityTwo.SetActive(false);
                    break;

                //Pokud ojoch ztrati veskerou pricetnost, informace o tom se ulozi a spusti se efekt soufl
                case 0:
                    sanityOne.SetActive(false);                    
                    SessionController.instance.GetComponent<EndGameScript>().sanityLost = true;
                    SessionController.instance.GetComponent<ShowingEffects>().soufl.SetActive(true);
                    ojoch.zakaleniTime = 1;
                    break;
            }
            
        }
    }

    //Funkce pri smrti Ojocha, kde se nejdrive pocka na prehrani animace a teprve pak se vypne hudba a spusti funkce na konci hry
    private IEnumerator DieOjoch()
    {        
        yield return new WaitForSeconds(0.75f);        
    }

    
}
