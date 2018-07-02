using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour {

    public Layer[] layerPriorities =
    {
        Layer.Enemy,
        Layer.Tower,
        Layer.Waypoint
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;


    RaycastHit m_hit;
    public RaycastHit Hit
    {
        get { return m_hit; }
    }

    Layer m_layerHit; 
    public Layer LayerHit
    {
        get { return m_layerHit;  }
    }

    private void Start()
    {
        viewCamera = Camera.main;
        layerChangeObservers += SomeLayerChangeHandler;
        layerChangeObservers();
    }

    public delegate void OnLayerChange();  //  declare the delegate type
    public OnLayerChange layerChangeObservers;

    void SomeLayerChangeHandler()
    {
        print("I handled it");
    }

    
    private void OnMouseDown()
    {
        foreach (Layer layer in layerPriorities)
        {
            var Hit = RaycastForLayer(layer);
            if (Hit.HasValue)
            {
                m_hit = Hit.Value;
                m_layerHit = layer;
                print(m_hit);
                return;
            }
        }

        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.RaycastEndStop;
        FindObjectOfType<CursorIcons>().PrintLayerHit();
    }
    
    /*
    private void Update()
    {
        foreach (Layer layer in layerPriorities)
        {
            var Hit = RaycastForLayer(layer);
            if (Hit.HasValue)
            {
                m_hit = Hit.Value;
                m_layerHit = layer;
                return;
            }
        }

        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.RaycastEndStop;
    }
    */
    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
