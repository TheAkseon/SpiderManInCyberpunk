using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GateType { 
    Damage,
    LifeTime,
    FiringFrequency,
    SingleShootMode,
    DoubleShootMode,
    TripleShootMode
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
    // ������ ����������/���������� ������
    [SerializeField] GameObject _expandLable;
    [SerializeField] GameObject _shrinkLable;
    // ������ ����������/���������� ������
    [SerializeField] GameObject _upLable;
    [SerializeField] GameObject _downLable;

    [Header("Sprites")]
    // ����������� ���� ��������
    [SerializeField] Sprite _singleShootSprite;
    [SerializeField] Sprite _doubleShootSprite;
    [SerializeField] Sprite _tripleShootSprite;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI _downText;
    [SerializeField] TextMeshProUGUI _topText;

    private static readonly string DamageText = "����";
    private static readonly string LifeTimeText = "��������� ��������";
    private static readonly string FiringFrequencyText = "������� ��������";
    private static readonly string SingleShootModeText = "��������� �������";
    private static readonly string DoubleShootModeText = "������� �������";
    private static readonly string TripleShootModeText = "������� �������";


    public void UpdateVisual(GateType deformationType, float value)
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

        _topText.text = deformationType switch
        {
            GateType.Damage => DamageText,
            GateType.LifeTime => LifeTimeText,
            GateType.FiringFrequency => FiringFrequencyText,
            GateType.SingleShootMode => SingleShootModeText,
            GateType.DoubleShootMode => DoubleShootModeText,
            GateType.TripleShootMode => TripleShootModeText,
            _ => string.Empty,
        };

        _downText.text = deformationType switch
        {
            GateType.FiringFrequency => prefix + value.ToString() + "/c",
            GateType.SingleShootMode => string.Empty,
            GateType.DoubleShootMode => string.Empty,
            GateType.TripleShootMode => string.Empty,
            _ => prefix + value.ToString()
        };

        _downImage.sprite = deformationType switch
        {
            GateType.SingleShootMode => _singleShootSprite,
            GateType.DoubleShootMode => _doubleShootSprite,
            GateType.TripleShootMode => _tripleShootSprite,
            _ => null
        };
    }

    void SetColor(Color color) {
        _topImage.color = color;
        color.a = 0.5f;
        _downImage.color = color;
        _glassImage.color = new Color(color.r, color.g, color.b, 0.5f);
    }

}


