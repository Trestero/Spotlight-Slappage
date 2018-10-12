using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0, .8f, 0);

    private Transform trackTarget;
    private Player player;

    private Camera cam;

    // Use this for initialization
    void Start () {
	}

    public void SetTarget(Player plyr)
    {
        player = plyr;
        trackTarget = plyr.AttachedCharacter.transform;
        GetComponentInChildren<UnityEngine.UI.Text>().text = player.Name;

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        GetComponent<UnityEngine.UI.Image>().color = ConfigInfo.GetColor(player.Index - 1);
    }

    // Update is called once per frame
    void Update ()
    {
        // if there's a target to seek towards
        if (trackTarget != null)
        {
            GetComponent<RectTransform>().anchoredPosition = cam.WorldToScreenPoint(trackTarget.position + offset);
        }
    }
}
