using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Selectable : MonoBehaviour
{
    [SerializeField] private SelectionType selectionType;
    [SerializeField] private Color selectedColor;
    
    [SerializeField] private ParticleSystem selectParticle;
    private Renderer renderer;
   
    private int emissionID;
    private Animator anim;
    private Outline otutline;
     
     
    void Start()
    {
        renderer = GetComponent<Renderer>();
        emissionID = Shader.PropertyToID("_EmissionColor");

        if(GetComponent<Animator>() != null && selectionType == SelectionType.animation)
        {
            anim = GetComponent<Animator>();
        }
        if (GetComponent<Outline>())
        {
            otutline = GetComponent<Outline>();
        }
    }

    public void SelectObject()
    {
        switch (selectionType)
        {
            case SelectionType.colorEmission:
                renderer.material.EnableKeyword("_EMISSION");
                renderer.material.SetColor(emissionID, selectedColor);
                break;
            case SelectionType.animation:
                anim.SetBool("selected", true);
            break;
            case SelectionType.effect:
                ParticleSystem.EmissionModule em= selectParticle.emission;
                em.rateOverTime = 10;
            break;
            case SelectionType.outline:
                otutline.enabled = true;
                break;
            default:
                renderer.material.EnableKeyword("_EMISSION");
                renderer.material.SetColor(emissionID, selectedColor);
                break;
        }
    }
    
    public void DeselectObject()
    {
        switch (selectionType)
        {
            case SelectionType.colorEmission:
                renderer.material.DisableKeyword("_EMISSION");
                break;
            case SelectionType.animation:
                anim.SetBool("selected", false);
                break;
            case SelectionType.effect:
                ParticleSystem.EmissionModule em = selectParticle.emission;
                em.rateOverTime = 0;
                break;
            case SelectionType.outline:
                otutline.enabled = false;
                break;
            default:
                renderer.material.DisableKeyword("_EMISSION");
                break;
        }
    }
}
