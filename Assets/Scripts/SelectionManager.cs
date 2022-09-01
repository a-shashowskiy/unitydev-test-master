using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    // [SerializeField] private Color selectedColor; 
    // Краще інєкцію чи взяття із бази ресурсів, наприклад ScriptableObject для зручності налаштування геймдизайнером,
    // ще можливо розширення по типу обєктів то що.
    // Класу не потрібно відповідати за саму актиацію виду вибору, і згідно приниипів единої відповідальності це перенесено в інший скрипт
    // що вже вмикає чи вимикає потрібний тип від налаштувань обьєкту.
    // private int emissionID;
    private Selectable lastSelected;
    private Selectable selected;

    //private void Awake() => emissionID = Shader.PropertyToID("_EmissionColor");

    private void Update()
    {
        if (!selected)
        {
            DisableLastSelected();
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            if (hit.transform.CompareTag(selectableTag))
            {
                DisableLastSelected();
                lastSelected = selected = hit.transform.GetComponent<Selectable>();
            }
            else
                selected = null;
        }
        else
            selected = null;

        if (selected)
        {
            selected.SelectObject();

            //var selectedRenderer = selected.GetComponent<Renderer>();
            //selectedRenderer.material.EnableKeyword("_EMISSION");
            //selectedRenderer.material.SetColor(emissionID, selectedColor);
        }
    }

    private void DisableLastSelected()
    {
        lastSelected?.DeselectObject();

        //lastSelected?.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }
}
