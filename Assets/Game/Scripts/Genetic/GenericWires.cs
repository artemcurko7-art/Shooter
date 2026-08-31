using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GeneticWires : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject wirePrefab; // Сюда перетащи префаб WirePrefab
    public Transform parentContainer; // Сюда перетащи WiresContainer (или сам Genetics)
    
    private List<GameObject> _activeWires = new List<GameObject>();

    // Вызывай этот метод каждый раз, когда статы меняются или появляются
    public void UpdateWires(List<RectTransform> statBars)
    {
        // 1. Удаляем старые провода
        foreach (var wire in _activeWires)
        {
            if (wire != null) Destroy(wire);
        }
        _activeWires.Clear();

        if (statBars == null || statBars.Count < 2) return;

        // 2. Рисуем новые провода между соседними статами
        for (int i = 0; i < statBars.Count - 1; i++)
        {
            RectTransform startRect = statBars[i];
            RectTransform endRect = statBars[i + 1];

            // Получаем мировые позиции центров статов
            Vector3 startPos = startRect.TransformPoint(startRect.rect.center);
            Vector3 endPos = endRect.TransformPoint(endRect.rect.center);

            // Создаем провод
            GameObject newWire = Instantiate(wirePrefab, parentContainer);
            _activeWires.Add(newWire);

            RectTransform wireRect = newWire.GetComponent<RectTransform>();
            Image wireImage = newWire.GetComponentInChildren<Image>();

            // Вычисляем длину и угол
            Vector3 diff = endPos - startPos;
            float length = diff.magnitude;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            // Настраиваем провод
            wireRect.position = startPos; // Ставим начало в центр первого стата
            wireRect.sizeDelta = new Vector2(length, 2); // Длина = дистанция, ширина = 2 пикселя (меняй как надо)
            wireRect.localEulerAngles = new Vector3(0, 0, angle); // Поворачиваем
            wireRect.pivot = new Vector2(0, 0.5f); // Важно: якорь слева по центру, чтобы он рос вправо
            
            // Опционально: меняем цвет для заблокированных/активных статов
            // wireImage.color = isUnlocked ? Color.green : Color.gray;
        }
    }
}
