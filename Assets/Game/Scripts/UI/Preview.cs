using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using static Game.Scripts.Character.Genetic.StatsData;

[RequireComponent(typeof(PreviewTransition))]
public class Preview : MonoBehaviour
{
    [SerializeField] private Image _iconFrame;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _buyStatButton;

    private PreviewTransition _transition;

    private void Awake()
    {
        _transition = GetComponent<PreviewTransition>();
    }

    public void Open(Stat stat, Vector3 startPosition)
    {
        _icon.sprite = stat.icon;
        _title.text = stat.GetLocalizedName(YG2.lang);

        _transition.Open(startPosition);
    }
}
