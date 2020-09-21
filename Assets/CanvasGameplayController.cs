using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplayController : MonoBehaviour
{
    private Text moneyPlayer1;
    private Text moneyPlayer2;

    private RawImage imagePlayer1;
    private RawImage imagePlayer2;

    private Image bonusBar1;
    private Image bonusBar2;

    private Text bonusText1;
    private Text bonusText2;

    private GameObject panelBonusP1;
    private GameObject panelBonusP2;

    private static CanvasGameplayController instance = null;

    private void Awake()
    {
        // Porquesi
        if (instance == null)
            instance = this;
    }

    public static CanvasGameplayController Instance {
        get
        {
            instance.gameObject.SetActive(true);
            return instance;
        }
    }
    
    private void Start()
    {
        var texts = GetComponentsInChildren<Text>();
        var rawImages = GetComponentsInChildren<RawImage>();
        var images = GetComponentsInChildren<Image>();

        moneyPlayer1 = texts.Single(x => x.name == "TextMoneyP1");
        moneyPlayer2 = texts.Single(x => x.name == "TextMoneyP2");
        
        imagePlayer1 = rawImages.Single(x => x.name == "ImageP1");
        imagePlayer2 = rawImages.Single(x => x.name == "ImageP2");

        bonusBar1 = images.Single(x => x.name == "BonusMoneyBarP1");
        bonusBar2 = images.Single(x => x.name == "BonusMoneyBarP2");
        
        bonusText1 = texts.Single(x => x.name == "BonusMoneyTextP1");
        bonusText2 = texts.Single(x => x.name == "BonusMoneyTextP2");

        var paneles = FindObjectsOfType<GameObject>().Where(x => x.name.Contains("Panel"));

        panelBonusP1 = paneles.Single(x => x.name == "PanelBonusP1");
        panelBonusP2 = paneles.Single(x => x.name == "PanelBonusP2");

        panelBonusP1.SetActive(false);
        panelBonusP2.SetActive(false);
        
        instance.gameObject.SetActive(false);
    }

    public void SetDinero(Player player, string money)
    {
        if (player.IdPlayer == 1)
            moneyPlayer2.text = money;
        if(player.IdPlayer == 0)
            moneyPlayer1.text = money;
    }

    public void SetTexture(Player player, Texture2D texture) {
        if (player.IdPlayer == 0)
            imagePlayer1.texture = texture;
        if (player.IdPlayer == 1)
            imagePlayer2.texture = texture;
    }

    public void BonusActive(Player player, bool active)
    {
        if (player.IdPlayer == 0)
            panelBonusP1.SetActive(active);
        if (player.IdPlayer == 1)
            panelBonusP2.SetActive(active);
    }

    public void SetBonus(Player player, float percent, string money) {
        BonusActive(player, true);

        if (player.IdPlayer == 0)
        {
            bonusBar1.fillAmount = percent;
            bonusText1.text = money;
        }
        if (player.IdPlayer == 1)
        {
            bonusBar2.fillAmount = percent;
            bonusText2.text = money;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
        Destroy(this);
    }
}
