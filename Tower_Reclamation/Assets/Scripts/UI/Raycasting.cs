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
    Singleton singleton;


    RaycastHit raycastHit;
    public RaycastHit hit
    {
        get { return raycastHit; }
    }

    Layer layerHit;
    public Layer currentLayerHit
    {
        get { return layerHit; }
    }

    public delegate void OnLayerChange();  //  declare the delegate type
    public OnLayerChange layerChangeObservers;

    private void Start()
    {
        singleton = FindObjectOfType<Singleton>();
        viewCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //print(layerHit);
            print(raycastHit.collider.name);
            singleton.SetPreferedEnemy(raycastHit.collider.GetComponentInChildren<EnemyHealth>());
                //preferedTargetEnemy = raycastHit.collider.GetComponentInChildren<EnemyHealth>();
        }

        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                raycastHit = hit.Value;
                if (layerHit != layer) // if layer has changed
                {
                    layerHit = layer;
                    layerChangeObservers(); // call the delegates
                }
                layerHit = layer;
                return;
            }
        }

        //FindObjectOfType<CursorIcons>().PrintLayerHit();
        // Otherwise return background hit
        raycastHit.distance = distanceToBackground;
        layerHit = Layer.RaycastEndStop;
    }



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


    //private void OnMouseDown()
    //{
    //    print(hit.collider.name);
    //}
}







/*
private void Start()
{
    viewCamera = Camera.main;
    layerChangeObservers += SomeLayerChangeHandler;
    layerChangeObservers();
}

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
    FindObjectOfType<CursorIcons>().PrintLayerHit();
}

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
} */
