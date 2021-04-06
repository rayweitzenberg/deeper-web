using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// https://catlikecoding.com/unity/tutorials/basics/building-a-graph/
// https://learn.unity.com/tutorial/introduction-to-scriptable-objects#5cf187b7edbc2a31a3b9b123
// https://en.wikipedia.org/wiki/The_Raven_(Lou_Reed_album)


public class Targets_01 : MonoBehaviour
{
    [SerializeField]
    Transform targetPrefab = default;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    public Transform[] targets;


    void Start()
    {
        float step = 2f / resolution;
        var position = Vector3.zero;
        //var scale = Vector3.one * step;
        var scale = Vector3.one * 0.07f;

        targets = new Transform[resolution];
        for (int i = 0; i < targets.Length; i++)
        {
            Transform oneTarget = Instantiate(targetPrefab);
            oneTarget.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            position.x = (i + 0.5f) * step - 1f;
            oneTarget.localPosition = position;
            oneTarget.localScale = scale;

            targets[i] = oneTarget;

            oneTarget.SetParent(transform, false);
        }

        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            bool looper = true;
            yield return new WaitForSeconds(1);
            targets[4].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            looper = false;
            looper = true;
            yield return new WaitForSeconds(1);
            targets[4].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
            looper = false;
        }
    }

    void Update()
    {
        float time = Time.time;
        for (int i = 0; i < targets.Length; i++)
        {
            Transform oneTarget = targets[i];
            Vector3 position = oneTarget.localPosition;

            //position.y = Mathf.Sin(Mathf.PI * (position.x + time));
            // Dividing by 2 flattens the curve
            position.y = Mathf.Sin(Mathf.PI * (position.x + time) / 2);
            oneTarget.localPosition = position;

            //singleDrop = targets[i].position;
            //Debug.Log("singleDrop " + singleDrop);
        }

        //InvokeRepeating("Flicker", 2.0f, 0.3f);
        //StartCoroutine(Flicker());
    }
}
