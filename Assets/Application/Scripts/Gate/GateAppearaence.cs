using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DeformationType { 
    Damage,
    LifeTime,
    DoubleShootMode,
    TripleShootMode,
    BulletSpeed
}

public class GateAppearaence : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Image _topImage;
    [SerializeField] Image _downImage;
    [SerializeField] Image _glassImage;

    [Header("Colors")]
    [SerializeField] Color _colorPositive;
    [SerializeField] Color _colorNegative;

    [Header("Title Icons")]
    // Иконки увеличения/уменьшения ширины
    [SerializeField] GameObject _expandLable;
    [SerializeField] GameObject _shrinkLable;
    // Иконки увеличения/уменьшения высоты
    [SerializeField] GameObject _upLable;
    [SerializeField] GameObject _downLable;

    [Header("Sprites")]
    // Изображения типа стрельбы
    [SerializeField] Sprite _doubleShootSprite;
    [SerializeField] Sprite _tripleShootSprite;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI _downText;
    [SerializeField] TextMeshProUGUI _topText;

    private static readonly string DamageText = "Урон";
    private static readonly string LifeTimeText = "Дальность выстрела";
    private static readonly string DoubleShootModeText = "Двойной выстрел";
    private static readonly string TripleShootModeText = "Тройной выстрел";
    private static readonly string BulletSpeedText = "Скорость выстрела";


    public void UpdateVisual(DeformationType deformationType, int value)
    {
        string prefix = "";

        
        if (value == 0) 
        {
            SetColor(Color.grey);
        } 
        else if (value > 0)
        {
            prefix = "+";
            SetColor(_colorPositive);
        }
        else
        {
            SetColor(_colorNegative);
        }

        _downText.text = prefix + value.ToString();

        _expandLable.SetActive(false);
        _shrinkLable.SetActive(false);
        _upLable.SetActive(false);
        _downLable.SetActive(false);

        _topText.text = deformationType switch
        {
            DeformationType.Damage => DamageText,
            DeformationType.LifeTime => LifeTimeText,
            DeformationType.DoubleShootMode => DoubleShootModeText,
            DeformationType.TripleShootMode => TripleShootModeText,
            DeformationType.BulletSpeed => BulletSpeedText,
            _ => string.Empty,
        };

        _downImage.sprite = deformationType switch
        {
            DeformationType.DoubleShootMode => _doubleShootSprite,
            DeformationType.TripleShootMode => _tripleShootSprite,
            _ => null
        };
    }

    void SetColor(Color color) {
        _topImage.color = color;
        _glassImage.color = new Color(color.r, color.g, color.b, 0.5f);
    }

}


