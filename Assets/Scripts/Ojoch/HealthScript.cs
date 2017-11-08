using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Skript řešící životy, příčetnost a zranění
/// </summary>
public class HealthScript : MonoBehaviour {
    /// <summary>
    /// Počet Ojochových životů
    /// </summary>
    public int hp = 1;           
    /// <summary>
    /// Maximální počet životů, které může Ojoch mít
    /// </summary>
    public int maxHP = 6;
    /// <summary>
    /// Ojochova příčetnost
    /// </summary>
    public int sanity = 3;                
    /// <summary>
    /// Reference na OjochScript
    /// </summary>
    OjochScript ojoch;   
    
    [Header("UI reference a listy UI objektů")]
    /// <summary>
    /// List se všemi objekty ojochových životů
    /// </summary>
    public List<GameObject> healthObjects;
    /// <summary>
    /// List se všemi objekty ojochovy příčetnosti
    /// </summary>
    public List<GameObject> sanityObjects;

    /// <summary>
    /// Kontejner, do kterého se budou přidávat všechna Ojocho UI srdce
    /// </summary>
    public GameObject healthContainer;
    /// <summary>
    /// Kontejner, kde se přidají všechny Ojochovy UI mozky
    /// </summary>
    public GameObject sanityContainer;
    
    /// <summary>
    /// Preloadeujeme si prefaby životů a možků, abychom neustále nevolali Resources.Load
    /// </summary>
    void Awake()
    {
        if (GameManager.instance.healthObj == null)
        {
            GameManager.instance.healthObj = Resources.Load("UI Elements/life") as GameObject;
        }

        if (GameManager.instance.sanityObj == null)
        {
            GameManager.instance.sanityObj = Resources.Load("UI Elements/sanity") as GameObject;
        }
    }
    void Start() {
        ojoch = OjochManager.instance.ojochScript;
        StartingHearts();
        StartingSanity();
        hp = maxHP;            
    }

    /// <summary>
    /// Přidá do UI startovní počet ikons srdcí
    /// </summary>
    void StartingHearts()
    {
        for (int i = 0; i < maxHP; i++)
        {
            CreateHeart();
        }
    }
    /// <summary>
    /// Metoda je zavolána při léčení. Podle počtu vyléčených životů přidá stejný počet srdcí do UI 
    /// </summary>
    void CheckHearts()
    {
        for (int i = hp; i <= maxHP; i++)
        {
            CreateHeart();
        }
    }
    /// <summary>
    /// Metoda vytvoří UI srdce na obrazovce (dá jej do containeru)
    /// </summary>
    void CreateHeart()
    {
        GameObject o = Instantiate(GameManager.instance.healthObj);
        o.transform.SetParent(healthContainer.transform);
        healthObjects.Add(o);
    }
    /// <summary>
    /// Odstraní poslední srdce v containeru srdcí
    /// </summary>
    public void RemoveHeart()
    {
        int last = healthObjects.Count - 1;

        if (last < 0)
            return;

        GameObject o = healthObjects[last];
        healthObjects.Remove(o);
        o.GetComponent<HS_Script>().animator.SetTrigger("Remove");
    }

    /// <summary>
    /// Přidá do UI startovní počet ikon mozků
    /// </summary>
    void StartingSanity()
    {
        for (int i = 0;  i < sanity-1; i++)
        {
            GameObject o = Instantiate(GameManager.instance.sanityObj);
            o.transform.SetParent(sanityContainer.transform);
            sanityObjects.Add(o);
        }
    }
    
    /// <summary>
    /// Odstraní jednu ikonu mozku z UI
    /// </summary>
    public void RemoveSanity()
    {
        //ještě jsme nepřišli o žádný mozek, zobrazíme tedy Ojochu příčetnost jako UI
        if (!sanityContainer.activeSelf)
        {
            sanityContainer.SetActive(true);
            return;
        }
            
        int last = sanityObjects.Count - 1;

        if (last < 0)
            return;

        GameObject o = sanityObjects[last];
        sanityObjects.Remove(o);
        o.GetComponent<HS_Script>().animator.SetTrigger("Remove");
    }

    /// <summary>
    /// Dá Ojochovi zranění a upraví ukazatel životů. Pokud ještě nezemřel, nastaví na dvě vteřiny nesmrtelnost, aktivuje smradoštít. Jinak začne courotine DieOjoch.
    /// Pokud dostane kladné zranění: Obnoví životy na 6 a upraví dle toho ukazatele.
    /// </summary>
    /// <param name="damage">Dané zranění</param>
    public void Damage(int damage)
    {
        //dáváme zranění
        if (damage > 0)
        {
            hp -= damage;
            ojoch.godMode = 2;
            ojoch.powerCombo.effects.smradostit.SetActive(true);
            OjochManager.instance.sprite.active = true;

            RemoveHeart();
            if (hp <= 0)
            {
                GameManager.instance.GetComponent<SoundManager>().PlaySound(GameManager.instance.GetComponent<SoundManager>().clipOjochDeath);
                ojoch.animator.SetTrigger("dead");
            }
        }
        //léčíme Ojocha
        else
        {
            CheckHearts();
            hp = 6;
        }
    }
    /// <summary>
    /// Funkce ojochovi ubere jednu příčetnost a následně smaže jeden mozek z UI
    /// </summary>
    /// <param name="damage">počet ztracené příčetnosti</param>
    public void LooseSanity(int damage) {
        if (sanity > 0)
        {
            sanity -= damage;
            RemoveSanity();

            if (sanity <= 0)
            {
                SessionController.instance.GetComponent<EndGameScript>().sanityLost = true;
                SessionController.instance.GetComponent<ShowingEffects>().soufl.SetActive(true);
                ojoch.zakaleniTime = 1;
            }
        }
    }    
}
