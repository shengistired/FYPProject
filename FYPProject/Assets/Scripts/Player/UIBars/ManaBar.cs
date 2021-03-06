using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour, IDataPersistence
{
    public Slider manaBar;
    public PlayerStat playerStat;
    public Text manaText;
    public float totalMana;
    private float currentMana;
    private float regenSpeed;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static ManaBar instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {



        totalMana = playerStat.MaxManaBar;
        manaText.text = (int)currentMana + " / " + totalMana;

        //Debug.Log("currentMana"+currentMana);
        //Debug.Log("totalMana"+totalMana);

        manaBar.maxValue = totalMana;
        manaBar.value = currentMana;
        regenMana();

    }





    public void onIntUp(float maxMana)
    {
        totalMana = maxMana;
        //player's total mana on slider
        manaBar.maxValue = totalMana;
        manaText.text = (int)currentMana + " / " + totalMana;

        regenMana();
    }

    public bool useMana(int mana)
    {
        if (currentMana > mana)
        {
            currentMana -= mana;
            manaBar.value = currentMana;
            manaText.text = (int)currentMana + " / " + totalMana;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regenMana();
            return true;
        }
        else
        {
            Debug.Log("Not enough mana!");
            return false;
        }
    }

    private void regenMana()
    {
        //mana regen
        if (currentMana == totalMana)
        {
            if (regen != null)
            {
                StopCoroutine(regen);
                regen = null;
            }

        }

        else
        {
            regen = StartCoroutine(manaRegen());
        }

    }

    public void onManaRegenSkillUp(float manaRegen)
    {
        regenSpeed = manaRegen;
    }

    //manaRegen
    private IEnumerator manaRegen()
    {
        regenSpeed = playerStat.manaRegen;
        yield return new WaitForSeconds(2);
        while (currentMana < totalMana)
        {
            // replace bottom with this for the mana regen you set in playerstat.cs >>> currentMana += playerStat.manaRegen;
            //currentMana += totalMana / 100;
            //Debug.Log("mana regening speed is " + regenSpeed);
            currentMana += regenSpeed;
            manaBar.value = currentMana;
            manaText.text = (int)currentMana + " / " + totalMana;
            yield return regenTick;
        }
        regen = null;
    }

     public void recoverManaFull()
    {
        currentMana = totalMana;
    }

    public void LoadData(GameData data)
    {
        currentMana = data.currentMana;
    }

    public void SaveData(ref GameData data)
    {
        data.currentMana = currentMana;
    }
}
