using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] int numChainSections = 5;
    [SerializeField] GameObject chainLinkPrefab;

    List<GameObject> chainSections = new List<GameObject>();
    GameObject body;

    void Start()
    {
        body = FindObjectOfType<Body>().gameObject;

        for (int i = 0; i < numChainSections; i++)
        {
            GameObject chainLink = Instantiate(chainLinkPrefab, transform);
            chainSections.Add(chainLink);
        }
    }

    void Update()
    {
        for (int i = 0; i < chainSections.Count; i++)
        {
            chainSections[i].transform.position = Vector2.Lerp(transform.position, body.transform.position, (float)i / (float)(numChainSections));
        }
    }
}
